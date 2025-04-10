using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the contract for a cell value mapper that assigns values to grid cells.
/// </summary>
public interface ICellValueMapper
{
    /// <summary>
    /// Assigns values to a grid of GameObjects based on the provided bar data.
    /// </summary>
    /// <param name="gridWidth">The width of the grid.</param>
    /// <param name="gridHeight">The height of the grid.</param>
    /// <param name="grid">The two-dimensional array of GameObjects representing the grid.</param>
    /// <param name="barDataList">The list of bar data to assign values from.</param>
    void AssignValues(int gridWidth, int gridHeight, GameObject[,] grid, List<HeatMapUsingBarsCellData> barDataList);
}
