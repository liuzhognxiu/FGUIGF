using FairyGUI;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public class UIConfigSetting
    {
        public void Init()
        {
            Input.multiTouchEnabled = true;
            UIConfig.defaultFont = "";
            UIConfig.makePixelPerfect = true;
            UIConfig.enhancedTextOutlineEffect = false;
            UIConfig.depthSupportForPaintingMode = true;
            UIConfig.modalLayerColor = new Color(0f, 0f, 0f, 0.7f);

            UIConfig.bringWindowToFrontOnClick = false;
            UIConfig.defaultComboBoxVisibleItemCount = 5;
            if (Screen.safeArea.x != 0 || Screen.safeArea.y != 0)
            {
                float w = GRoot.inst.size.x - Screen.safeArea.x;
                float h = GRoot.inst.size.y;
                GRoot.inst.SetPosition(Screen.safeArea.x, 0, 0);
                Log.Info("Screen.safeArea.x:" + Screen.safeArea.x);
                GRoot.inst.SetSize((int) w, (int) h, true);
            }

            UIConfig.touchDragSensitivity = 1;
            StageCamera.main.backgroundColor = Color.black;
        }
    }
}