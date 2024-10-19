using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    //Declarations
    [SerializeField] private AudioClip backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusic(backgroundMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
