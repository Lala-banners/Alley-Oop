using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHoop : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;

    IEnumerator Start()
    {
        pointA = transform.position;

        while (true)
        {
            yield return StartCoroutine(MoveObstacle(transform, pointA, pointB, 2.0f));
            yield return StartCoroutine(MoveObstacle(transform, pointB, pointA, 2.0f));
        }
    }

    IEnumerator MoveObstacle(Transform thisT, Vector3 startPos, Vector3 endPos, float time)
    {
        float index = 0.0f;
        float rate = 1.0f / time;

        while (index < 1.0f)
        {
            index += Time.deltaTime * rate;
            thisT.position = Vector3.Lerp(startPos, endPos, index);
            yield return null;
        }
    }
}
