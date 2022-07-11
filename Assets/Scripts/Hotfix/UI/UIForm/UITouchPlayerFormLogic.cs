using GameFramework.Event;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace MetaArea.Hotfix
{
    public partial class UITouchPlayerFormLogic : UGuiFormLogic
    {
        private Transform CurBuild;
        private ProcedureMain ProcedureMainData;
        private float ChangeScaleSpeed = 0.1f;
        private float ChangeRotationSpeed = 2f;
    
        protected override void OnInit(object userdata)
        {
            base.OnInit(userdata);
            GetBindComponents(gameObject);
        }
        protected override void OnOpen(object userData)
        {
            // ProcedureMainData = (ProcedureMain) userData;
            // if (ProcedureMainData == null)
            // {
            //     return;
            // }
    
    #if UNITY_ANDROID || UNITY_IPHONE
            CrossPlatformInputManager.SwitchActiveInputMethod(CrossPlatformInputManager.ActiveInputMethod.Touch);
            MobileJoystick.SetActive(true);
    #endif
    
            GameEntry.Event.Subscribe(CreateBuildSuccessArgs.EventId, OpenBuildController);
            m_Button_ChangeToBuild.onClick.AddListener(IntoBuildModel);
            m_Button_AddScale.onClick.AddListener(AddScale);
            m_Button_MinusScale.onClick.AddListener(MinusScale);
            m_Button_TurnLeft.onClick.AddListener(TurnLeft);
            m_Button_TurnRight.onClick.AddListener(TurnRight);
            m_Button_Put.onClick.AddListener(PutBuild);
            
        }
    
        private void OpenBuildController(object sender, GameEventArgs e)
        {
            m_Transform_BuildController.gameObject.SetActive(true);
            m_Transform_Jump.gameObject.SetActive(false);
            CreateBuildSuccessArgs buildItemArgs = (CreateBuildSuccessArgs) e;
            CurBuild = buildItemArgs.Entity.transform;
        }
    
        private void IntoBuildModel()
        {
            GameEntry.UI.OpenUIForm(UIFormId.BuildSelectForm, this);
        }
    
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            m_Button_AddScale.onClick.RemoveListener(AddScale);
            m_Button_MinusScale.onClick.RemoveListener(MinusScale);
            m_Button_ChangeToBuild.onClick.RemoveListener(IntoBuildModel);
            m_Button_TurnLeft.onClick.RemoveListener(TurnLeft);
            m_Button_TurnRight.onClick.RemoveListener(TurnRight);
            m_Button_Put.onClick.RemoveListener(PutBuild);
            GameEntry.Event.Unsubscribe(CreateBuildSuccessArgs.EventId, OpenBuildController);
        }
    
        private void TurnLeft()
        {
            var curBuildTransform = CurBuild.transform;
            Quaternion rotation = curBuildTransform.localRotation;
            Vector3 temp = rotation.eulerAngles;
            curBuildTransform.localRotation = Quaternion.Euler(new Vector3(temp.x, temp.y + ChangeRotationSpeed, temp.z));
        }
    
        private void TurnRight()
        {
            var curBuildTransform = CurBuild.transform;
            Quaternion rotation = curBuildTransform.localRotation;
            Vector3 temp = rotation.eulerAngles;
            curBuildTransform.localRotation = Quaternion.Euler(new Vector3(temp.x, temp.y - ChangeRotationSpeed, temp.z));
        }
    
        private void AddScale()
        {
            CurBuild.transform.localScale *= (1 + ChangeScaleSpeed);
        }
    
        private void MinusScale()
        {
            CurBuild.transform.localScale *= (1 - ChangeScaleSpeed);
        }
    
        private void PutBuild()
        {
            GameEntry.Event.Fire(this, CreateBuildEntityArgs.Create(GameEntry.Entity.GenerateSerialBuildItemId()));
            m_Transform_BuildController.gameObject.SetActive(false);
        }
    }
}
