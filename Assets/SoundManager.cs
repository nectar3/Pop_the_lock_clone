using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    AudioSource audioSource;

    private static SoundManager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
                return null;
            return instance;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayPointSound()
    {
        audioSource.Play();
    }

}
