using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScreenObject_Controller : MonoBehaviour
{
    Vector3 Right = Vector3.right;

    public enum KINDS
    {
        NORMAL,TREE,GRASS, GRASS_BACK, CLOUD_L,CLOUD_S,MURSHROOM
    }
    public KINDS myState = KINDS.NORMAL;
    public float MoveSpeed = 1f;
    private void Awake()
    {
        
        switch (myState)
        {
            case KINDS.NORMAL:
                Destroy(this.gameObject, 4f);

                break;
            case KINDS.TREE:
                Destroy(this.gameObject, 10f);

                break;
            case KINDS.MURSHROOM:
                {
                    Destroy(this.gameObject, 10f);
                    Vector3 landHeight = new Vector3(0f, 0f, Random.Range(-1f, 1f));
                    this.transform.position += landHeight;


                }

                break;

            case KINDS.GRASS:
                {
                    Destroy(this.gameObject, 10f);
                    Vector3 landHeight = new Vector3(0f,0f, Random.Range(-2f, 2f));
                    this.transform.position += landHeight;

                }

                break;
            case KINDS.GRASS_BACK:
                {
                    Destroy(this.gameObject, 9f);
                    Vector3 landHeight = new Vector3(0f, 0f, Random.Range(-0.5f, 0.5f));
                    this.transform.position += landHeight;

                }

                break;
            case KINDS.CLOUD_L:
                {
                    Destroy(this.gameObject, 34f);
                    Vector3 landHeight = new Vector3(0f, Random.Range(-2f, 2f), 0f);
                    this.transform.position += landHeight;
                }


                break;
            case KINDS.CLOUD_S:
                {
                    Destroy(this.gameObject, 18f);
                    Vector3 landHeight = new Vector3(0f, Random.Range(-2f, 0f), 0f);
                    this.transform.position += landHeight;

                }

                break;
        }

    }


    void Update()
    {

        this.transform.Translate(Right * Time.deltaTime * MoveSpeed);

    }
}
