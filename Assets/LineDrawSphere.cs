using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Store a reference to the collided object
    public GameObject collidedObject;

    // This method is called when a collision is detected
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has a specific tag
        if (collision.gameObject.layer == LayerMask.NameToLayer("TowerLayer"))
        {
            collidedObject = collision.gameObject;
        }
    }
}
