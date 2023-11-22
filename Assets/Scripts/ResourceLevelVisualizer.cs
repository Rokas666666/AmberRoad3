using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLevelVisualizer : MonoBehaviour
{
    private Building building;
    [SerializeField]
    GameObject levelPlane;
    [SerializeField]
    private float heightMax = 0;
    [SerializeField]
    private float heightMin = 0;
    [SerializeField]
    private int resourceTarget = 400;
    private void Awake()
    {
        building = GetComponent<Building>();
    }

    // Update is called once per frame
    void Update()
    {
        RecalculatePosition();
        HandleNoResources();
    }
    public void RecalculatePosition()
    {
        float heightFraction = 1-(resourceTarget - building.resources) / (float)resourceTarget;
        float height = heightMin + Mathf.Min(1, heightFraction)*(heightMax-heightMin);
        SetHeight(height);
    }
    public void SetHeight(float height)
    {
        levelPlane.transform.localPosition = new Vector3(levelPlane.transform.localPosition.x, height, levelPlane.transform.localPosition.z);
    }
    public void HandleNoResources()
    {
        if (building.resources <= 0)
        {
            levelPlane.SetActive(false);
        }
        else
        {
            levelPlane.SetActive(true);
        }
    }
}
