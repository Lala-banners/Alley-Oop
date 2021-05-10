using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlleyOop.PC
{
    public class ExampleSpawn : MonoBehaviour
    {
        [SerializeField] private Transform spawnPos;
        public AudioSource rainbowFX;

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
                    rainbowFX.Play();
                }
            }
        }
    }
}