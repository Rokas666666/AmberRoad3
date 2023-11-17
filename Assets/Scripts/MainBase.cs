using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBase : Building
{
    public float period = 0.0f;
    public float sendPeriod = 0.0f;
    public float setPeriod = 0.0f;
    public ObjectPlacement obj;
    public List<GameObject> line;
    public GameObject resourceNodeObject;

    // Start is called before the first frame update
    void Start()
    {
        resources = 0;
    }

    void Update()
    {
        if (period > 1)
        {
            period = 0;
            resources += 1;
        }
        if (setPeriod > 5)
        {
            setPeriod = 0;
            if (obj.drawState == "disabled")
            {
                line = obj.spheres;
            }
            line = obj.spheres;
        }
        if (sendPeriod > 2)
        {
            sendPeriod = 0;
            GameObject newResourceNode = Instantiate(resourceNodeObject, line[0].transform.position, Quaternion.identity);
            ResourceNode resourceNodeScript = newResourceNode.GetComponent<ResourceNode>();

            if (resourceNodeScript != null)
            {
                resourceNodeScript.line = line;
            }
        }
        period += Time.deltaTime;
        sendPeriod += Time.deltaTime;
        setPeriod += Time.deltaTime;
    }
}
