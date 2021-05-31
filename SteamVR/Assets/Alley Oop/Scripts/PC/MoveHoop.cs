using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlleyOop
{
    public class MoveHoop : MonoBehaviour
    {
        public Transform pointA;
        public Transform pointB;

        private void Update()
        {
            transform.position = Vector3.Lerp(pointA.position, pointB.position, Mathf.PingPong(Time.time / 5, 1));
        }
    }
}
