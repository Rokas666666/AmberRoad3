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

    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("OrangeLine"));
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
                    spheres.Add(Instantiate(objectToPlace, hit.point, Quaternion.identity));
                }
            }
        }
        else
        {
            if (spheres.Count > 10)
            {
                
                lineRenderer.widthMultiplier = 0.03f; // Adjust the width of the line

                lineRenderer.positionCount = spheres.Count;

                for (int i = 0; i < spheres.Count; i++)
                {
                    lineRenderer.SetPosition(i, spheres[i].transform.position);
                }
            }
            spheres.Clear();
        }
    }
}
