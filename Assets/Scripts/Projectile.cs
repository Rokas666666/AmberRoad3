using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private GameObject Target;
    [SerializeField]
    private float MoveSpeed = 1f;
    [SerializeField]
    private string EnemyTag = "Enemy";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null) { return; }
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, MoveSpeed*Time.deltaTime);
    }
    public void SetTarget(GameObject target)
    {
        this.Target = target;
    }
    public void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;
        if (other.CompareTag(EnemyTag))
        {
            Destroy(this.gameObject);
        }
    }
}
