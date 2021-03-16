using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        Instance = GetComponent<SoundManager>();
    }

    
}
