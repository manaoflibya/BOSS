using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScreenObject : MonoBehaviour
{
    public List<GameObject> MapObjects;
    public float CreateTime = 0f;
    private void Awake()
    {

        StartCoroutine(AddObject());

    }
    private void Update()
    {

    }

    IEnumerator AddObject()
    {
        Instantiate(MapObjects[Random.Range(0, MapObjects.Count)], this.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(CreateTime);

        StartCoroutine(AddObject());

    }
}
