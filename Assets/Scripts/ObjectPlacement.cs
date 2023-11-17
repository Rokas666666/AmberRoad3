using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject objectToPlace; // This is the object you want to place.
    public XRController controller; // Reference to the XRController.
    public XRRayInteractor ray;

    public List<GameObject> spheres;
    public LineRenderer lineRenderer;

    public string drawState;
    public int startID;

    private void Start()
    {
        drawState = "disabled";
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Standard"));
        lineRenderer.material.color = Color.yellow;
    }

    private void Update()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool isButtonPressed) && isButtonPressed)
        {
            RaycastHit hit;
            if (ray.TryGetCurrent3DRaycastHit(out hit))
            {
                if (hit.collider.gameObject.name == "Landscape")
                {
                    //Debug.Log("Hit the Landscape!");
                    GameObject newSphere = Instantiate(objectToPlace, hit.point, Quaternion.identity);
                    RaycastHit hitInfo;
                    // Perform a raycast from the position where the object was instantiated
                    if (Physics.Raycast(newSphere.transform.position, Vector3.up, out hitInfo, Mathf.Infinity))
                    {
                        // Check if the raycast hit something
                        if (hitInfo.collider.gameObject.layer == 11) //TowerLayer
                        {
                            if (drawState == "disabled")
                            {
                                Debug.Log("started");
                                startID = hitInfo.collider.gameObject.GetInstanceID();
                                drawState = "started";
                            }
                            else if (drawState == "started" && hitInfo.collider.gameObject.GetInstanceID() != startID)
                            //else if (drawState == "started")
                            {
                                Debug.Log("finished");
                                drawState = "finished";
                            }
                        }
                    }
                    if (drawState == "started")
                    {
                        Debug.Log("added");
                        spheres.Add(newSphere);
                    }
                }
            }
        }
        else
        {
            Debug.Log("drawState = " + drawState);
            if (spheres.Count > 3 && drawState == "finished")
            {
                lineRenderer.widthMultiplier = 0.03f; // Adjust the width of the line
                lineRenderer.positionCount = spheres.Count;
                for (int i = 0; i < spheres.Count; i++)
                {
                    lineRenderer.SetPosition(i, spheres[i].transform.position);
                }
            }
            //spheres.Clear();
            drawState = "disabled";
        }
    }
}
