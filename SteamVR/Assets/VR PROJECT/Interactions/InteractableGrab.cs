using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlleyOop.VR.Interaction
{
    [RequireComponent(typeof(VrCtrlInput))]
    public class InteractableGrab : MonoBehaviour
    {
        //For if we want to do something that is specific to the object. Eg turns red or increment counter
        public InteractionEvent grabbed = new InteractionEvent();
        public InteractionEvent released = new InteractionEvent();

        private VrCtrlInput input;
        private InteractableObject collidingObject; //We are colliding with that we are not holding
        private InteractableObject heldObject; //Object we are holding

        // Start is called before the first frame update
        void Start()
        {
            input = gameObject.GetComponent<VrCtrlInput>();

            input.OnGrabPressed.AddListener((_args) => { if (collidingObject != null) GrabObject(); });
            input.OnGrabReleased.AddListener((_args) => { if (heldObject != null) ReleaseObject(); });
        }

        private void SetCollidingObject(Collider _other)
        {
            InteractableObject interactable = _other.GetComponent<InteractableObject>();

            //Prevents overiding interactable object, only registers first held object
            if (collidingObject != null || interactable == null) return;
            collidingObject = interactable;
        }

        // OnTriggerEnter is called when the Collider other enters the trigger
        private void OnTriggerEnter(Collider _other) => SetCollidingObject(_other);

        // OnTriggerExit is called when the Collider other has stopped touching the trigger
        private void OnTriggerExit(Collider _other)
        {
            if (collidingObject == _other.GetComponent<InteractableObject>()) collidingObject = null;
        }

        private void GrabObject()
        {
            //Safety measure to prevent connecting to something that doesn't exist
            if (collidingObject == null)
                return;

            heldObject = collidingObject;
            collidingObject = null;
            FixedJoint joint = AddJoint(heldObject.Rigidbody); //The other object is the interactable object NOT the controller

            if(heldObject.AttachPoint != null)
            {
                //Adjusting the object's position to the controller attach point
                heldObject.transform.position = 
                    transform.position - (heldObject.AttachPoint.position - heldObject.transform.position);

                //Adjust the object's rotation to the controller attach point
                heldObject.transform.rotation = 
                    transform.rotation * Quaternion.Euler(heldObject.AttachPoint.localEulerAngles);
            }
            else
            {
                //If there is no attach point then sync the position and rotation's up
                heldObject.transform.position = transform.position;
                heldObject.transform.rotation = transform.rotation;
            }

            //Run the interaction events for Grab
            grabbed.Invoke(new InteractEventArgs(input.Controller, heldObject.Rigidbody, heldObject.Collider));
            heldObject.OnObjectGrabbed(input.Controller);
        }

        private void ReleaseObject()
        {
            RemoveJoint(gameObject.GetComponent<FixedJoint>());
            released.Invoke(new InteractEventArgs(input.Controller, heldObject.Rigidbody, heldObject.Collider));
            heldObject.OnObjectReleased(input.Controller);
            heldObject = null;
        }

        /// <summary>
        /// Adds connection between the controller and object and adds velocity for realism when throwing an object.
        /// </summary>
        private FixedJoint AddJoint(Rigidbody _richibody)
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.breakForce = 20000;
            joint.breakTorque = 20000;
            joint.connectedBody = _richibody;
            return joint;
        }

        /// <summary>
        /// Removed connection between the controller and object and adds velocity for realism when throwing an object.
        /// </summary>
        private void RemoveJoint(FixedJoint _joint)
        {
            if(_joint != null)
            {
                //Disconnects the rigidbody from the joint's connected body
                _joint.connectedBody = null; 
                Destroy(_joint);
                heldObject.Rigidbody.velocity = input.Controller.Velocity;
                heldObject.Rigidbody.angularVelocity = input.Controller.AngularVelocity;
            }
        }
    }
}
