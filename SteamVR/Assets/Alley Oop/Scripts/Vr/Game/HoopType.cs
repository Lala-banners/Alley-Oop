using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopTypes : MonoBehaviour
{
    private GameObject[] scoreHoop;
    // Start is called before the first frame update
    public void OnHoopTypePress(int hoopTypes)
    {
        GameObject[] hoops = GameObject.FindGameObjectsWithTag("Hoop");
        foreach (GameObject hoop in hoops)
        {
            Destroy(hoop);
        }

        Instantiate(scoreHoop[hoopTypes]);

    }
}
