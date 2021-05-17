using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AlleyOop
{
    
    public class VersionManager : MonoBehaviour
    {
        public GameObject VrRig;
        public GameObject PcRig;

        public GameObject VRUI;
        public GameObject PCUI;

        void Start()
        {
            if(VR.VrUtils.IsVREnabled())
            {
                VRUI.gameObject.SetActive(true);
                
            }
            else
            {
                VrRig.gameObject.SetActive(false);
                PcRig.gameObject.SetActive(true);
                PCMenuActive();
                VRUI.gameObject.SetActive(false);
            }
        }
        private void Update()
        {
            if (VR.VrUtils.IsVREnabled())
            {

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {

                    PCMenuActive();

                    
                }
                
            }
        }

        public void PCMenuActive()
        {
            if (!PCUI.gameObject.activeSelf)
            {
                PCUI.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
            else
            {
                PCUI.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
            }
        }
    }
    
}

