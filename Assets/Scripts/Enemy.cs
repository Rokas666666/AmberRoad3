using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    HealthManager towerHP;
    [SerializeField] float damage = 10;
    [SerializeField] float enemyHP = 100;
    Rigidbody rb;
    MonsterSpawner monsterSpawner;
    //GameObject target;
    
    public event Action<Enemy> OnEnemyKilled;
    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        towerHP = GameObject.FindWithTag("Tower").GetComponent<HealthManager>();
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        var targetHealth = collision.gameObject.GetComponent<HealthManager>();
        if (targetHealth)
        {
            DamageBuilding();
        }

    }*/
    /// <summary>
    /// damage done to towers and buildings
    /// </summary>
    public void DamageBuilding(float _damage)
    {
        towerHP.Damage(_damage);
        
    }
    public void DamageEnemy(float _damage)
    {
        enemyHP -= _damage;
        if(enemyHP <= 0)
        {
            KillEnemy();
            return;
        }

        rb.velocity = Vector3.up*5;
        
    }
    void KillEnemy()
    {
        if (!isDead)
        {
            if(monsterSpawner != null)
            {
                monsterSpawner.currentMonster.Remove(this.gameObject);
            }
           
            isDead = true;
            StartCoroutine(DelayedDestroy());

            // Notify subscribers that this enemy is killed
            OnEnemyKilled?.Invoke(this);
        }
    }
    public void SetSpawner(MonsterSpawner _monsterSpawner)
    {
        monsterSpawner = _monsterSpawner;
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(0.1f); // Adjust the delay as needed
        Destroy(gameObject);
    }
}
