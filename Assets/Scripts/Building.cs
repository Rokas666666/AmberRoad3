using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [SerializeField]
    public int resources;
    [SerializeField]
    public Building target;

    public int takeResources(int amount)
    {
        if (amount > resources)
        {
            resources -= amount;
            return amount;
        }
        else
        {
            int returnAmount = resources;
            resources = 0;
            return returnAmount;
        }
    }

    public void addResources(int amount)
    {
        resources += amount;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
