using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float damage = 10;
    HealthManager healthManager;
    Enemy health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        var targetHealth = collision.gameObject.GetComponent<HealthManager>();
        if(targetHealth)
        {
            DamageTarget();
        }
        
    }
    void DamageTarget()
    {
        healthManager.Damage(damage);
    }
}
