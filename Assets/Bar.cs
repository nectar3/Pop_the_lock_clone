using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{

    public GameObject DotSpawner;

    public bool isPlaying = false;

    public float speed = 8f;

    public bool clockwise { get; private set; } = false;
    // z축 각도 증가 => 반시계


    void Start()
    {
    }



    public void ResetBar()
    {
        clockwise = false;
        transform.rotation = Quaternion.identity;
        DotSpawner.GetComponent<DotSpawn>().Null_CurDot();
    }


    public void KeyPressed()
    {
        clockwise = !clockwise;
    }


    void Update()
    {
        if (isPlaying == false) return;

        var dir = clockwise ? -1 : 1;
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + speed * Time.deltaTime * dir);

    }
}
