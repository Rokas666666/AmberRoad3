using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject objectToPlace; // This is the object you want to place.
    public XRController controller; // Reference to the XRController.

    private void Update()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool isButtonPressed) && isButtonPressed)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.Log(hit.point);
                Instantiate(objectToPlace, hit.point, Quaternion.identity);
                if (hit.collider.gameObject.name == "SpawnSphere")
                {
                    Debug.Log("Hit the Landscape!");
                    Instantiate(objectToPlace, hit.point, Quaternion.identity);
                }
            }
        }
    }
}
