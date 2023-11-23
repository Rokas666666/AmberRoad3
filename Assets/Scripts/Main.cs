using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : Building
{
    public float period = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCommon();
        sendPeriod = 0.5f;
    }

    void Update()
    {
        UpdateCommon();
        if (period > 1)
        {
            period = 0;
            resources += 1;
        }
        period += Time.deltaTime;
    }
}
