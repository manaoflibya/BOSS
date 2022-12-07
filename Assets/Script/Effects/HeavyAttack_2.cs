using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack_2 : MonoBehaviour
{
    GameObject sword;

    Vector3 myVec;
    List<Vector3> moveVec = new List<Vector3>();
    List<float> move = new List<float>();
    List<GameObject> instantiater = new List<GameObject>();
    FollowPlayer_Camera camerashake;

    private void Awake()
    {
        camerashake = FindObjectOfType<FollowPlayer_Camera>();


        moveVec.Add(Vector3.forward);
        moveVec.Add(Vector3.right);
        moveVec.Add(Vector3.back);
        moveVec.Add(Vector3.left);

        move.Add(1f);
        move.Add(1.7f);
        move.Add(2.5f);
       // move.Add(3f);
        instantiater.Add(EffectManager.I.HeavyAttackEffect3);
        instantiater.Add(EffectManager.I.HeavyAttackEffect4);

        sword = GameObject.Find("HeavyAttackPos_2");
        this.transform.rotation = sword.transform.rotation;
        StartCoroutine(EffectTime());

        
    }
    private void Start()
    {
        Vector3 offset = new Vector3(0f, 0.1f, 0f);
        myVec = this.transform.position + offset;

    }



    IEnumerator EffectTime()
    {


        yield return new WaitForSeconds(0.5f);

        for(int i =0;i<move.Count;i++)
        {
            for(int j = 0;j<moveVec.Count;j++)
            {
                Instantiate(instantiater[0], myVec + moveVec[j] * move[i], this.transform.rotation);
                Instantiate(instantiater[1], myVec + moveVec[j] * move[i], this.transform.rotation);
              
            }
            if(!PlayerManager.I.IsCameraChange)
            {
                StartCoroutine(camerashake.Shake(0.2f, 0.2f));
            }
            yield return new WaitForSeconds(0.45f);
        }

        Destroy(gameObject);
    }
}
