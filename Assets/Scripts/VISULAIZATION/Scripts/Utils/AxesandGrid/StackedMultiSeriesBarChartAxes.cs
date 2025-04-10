using System;
using UnityEngine;

/// <summary>
/// Handles the creation of axes for a stacked multi-series bar chart visualization.
/// </summary>
public class StackedMultiSeriesBarChartAxes : AxesHelper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StackedMultiSeriesBarChartAxes"/> class.
    /// </summary>
    /// <param name="boundingCube">The bounding cube within which the chart is rendered.</param>
    public StackedMultiSeriesBarChartAxes(BoundingShape boundingCube) : base(boundingCube) { }

    /// <summary>
    /// Adds axis numbers to the chart based on the provided stacked multi-series data.
    /// </summary>
    /// <param name="data">The processed data containing stacked series information.</param>
    public override void AddAxisNumbers(IProcessedData data)
    {
        if (data is not StackedMultiSeriesData stackedData)
        {
            Debug.LogError("Invalid data type for axis numbers. Expected StackedMultiSeriesData.");
            return;
        }

        GameObject axisNumbersParent = new GameObject("Axis Numbers");
        axisNumbersParent.transform.SetParent(boundingCube.Transform, worldPositionStays: true);

        Vector3 cubeCenter = boundingCube.Transform.position;
        Vector3 halfScale = boundingCube.Transform.localScale / 2.0f;

        // Add Y-axis numbers
        double maxStackedValue = GetMaxStackedValue(stackedData);
        int ySteps = Math.Max(5, (int)(maxStackedValue / 10));
        for (int i = 0; i <= ySteps; i++)
        {
            double normalizedHeight = i / (double)ySteps;
            double height = normalizedHeight * boundingCube.Transform.localScale.y;

            CreateAxisLabel(axisNumbersParent.transform,
                cubeCenter + new Vector3(-halfScale.x * 1.1f, (float)(-halfScale.y + height), halfScale.z),
                (normalizedHeight * maxStackedValue).ToString("F1"),
                TextAnchor.MiddleRight);
        }

        // Add Z-axis labels (categories or series)
        AddAxisLabels(stackedData.StackedSeriesData.Count, axisNumbersParent.transform, cubeCenter, halfScale, isXAxis: false);

        // Add X-axis labels (segment indices)
        if (stackedData.StackedSeriesData.Count > 0)
        {
            int maxSegments = stackedData.StackedSeriesData[0].Bars.Count;
            AddAxisLabels(maxSegments, axisNumbersParent.transform, cubeCenter, halfScale, isXAxis: true);
        }
    }

    /// <summary>
    /// Calculates the maximum stacked value across all bars in the dataset.
    /// </summary>
    /// <param name="data">The stacked multi-series data.</param>
    /// <returns>The maximum stacked value.</returns>
    private double GetMaxStackedValue(StackedMultiSeriesData data)
    {
        double maxValue = 0;
        foreach (var series in data.StackedSeriesData)
        {
            foreach (var bar in series.Bars)
            {
                double barSum = 0;
                foreach (var segmentValue in bar.SegmentValues)
                {
                    barSum += segmentValue;
                }
                maxValue = Math.Max(maxValue, barSum);
            }
        }
        return maxValue;
    }
}
