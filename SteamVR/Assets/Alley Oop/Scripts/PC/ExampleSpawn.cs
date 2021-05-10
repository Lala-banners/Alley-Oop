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
                GameObject objectToSpawn = Pool.instance.GetPooledBasketball("Ball");
                if (objectToSpawn != null)
                {
                    objectToSpawn.transform.position = spawnPos.transform.position;
                    objectToSpawn.transform.rotation = spawnPos.transform.rotation;
                    objectToSpawn.SetActive(true);
                }
            }
        }
    }
}