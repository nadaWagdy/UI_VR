using System.Collections.Generic;
using System;

/// <summary>
/// Represents the processed data for a heat map visualization using bars.
/// This class holds a collection of cell data that is used to render each bar in the heat map.
/// </summary>
public class HeatMapUsingBarsData : IProcessedData
{
    /// <summary>
    /// Gets or sets the list of cell data used for the heat map bars.
    /// Each cell contains information necessary to render a bar in the heat map.
    /// </summary>
    public List<HeatMapUsingBarsCellData> HeatMapUsingBarsDataList { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HeatMapUsingBarsData"/> class.
    /// This constructor creates an empty heat map data list.
    /// </summary>
    public HeatMapUsingBarsData()
    {
        HeatMapUsingBarsDataList = new List<HeatMapUsingBarsCellData>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HeatMapUsingBarsData"/> class
    /// with a given list of cell data.
    /// </summary>
    /// <param name="heatMapUsingBarsDataList">The list of cell data used to render bars in the heat map.</param>
    public HeatMapUsingBarsData(List<HeatMapUsingBarsCellData> heatMapUsingBarsDataList)
    {
        HeatMapUsingBarsDataList = heatMapUsingBarsDataList ?? throw new ArgumentNullException(nameof(heatMapUsingBarsDataList), "Heat map data list cannot be null.");
    }

    /// <summary>
    /// Adds a new cell data to the heat map's data list.
    /// </summary>
    /// <param name="cellData">The cell data to be added to the list.</param>
    public void AddCellData(HeatMapUsingBarsCellData cellData)
    {
        if (cellData == null)
        {
            throw new ArgumentNullException(nameof(cellData), "Cell data cannot be null.");
        }
        HeatMapUsingBarsDataList.Add(cellData);
    }

    /// <summary>
    /// Clears all the cell data from the heat map's data list.
    /// </summary>
    public void ClearData()
    {
        HeatMapUsingBarsDataList.Clear();
    }

}


