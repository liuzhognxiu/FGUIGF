
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace MetaArea.Hotfix
{
    public partial class UIKeyCodeCotrolFormLogic : UGuiFormLogic
    {

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            GetBindComponents(gameObject);
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            void SwitchButtonState(Image image, Button button, bool active)
            {
                image.color = active ? button.colors.pressedColor : button.colors.normalColor;
            }

            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            var jump = CrossPlatformInputManager.GetButtonDown("Jump");

            SwitchButtonState(m_Image_ForwardButton, m_Button_ForwardButton, v > 0);
            SwitchButtonState(m_Image_BackButton, m_Button_BackButton, v < 0);
            SwitchButtonState(m_Image_LeftButton, m_Button_LeftButton, h < 0);
            SwitchButtonState(m_Image_RightButton, m_Button_RightButton, h > 0);
            SwitchButtonState(m_Image_JumpButton, m_Button_JumpButton, jump);
        }
    }
}



