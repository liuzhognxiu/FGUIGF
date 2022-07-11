using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;
using Entity = UnityGameFramework.Runtime.Entity;

namespace MetaArea
{
      public static partial class AwaitableExtensions
    {
        private static readonly Dictionary<int, ETTask<UIForm>> s_UIFormTcs =
            new Dictionary<int, ETTask<UIForm>>();  
        
        private static readonly Dictionary<int, ETTask<FGUIForm>> s_FGUIFormTcs =
            new Dictionary<int, ETTask<FGUIForm>>();

        private static readonly Dictionary<int, ETTask<Entity>> s_EntityTcs =
            new Dictionary<int, ETTask<Entity>>();

        private static readonly Dictionary<string, ETTask<bool>> s_DataTableTcs =
            new Dictionary<string, ETTask<bool>>();

        private static readonly Dictionary<string, ETTask<bool>> s_SceneTcs =
            new Dictionary<string, ETTask<bool>>();

        private static readonly HashSet<int> s_WebSerialIDs = new HashSet<int>();
        private static readonly List<WebResult> s_DelayReleaseWebResult = new List<WebResult>();

        private static readonly HashSet<int> s_DownloadSerialIds = new HashSet<int>();
        private static readonly List<DownloadResult> s_DelayReleaseDownloadResult = new List<DownloadResult>();

#if UNITY_EDITOR
        private static bool s_IsSubscribeEvent = false;
#endif

        /// <summary>
        /// 注册需要的事件 (需再流程入口处调用 防止框架重启导致事件被取消问题)
        /// </summary>game
        public static void SubscribeEvent()
        {
            EventComponent eventComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();
            eventComponent.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            eventComponent.Subscribe(OpenUIFormFailureEventArgs.EventId, OnOpenUIFormFailure);

            eventComponent.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            eventComponent.Subscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);

            eventComponent.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            eventComponent.Subscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);

            eventComponent.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            eventComponent.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);

            eventComponent.Subscribe(WebRequestSuccessEventArgs.EventId, OnWebRequestSuccess);
            eventComponent.Subscribe(WebRequestFailureEventArgs.EventId, OnWebRequestFailure);

            eventComponent.Subscribe(DownloadSuccessEventArgs.EventId, OnDownloadSuccess);
            eventComponent.Subscribe(DownloadFailureEventArgs.EventId, OnDownloadFailure);
#if UNITY_EDITOR
            s_IsSubscribeEvent = true;
#endif
        }

#if UNITY_EDITOR
        private static void TipsSubscribeEvent()
        {
            if (!s_IsSubscribeEvent)
            {
                throw new Exception("Use await/async extensions must to subscribe event!");
            }
        }
