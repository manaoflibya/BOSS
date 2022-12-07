using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackSecond : MonoBehaviour
{

    SphereCollider mySphereCollider;

    void Awake()
    {
        mySphereCollider = GetComponent<SphereCollider>();

    }

}
