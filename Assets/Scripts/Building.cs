using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class Building : MonoBehaviour
{
    [SerializeField]
    public int resources;
    [SerializeField]
    public Building target;
    [SerializeField]
    public List<List<GameObject>> lines;
    public List<Building> targetList;
    public Material lineMat;

    public float sendTime = 0.0f;
    public GameObject resourceNodeObject;
    public float sendPeriod;

    public List<LineRenderer> finishedLines;
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

    public void addLine(List<GameObject> line)
    {
        List<GameObject> temp = new List<GameObject>();
        foreach (GameObject g in line)
        {
            temp.Add(g);
        }
        lines.Add(temp);
        finishedLines.Add(new GameObject().AddComponent<LineRenderer>());
        finishedLines[finishedLines.Count - 1].widthMultiplier = 0.03f; // Adjust the width of the line
        finishedLines[finishedLines.Count - 1].positionCount = temp.Count;
        finishedLines[finishedLines.Count - 1].material = lineMat;
        for (int i = 0; i < temp.Count; i++)
        {
            finishedLines[finishedLines.Count - 1].SetPosition(i, temp[i].transform.position);
        }
    }

    public void addTarget(Building t)
    {
        targetList.Add(t);
    }

    public void StartCommon()
    {
        resources = 0;
        lines = new List<List<GameObject>>();
    }

    public void OnGrab()
    {
        foreach (LineRenderer line in finishedLines)
        {
            line.enabled = false;
        }
        finishedLines = new List<LineRenderer>();
        lines = new List<List<GameObject>>();
        Debug.Log("killed lines");
    }

    public void OffGrab()
    {
        Debug.Log("asdsdakkkkkkkkkkkkkkkkkkasdasdas");
    }

    public void UpdateCommon()
    {
        if (sendPeriod == 0)
        {
            Debug.Log("UNSET sendPeriod IN BUILDING REALIZATION, SO NOT IN Building.cs");
        }
        if (sendTime > sendPeriod && lines.Count > 0 && resources > 0)
        {
            int index = 0;
            foreach (List<GameObject> line in lines)
            {
                resources -= 1;
                GameObject newResourceNode = Instantiate(resourceNodeObject, line[0].transform.position, Quaternion.identity);
                ResourceNode resourceNodeScript = newResourceNode.GetComponent<ResourceNode>();
                if (resourceNodeScript != null)
                {
                    resourceNodeScript.line = line;
                    resourceNodeScript.active = true;
                    resourceNodeScript.target = targetList[index];
                }
                index++;
            }
            sendTime = 0;
        }
        sendTime += Time.deltaTime;
    }
}
