using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public List<GameObject> line;
    public Building source;
    public int pos = 0;
    public bool active = false;
    public Building target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (line != null)
            {
                if (pos == line.Count && pos != 0)
                {
                    RaycastHit hitInfo;
                    if (Physics.Raycast(transform.position, Vector3.up, out hitInfo, Mathf.Infinity))
                    {
                        if (hitInfo.collider.gameObject.layer == 11) //TowerLayer
                        {
                            target.addResources(1);
                        }
                    }
                    Destroy(gameObject);
                    return;
                }
                if (line[pos] != null)
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
                        pos++;
                        /*
                        if (pos == line.Count)
                        {
                            target.addResources(1);
                            Destroy(gameObject);
                        }*/
                    }
                }
            }
        }
    }
}
