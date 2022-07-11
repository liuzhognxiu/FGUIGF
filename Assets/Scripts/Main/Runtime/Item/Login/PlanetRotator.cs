using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MetaArea
{
    public class PlanetRotator : SerializedMonoBehaviour
    {
        private void Start()
        {
            transform.DORotate(Vector3.down * 360f, 60f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        }
    }
}