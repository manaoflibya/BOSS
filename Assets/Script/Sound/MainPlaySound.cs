using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlaySound : MonoBehaviour
{
    public AudioClip StartBgm;
    AudioClip secondBgm;


    private void Start()
    {
        SoundManager.I.PlayBMG(StartBgm);
    }
}
