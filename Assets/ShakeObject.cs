using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    public float speed = 20f;
    public float amount = 1f;
    public float duration = 0.3f;

    private float elapsedTime = 0f;
    private Vector3 startPos;

    private void Start()
    {
    }

    public IEnumerator ShakeCorout()
    {
        elapsedTime = 0;
        startPos = transform.position;

        while (true)
        {
            yield return null;

            transform.position = new Vector2(startPos.x + Mathf.Sin(Time.time * speed) * amount, startPos.y);

            elapsedTime += Time.deltaTime;
            if (elapsedTime >= duration)
                break;
        }
        transform.position = startPos;
        yield return new WaitForSeconds(1f);
    }




}
