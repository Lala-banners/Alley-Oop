using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public GameObject hoop;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

   
    public void OnTriggerEnter(Collider collider)
    {

        collider = hoop.GetComponent<Collider>();
        if (collider.gameObject.CompareTag("Ball"))
        {
            score++;
        }
    }

}
