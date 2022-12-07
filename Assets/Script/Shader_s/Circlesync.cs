using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circlesync : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_Position");
    public static int SizeID = Shader.PropertyToID("_Size");
    

    public Material Wallmaterial;
    public Camera Camera;
    public LayerMask Mask;

    void Update()
    {
        var dir = Camera.transform.position - this.transform.position;
        var ray = new Ray(this.transform.position, dir.normalized);

        if(Physics.Raycast(ray, 3000,Mask))
        {
            Wallmaterial.SetFloat(SizeID, 1);

        }
        else
        {
            Wallmaterial.SetFloat(SizeID, 0);
        }

        var view = Camera.WorldToViewportPoint(this.transform.position);
        Wallmaterial.SetVector(PosID, view);
    }
}
