using System.Collections.Generic;
using UnityEngine;

public class Tower : Building
{
    [SerializeField]
    private List<GameObject> Targets = new List<GameObject>();
    [SerializeField]
    private string EnemyTag;
    [SerializeField]
    float FireRate = 1.0f;
    [SerializeField]
    private Projectile projectile;
    [SerializeField]
    private Transform shootingPoint;
    [SerializeField]
    private bool ShootEnabled = true;

    void Start()
    {
        InvokeRepeating("Shoot", 1, FireRate);
        resources = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(EnemyTag))
        {
            Targets.Add(other.gameObject);
            Debug.Log("Add method Enemies: " + Targets.Count);

            // Assuming the enemy script is on the same GameObject
            Enemy enemyScript = other.GetComponent<Enemy>();

            if (enemyScript != null)
            {
                // Subscribe to the OnEnemyKilled event
                enemyScript.OnEnemyKilled += HandleEnemyKilled;
            }
        }
    }
    
    private void OnTriggerExit(Collider other)//dis no work
    {
        if (other.CompareTag(EnemyTag))
        {
            Targets.Remove(other.gameObject);
            Debug.Log("Remove method Enemies: " + Targets.Count);
        }
    }

    private void HandleEnemyKilled(Enemy enemy)
    {
        // This method will be called every time an enemy is killed
        // You can put your logic here
        Targets.Remove(enemy.gameObject);
    }

    private void Shoot()
    {
        // Check and clean up the Targets list periodically
        CleanUpTargetsList();

        if (Targets.Count <= 0 || !ShootEnabled) return;
        projectile.SetTarget(Targets[0]);
        Instantiate(projectile, shootingPoint.position, projectile.transform.rotation);
    }

    private void CleanUpTargetsList()
    {
        // Remove null entries from the Targets list
        Targets.RemoveAll(target => target == null);
    }
}
