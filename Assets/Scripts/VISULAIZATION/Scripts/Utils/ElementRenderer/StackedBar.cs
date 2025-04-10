using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Represents a single stacked bar used in a multi-series stacked bar chart.
/// Implements the IElementVisualization interface to render stacked bars based on segment values.
/// </summary>
public class StackedBar : IElementVisualization
{
    private readonly GameObject barPrefab; 
    private readonly float barWidth;        
    private readonly float spacing;         

    /// <summary>
    /// Initializes a new instance of the StackedBar class.
    /// </summary>
    /// <param name="barPrefab">The prefab used to create each segment of the stacked bar.</param>
    /// <param name="barWidth">The width of the bar segments.</param>
    /// <param name="spacing">The spacing between bars.</param>
    public StackedBar(GameObject barPrefab, float barWidth, float spacing)
    {
        this.barPrefab = barPrefab;
        this.barWidth = barWidth;
        this.spacing = spacing;
    }

    /// <summary>
    /// Renders a stacked bar GameObject based on the specified segment values, value index, and series index.
    /// </summary>
    /// <param name="parentTransform">The parent transform under which the stacked bar will be instantiated.</param>
    /// <param name="parameters">Parameters needed for rendering: segmentValues, valueIndex, seriesIndex.</param>
    /// <returns>The base GameObject for the stacked bar, or null if the prefab is missing or parameters are invalid.</returns>
    public GameObject RenderElement(Transform parentTransform, params object[] parameters)
    {
        // Validate parameters
        if (parameters.Length < 3 || 
            parameters[0] is not List<double> segmentValues || 
            parameters[1] is not int valueIndex || 
            parameters[2] is not int seriesIndex)
        {
            Debug.LogError("Invalid parameters for rendering StackedBar.");
            return null;
        }

        if (barPrefab == null)
        {
            Debug.LogError("Bar prefab is missing.");
            return null;
        }

        const float scalingFactor = 0.4f; 
        float adjustedBarWidth = barWidth * scalingFactor;
        float spacing = adjustedBarWidth;

        Vector3 basePosition = new(valueIndex * spacing, 0, seriesIndex * spacing);
        double cumulativeHeight = 0f;

        GameObject stackedBarParent = new($"StackedBar_{valueIndex}_{seriesIndex}");

        foreach (double segmentValue in segmentValues)
        {
            GameObject segment = GameObject.Instantiate(barPrefab);

            double segmentHeight = Math.Max(segmentValue / 10.0f, 0.1f);
            float adjustedSegmentHeight = (float)segmentHeight * scalingFactor;

            segment.transform.localScale = new Vector3(adjustedBarWidth, adjustedSegmentHeight, adjustedBarWidth);
            segment.transform.position = basePosition + new Vector3(0, (float)(cumulativeHeight + (adjustedSegmentHeight / 2)), 0);
            segment.name = $"Segment_{segmentValue}_{valueIndex}_{seriesIndex}";

            segment.transform.SetParent(stackedBarParent.transform);

            cumulativeHeight += adjustedSegmentHeight;
        }

        stackedBarParent.transform.position = basePosition;

        if (parentTransform != null)
        {
            stackedBarParent.transform.SetParent(parentTransform, true);
        }

        return stackedBarParent;
    }

}
