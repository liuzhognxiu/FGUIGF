using System.Threading.Tasks;
using UnityGameFramework.Runtime;
using Entity = UnityGameFramework.Runtime.Entity;
using System;

namespace MetaArea
{
    public partial class AwaitableExtensions
    {
        /// <summary>
        /// 打开界面（可等待）
        /// </summary>
        public static ETTask<UIForm> OpenUIFormAsync(this UIComponent uiComponent, int uiFormId,
            object userData = null)
        {
#if UNITY_EDITOR
            TipsSubscribeEvent();
#endif
            int? serialId = uiComponent.OpenUIForm(uiFormId, userData);
            if (serialId == null)
            {
                var temp = ETTask<UIForm>.Create(true);
                temp.SetResult(null);
                return temp;
            }

            var tcs = ETTask<UIForm>.Create(true);
            s_UIFormTcs.Add(serialId.Value, tcs);
            return tcs;
        }
        
        /// <summary>
        /// 打开FGUI界面（可等待）
        /// </summary>
        public static ETTask<FGUIForm> OpenFGUIFormAsync(this FGUIComponent uiComponent, int uiFormId,
            object userData = null)
        {
#if UNITY_EDITOR
            TipsSubscribeEvent();
#endif
            int? serialId = uiComponent.OpenUIForm(uiFormId,userData);
            if (serialId == null)
            {
                var temp = ETTask<FGUIForm>.Create(true);
                temp.SetResult(null);
                return temp;
            }

            var tcs = ETTask<FGUIForm>.Create(true);
            s_FGUIFormTcs.Add(serialId.Value, tcs);
            return tcs;
        }

        /// <summary>
        /// 打开界面（可等待）
        /// </summary>
        public static ETTask<UIForm> OpenUIFormAsync(this UIComponent uiComponent, UIFormId uiFormId,
            object userData = null)
        {
#if UNITY_EDITOR
            TipsSubscribeEvent();
#endif
            int? serialId = uiComponent.OpenUIForm(uiFormId, userData);
            if (serialId == null)
            {
                var temp = ETTask<UIForm>.Create(true);
                temp.SetResult(null);
                return temp;
            }

            var tcs = ETTask<UIForm>.Create(true);
            s_UIFormTcs.Add(serialId.Value, tcs);
            return tcs;
        }

        /// <summary>
        /// 显示实体（可等待）
        /// </summary>
        public static ETTask<Entity> ShowEntityAsync(this EntityComponent entityComponent, Type logicType,
            int priority,
            EntityDataBase dataBase)
        {
#if UNITY_EDITOR
            TipsSubscribeEvent();
#endif
            var tcs =ETTask<Entity>.Create(true);
            s_EntityTcs.Add(dataBase.Id, tcs);
            entityComponent.ShowEntity(logicType, priority, dataBase);
            return tcs;
        }

        /// <summary>
        /// 显示实体（可等待）
        /// </summary>
        public static ETTask<Entity> ShowEntityAsync(this EntityComponent entityComponent, Type logicType,
            int priority,
            string entityGroup,
            EntityDataBase dataBase)
        {
#if UNITY_EDITOR
            TipsSubscribeEvent();
#endif
            var tcs = ETTask<Entity>.Create(true);
            s_EntityTcs.Add(dataBase.Id, tcs);
            entityComponent.ShowEntity(logicType, entityGroup, priority, dataBase);
            return tcs;
        }
    }
}