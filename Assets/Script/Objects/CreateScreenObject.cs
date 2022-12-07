using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScreenObject1 : MonoBehaviour
{
    public List<GameObject> MapObjects;

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

        yield return new WaitForSeconds(7f);

        StartCoroutine(AddObject());

    }
}
