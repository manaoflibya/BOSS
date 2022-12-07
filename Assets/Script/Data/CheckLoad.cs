using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLoad : MonoBehaviour
{
    public bool isLoad = false;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


}
