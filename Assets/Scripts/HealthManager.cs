using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] float maxHP;
    float HP;
    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
    }
    private void Update()
    {
        if (HP == 0)
        {
            Destroy(gameObject);
        }
    }

    public float getHP()
    {
        return HP;
    }
    public void Damage(float damage)
    {
        Debug.Log("Tower Hp: " + HP);
        HP -= damage;
        if(HP < 0) HP = 0;
    }
}
