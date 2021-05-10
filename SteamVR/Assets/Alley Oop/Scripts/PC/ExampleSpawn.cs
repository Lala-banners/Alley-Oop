using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlleyOop.PC
{
    public class ExampleSpawn : MonoBehaviour
    {
        [SerializeField] private Transform spawnPos;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //To spawn specific pooled object with the tag Basketball
                GameObject basketball = BallPool.instance.GetPooledBasketball("Ball");
                if (basketball != null)
                {
                    basketball.transform.position = spawnPos.transform.position;
                    basketball.transform.rotation = spawnPos.transform.rotation;
                    basketball.SetActive(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                GameObject hoop = BallPool.instance.GetPooledBasketball("Hoop");
                if(hoop != null)
                {
                    hoop.transform.position = spawnPos.transform.position;
                    hoop.transform.rotation = spawnPos.transform.rotation;
                    hoop.SetActive(true);
                }
            }
        }
    }
}