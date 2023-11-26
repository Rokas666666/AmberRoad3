using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AoEffect : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SelfDestructCoroutine());
    }

    private IEnumerator SelfDestructCoroutine()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        GroundEnemy enemy = other.gameObject.GetComponent<GroundEnemy>();
        if (enemy != null)
        {
            enemy.ApplySpeedDebuff();
        }
        else
        {
            enemy = other.gameObject.GetComponentInParent<GroundEnemy>();
            if (enemy != null)
            {
                enemy.ApplySpeedDebuff();
            }
        }
    }
}
