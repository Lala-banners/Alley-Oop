using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnEffect()
    {
        StartCoroutine(Slow());
    }
    IEnumerator Slow()
    {
        Time.timeScale = 0.5f;
        
        yield return new WaitForSeconds(15);

        Time.timeScale = 1;
        yield return null;
    }
}
