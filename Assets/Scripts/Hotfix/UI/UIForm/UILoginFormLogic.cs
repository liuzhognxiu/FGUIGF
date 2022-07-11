using System.Text;
using System.Threading.Tasks;
using GameFramework;
using LitJson;
using Main.Runtime.JsonClass;
using UnityEngine;
using UnityGameFramework.Runtime;

//自动生成于：2022/5/10 16:51:19
namespace MetaArea.Hotfix
{
    public partial class UILoginFormLogic : UGuiFormLogic
    {
        private ProcedureLogin _procedureLogin;

        protected override void OnInit(object userdata)
        {
            base.OnInit(userdata);
            GetBindComponents(gameObject);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            _procedureLogin = userData as ProcedureLogin;
            if (_procedureLogin == null)
            {
                Log.Error("Procedure login is invalid when openning login form.");
                return;
            }

            m_Button_SendVerify.onClick.AddListener(OnSendVerifyButtonDown);
            m_Button_Login.onClick.AddListener(OnLoginButtonDown);
            m_Button_Join.onClick.AddListener(OnJoinButtonDown);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            m_Button_SendVerify.onClick.RemoveListener(OnSendVerifyButtonDown);
            m_Button_Login.onClick.RemoveListener(OnLoginButtonDown);
            m_Button_Join.onClick.RemoveListener(OnJoinButtonDown);
        }

        private async void OnLoginButtonDown()
        {
            var loginData = new JsonData()
            {
                ["smsCode"] = m_TMP_InputField_VerifyCode.text,
                ["phone"] = "86" + m_TMP_InputField_PhoneNum.text,
                ["openId"] = "",
                ["unionId"] = ""
            };

            string json_string = loginData.ToJson();
            byte[] bytes = Encoding.Default.GetBytes(json_string);
            await Login(bytes);
        }

        private async void OnSendVerifyButtonDown()
        {
            var loginData = new JsonData()
            {
                ["uuid"] = SystemInfo.deviceUniqueIdentifier,
                ["phone"] = "86" + m_TMP_InputField_PhoneNum.text,
                ["ClientType"] = "APP"
            };

            string json_string = loginData.ToJson();
            byte[] bytes = Encoding.Default.GetBytes(json_string);
            await GetScanMessage(bytes);
        }


        private async ETTask GetScanMessage(byte[] bytes)
        {
            var webResult =
                await GameEntry.WebRequest.AddWebRequestAsync(
                    Constant.WebUrl.TestWebUrl + Constant.WebUrl.GetPhoneMessageUrl,
                    bytes, this);
            if (!webResult.IsError)
            {
                m_Transform_PhoneNum.gameObject.SetActive(false);
                m_TMP_InputField_VerifyCode.gameObject.SetActive(true);
                m_Button_Login.gameObject.SetActive(true);
            }
        }

        private async ETTask Login(byte[] bytes)
        {
            var webResult =
                await GameEntry.WebRequest.AddWebRequestAsync(
                    Constant.WebUrl.TestWebUrl + Constant.WebUrl.PhoneLoginUrl, bytes,
                    this);
            if (!webResult.IsError)
            {
                var jsonData = Utility.Json.ToObject<loginData>(Encoding.UTF8.GetString(webResult.Bytes));
                if (jsonData.code == 10000)
                {
                    GameEntry.Data.UID = jsonData.singleData.uid;
                    GameEntry.Data.UserID = jsonData.singleData.userId;
                    GameEntry.Data.UserToken = jsonData.singleData.token;
                    GameEntry.Event.Fire(this, JoinToGameArgs.Create());
                }
                else
                {
                    Debug.LogError($"ErrorCode:{jsonData.code} , ErrorMessage:{jsonData.msg}");
                }
            }
        }

        private void OnJoinButtonDown()
        {
            GameEntry.Event.Fire(this, JoinToGameArgs.Create());
        }
    }
}