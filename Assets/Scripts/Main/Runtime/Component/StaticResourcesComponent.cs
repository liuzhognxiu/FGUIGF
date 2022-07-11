using System.Collections.Generic;
using TMPro;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public class StaticResourcesComponent : GameFrameworkComponent
    {
        public Dictionary<FontType, TMP_FontAsset> FontDict = new Dictionary<FontType, TMP_FontAsset>();
    }
}