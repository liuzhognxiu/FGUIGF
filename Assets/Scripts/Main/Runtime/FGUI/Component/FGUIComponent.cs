using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codice.Client.Common;
using FairyGUI;
using GameFramework;
using GameFramework.DataTable;
using GameFramework.ObjectPool;
using GameFramework.Resource;
using GameFramework.UI;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public class FGUIComponent : GameFrameworkComponent
    {
        [SerializeField] private float m_InstanceAutoReleaseInterval = 60f;

        [SerializeField] private int m_InstanceCapacity = 16;

        [SerializeField] private float m_InstanceExpireTime = 60f;

        [SerializeField] private int m_InstancePriority = 0;

        [SerializeField] private Transform m_InstanceRoot;

        private int m_Serial;

        private UIConfigSetting uiConfigSetting;
        
        private static LoadAssetCallbacks LoadPackageAssetCallbacks;

        private FGUIFormHelper m_Helper;

        private IObjectPool<FGUIFormInstanceObject> m_InstancePool;
        private Queue<FGUIForm> m_RecycleQueue;
        private Dictionary<int, FGUIForm> m_FuiFormsBeingOpened;

        private static ResourceComponent m_ResourceComponent;
        private EventComponent m_EventComponent;

        private static readonly UIPackage.LoadResourceAsync LoadWithDependencies = (string name, string extension, System.Type type, PackageItem item) =>
        {
            var assetFullName = name + extension;
            Log.Debug(assetFullName);
            m_ResourceComponent.LoadAsset(assetFullName, type, LoadPackageAssetCallbacks, item);
        };

        private void Start()
        {
            m_ResourceComponent = GameEntry.Resource;
            m_EventComponent = GameEntry.Event;
            m_InstancePool = GameFrameworkEntry.GetModule<IObjectPoolManager>().CreateSingleSpawnObjectPool<FGUIFormInstanceObject>("FGUI Instance Pool");
            m_RecycleQueue = new Queue<FGUIForm>();
            m_FuiFormsBeingOpened = new Dictionary<int, FGUIForm>();

            m_InstancePool.AutoReleaseInterval = m_InstanceAutoReleaseInterval;
            m_InstancePool.Capacity = m_InstanceCapacity;
            m_InstancePool.ExpireTime = m_InstanceExpireTime;
            m_InstancePool.Priority = m_InstancePriority;

            m_Helper = new GameObject().AddComponent<FGUIFormHelper>();
            m_Helper.name = "FGUI Form Helper";
            var helperTransform = m_Helper.transform;
            helperTransform.SetParent(transform);
            helperTransform.localScale = Vector3.one;

            LoadPackageAssetCallbacks = new LoadAssetCallbacks(LoadPackageAssetSuccess, LoadPackageAssetFailure);

            uiConfigSetting = new UIConfigSetting();
            uiConfigSetting.Init();

        }


        private void Update()
        {
            while (m_RecycleQueue.Count > 0)
            {
                FGUIForm fguiForm = m_RecycleQueue.Dequeue();
                fguiForm.OnRecycle();
                m_InstancePool.Unspawn(fguiForm.Handle);
            }
        }

        private void OnDestroy()
        {
            m_RecycleQueue.Clear();
            m_FuiFormsBeingOpened.Clear();
        }


        public FGUIForm GetFUIForm(int _serialId)
        {
            if (!m_FuiFormsBeingOpened.ContainsKey(_serialId))
            {
                Log.Error($"没有在已打开窗口列表中找到ID为{_serialId}的窗口。");
            }

            return m_FuiFormsBeingOpened[_serialId];
        }

        public void CloseUIForm(int serialId)
        {
            if (!m_FuiFormsBeingOpened.ContainsKey(serialId))
            {
                Log.Error($"没有在已打开窗口列表中找到ID为{serialId}的窗口。");
            }

            CloseUIForm(m_FuiFormsBeingOpened[serialId]);
        }

        public void CloseUIForm(FGUIFormLogic fguiFormLogic)
        {
            CloseUIForm(fguiFormLogic.fguiForm);
        }

        public void CloseUIForm(FGUIForm fguiForm)
        {
            if (fguiForm == null)
            {
                throw new GameFrameworkException("FGUI form is invalid.");
            }

            m_FuiFormsBeingOpened.Remove(fguiForm.SerialId);

            fguiForm.OnClose();

            m_RecycleQueue.Enqueue(fguiForm);
        }


        public int? OpenUIForm(string AssetPath, string BytesAssetName)
        {
            int serialId = ++m_Serial;

            FGUIFormInstanceObject uiFormInstanceObject = m_InstancePool.Spawn(AssetPath);
            if (uiFormInstanceObject == null)
            {
                InternalCreateUIForm(AssetPath, BytesAssetName, serialId);
            }
            else
            {
                InternalOpenUIForm(serialId, AssetPath, uiFormInstanceObject.Target, m_InstanceRoot, false);
            }

            return serialId;
        }

        private async void InternalCreateUIForm(string AssetPath, string FGUIBytesName, int _serialId)
        {
            //加载FGUI的描述文件
            var textAsset = await m_ResourceComponent.LoadAssetAsync<TextAsset>(FGUIBytesName);
            byte[] descData = textAsset.bytes;

            //获取FGUI资源的前缀名
            var assetNamePrefix = FGUIBytesName.Remove(FGUIBytesName.IndexOf('_'));

            //异步加载所有的引用资源
            while (await LoadFGUIPackage(descData, assetNamePrefix))
            {
                break;
            }

            object prefabGameObject = await m_ResourceComponent.LoadAssetAsync<GameObject>(AssetPath);
            FGUIFormInstanceObject fguiFormInstanceObject = FGUIFormInstanceObject.Create(AssetPath, prefabGameObject, m_Helper.InstantiateUIForm(prefabGameObject), m_Helper);

            m_InstancePool.Register(fguiFormInstanceObject, true);

            InternalOpenUIForm(_serialId, AssetPath, fguiFormInstanceObject.Target, m_InstanceRoot, true);

            // UIPackage.CreateObjectAsync(AssetPath, AssetPath, result =>
            // {
            //     if (result is null || result.asCom is null)
            //         throw new System.Exception("创建" + AssetPath + "窗口页面失败");
            //
            //     FGUIFormInstanceObject fguiFormInstanceObject = FGUIFormInstanceObject.Create(AssetPath, result, m_Helper.InstantiateUIForm(result), m_Helper);
            //
            //     m_InstancePool.Register(fguiFormInstanceObject, true);
            //
            //     InternalOpenUIForm(_serialId, AssetPath, fguiFormInstanceObject.Target, m_InstanceRoot, true);
            // });
        }

        private async Task<bool> LoadFGUIPackage(byte[] descData, string assetNamePrefix)
        {
            UIPackage package = UIPackage.AddPackage(descData, assetNamePrefix, LoadWithDependencies);
            if (package != null && package.dependencies.Length > 0)
            {
                foreach (var kv in package.dependencies)
                {
                    if (string.IsNullOrEmpty(kv["name"]))
                    {
                        continue;
                    }

                    string bytesName = AssetUtility.GetUIBytesAsset(kv["name"]);
                    string assetNameDepend = bytesName.Remove(bytesName.IndexOf('_'));
                    var textAssetDepend = await m_ResourceComponent.LoadAssetAsync<TextAsset>(bytesName);
                    byte[] descDataDepend = textAssetDepend.bytes;
                    await LoadFGUIPackage(descDataDepend, assetNameDepend);
                }

                return false;
            }

            return true;
        }

        // private void LoadFGUIPackage(byte[] descData,string assetNamePrefix)
        // {
        //     UIPackage package = UIPackage.AddPackage(descData, assetNamePrefix, LoadWithDependencies);
        //
        //     if (package.dependencies.Length > 0)
        //     {
        //         foreach (var kv in package.dependencies)
        //         {
        //             if (string.IsNullOrEmpty(kv["name"]))
        //             {
        //                 continue;
        //             }
        //
        //             string bytesName = AssetUtility.GetUIBytesAsset(kv["name"]);
        //             string assetNameDepend = bytesName.Remove(bytesName.IndexOf('_'));
        //             GameEntry.Resource.LoadAsset(bytesName, typeof(TextAsset), new LoadAssetCallbacks((assetName, asset, duration, userData) =>
        //             {
        //                 TextAsset textAssetDepend = (TextAsset) asset;
        //                 byte[] descDataDepend = textAssetDepend.bytes;
        //                 LoadFGUIPackage(descDataDepend, assetNameDepend);
        //             }));
        //         }
        //     }
        // }

        private void InternalOpenUIForm(int serialId, string fguiAssetName, object fguiFormInstance, Transform root, bool isNewInstance)
        {
            try
            {
                FGUIForm fguiForm = m_Helper.CreateUIForm(fguiFormInstance, root);
                if (fguiForm == null)
                {
                    throw new GameFrameworkException("Can not create FGui form in UI form FGui Helper.");
                }

                m_FuiFormsBeingOpened.Add(serialId, fguiForm);
                fguiForm.OnInit(serialId, fguiAssetName, isNewInstance);
                fguiForm.OnOpen();

                var openFGuiFormSuccessEventArgs = OpenFGUIFormSuccessEventArgs.Create(fguiForm);
                m_EventComponent.Fire(this, openFGuiFormSuccessEventArgs);
            }
            catch (Exception e)
            {
                var openFGuiFormFailureEventArgs = OpenFGUIFormFailureEventArgs.Create(serialId, fguiAssetName, e.Message);
                m_EventComponent.Fire(this, openFGuiFormFailureEventArgs);
            }
        }

        private void LoadPackageAssetFailure(string assetname, LoadResourceStatus status, string errormessage, object userdata)
        {
            Log.Error($"加载UI包资源{assetname}失败,错误信息：{errormessage}.");
        }

        private void LoadPackageAssetSuccess(string assetname, object asset, float duration, object userdata)
        {
            Log.Info($"加载UI包资源{assetname}成功。");
            var packageItem = (PackageItem) userdata;
            packageItem.owner.SetItemAsset(packageItem, asset, DestroyMethod.None);
        }
    }
}