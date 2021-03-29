using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadAndButterVR
{
    public class VrCtrlInput : MonoBehaviour
    {
        private VrCtrl controller;

        public void Initialise(VrCtrl _controller)
        {
            controller = _controller;
        }
    }
}
