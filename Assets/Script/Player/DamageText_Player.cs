using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText_Player : MonoBehaviour
{
    Transform myCamera;

    float DestroyTime = 1f;
    Vector3 offset = new Vector3(0, 2.2f, 0);
    // Start is called before the first frame update
    void Start()
    {
        myCamera = GameObject.Find("PlayerCamera").transform;


        Destroy(gameObject, DestroyTime);
        Vector3 RandomInit = new Vector3(0.25f, 0.25f, 0.25f);

        this.transform.localPosition += offset;
        this.transform.localPosition += new Vector3(Random.Range(-RandomInit.x, RandomInit.x),
            Random.Range(-RandomInit.y, RandomInit.y),
            Random.Range(-RandomInit.z, RandomInit.z));
    }

    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(myCamera.localRotation.eulerAngles);

    }

}
