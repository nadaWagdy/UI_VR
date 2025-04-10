using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Standardizes raw JSON data into a format suitable for scatter plot visualization.
/// This includes calculating min/max values, normalizing positions, and retaining the original data.
/// </summary>
public class ScatterPlotStandardizer : IDataStandardizer
{
    /// <summary>
    /// Processes the raw JSON data to produce standardized scatter plot data.
    /// Performs min/max calculation, normalization, and retains the original positions.
    /// </summary>
    /// <param name="jsonData">The raw JSON string containing scatter plot data.</param>
    /// <returns>
    /// A <see cref="ScatterPlotData"/> object containing original positions,
    /// normalized positions, and min/max values for each axis.
    /// </returns>
    public IProcessedData StandardizeData(string jsonData)
    {
        ScatterPlotValues scatterPlotValues = ParseJsonData(jsonData);

        List<Vector3> originalPositions = new();
        List<Vector3> normalizedPositions = new();
        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;
        float minZ = float.MaxValue, maxZ = float.MinValue;

        for (int i = 0; i < scatterPlotValues.x_values.Length; i++)
        {
            float x = scatterPlotValues.x_values[i];
            float y = scatterPlotValues.y_values[i];
            float z = scatterPlotValues.z_values[i];

            UpdateMinMax(ref minX, ref maxX, x);
            UpdateMinMax(ref minY, ref maxY, y);
            UpdateMinMax(ref minZ, ref maxZ, z);

            originalPositions.Add(new Vector3(x, y, z));
        }

        NormalizePositions(originalPositions, normalizedPositions, minX, maxX, minY, maxY, minZ, maxZ);

        return new ScatterPlotData(originalPositions, normalizedPositions, minX, maxX, minY, maxY, minZ, maxZ);
    }

    /// <summary>
    /// Parses the raw JSON string into a <see cref="ScatterPlotValues"/> object.
    /// </summary>
    /// <param name="jsonData">The raw JSON string containing scatter plot data.</param>
    /// <returns>
    /// A <see cref="ScatterPlotValues"/> object containing the raw x, y, and z values.
    /// </returns>
    private ScatterPlotValues ParseJsonData(string jsonData)
    {
        return JsonUtility.FromJson<ScatterPlotValues>(jsonData);
    }

    /// <summary>
    /// Updates the minimum and maximum values for a given axis.
    /// </summary>
    /// <param name="min">The current minimum value, updated if the new value is smaller.</param>
    /// <param name="max">The current maximum value, updated if the new value is larger.</param>
    /// <param name="value">The new value to compare.</param>
    private void UpdateMinMax(ref float min, ref float max, float value)
    {
        min = Mathf.Min(min, value);
        max = Mathf.Max(max, value);
    }

    /// <summary>
    /// Normalizes the original positions to a range of [0, 1] based on min/max values for each axis.
    /// </summary>
    /// <param name="original">
    /// A list of <see cref="Vector3"/> objects representing the original positions.
    /// </param>
    /// <param name="normalized">
    /// A list of <see cref="Vector3"/> objects where normalized positions will be stored.
    /// </param>
    /// <param name="minX">The minimum x-coordinate value.</param>
    /// <param name="maxX">The maximum x-coordinate value.</param>
    /// <param name="minY">The minimum y-coordinate value.</param>
    /// <param name="maxY">The maximum y-coordinate value.</param>
    /// <param name="minZ">The minimum z-coordinate value.</param>
    /// <param name="maxZ">The maximum z-coordinate value.</param>
    private void NormalizePositions(List<Vector3> original, List<Vector3> normalized,
        float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        foreach (Vector3 position in original)
        {
            float normalizedX = Mathf.InverseLerp(minX, maxX, position.x);
            float normalizedY = Mathf.InverseLerp(minY, maxY, position.y);
            float normalizedZ = Mathf.InverseLerp(minZ, maxZ, position.z);

            normalized.Add(new Vector3(normalizedX, normalizedY, normalizedZ));
        }
    }
}

[System.Serializable]
public class NodeDataWrapper
{
    public List<NodeData> nodes;
}

[System.Serializable]
public class NodeData
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class ScatterPlotValues
{
    public float[] x_values;
    public float[] y_values;
    public float[] z_values;
}