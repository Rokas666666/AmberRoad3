using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public List<GameObject> line;
    public int pos = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (line != null)
        {
            Vector3 direction = line[pos].transform.position - transform.position;

            // Normalize the direction to get a unit vector
            direction.Normalize();

            // Move towards the target node
            transform.Translate(direction * 0.5f * Time.deltaTime);

            // Check if the object has reached the target node
            if (Vector3.Distance(transform.position, line[pos].transform.position) < 0.1f)
            {
                // Move to the next node in the line
                pos = (pos + 1) % line.Count;
            }
        }
    }
}
