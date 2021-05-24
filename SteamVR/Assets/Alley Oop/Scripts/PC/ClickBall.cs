using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace AlleyOop.PC
{
    public class ClickBall : MonoBehaviour
    {
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

        private void Start()
        {
            pickupText.gameObject.SetActive(false);
            ballGrabSpeedProper = ballGrabSpeed * Time.deltaTime;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            ballGrabbed = false;
        }


        private void Update()
        {
           
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(ballGrabbed == false)
                {
                    RaycastHit hit;
                    LayerMask mask = LayerMask.GetMask("Ball");
                    ballHolder.rotation = cam.transform.rotation;
                    
                    if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit,
                        pickupDist, mask))
                    {
                        StartCoroutine(GrabBall(selectedBall, rigi));
                    }
                }
               else if (ballGrabbed == true)
                {
                    ReleaseBall(selectedBall, rigi);

                }
                
            }
            
            if (Input.GetMouseButtonDown(0) && ballGrabbed == true)
            {
                ShootBall(selectedBall, rigi);
            }
        }
        

        private void FixedUpdate()
        {
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Ball");
            ballHolder.rotation = cam.transform.rotation;

            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, pickupDist, mask))
            {
                selectedBall = hit.transform.gameObject.GetComponent<Ball>();
                rigi = selectedBall.gameObject.GetComponent<Rigidbody>();
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

        IEnumerator GrabBall(Ball ball, Rigidbody rigi)
        {

            ballGrabbed = true;
            while (ball.gameObject.transform.position != ballHolder.position)
            {
                Debug.Log("Grabbing Ball");
                
                ball.gameObject.transform.position = Vector3.MoveTowards(ball.gameObject.transform.position, ballHolder.position, ballGrabSpeedProper);
                if (ball.gameObject.transform.position == ballHolder.position)
                {
                    ball.gameObject.transform.SetParent(ballHolder);
                   
                    
                    rigi.constraints = RigidbodyConstraints.FreezeAll;
                    ball.gameObject.transform.rotation = ballHolder.gameObject.transform.rotation;

                }
                yield return null;
            }
        }

        public void ReleaseBall(Ball ball, Rigidbody rigi)
        {
            rigi.constraints = RigidbodyConstraints.None;
            ball.gameObject.transform.SetParent(null);
            ballGrabbed = false;
        }
        public void ShootBall(Ball ball, Rigidbody rigi)
        {
            rigi.constraints = RigidbodyConstraints.None;
            ball.gameObject.transform.SetParent(null);
            rigi.AddRelativeForce(0, 0.03f,0.06f, ForceMode.Impulse); 
            ballGrabbed = false;
        }

    }
}

