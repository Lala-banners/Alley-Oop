using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public GameObject hoop;
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
       score = 0;
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }


    public void OnTriggerEnter(Collider collider)
    {

        //collider = hoop.GetComponent<Collider>();
        if (collider.gameObject.CompareTag("Ball"))
        {
            score++;
        }
    }

}
