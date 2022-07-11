using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public class RotateAround : MonoBehaviour
    {
        public Transform target;
    
        public Vector3 RotateCenter;
    
        public bool isMove = false;
    
        public float zoomSpeed = 1f;
        public void Update()
        {
            if (!isMove)
            {
                if (Input.GetMouseButton(0))
                {
                    var scroll = Input.GetAxis("Mouse X");
                    transform.RotateAround(RotateCenter, Vector3.up, scroll);
                }
            }
        }
    
    
        public void MoveToTarget()
        {
            if (target)
            {
                isMove = true;
                Vector3 position = target.transform.position;
                RotateCenter = position;
                transform.DOMove(new Vector3(position.x, position.y + 60f, position.z - 90f), 0.4f).onComplete= () =>
                {
                    isMove = false;
                };
            }
        }
    }
}
