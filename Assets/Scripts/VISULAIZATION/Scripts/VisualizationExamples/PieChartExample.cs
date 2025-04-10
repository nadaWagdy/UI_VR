using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Example implementation of a pie chart visualization.
/// </summary>
public class PieChartExample : MonoBehaviour
{
    private IVisualization pieChart;

    /// <summary>
    /// Initializes pie chart example.
    /// </summary>
    void Start()
    {
        
        VisualizationFactory factory = new();
        pieChart = factory.CreateVisualization(VisualizationType.PieChart);

        // Example data for the pie chart
        List<PieSliceData> pieSliceDataList = new()
        {
            new PieSliceData("Slice A", 10),
            new PieSliceData("Slice B", 70),
            new PieSliceData("Slice C", 20)
        };

        PieChartData pieChartData = new(pieSliceDataList);
        pieChart.Render(pieChartData);
    }
}
