using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlleyOop
{
    public class Spawn : MonoBehaviour
    {
        public AudioSource rainbowFX;
        public GameObject button;

        private void Start()
        {
            button.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SpawnBall();
            }
        }

        public void SpawnBall()
        {
            //To spawn specific pooled object with the tag Basketball

            //Originally: GameObject basketball = BallPool.instance.GetPooledBasketball("Ball"); 
            GameObject basketball = Pool.instance.GetPooledBasketball("Ball");
            if (basketball != null)
            {
                basketball.transform.position = Pool.instance.spawnPos.transform.position;
                basketball.transform.rotation = Pool.instance.spawnPos.transform.rotation;
                basketball.SetActive(true);
                rainbowFX.Play();
            }
        }
    }
}