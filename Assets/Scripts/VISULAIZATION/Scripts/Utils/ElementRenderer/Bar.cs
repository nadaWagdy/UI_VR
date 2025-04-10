using UnityEngine;
using System;

/// <summary>
/// Represents a single bar used in a multi-series bar chart.
/// Implements the <see cref="IElementVisualization"/> interface to render bars based on data.
/// </summary>
public class Bar : IElementVisualization
{
    private readonly GameObject barPrefab; 
    private readonly float barWidth;         
    private readonly float spacing;       

    /// <summary>
    /// Initializes a new instance of the <see cref="Bar"/> class.
    /// </summary>
    /// <param name="barPrefab">The prefab used to create the bar.</param>
    /// <param name="barWidth">The width of the bar.</param>
    /// <param name="spacing">The spacing between bars.</param>
    public Bar(GameObject barPrefab, float barWidth, float spacing)
    {
        this.barPrefab = barPrefab;
        this.barWidth = barWidth;
        this.spacing = spacing;
    }

    /// <summary>
    /// Renders a bar GameObject based on the specified parameters: dataValue, valueIndex, and seriesIndex.
    /// </summary>
    /// <param name="parentTransform">The parent transform under which the bar will be instantiated.</param>
    /// <param name="parameters">An array of parameters needed for rendering: dataValue, valueIndex, seriesIndex.</param>
    /// <returns>The instantiated bar GameObject, or null if the prefab is missing or parameters are invalid.</returns>
public GameObject RenderElement(Transform parentTransform, params object[] parameters)
{
    // Validate parameters
    if (parameters.Length < 3 || 
        parameters[0] is not double dataValue || 
        parameters[1] is not int valueIndex || 
        parameters[2] is not int seriesIndex)
    {
        Debug.LogError("Invalid parameters for rendering Bar.");
        return null;
    }

    if (barPrefab == null)
    {
        Debug.LogError("Bar prefab is missing.");
        return null;
    }

    const float scalingFactor = 0.2f; 
    float adjustedBarWidth = barWidth * scalingFactor;
    float spacing = adjustedBarWidth * 2.0f;

    // Position the bar
    Vector3 barPosition = new(valueIndex * spacing, 0, seriesIndex * spacing);
    GameObject bar = GameObject.Instantiate(barPrefab, barPosition, Quaternion.identity);

    // Calculate bar dimensions
    double barHeight = Math.Max(dataValue / 10.0f, 0.1f);
    float adjustedBarHeight = (float)barHeight * scalingFactor;

    bar.transform.localScale = new Vector3(adjustedBarWidth, adjustedBarHeight, adjustedBarWidth);
    bar.transform.position = new Vector3(barPosition.x, adjustedBarHeight / 2, barPosition.z);

    bar.name = $"Bar_{dataValue}_{valueIndex}_{seriesIndex}";

    if (parentTransform != null)
    {
        bar.transform.SetParent(parentTransform, true);
    }

    return bar;
}




}