#endif

        /// <summary>
        /// 加载数据表（可等待）
        /// </summary>
        public static async ETTask<IDataTable<T>> LoadDataTableAsync<T>(this DataTableComponent dataTableComponent,
            string dataTableName, bool formBytes, object userData = null) where T : IDataRow
        {
#if UNITY_EDITOR
            TipsSubscribeEvent();
#endif
            IDataTable<T> dataTable = dataTableComponent.GetDataTable<T>();
            if (dataTable != null)
            {
                var loadTask = ETTask<IDataTable<T>>.Create(true);
                loadTask.SetResult(dataTable);
                return await loadTask ;
            }

            var etTask = ETTask<bool>.Create(true);
            var dataTableAssetName = AssetUtility.GetDataTableAsset(dataTableName, formBytes);
            s_DataTableTcs.Add(dataTableAssetName, etTask);
            dataTableComponent.LoadDataTable(dataTableName, dataTableAssetName, userData);
            bool isLoaded = await etTask;
            dataTable = isLoaded ? dataTableComponent.GetDataTable<T>() : null;
            var task = ETTask<IDataTable<T>>.Create(true);
            task.SetResult(dataTable);
            return await task ;
        }


        private static void OnLoadDataTableSuccess(object sender, GameEventArgs e)
        {
            var ne = (LoadDataTableSuccessEventArgs)e;
            s_DataTableTcs.TryGetValue(ne.DataTableAssetName, out ETTask<bool> tcs);
            if (tcs != null)
            {
                Log.Info("Load data table '{0}' OK.", ne.DataTableAssetName);
                tcs.SetResult(true);
                s_DataTableTcs.Remove(ne.DataTableAssetName);
            }
        }

        private static void OnLoadDataTableFailure(object sender, GameEventArgs e)
        {
            var ne = (LoadDataTableFailureEventArgs)e;
            s_DataTableTcs.TryGetValue(ne.DataTableAssetName, out ETTask<bool> tcs);
            if (tcs != null)
            {
                Log.Error("Can not load data table '{0}' from '{1}' with error message '{2}'.", ne.DataTableAssetName,
                    ne.DataTableAssetName, ne.ErrorMessage);
                tcs.SetResult(false);
                s_DataTableTcs.Remove(ne.DataTableAssetName);
            }
        }

        
        /// <summary>
        /// 打开界面（可等待）
        /// </summary>
        public static ETTask<UIForm> OpenUIFormAsync(this UIComponent uiComponent,string uiFormAssetName, string uiGroupName, int priority, bool pauseCoveredUIForm, object userData)
        {
#if UNITY_EDITOR
            TipsSubscribeEvent();
#endif
            int serialId = uiComponent.OpenUIForm(uiFormAssetName, uiGroupName, priority, pauseCoveredUIForm, userData);
            var tcs = ETTask<UIForm>.Create(true);
            s_UIFormTcs.Add(serialId, tcs);
            return tcs;
        }

        private static void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            s_UIFormTcs.TryGetValue(ne.UIForm.SerialId, out ETTask<UIForm> tcs);
            if (tcs != null)
            {
                tcs.SetResult(ne.UIForm);
                s_UIFormTcs.Remove(ne.UIForm.SerialId);
            }
        }

        private static void OnOpenUIFormFailure(object sender, GameEventArgs e)
        {
            OpenUIFormFailureEventArgs ne = (OpenUIFormFailureEventArgs)e;
            s_UIFormTcs.TryGetValue(ne.SerialId, out ETTask<UIForm> tcs);
            if (tcs != null)
            {
                tcs.SetException(new GameFrameworkException(ne.ErrorMessage));
                s_UIFormTcs.Remove(ne.SerialId);
            }
        }

        /// <summary>
        /// 显示实体（可等待）
        /// </summary>
        public static ETTask<Entity> ShowEntityAsync(this EntityComponent entityComponent, int entityId,
            Type entityLogicType, string entityAssetName, string entityGroupName, int priority,object userData)
        {
#if UNITY_EDITOR
            TipsSubscribeEvent();
#endif
            var tcs = ETTask<Entity>.Create(true);
            s_EntityTcs.Add(entityId, tcs);
            entityComponent.ShowEntity(entityId, entityLogicType, entityAssetName, entityGroupName, priority, userData);
            return tcs;
        }


        private static void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs) e;
            EntityDataBase dataBase = (EntityDataBase) ne.UserData;
            s_EntityTcs.TryGetValue(dataBase.Id, out ETTask<Entity> tcs);
            if (tcs != null)
            {
                tcs.SetResult(ne.Entity);
                s_EntityTcs.Remove(dataBase.Id);
            }
        }

        private static void OnShowEntityFailure(object sender, GameEventArgs e)
        {
            ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs)e;
            s_EntityTcs.TryGetValue(ne.EntityId, out var tcs);
            if (tcs != null)
            {
                tcs.SetException(new GameFrameworkException(ne.ErrorMessage));
                s_EntityTcs.Remove(ne.EntityId);
            }
        }


        /// <summary>
        /// 加载场景（可等待）
        /// </summary>
        public static ETTask<bool> LoadSceneAsync(this SceneComponent sceneComponent, string sceneAssetName)
        {
#if UNITY_EDITOR
            TipsSubscribeEvent();
#endif
            var tcs = ETTask<bool>.Create(true);
            s_SceneTcs.Add(sceneAssetName, tcs);
            sceneComponent.LoadScene(sceneAssetName);
            return tcs;
        }

        private static void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {
            LoadSceneSuccessEventArgs ne = (LoadSceneSuccessEventArgs)e;
            s_SceneTcs.TryGetValue(ne.SceneAssetName, out var tcs);
            if (tcs != null)
            {
                tcs.SetResult(true);
                s_SceneTcs.Remove(ne.SceneAssetName);
            }
        }

        private static void OnLoadSceneFailure(object sender, GameEventArgs e)
        {
            LoadSceneFailureEventArgs ne = (LoadSceneFailureEventArgs)e;
            s_SceneTcs.TryGetValue(ne.SceneAssetName, out var tcs);
            if (tcs != null)
            {
                tcs.SetException(new GameFrameworkException(ne.ErrorMessage));
                s_SceneTcs.Remove(ne.SceneAssetName);
            }
        }

        /// <summary>
        /// 加载资源（可等待）
        /// </summary>
        public static ETTask<T> LoadAssetAsync<T>(this ResourceComponent resourceComponent, string assetName)
            where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            TipsSubscribeEvent();
