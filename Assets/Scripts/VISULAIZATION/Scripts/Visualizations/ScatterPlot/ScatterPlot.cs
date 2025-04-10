using System;
using System.Collections.Generic;
using UnityEngine;

class ScatterPlot : IVisualization
{
    private const string BoundingCubePath = "Prefabs/ScatterPlotPrefabs/BoundingCubePrefab";
    private const string NodePrefabPath = "Prefabs/ScatterPlotPrefabs/PlotPointPrefab";
    private GameObject _nodePrefab;
    private GameObject _boundingCube;
    private readonly List<Vector3> _nodePositions = new List<Vector3>();
    private float _minX, _maxX, _minY, _maxY, _minZ, _maxZ;
    private List<GameObject> _textObjects = new List<GameObject>();

    public ScatterPlot()
    {
        LoadPrefabs();
    }

    private void LoadPrefabs()
    {
        _nodePrefab = Resources.Load<GameObject>(NodePrefabPath);
        GameObject boundingCubeResource = Resources.Load<GameObject>(BoundingCubePath);

        if (_nodePrefab == null || boundingCubeResource == null)
        {
            Debug.LogError("Node or bounding cube prefab not found! " +
                           "Ensure they're in the Prefabs folder.");
        }
        
        _boundingCube = UnityEngine.Object.Instantiate(boundingCubeResource);
    }

    /// <summary>
    /// Renders the scatter plot visualization based on the provided data.
    /// Uses normalized positions for node placement and draws axes based on min/max values.
    /// </summary>
    /// <param name="data">
    /// The processed data to visualize, expected to be an instance of <see cref="ScatterPlotData"/>.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown if the provided data is not of type <see cref="ScatterPlotData"/>.
    /// </exception>
    public void Render(IProcessedData data)
    {
        if (data is not ScatterPlotData plotData)
        {
            throw new ArgumentException("Expected ScatterPlotData", nameof(data));
        }

        // use normalized positions for rendering
        GenerateGraph(plotData.NormalizedPositions, plotData.OriginalPositions);

        // draw axes using min/max values
        AxisDrawer axisDrawer = new AxisDrawer(_boundingCube);
        _textObjects = axisDrawer.DrawAxes(
            plotData.MinX, plotData.MaxX, plotData.MinY, plotData.MaxY, plotData.MinZ, plotData.MaxZ);
    }

    /// <summary>
    /// Generates the scatter plot nodes within the bounding box using normalized positions.
    /// </summary>
    /// <param name="normalizedPositions">
    /// A list of <see cref="Vector3"/> objects representing the normalized positions of the nodes.
    /// </param>
    /// <param name="originalPositions">
    /// A list of <see cref="Vector3"/> objects representing the original positions of the nodes.
    /// </param>
    private void GenerateGraph(List<Vector3> normalizedPositions, List<Vector3> originalPositions)
    {
        Renderer boundingCubeRenderer = _boundingCube.GetComponent<Renderer>();
        Vector3 boundingBoxSize = boundingCubeRenderer.bounds.size;
        Vector3 boundingBoxCenter = boundingCubeRenderer.bounds.center;

        for (int i = 0; i < normalizedPositions.Count; i++)
        {
            Vector3 normalized = normalizedPositions[i];
            Vector3 localPosition = new(
                (normalized.x - 0.5f) * boundingBoxSize.x,
                (normalized.y - 0.5f) * boundingBoxSize.y,
                (normalized.z - 0.5f) * boundingBoxSize.z
            );

            Vector3 worldPosition = boundingBoxCenter + _boundingCube.transform.rotation * localPosition;

            GameObject node = UnityEngine.Object.Instantiate(_nodePrefab, worldPosition, Quaternion.identity);
            node.transform.SetParent(_boundingCube.transform);
            PointData.nodePositions.Add(node.transform, $"{originalPositions[i].x}, {originalPositions[i].y}, {originalPositions[i].z}");
        }
    }


    public void Update()
    {
        foreach (GameObject textObj in _textObjects)
        {
            textObj.transform.LookAt(Camera.main.transform);
            textObj.transform.Rotate(0, 180, 0);
        }
    }
}