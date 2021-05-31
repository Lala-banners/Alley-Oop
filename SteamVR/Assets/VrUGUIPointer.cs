using UnityEngine;
using Valve.VR;

namespace AlleyOop.VR
{
    [RequireComponent(typeof(VrCtrlInput))]
    public class VrUGUIPointer : MonoBehaviour
    {
        [SerializeField] private SteamVR_Action_Boolean clickAction;
        [SerializeField] private LayerMask uiMask;
        private VrInputModule inputMod;
        [SerializeField] private Pointer pointer;

        // Start is called before the first frame update
        void Start() {
            inputMod = FindObjectOfType<VrInputModule>();
        }

        // Update is called once per frame
        void Update() {
            inputMod.ControllerButtonDown = clickAction.stateDown;
            inputMod.ControllerButtonUp = clickAction.stateUp;
            
            Vector3 position = Vector3.zero;
            bool hitUI = false;
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, uiMask))
            {
                position = hit.point;
                hitUI = true;
            }

            inputMod.ControllerPosition = position;
            
            //Makes laser appear from pointer when raycast hits UI
            if (pointer != null)
                pointer.Active = hitUI;
        }
    }
}