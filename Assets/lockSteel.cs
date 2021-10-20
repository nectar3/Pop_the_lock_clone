using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockSteel : MonoBehaviour
{

    public float speed = 2f;
    public float targetDiff = 1f;

    Vector2 firstPos;

    Vector2 targetPos;

    void Start()
    {

        firstPos = transform.position;
        targetPos = new Vector2(firstPos.x, firstPos.y + targetDiff);
    }


    public IEnumerator Unlock()
    {
        while (true)
        {

            yield return null;

            transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * speed);
            if (Vector2.Distance(transform.position, targetPos) < 0.03f)
            {
                transform.position = targetPos;
                break;
            }
        }
        yield return new WaitForSeconds(1f);
        transform.position = firstPos;
    }



}
