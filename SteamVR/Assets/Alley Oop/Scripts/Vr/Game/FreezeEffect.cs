using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnEffect()
    {
        StartCoroutine(Freeze());
    }
    IEnumerator Freeze()
    {
        Time.timeScale = 0f;
        
        yield return new WaitForSeconds(10);

        Time.timeScale = 1;
        yield return null;
    }
}
