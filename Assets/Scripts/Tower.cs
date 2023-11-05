using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Targets = new List<GameObject>();
    [SerializeField]
    private string EnemyTag = "Enemy";
    [SerializeField]
    float FireRate = 1.0f;
    [SerializeField]
    private Projectile projectile;
    [SerializeField]
    private Transform shootingPoint;
    [SerializeField]
    private bool ShootEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 1, FireRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTarget(GameObject gameObject)
    {
        this.Targets.Add(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(EnemyTag))
        {
            Targets.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(EnemyTag))
        {
            Targets.Remove(other.gameObject);
        }
    }
    private void Shoot()
    {
        if (Targets.Count <= 0 || !ShootEnabled) return;
        projectile.SetTarget(Targets[0]);
        Instantiate(projectile, shootingPoint.position, projectile.transform.rotation);
    }
}
