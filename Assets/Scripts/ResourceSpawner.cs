using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject resourceNode;
    [SerializeField]
    int maxCount = 20;
    [SerializeField]
    int currentCount;

    // Original NavMesh surface ----------------------
    private NavMeshSurface navMeshSurface;
    //------------------------------------

    void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Start()
    {
        Instantiate(resourceNode, GetRandomPoint(), Quaternion.identity);
        currentCount++;
        Instantiate(resourceNode, GetRandomPoint(), Quaternion.identity);
        currentCount++;
        Instantiate(resourceNode, GetRandomPoint(), Quaternion.identity);
        currentCount++;
        Instantiate(resourceNode, GetRandomPoint(), Quaternion.identity);
        currentCount++;
        Instantiate(resourceNode, GetRandomPoint(), Quaternion.identity);
        currentCount++;
        Instantiate(resourceNode, GetRandomPoint(), Quaternion.identity);
        currentCount++;
        Instantiate(resourceNode, GetRandomPoint(), Quaternion.identity);
        currentCount++;
        StartCoroutine(SpawnResource());
    }

    void Update()
    {
        // You can add additional logic related to the NavMesh if needed.
    }

    private Vector3 GetRandomPoint()
    {
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

        int randomIndex = Random.Range(0, triangulation.vertices.Length);
        return triangulation.vertices[randomIndex];
    }

    public void DecreaseCount()
    {
        currentCount--;
    }

    private IEnumerator SpawnResource()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            if (currentCount < maxCount)
            {
                Instantiate(resourceNode, GetRandomPoint(), Quaternion.identity);
                currentCount++;
                // Update the NavMesh after spawning a new resource
                UpdateNavMesh();
            }
        }
    }

    private void UpdateNavMesh()
    {
        // You may need to bake the NavMesh again to reflect the changes.
        navMeshSurface.BuildNavMesh();
    }
}