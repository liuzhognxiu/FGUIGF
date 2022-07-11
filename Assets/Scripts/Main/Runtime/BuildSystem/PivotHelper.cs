using UnityEngine;

namespace MetaArea
{
    public class PivotHelper : MonoBehaviour
    {
        public Transform DeletePivot()
        {
            Transform t = GetComponentsInChildren<Transform>()[1];
            t.parent = null;
            Destroy(this.gameObject,0.1f);
            return t;
        }

    }
}
