using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLine : MonoBehaviour
{
    [SerializeField]
    private Material outlineMaterial;
    [SerializeField]
    private float outlineScaleFactor;
    [SerializeField]
    private Color outlineColor;
    private Renderer outlineRenderer;


    private void Awake()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor); 
        outlineRenderer.enabled = true;

    }

    Renderer CreateOutline(Material outlineMat, float scaleFactory,Color color)
    {
        GameObject outlineobject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        Renderer rend = outlineobject.GetComponent<Renderer>();

        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_Scale", scaleFactory);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineobject.GetComponent<OutLine>().enabled = false;
        outlineobject.GetComponent<Collider>().enabled = false;

        rend.enabled = false;

        return rend;
    }
}
