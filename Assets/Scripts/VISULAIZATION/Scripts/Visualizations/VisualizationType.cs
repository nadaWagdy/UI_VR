using System;
using System.Collections.Generic;

/// <summary>
/// Enumeration for different types of visualizations.
/// </summary>
public enum VisualizationType
{
    ScatterPlot,
    HeatMapUsingBars,
    MultiSeriesBarChart,
    StackedMultiSeriesBarChart,
    PieChart
}

/// <summary>
/// Extension methods for VisualizationType.
/// </summary>
public static class VisualizationTypeExtensions
{
    // Dictionary to map enum values to string representations
    private static readonly Dictionary<VisualizationType, string> VisualizationTypeToStringMap = new()
    {
        { VisualizationType.ScatterPlot, "scatter_plot" },
        { VisualizationType.HeatMapUsingBars, "heat_map_using_bars" },
        { VisualizationType.MultiSeriesBarChart, "multi_series_bar_chart" },
        { VisualizationType.StackedMultiSeriesBarChart, "stacked_multi_series_bar_chart" },
        { VisualizationType.PieChart, "pie_chart" }
    };

    // Reverse map for string to enum lookup
    private static readonly Dictionary<string, VisualizationType> StringToVisualizationTypeMap = new();

    static VisualizationTypeExtensions()
    {
        // Populate the reverse map
        foreach (var kvp in VisualizationTypeToStringMap)
        {
            StringToVisualizationTypeMap[kvp.Value] = kvp.Key;
        }
    }

    /// <summary>
    /// Get the string representation of the VisualizationType.
    /// </summary>
    public static string ToString(this VisualizationType visualizationType)
    {
        return VisualizationTypeToStringMap.TryGetValue(visualizationType, out var value)
            ? value
            : throw new ArgumentException($"No string representation found for {visualizationType}");
    }

    /// <summary>
    /// Get the VisualizationType from a string.
    /// </summary>
    public static VisualizationType FromString(string visualizationString)
    {
        return StringToVisualizationTypeMap.TryGetValue(visualizationString, out var value)
            ? value
            : throw new ArgumentException($"No VisualizationType found for string '{visualizationString}'");
    }
}
