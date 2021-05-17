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
                    if (Input.GetKeyDown(KeyCode.E) && ballGrabbed == false)
                    {
                        StartCoroutine(GrabBall(selectedBall, rigi));
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && ballGrabbed == true)
                    {
                        ReleaseBall(selectedBall, rigi);
                    }
                    else if (Input.GetMouseButtonDown(0) && ballGrabbed == true)
                    {
                         ShootBall(selectedBall, rigi);
                    }
                  



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
            rigi.AddForce(0,ballHolder.transform.rotation.y,ballHolder.transform.rotation.z + 0.1f, ForceMode.Impulse); 
            ballGrabbed = false;
        }

    }
}

