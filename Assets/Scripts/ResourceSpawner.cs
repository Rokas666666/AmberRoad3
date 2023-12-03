using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject resourceNode;
    [SerializeField]
    int maxCount = 5;
    [SerializeField]
    int currentCount;

    //Original mesh ----------------------
    private MeshFilter originalMeshFilter;
    private Mesh originalMesh;
    //------------------------------------

    void Awake()
    {
        originalMeshFilter = GetComponent<MeshFilter>();
        originalMesh = originalMeshFilter.mesh;
    }
    private void Start()
    {
        StartCoroutine(SpawnResource());
    }

    void Update()
    {
        
    }

    private Vector3 GetRandomPoint()
    {
        Vector3[] vertices = originalMesh.vertices;
        int randomVertexIndex = Random.Range(0, vertices.Length);
        return transform.TransformPoint(vertices[randomVertexIndex]);
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
            }
        }
    }
}
