using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace AlleyOop.PC
{
    public class ClickBall : MonoBehaviour
    {
        #region Variables
        [Header("Ball")]
        Ball selectedBall;
        bool ballGrabbed;
        public float ballGrabSpeed;
        float ballGrabSpeedProper;

        [Header("Utility")]
        public Camera cam;
        public float pickupDist;
        public Transform ballHolder;
        public Rigidbody rigi;

        [Header("UI")]
        public Text pickupText;
        public LayerMask layer;
        #endregion
        #region Start
        private void Start()
        {
            pickupText.gameObject.SetActive(false);
            ballGrabSpeedProper = ballGrabSpeed * Time.deltaTime;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            ballGrabbed = false;
        }
        #endregion
        #region Update
        private void Update()
        {
            //Grabbing and releasing ball
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(ballGrabbed == false)
                {
                    RaycastHit hit;
                    LayerMask mask = LayerMask.GetMask("Ball");
                    ballHolder.rotation = cam.transform.rotation;

                    //
                    if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit,
                        pickupDist, mask))
                    {
                        StartCoroutine(GrabBall(selectedBall, rigi));
                    }
                }

                //Release ball if ball is being carried
                else if (ballGrabbed == true)
                {
                    ReleaseBall(selectedBall, rigi);

                }
                
            }

            //Shooting Ball
            if (Input.GetMouseButtonDown(0) && ballGrabbed == true)
            {
                ShootBall(selectedBall, rigi);
            }
        }
        #endregion

        #region Fixed Update
        private void FixedUpdate()
        {
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Ball");

            //Match the grab point for the ball to the rotation of the camera
            ballHolder.rotation = cam.transform.rotation;


            // Shoot out ray to pickup
            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, pickupDist, mask))
            {
                selectedBall = hit.transform.gameObject.GetComponent<Ball>();
                rigi = selectedBall.gameObject.GetComponent<Rigidbody>();

                // UI Prompt to pickup when ball is highlighted
                if (selectedBall != null && pickupText != null)
                {
                    pickupText.gameObject.SetActive(true);
                    pickupText.text = "Pick up ball (E)";
                }

            }
            else
            {
                selectedBall = null;
                pickupText.gameObject.SetActive(false);
            }

        }
        #endregion
        #region Grabbing Ball
        IEnumerator GrabBall(Ball ball, Rigidbody rigi)
        {
            // Move the ball to the grab point if there isn't already a ball
            ballGrabbed = true;
            while (ball.gameObject.transform.position != ballHolder.position)
            {
                Debug.Log("Grabbing Ball");
                
                ball.gameObject.transform.position = Vector3.MoveTowards(ball.gameObject.transform.position, ballHolder.position, ballGrabSpeedProper);

                // Freeze the ball in front of the player as if it is being held
                if (ball.gameObject.transform.position == ballHolder.position)
                {
                    ball.gameObject.transform.SetParent(ballHolder);
                    rigi.constraints = RigidbodyConstraints.FreezeAll;
                    ball.gameObject.transform.rotation = ballHolder.gameObject.transform.rotation;

                }
                yield return null;
            }
        }
        #endregion
        #region Release Ball
        public void ReleaseBall(Ball ball, Rigidbody rigi)
        {

            // Unfreeze the constraints placed upon the ball when grabbed
            rigi.constraints = RigidbodyConstraints.None;
            ball.gameObject.transform.SetParent(null);
            ballGrabbed = false;
        }
        #endregion
        #region Shoot Ball
        public void ShootBall(Ball ball, Rigidbody rigi)
        {
            // Unfreeze the constraints upon the ball
            rigi.constraints = RigidbodyConstraints.None;
            ball.gameObject.transform.SetParent(null);

            //Shoot the ball
            rigi.AddRelativeForce(0, 0.03f,0.06f, ForceMode.Impulse); 
            ballGrabbed = false;
        }
        #endregion
    }
}

