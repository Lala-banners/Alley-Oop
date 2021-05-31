using UnityEngine;
using UnityEngine.EventSystems;

namespace AlleyOop.VR
{
    public class VrInputModule : BaseInputModule
    {
        public Vector3 ControllerPosition { get; set; }
        public bool ControllerButtonDown { get; set; }
        public bool ControllerButtonUp { get; set; }

        //Store object clicked on
        private GameObject currentObject = null;
        
        //Data to be passed to the input module
        private PointerEventData data = null;
        private new Camera cam;

        protected override void Awake() {
            base.Awake();

            //Simulating click on screen,
            //data is everything needed for clicking
            data = new PointerEventData(eventSystem);
        }

        protected override void Start() {
            base.Start();

            cam = VrRig.instance.Headset.GetComponent<Camera>();
        }

        // This is the same as the update loop for input modules
        public override void Process() {
            //Reset pointer event data
            data.Reset();
            
            //Update position
            data.position = cam.WorldToScreenPoint(ControllerPosition);
            
            //Raycast 
            eventSystem.RaycastAll(data, m_RaycastResultCache);
            data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            currentObject = data.pointerCurrentRaycast.gameObject;

            //Clear raycast data cache
            m_RaycastResultCache.Clear();
            
            // Handle hovering for selectable UI elements
            HandlePointerExitAndEnter(data, currentObject);
            
            // Handle press and releasing of the controller buttons
            if (ControllerButtonDown) ProcessPress();
            if (ControllerButtonUp) ProcessRelease();

            //Reset the button flags to prevent multiple calling of events
            ControllerButtonDown = false;
            ControllerButtonUp = false;
        }

        private void ProcessPress() {
            // Set the press raycast to the current raycast
            data.pointerPressRaycast = data.pointerCurrentRaycast;
            
            // Check for the hit object, get down handler and call it
            GameObject newPointerPress =
                ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerDownHandler);
            
            // If no down handler was found, try and get click handler
            if (newPointerPress.Equals(null))
                newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);
            
            // Copy the pointer event data into the data variable
            data.pressPosition = data.position;
            data.pointerPress = newPointerPress;
            data.rawPointerPress = currentObject;
        }

        private void ProcessRelease() {
            // Executing pointer up function
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);
            
            // Check for click handler
            GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);
            
            //Check if the clicked object matches the set one in press function
            if (data.pointerPress.Equals(pointerUpHandler))
                ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
            
            // Clear selected go and reset pointer data
            eventSystem.SetSelectedGameObject(null);
            data.pressPosition = Vector2.zero;
            data.pointerPress = null;
            data.rawPointerPress = null;
            
            //Handle dragging alone
        }
        
        
        
        
    }
}