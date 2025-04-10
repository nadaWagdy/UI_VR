using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Example implementation of a heat map using bars visualization.
/// </summary>
public class HeatMapUsingBarsExample : MonoBehaviour
{
    private IVisualization heatMapUsingBars;

    /// <summary>
    /// Initializes the heat map example.
    /// </summary>
    void Start()
    {
        VisualizationFactory factory = new();
        heatMapUsingBars = factory.CreateVisualization(VisualizationType.HeatMapUsingBars);

        // Generate example data for the heat map
        List<HeatMapUsingBarsCellData> heatMapCells = new()
        {
            new HeatMapUsingBarsCellData (0, 0, 0.2f),
            new HeatMapUsingBarsCellData (1, 0, 0.4f),
            new HeatMapUsingBarsCellData (2, 0, 0.6f),
            new HeatMapUsingBarsCellData (3, 0, 0.8f),
            new HeatMapUsingBarsCellData (4, 0, 1.0f),
            new HeatMapUsingBarsCellData (0, 1, 0.3f),
            new HeatMapUsingBarsCellData (1, 1, 0.5f),
            new HeatMapUsingBarsCellData (2, 1, 0.7f),
            new HeatMapUsingBarsCellData (3, 1, 0.9f)
            
        };

        IProcessedData heatMapData = new HeatMapUsingBarsData(heatMapCells);

        heatMapUsingBars.Render(heatMapData);
    }
}
