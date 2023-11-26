using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GlueProjectile : Projectile
{
    [SerializeField]
    private float arcHeight = 2f;
    [SerializeField]
    GameObject AoEprefab;
    public override void Move()
    {
        float journeyLength = Vector3.Distance(startPosition, Target.transform.position);
        float fractionOfJourney = (Time.time - startTime) * MoveSpeed / journeyLength;

        // Calculate the trajectory in an arc
        Vector3 nextPosition = Vector3.Lerp(startPosition, Target.transform.position, fractionOfJourney);
        nextPosition.y += Mathf.Sin(fractionOfJourney * Mathf.PI) * arcHeight;

        // Move the projectile
        transform.position = nextPosition;

        // Rotate towards the target
        transform.LookAt(Target.transform);

        // If the projectile has reached the target, you can destroy it or apply other logic
        if (fractionOfJourney >= 1.0f)
        {
            // You may want to destroy the projectile or trigger some other behavior here
            Destroy(gameObject);
        }
    }
    public void InstantiateAoE()
    {
        if (AoEprefab != null)
        {
            // Instantiate the AoE effect at the projectile's position
            GameObject aoeInstance = Instantiate(AoEprefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("AoE effect prefab is not assigned to the projectile.");
        }
    }
    private void OnDestroy()
    {
        InstantiateAoE();
    }
}
