using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Example implementation of a multi-series bar chart visualization.
/// </summary>
public class MultiSeriesBarChartExample : MonoBehaviour
{

    private IVisualization multiSeriesBarChart;

    /// <summary>
    /// Initializes the multi-series bar chart example.
    /// </summary>
    void Start()
    {
        
        VisualizationFactory factory = new();
        multiSeriesBarChart = factory.CreateVisualization(VisualizationType.MultiSeriesBarChart);

        // Example data for the multi-series bar chart.
        List<SeriesData> seriesDataList = new()
        {
            new SeriesData(0, new List<double> { 10, 20, 30}), // Series at altitude 0
            new SeriesData(0, new List<double> { 10, 20, 30 }),
            new SeriesData(0, new List<double> { 10, 20, 30 }),
            new SeriesData(1000, new List<double> { 15, 25, 35 }),
            new SeriesData(0, new List<double> { 10, 20, 30 }),// Series at altitude 1000
            new SeriesData(2000, new List<double> { 20, 30, 40 }) // Series at altitude 2000
        };

        // Create processed data for the chart
        IProcessedData data = new MultiSeriesData(seriesDataList);

        multiSeriesBarChart.Render(data);
    }
}



