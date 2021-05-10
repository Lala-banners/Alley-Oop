using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlleyOop
{
    
    public class VersionManager : MonoBehaviour
    {
        public GameObject VrRig;
        public GameObject PcRig;
        void Start()
        {
            if(VR.VrUtils.IsVREnabled())
            {
                
            }
            else
            {
                VrRig.gameObject.SetActive(false);
                PcRig.gameObject.SetActive(true);
            }
        }


    }
}

