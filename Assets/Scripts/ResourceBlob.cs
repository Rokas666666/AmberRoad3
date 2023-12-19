using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ResourceBlob : MonoBehaviour
{
    [SerializeField]
    private ResourceSpawner spawner;
    [SerializeField]
    int maxResources = 100;
    [SerializeField]
    int resourcesLeft;
    Vector3 originalScale;
    // Start is called before the first frame update
    void Start()
    {
        resourcesLeft = maxResources;
        originalScale = transform.localScale;
        spawner = GameObject.Find("Terrain").GetComponent<ResourceSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        float size = ((float)resourcesLeft / (float)maxResources)/2;
        size = 0.5f + size;
        transform.localScale = originalScale * size;
        if (resourcesLeft <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
    public int ExtractResources(int rate)
    {
        int extracted = Mathf.Min(resourcesLeft, rate);
        resourcesLeft -= extracted;
        return extracted;
    }
    public void SetResources(int ammount)
    {
        resourcesLeft = Mathf.Min(ammount, maxResources);
    }
    private void OnDestroy()
    {
        spawner.DecreaseCount();
    }
}
