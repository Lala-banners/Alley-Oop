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

        [Header("UI")]
        public Text pickupText;
        public LayerMask layer;

        private void Start()
        {
            pickupText.gameObject.SetActive(false);
            ballGrabSpeedProper = ballGrabSpeed * Time.deltaTime;
            ballGrabbed = false;
        }


        private void Update()
        {



            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Ball");


            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, pickupDist, mask))
            {
                selectedBall = hit.transform.gameObject.GetComponent<Ball>();

                if (selectedBall != null && pickupText != null)
                {
                    pickupText.gameObject.SetActive(true);
                    pickupText.text = "Pick up ball (E)";
                    if (Input.GetKeyDown(KeyCode.E) && ballGrabbed == false)
                    {
                        StartCoroutine(GrabBall(selectedBall));
                    }



                }
               

            }
            else
            {
                selectedBall = null;
                pickupText.gameObject.SetActive(false);
            }

            if (ballGrabbed == true)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    
                }
            }
            
        }
       
       

        IEnumerator GrabBall(Ball ball)
        {

            ballGrabbed = true;
            while (ball.gameObject.transform.position != ballHolder.position)
            {
                ball.gameObject.transform.position = Vector3.MoveTowards(ball.gameObject.transform.position, ballHolder.position, ballGrabSpeedProper);

                yield return null;
            }
        }

    }
}

