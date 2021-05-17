using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  AlleyOop.VR
{
    public class LockOnEffect : MonoBehaviour
    {
        private float ballSpeed = 2 * Time.deltaTime;
        void OnNearby()
        {
            GameObject[] hoops = GameObject.FindGameObjectsWithTag("Hoop");
            float minDistance = Mathf.Infinity;
            Transform t = null;
        
            foreach (GameObject hoop in hoops)
            {
                float distance = Vector3.Distance(hoop.transform.position, this.gameObject.transform.position);
                if (distance < minDistance)
                {
                    t = hoop.transform;
                    minDistance = distance;
                }
            }

            StartCoroutine(LockOn(t));
        
        
        }
        IEnumerator LockOn(Transform lockedHoop)
        {
            while (gameObject.transform.position != lockedHoop.position)
            {
                this.gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, lockedHoop.position, ballSpeed );
            }

            yield return null;

        }
    }

}

