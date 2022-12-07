using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField]
    private Transform targetObejct;

    [SerializeField]
    private LayerMask wallMask;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();

    }
    // Update is called once per frame
    void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObejct.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = targetObejct.position - transform.position;

        RaycastHit[] hitObejcts = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        for(int i =0;i<hitObejcts.Length;++i)
        {
            Material[] materials = hitObejcts[i].transform.GetComponent<MeshRenderer>().materials;

            for(int m = 0; m<materials.Length;++m)
            {
                materials[m].SetVector("_CoutoutPos", cutoutPos);
                materials[m].SetFloat("_CoutoutSize", 0.1f);
                materials[m].SetFloat("_FalloffSize", 0.05f);

            }
        }
    }
}
