using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPool : MonoBehaviour
{
    public static BallPool instance;
    public List<GameObject> pooledBalls;
    public GameObject basketballObject;
    public int amountToPool;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pooledBalls = new List<GameObject>();
        GameObject temp;

        for (int i = 0; i < amountToPool; i++)
        {
            temp = Instantiate(basketballObject);
            temp.SetActive(false);
            pooledBalls.Add(temp);
        }
    }

    public GameObject GetPooledBasketball()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if(!pooledBalls[i].activeInHierarchy)
            {
                return pooledBalls[i];
            }
        }
        return null;
    }
}
