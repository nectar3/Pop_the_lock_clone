using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{

    public float growSpeed = 5f;

    void Start()
    {
        transform.localScale = new Vector2(0f, 0f);
        StartCoroutine(GrowSize(0f, 1f));

    }


    IEnumerator GrowSize(float start, float end)
    {
        transform.localScale = new Vector2(start, start);
        while (true)
        {
            yield return null;

            var next = transform.localScale.x + growSpeed * Time.deltaTime;
            if (next > end)
            {
                transform.localScale = new Vector2(end, end);
                break;
            }
            else
                transform.localScale = new Vector2(next, next);
        }
    }






}
