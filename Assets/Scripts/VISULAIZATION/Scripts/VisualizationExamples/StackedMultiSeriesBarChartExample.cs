using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Example implementation of a stacked multi-series bar chart visualization.
/// </summary>
public class StackedMultiSeriesBarChartExample : MonoBehaviour
{
    private IVisualization stackedMultiSeriesBarChart;

    /// <summary>
    /// Initializes the stacked multi-series bar chart example.
    /// </summary>
    void Start()
    {
        VisualizationFactory factory = new();
        stackedMultiSeriesBarChart = factory.CreateVisualization(VisualizationType.StackedMultiSeriesBarChart); 

        // Example data for the stacked multi-series bar chart
        StackedMultiSeriesData stackedMultiSeriesData = new()
        {
            StackedSeriesData = new List<StackedSeriesData>
            {
                new() {
                    Bars = new List<StackedBarData>
                    {
                        new() { SegmentValues = new List<double> { 3, 8, 2 } }, // Series 1
                        new() { SegmentValues = new List<double> { 4, 6, 1 } }, // Series 2
                        new() { SegmentValues = new List<double> { 2, 3, 5 } }, // Series 3
                    }
                },
                new() {
                    Bars = new List<StackedBarData>
                    {
                        new() { SegmentValues = new List<double> { 1, 2, 8 } }, // Series 4
                        new() { SegmentValues = new List<double> { 2, 4, 2 } }, // Series 5
                        new() { SegmentValues = new List<double> { 3, 5, 4 } }, // Series 6
                    }
                },
                new() {
                    Bars = new List<StackedBarData>
                    {
                        new() { SegmentValues = new List<double> { 7, 6, 3 } }, // Series 7
                        new() { SegmentValues = new List<double> { 3, 4, 1 } }, // Series 8
                        new() { SegmentValues = new List<double> { 1, 7, 4 } }, // Series 9
                    }
                }
            }
        };

        stackedMultiSeriesBarChart.Render(stackedMultiSeriesData);
    }
}


