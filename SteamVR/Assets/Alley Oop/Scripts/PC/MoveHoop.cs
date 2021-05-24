using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHoop : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    private void OnEnable()
    {
        StartCoroutine(MovePlease());
    }

    IEnumerator MovePlease()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(pointA.position, pointB.position, 2.0f);
            //yield return StartCoroutine(MoveObstacle(transform, pointA, pointB, 2.0f));
            //yield return StartCoroutine(MoveObstacle(transform, pointB, pointA, 2.0f));
        }
    }

   /* IEnumerator MoveObstacle(Transform thisT, Vector3 startPos, Vector3 endPos, float time)
    {
        float index = 0.0f;
        float rate = 1.0f / time;

        while (index < 1.0f)
        {
            index += Time.deltaTime * rate;
            thisT.position = Vector3.Lerp(startPos, endPos, index);
            yield return null;
        }
    }*/
}
