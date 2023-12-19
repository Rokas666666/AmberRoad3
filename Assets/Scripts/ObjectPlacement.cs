using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject objectToPlace; // This is the object you want to place.
    public GameObject test;
    public XRController controller; // Reference to the XRController.
    public XRRayInteractor ray;

    public Building sourceBuilding;
    public Building targetBuilding;
    public GameObject sourceObject;

    public List<GameObject> spheres;
    //public LineRenderer lineRenderer;
    public Material lineMat;

    public List<LineRenderer> finishedLines;
    public List<List<GameObject>> finishedSpheres;
    public int lineCount;

    public string drawState;
    public int startID;

    private void Start()
    {
        drawState = "disabled";
        finishedLines = new List<LineRenderer>();
        finishedSpheres = new List<List<GameObject>>();
        finishedLines.Add(gameObject.AddComponent<LineRenderer>());
        lineCount = 1;
        finishedLines[lineCount-1].material = lineMat;
        
    }

    private void Update()
    {
        // if (a is pressed or drawState is finished), raycast works and it hits the landscape - then continue
        if ((controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool isButtonPressed) && isButtonPressed) && drawState != "finished")
        {
            RaycastHit hit;
            if (ray.TryGetCurrent3DRaycastHit(out hit))
            {
                GameObject newSphere = Instantiate(objectToPlace, hit.point, Quaternion.identity);
                if (drawState == "disabled")
                {
                    if (hit.collider.gameObject.layer == 11)
                    {
                        newSphere.transform.position = hit.collider.gameObject.transform.position;
                        startID = hit.collider.gameObject.GetInstanceID();
                        sourceObject = hit.collider.gameObject;
                        drawState = "started";
                        spheres.Add(newSphere);
                    }
                }
                else if (drawState == "started")
                {
                    if (hit.collider.gameObject.layer == 12)
                    {
                        spheres.Add(newSphere);
                    }
                    if (hit.collider.gameObject.layer == 11 && hit.collider.gameObject.GetInstanceID() != startID)
                    {
                        drawState = "finished";
                        spheres[0].transform.position = sourceObject.transform.position;
                        spheres[spheres.Count - 1].transform.position = hit.collider.gameObject.transform.position; //HERE, WHY DOES IT NOT FIND IT
                        sourceBuilding = GetBuildingType(sourceObject);
                        targetBuilding = GetBuildingType(hit.collider.gameObject);
                    }
                }
            }
        }
        else
        {
            
            if (spheres.Count >= 2 && drawState == "finished")
            {
                drawState = "disabled";
                
                /*
                finishedLines[lineCount - 1].widthMultiplier = 0.03f; // Adjust the width of the line
                finishedLines[lineCount - 1].positionCount = spheres.Count;
                finishedLines[lineCount - 1].material = lineMat;
                for (int i = 0; i < spheres.Count; i++)
                {
                    finishedLines[lineCount - 1].SetPosition(i, spheres[i].transform.position);
                }*/
                lineCount++;
                finishedSpheres.Add(spheres);
                if (sourceBuilding == null)
                {
                    Debug.Log("YOU PROBABLY ADDED A NEW TYPE OF BUILDING AND DIDNT ADD IT TO GetBuildingType OR YOU PUT THE SCRIPT ON THE OBJECT NOT THE PREFAB");
                }
                sourceBuilding.addLine(spheres);
                sourceBuilding.addTarget(targetBuilding);
                LineRenderer joe = new GameObject().AddComponent<LineRenderer>();
                finishedLines.Add(joe);

                startID = 0;
            }
            spheres.Clear();
            drawState = "disabled";
        }
    }
    public static Building GetBuildingType(GameObject gameObject)
    {
        if (gameObject == null)
        {
            Debug.LogError("GameObject is null.");
            return null;
        }
        
        // Check if the GameObject has MainBase component
        Main mainBaseComponent = gameObject.GetComponent<Main>();
        if (mainBaseComponent != null)
        {
            return mainBaseComponent;
        }

        // Check if the GameObject has Tower component
        Tower towerComponent = gameObject.GetComponent<Tower>();
        if (towerComponent != null)
        {
            return towerComponent;
        }

        ResourcePump pumpComponent = gameObject.GetComponent<ResourcePump>();
        if (pumpComponent != null)
        {
            return pumpComponent;
        }

        ResourcePipe pipeComponent = gameObject.GetComponent<ResourcePipe>();
        if (pipeComponent != null)
        {
            return pipeComponent.pump;
        }

        // If the GameObject has neither MainBase nor Tower component
        Debug.LogWarning("GameObject has neither MainBase nor Tower component.");
        return null;
    }

}


/*
 * RaycastHit hit;
            if (ray.TryGetCurrent3DRaycastHit(out hit))
            {
                if (hit.collider.gameObject.name == "Landscape")
                {
                    //create a sphere and check if it raycast hits a tower above it
                    GameObject newSphere = Instantiate(objectToPlace, hit.point, Quaternion.identity);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(newSphere.transform.position, Vector3.up, out hitInfo, Mathf.Infinity))
                    {
                        if (hitInfo.collider.gameObject.layer == 11) //TowerLayer
                        {
                            if (drawState == "disabled")
                            {
                                //start the drawState if it hasnt started and get the id of the first tower
                                startID = hitInfo.collider.gameObject.GetInstanceID();
                                sourceObject = hitInfo.collider.gameObject;
                                drawState = "started";
                            }
                            else if (drawState == "started" && hitInfo.collider.gameObject.GetInstanceID() != startID)
                            {
                                //end the drawState if the second tower was reached, then find the start end objects and set the line to them
                                drawState = "finished";
                                //GameObject startObject = (GameObject)EditorUtility.InstanceIDToObject(startID);
                                spheres[0].transform.position = sourceObject.transform.position;
                                spheres[spheres.Count-1].transform.position = hitInfo.collider.gameObject.transform.position; //HERE, WHY DOES IT NOT FIND IT
                                sourceBuilding = GetBuildingType(sourceObject);
                                targetBuilding = GetBuildingType(hitInfo.collider.gameObject);
                                //sourceBuilding.target = targetBuilding;
                                Debug.Log(sourceObject.name + " + " + hitInfo.collider.gameObject.name);
                            }
                        }
                    }
                    //if we are drawing a line, add the sphere
                    if (drawState == "started")
                    {
                        spheres.Add(newSphere);
                    }
                    
                }
            }
*/