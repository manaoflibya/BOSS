using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove_UI : MonoBehaviour
{
    MeshRenderer myRenderer;
    float offset;
    public float Materialspeed = 0f;
    private void Awake()
    {
        myRenderer = GetComponent<MeshRenderer>();


    }
    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * Materialspeed;
        myRenderer.material.mainTextureOffset  = new Vector2(offset, 0);
    }
}
