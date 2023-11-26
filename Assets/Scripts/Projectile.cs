using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected GameObject Target;
    [SerializeField]
    protected float MoveSpeed = 1f;
    [SerializeField]
    private string EnemyTag = "Enemy";
    [SerializeField]
    float damage;

    protected float startTime;
    protected Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null) { 
            Destroy(this.gameObject);
            return; }
        Move();
    }
    public virtual void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, MoveSpeed * Time.deltaTime);
    }
    public void SetTarget(GameObject target)
    {
        this.Target = target;
    }
    public void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject.GetComponent<Enemy>();
        var building = collision.gameObject.GetComponent<HealthManager>();
        var other = collision.gameObject;
        if (other.CompareTag(EnemyTag) && EnemyTag == "Enemy")
        {
            target.DamageEnemy(damage);
            Destroy(this.gameObject);
        }
        if (other.CompareTag(EnemyTag) && EnemyTag == "Tower")
        {
            building.Damage(damage);
            Destroy(this.gameObject);
        }
    }
    
}
