using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Action();

public class RangeSystem : MonoBehaviour
{
    public string Enemy = "Player";
    public Action battle = null;

    public Transform Target = null;
    AttackCheck weapon;

    private void Awake()
    {
        weapon = FindObjectOfType<AttackCheck>();
        Physics.IgnoreCollision(weapon.GetComponent<BoxCollider>(), this.GetComponent<SphereCollider>());
        Physics.IgnoreLayerCollision(29, 30);
        Physics.IgnoreLayerCollision(28, 30);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == Enemy)
        {
            Target = other.transform;
            battle?.Invoke();
            

        }

    }
}
