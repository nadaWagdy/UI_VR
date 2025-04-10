using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Maps cell values in a grid based on provided bar data and assigns colors to the cells.
/// </summary>
public class CellValueMapper : ICellValueMapper
{
    private readonly IColorMapper _colorMapper;
    private readonly float _maxHeight;

    /// <summary>
    /// Initializes a new instance of the <see cref="CellValueMapper"/> class.
    /// </summary>
    /// <param name="colorMapper">The color mapper used for mapping colors based on values.</param>
    /// <param name="maxHeight">The maximum height for scaling the cells.</param>
    public CellValueMapper(IColorMapper colorMapper, float maxHeight)
    {
        _colorMapper = colorMapper;
        _maxHeight = maxHeight;
    }

    /// <summary>
    /// Assigns values and colors to the cells in the grid based on the provided bar data.
    /// </summary>
    /// <param name="gridWidth">The width of the grid.</param>
    /// <param name="gridHeight">The height of the grid.</param>
    /// <param name="grid">The 2D array of GameObjects representing the grid.</param>
    /// <param name="barDataList">The list of bar data containing values to assign.</param>
    public void AssignValues(int gridWidth, int gridHeight, GameObject[,] grid, List<HeatMapUsingBarsCellData> barDataList)
    {
        int sampleIndex = 0;

        Debug.Log($"Grid Size: {gridWidth} x {gridHeight}. BarData List Count: {barDataList.Count}");

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                if (sampleIndex < barDataList.Count)
                {
                    HeatMapUsingBarsCellData barData = barDataList[sampleIndex];
                    double value = barData.Intensity;

                    GameObject cell = grid[x, z];
                    Debug.Log($"Attempting to assign value {value} to cell at grid[{x}, {z}]");

                    if (cell != null)
                    {
                        AssignCellValues(cell, value, x, z);
                        Debug.Log($"Successfully assigned value {value} to cell at grid[{x}, {z}]");
                    }
                    else
                    {
                        Debug.LogError($"Cell at grid[{x}, {z}] is null!");
                    }

                    sampleIndex++;
                }
            }
        }
    }

    /// <summary>
    /// Assigns the specified value and updates the cell's color and height.
    /// </summary>
    /// <param name="cell">The GameObject representing the cell.</param>
    /// <param name="value">The value to assign to the cell.</param>
    /// <param name="x">The x index of the cell in the grid.</param>
    /// <param name="z">The z index of the cell in the grid.</param>
    private void AssignCellValues(GameObject cell, double value, int x, int z)
    {
        if (cell == null)
        {
            Debug.LogError("Cell is null when assigning values!");
            return;
        }

        // Clamp the value for color mapping
        double clampedValue = Math.Clamp(value, 0, 100);
        Color color = _colorMapper.GetColorForValue((float)clampedValue);
        Debug.Log($"Assigning color {color} to cell at grid[{x}, {z}] with value {value}");

        Renderer renderer = cell.GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError($"Renderer is null on cell at grid[{x}, {z}]!");
            return; 
        }

        
        renderer.material.color = color;
        double height = value * _maxHeight;
        cell.transform.localScale = new Vector3(cell.transform.localScale.x, (float)height, cell.transform.localScale.z);

        
        Vector3 position = cell.transform.position;
        position.y = (float)height / 2f;
        cell.transform.position = position;

       
        cell.name = $"Cell_{x}_{z}_Value_{value:F2}";
    }
}


