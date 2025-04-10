using UnityEngine;
using System;

/// <summary>
/// Class responsible for rendering the axes, axis numbers, and labels for a multi-series bar chart in a 3D bounding cube.
/// </summary>
public class MultiSeriesBarChartAxes : AxesHelper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MultiSeriesBarChartAxes"/> class.
    /// </summary>
    /// <param name="boundingCube">The bounding cube to associate with this helper.</param>
    public MultiSeriesBarChartAxes(BoundingShape boundingCube) : base(boundingCube)
    {
    }

    /// <summary>
    /// Adds axis numbers and labels to the multi-series bar chart's axes.
    /// </summary>
    /// <param name="data">The processed data used to generate axis numbers and labels.</param>
    public override void AddAxisNumbers(IProcessedData data)
    {
        // Validate data type
        if (data is not MultiSeriesData barChartData)
        {
            Debug.LogError("Invalid data type for axis numbers. Expected MultiSeriesData.");
            return;
        }

        GameObject axisNumbersParent = new("Axis Numbers");
        axisNumbersParent.transform.SetParent(boundingCube.Transform, worldPositionStays: true);

        Vector3 cubeCenter = boundingCube.Transform.position;
        Vector3 halfScale = boundingCube.Transform.localScale / 2.0f;

        // Add Y-axis numbers
        double maxDataValue = GetMaxDataValue(barChartData);
        int ySteps = Math.Max(5, (int)(maxDataValue / 10));
        for (int i = 0; i <= ySteps; i++)
        {
            double normalizedHeight = i / (double)ySteps;
            double height = normalizedHeight * boundingCube.Transform.localScale.y;

            CreateAxisLabel(axisNumbersParent.transform,
                cubeCenter + new Vector3(-halfScale.x * 1.1f, (float)(-halfScale.y + height), halfScale.z),
                (normalizedHeight * maxDataValue).ToString("F1"),
                TextAnchor.MiddleRight);
        }

        // Add Z-axis series labels
        AddAxisLabels(barChartData.SeriesData.Count, axisNumbersParent.transform, cubeCenter, halfScale, isXAxis: false);

        // Add X-axis altitude labels
        for (int seriesIndex = 0; seriesIndex < barChartData.SeriesData.Count; seriesIndex++)
        {
            SeriesData series = barChartData.SeriesData[seriesIndex];
            double normalizedAltitude = series.Altitude / GetMaxAltitude(barChartData);
            double xPos = normalizedAltitude * boundingCube.Transform.localScale.x;

            CreateAxisLabel(axisNumbersParent.transform,
                cubeCenter + new Vector3((float)(-halfScale.x + xPos), -halfScale.y, (float)(-halfScale.z * 1.1f)),
                $"{series.Altitude:F1}",
                TextAnchor.MiddleCenter);
        }
    }

    /// <summary>
    /// Gets the maximum altitude value from the multi-series data.
    /// </summary>
    /// <param name="data">The multi-series data to analyze.</param>
    /// <returns>The maximum altitude value.</returns>
    private double GetMaxAltitude(MultiSeriesData data)
    {
        double maxAltitude = 0;
        foreach (var series in data.SeriesData)
        {
            maxAltitude = Math.Max(maxAltitude, series.Altitude);
        }
        return maxAltitude;
    }

    /// <summary>
    /// Gets the maximum data value from the multi-series data.
    /// </summary>
    /// <param name="data">The multi-series data to analyze.</param>
    /// <returns>The maximum data value.</returns>
    private double GetMaxDataValue(MultiSeriesData data)
    {
        double maxValue = double.MinValue;
        foreach (var series in data.SeriesData)
        {
            foreach (var value in series.Values)
            {
                maxValue = Math.Max(maxValue, value);
            }
        }
        return maxValue;
    }
}

