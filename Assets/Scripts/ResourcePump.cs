using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePump : Building
{
    // Start is called before the first frame update
    void Start()
    {
        StartCommon();
        sendPeriod = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCommon();
    }
}
