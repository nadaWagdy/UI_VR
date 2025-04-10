using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles rendering a heat map using bars in 3D space.
/// </summary>
public class HeatMapUsingBars : MonoBehaviour, IVisualization
{
    private GameObject cellPrefab;
    private GameObject hoverDataPanelPrefab;
    private const string cellPrefabPath = "Prefabs/HeatMapUsingBars/HeatMapCell";
    private const string hoverDataPanelPrefabPath = "Prefabs/HeatMapUsingBars/HoverDataPanel";
    private const float spacing = 0.1f;
    private const float maxHeight = 0.01f;

    //private GridInitializer _gridInitializer;
    private readonly CellValueMapper _cellValueMapper;
    private readonly HeatMapUsingBarsUIManager _barChartUIManager;

    private int gridWidth;
    private int gridHeight;

    public HeatMapUsingBars()
    {
        LoadPrefabs();
        //_gridInitializer = new GridInitializer();
        _barChartUIManager = new HeatMapUsingBarsUIManager();
        _barChartUIManager.SetTextPrefab(hoverDataPanelPrefab);

        IColorMapper colorMapper = new ColorMapper();
        _cellValueMapper = new CellValueMapper(colorMapper, maxHeight);
    }

    private void LoadPrefabs()
    {
        cellPrefab = Resources.Load<GameObject>(cellPrefabPath);
        hoverDataPanelPrefab = Resources.Load<GameObject>(hoverDataPanelPrefabPath);

        if (cellPrefab == null || hoverDataPanelPrefab == null)
        {
            Debug.LogError("Prefab not found! Ensure it's in the Prefabs folder.");
        }
    }

    public void Render(IProcessedData data)
    {
        if (data is not HeatMapUsingBarsData heatMapData)
        {
            throw new ArgumentException("Expected HeatMapUsingBarsData", nameof(data));
        }

        gridWidth = Mathf.CeilToInt(Mathf.Sqrt(heatMapData.HeatMapUsingBarsDataList.Count));
        gridHeight = Mathf.CeilToInt((float)heatMapData.HeatMapUsingBarsDataList.Count / gridWidth);

        GameObject[,] grid = new GameObject[gridWidth, gridHeight];
        Vector3 gridOrigin = new(-2.4f, 0.0f, 2.15f);

        GenerateHeatMapUsingBarsModel(gridWidth, gridHeight, grid, heatMapData.HeatMapUsingBarsDataList, gridOrigin);

        _cellValueMapper.AssignValues(gridWidth, gridHeight, grid, heatMapData.HeatMapUsingBarsDataList);

        // Create the 3D grid
        //_gridInitializer.Create3DGrid(transform, gridWidth, gridHeight, spacing);
    }

    private void GenerateHeatMapUsingBarsModel(int width, int height, GameObject[,] grid, List<HeatMapUsingBarsCellData> data, Vector3 gridOrigin)
    {
        if (grid == null)
        {
            Debug.LogError("Grid is null");
            return;
        }

        if (data == null)
        {
            Debug.LogError("Data list is null");
            return;
        }

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                int index = x * height + z;
                if (index >= data.Count)
                {
                    Debug.LogWarning($"Index {index} is out of range for data list with count {data.Count}");
                    continue;
                }

                HeatMapUsingBarsCellData cellData = data[index];
                if (cellData == null)
                {
                    Debug.LogWarning($"Cell data at index {index} is null");
                    continue;
                }

                Vector3 position = new(gridOrigin.x + x, gridOrigin.y, gridOrigin.z - z);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);
                cell.transform.localScale = new Vector3(1, (float)cellData.Intensity * 100.0f, 1);
                grid[x, z] = cell;
            }
        }
    }
}
