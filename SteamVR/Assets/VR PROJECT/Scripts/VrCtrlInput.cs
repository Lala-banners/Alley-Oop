using UnityEngine;

using Valve.VR;

namespace BreadAndButterVR
{
    public class VrCtrlInput : MonoBehaviour
    {
        #region VR Event Properties
        public VrCtrl Controller => controller;
        public VRInputEvent OnPointerPressed => onPointerPressed;
        public VRInputEvent OnPointerReleased => onPointerReleased;
        public VRInputEvent OnTeleportPressed => onTeleportPressed;
        public VRInputEvent OnTeleportReleased => onTeleportReleased;
        public VRInputEvent OnUsePressed => onUsePressed;
        public VRInputEvent OnUseReleased => onUseReleased;
        public VRInputEvent OnGrabPressed => onGrabPressed;
        public VRInputEvent OnGrabReleased => onGrabReleased;
        public VRInputEvent OnTouchPadAxisChanged => onTouchPadChanged;
        #endregion

        #region Steam Actions (The Input Actions)
        [Header("Steam Actions")]
        [SerializeField, Tooltip("Pointer action in VR")] private SteamVR_Action_Boolean pointer;
        [SerializeField, Tooltip("Teleport action in VR")] private SteamVR_Action_Boolean teleport;
        [SerializeField, Tooltip("Use action in VR")] private SteamVR_Action_Boolean use;
        [SerializeField, Tooltip("Grab action in VR")] private SteamVR_Action_Boolean grab;
        [SerializeField, Tooltip("Touch pad in VR")] private SteamVR_Action_Vector2 touchPadAxis;
        #endregion

        #region Unity Input Events
        [Header("Unity Events")]
        [SerializeField] private VRInputEvent onPointerPressed = new VRInputEvent();
        [SerializeField] private VRInputEvent onPointerReleased = new VRInputEvent();
        [SerializeField] private VRInputEvent onTeleportPressed = new VRInputEvent();
        [SerializeField] private VRInputEvent onTeleportReleased = new VRInputEvent();
        [SerializeField] private VRInputEvent onUsePressed = new VRInputEvent();
        [SerializeField] private VRInputEvent onUseReleased = new VRInputEvent();
        [SerializeField] private VRInputEvent onGrabPressed = new VRInputEvent();
        [SerializeField] private VRInputEvent onGrabReleased = new VRInputEvent();
        [SerializeField] private VRInputEvent onTouchPadChanged = new VRInputEvent();
        #endregion

        #region SteamVR Input Callbacks (Converting Steam System to Unity System)
        private void OnPointerDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onPointerPressed.Invoke(GenerateArgs()); 
        private void OnPointerUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onPointerReleased.Invoke(GenerateArgs()); 
        private void OnTeleportDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onTeleportPressed.Invoke(GenerateArgs()); 
        private void OnTeleportUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onTeleportReleased.Invoke(GenerateArgs()); 
        private void OnUseDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onUsePressed.Invoke(GenerateArgs()); 
        private void OnUseUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onUseReleased.Invoke(GenerateArgs()); 
        private void OnGrabDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onGrabPressed.Invoke(GenerateArgs()); 
        private void OnGrabUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onGrabReleased.Invoke(GenerateArgs());
        private void OnTouchPadChanged(SteamVR_Action_Vector2 _action, SteamVR_Input_Sources _source, Vector2 _axis, Vector2 _delta) 
            => onTouchPadChanged.Invoke(GenerateArgs());
        #endregion

        private VrCtrl controller;

        public void Initialise(VrCtrl _controller)
        {
            controller = _controller;

            //Specify to link to a specific input source
            pointer.AddOnStateDownListener(OnPointerDown, controller.InputSource); 
            pointer.AddOnStateUpListener(OnPointerUp, controller.InputSource); 

            teleport.AddOnStateDownListener(OnTeleportDown, controller.InputSource); 
            teleport.AddOnStateUpListener(OnTeleportUp, controller.InputSource); 

            use.AddOnStateDownListener(OnUseDown, controller.InputSource); 
            use.AddOnStateUpListener(OnUseUp, controller.InputSource); 

            grab.AddOnStateDownListener(OnGrabDown, controller.InputSource); 
            grab.AddOnStateUpListener(OnGrabUp, controller.InputSource); 

            touchPadAxis.AddOnChangeListener(OnTouchPadChanged, controller.InputSource); 
        }

        /// <summary>
        /// Sets up an instance of InputEventArgs based on the controller and touchpad values.
        /// </summary>
        private InputEventArgs GenerateArgs() => new InputEventArgs(controller, controller.InputSource, touchPadAxis.axis);
    }
}
