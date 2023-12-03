using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePipe : MonoBehaviour
{
    [SerializeField]
    private ResourcePump pump;
    [SerializeField]
    private ResourceBlob blob;
    [SerializeField]
    private float pumpRate = 1.0f;
    [SerializeField]
    private bool canPump = true;
    // Start is called before the first frame update
    void Awake()
    {
        pump = GetComponentInParent<ResourcePump>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPump && blob != null)
        {
            canPump = false;
            StartCoroutine(PumpResource());
        }
    }

    private IEnumerator PumpResource()
    {
        pump.addResources(blob.ExtractResources(5));
        yield return new WaitForSeconds(pumpRate);
        canPump = true;
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        var otherblob = other.GetComponent<ResourceBlob>();
        if (otherblob != null)
        {
            blob = otherblob;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var exitedBlob = other.GetComponent<ResourceBlob>();
        if (exitedBlob)
        {
            blob = null;
        }
    }
}
