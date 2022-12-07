using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Trail_Edit : MonoBehaviour
{
    TrailRenderer trail;
    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.I.IsAttack)
            trail.enabled = true;
        else
            trail.enabled = false;

    }
}