#endif
            ETTask<T> loadAssetTcs =  ETTask<T>.Create();
            resourceComponent.LoadAsset(assetName, typeof(T), new LoadAssetCallbacks(
                (tempAssetName, asset, duration, userdata) =>
                {
                    var source = loadAssetTcs;
                    loadAssetTcs = null;
                    T tAsset = asset as T;
                    if (tAsset != null)
                    {
                        source.SetResult(tAsset);
                    }
                    else
                    {
                        source.SetException(new GameFrameworkException(
                            $"Load asset failure load type is {asset.GetType()} but asset type is {typeof(T)}."));
                    }
                },
                (tempAssetName, status, errorMessage, userdata) =>
                {
                    loadAssetTcs.SetException(new GameFrameworkException(errorMessage));
                }
            ));

            return loadAssetTcs;
        }

        /// <summary>
        /// 加载多个资源（可等待）
        /// </summary>
        public static async ETTask<T[]> LoadAssetsAsync<T>(this ResourceComponent resourceComponent, string[] assetName) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            TipsSubscribeEvent();
#endif
            if (assetName == null)
            {
                return null;
            }
            T[] assets = new T[assetName.Length];
            ETTask<T>[] tasks = new ETTask<T>[assets.Length];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = resourceComponent.LoadAssetAsync<T>(assetName[i]);
            }

            await ETTaskHelper.WaitAll(tasks);
            for (int i = 0; i < assets.Length; i++)
            {
                assets[i] = tasks[i].GetResult();
            }

            return assets;
        }


        /// <summary>
        /// 增加Web请求任务（可等待）
        /// </summary>
        public static ETTask<WebResult> AddWebRequestAsync(this WebRequestComponent webRequestComponent,
            string webRequestUri, WWWForm wwwForm = null, object userdata = null)
        {
#if UNITY_EDITOR
            TipsSubscribeEvent();
#endif
            var tsc = ETTask<WebResult>.Create(true);
            int serialId = webRequestComponent.AddWebRequest(webRequestUri, wwwForm,
                AwaitDataWrap<WebResult>.Create(userdata, tsc));
            s_WebSerialIDs.Add(serialId);
            return tsc;
        }

        /// <summary>
        /// 增加Web请求任务（可等待）
        /// </summary>
        public static ETTask<WebResult> AddWebRequestAsync(this WebRequestComponent webRequestComponent,
            string webRequestUri, byte[] postData,object userdata = null)
        {
#if UNITY_EDITOR
            TipsSubscribeEvent();
#endif
            var tsc = ETTask<WebResult>.Create(true);
            int serialId = webRequestComponent.AddWebRequest(webRequestUri, postData,
                AwaitDataWrap<WebResult>.Create(userdata, tsc));
            s_WebSerialIDs.Add(serialId);
            return tsc;
        }

        private static void OnWebRequestSuccess(object sender, GameEventArgs e)
        {
            WebRequestSuccessEventArgs ne = (WebRequestSuccessEventArgs)e;
            if (s_WebSerialIDs.Contains(ne.SerialId))
            {
                if (ne.UserData is AwaitDataWrap<WebResult> webRequestUserdata)
                {
                    WebResult result = WebResult.Create(ne.GetWebResponseBytes(), false, string.Empty,
                        webRequestUserdata.UserData);
                    s_DelayReleaseWebResult.Add(result);
                    webRequestUserdata.Source.SetResult(result);
                    ReferencePool.Release(webRequestUserdata);
                }

                s_WebSerialIDs.Remove(ne.SerialId);
                if (s_WebSerialIDs.Count == 0)
                {
                    for (int i = 0; i < s_DelayReleaseWebResult.Count; i++)
                    {
                        ReferencePool.Release(s_DelayReleaseWebResult[i]);
                    }

                    s_DelayReleaseWebResult.Clear();
                }
            }
        }

        private static void OnWebRequestFailure(object sender, GameEventArgs e)
        {
            WebRequestFailureEventArgs ne = (WebRequestFailureEventArgs)e;
            if (s_WebSerialIDs.Contains(ne.SerialId))
            {
                if (ne.UserData is AwaitDataWrap<WebResult> webRequestUserdata)
                {
                    WebResult result = WebResult.Create(null, true, ne.ErrorMessage, webRequestUserdata.UserData);
                    webRequestUserdata.Source.SetResult(result);
                    s_DelayReleaseWebResult.Add(result);
                    ReferencePool.Release(webRequestUserdata);
                }

                s_WebSerialIDs.Remove(ne.SerialId);
                if (s_WebSerialIDs.Count == 0)
                {
                    for (int i = 0; i < s_DelayReleaseWebResult.Count; i++)
                    {
                        ReferencePool.Release(s_DelayReleaseWebResult[i]);
                    }

                    s_DelayReleaseWebResult.Clear();
                }
            }
        }

        /// <summary>
        /// 增加下载任务（可等待)
        /// </summary>
        public static ETTask<DownloadResult> AddDownloadAsync(this DownloadComponent downloadComponent,
            string downloadPath,
            string downloadUri,
            object userdata = null)
        {
#if UNITY_EDITOR
            TipsSubscribeEvent();
#endif
            var tcs = ETTask<DownloadResult>.Create(true);
            int serialId = downloadComponent.AddDownload(downloadPath, downloadUri,
                AwaitDataWrap<DownloadResult>.Create(userdata, tcs));
            s_DownloadSerialIds.Add(serialId);
            return tcs;
        }

        private static void OnDownloadSuccess(object sender, GameEventArgs e)
        {
            DownloadSuccessEventArgs ne = (DownloadSuccessEventArgs)e;
            if (s_DownloadSerialIds.Contains(ne.SerialId))
            {
                if (ne.UserData is AwaitDataWrap<DownloadResult> awaitDataWrap)
                {
                    DownloadResult result = DownloadResult.Create(false, string.Empty, awaitDataWrap.UserData);
                    s_DelayReleaseDownloadResult.Add(result);
                    awaitDataWrap.Source.SetResult(result);
                    ReferencePool.Release(awaitDataWrap);
                }

                s_DownloadSerialIds.Remove(ne.SerialId);
                if (s_DownloadSerialIds.Count == 0)
                {
                    for (int i = 0; i < s_DelayReleaseDownloadResult.Count; i++)
                    {
                        ReferencePool.Release(s_DelayReleaseDownloadResult[i]);
                    }

                    s_DelayReleaseDownloadResult.Clear();
                }
            }
        }

        private static void OnDownloadFailure(object sender, GameEventArgs e)
        {
            DownloadFailureEventArgs ne = (DownloadFailureEventArgs)e;
            if (s_DownloadSerialIds.Contains(ne.SerialId))
            {
                if (ne.UserData is AwaitDataWrap<DownloadResult> awaitDataWrap)
                {
                    DownloadResult result = DownloadResult.Create(true, ne.ErrorMessage, awaitDataWrap.UserData);
                    s_DelayReleaseDownloadResult.Add(result);
                    awaitDataWrap.Source.SetResult(result);
                    ReferencePool.Release(awaitDataWrap);
                }

                s_DownloadSerialIds.Remove(ne.SerialId);
                if (s_DownloadSerialIds.Count == 0)
                {
                    for (int i = 0; i < s_DelayReleaseDownloadResult.Count; i++)
                    {
                        ReferencePool.Release(s_DelayReleaseDownloadResult[i]);
                    }

                    s_DelayReleaseDownloadResult.Clear();
                }
            }
        }
    }
}