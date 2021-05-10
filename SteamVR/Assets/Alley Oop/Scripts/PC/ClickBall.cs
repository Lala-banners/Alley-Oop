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


        private void Start()
        {
            ballGrabSpeedProper = ballGrabSpeed * Time.deltaTime;
            ballGrabbed = false;
        }


        private void Update()
        {



            RaycastHit hit;
            

            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, pickupDist))
            {
                selectedBall = hit.transform.gameObject.GetComponent<Ball>();
                
                if(selectedBall != null && pickupText != null)
                {
                    pickupText.gameObject.SetActive(true);
                    pickupText.text = "Pick up ball (E)";
                    if (Input.GetKeyDown(KeyCode.E) && ballGrabbed == false)
                    {
                        StartCoroutine(GrabBall(selectedBall));
                    }
                   


                }
                else
                {
                    pickupText.gameObject.SetActive(false);
                }
                

            }
            if(ballGrabbed == true)
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

