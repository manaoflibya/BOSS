using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCanvas_Controller : MonoBehaviour
{
    public AudioClip thouch_S;

    public void OnLoadingScene()
    {
        LoadingCanvas_Controller.LoadSene("MainMenu");

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.I.PlayEffectSound(thouch_S);
        }

    }
}
