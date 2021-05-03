using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlleyOop.VR.Interaction
{
    public class MouseThrow : MonoBehaviour
    {
        public GameObject basketball;
        [SerializeField] private float throwForce = 5f;
        [SerializeField] private Camera mainCam;

        // Start is called before the first frame update
        void Start()
        {
            basketball.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Release mouse button and launch the basketball
                GameObject clone = Instantiate(basketball, transform.position, transform.rotation);
                mainCam.ScreenToWorldPoint(Input.mousePosition);
                clone.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
            }
        }
    }
}
