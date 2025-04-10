using UnityEngine;

/// <summary>
/// Maps numerical values to a color gradient based on a predefined set of colors.
/// </summary>
public class ColorMapper : IColorMapper
{
    private readonly Color[] _colors;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorMapper"/> class with a predefined color gradient.
    /// </summary>
    public ColorMapper()
    {
        _colors = new Color[]
        {
            Color.blue,
            new(0.0f, 0.2f, 1.0f),
            new(0.0f, 0.4f, 1.0f),
            new(0.0f, 0.6f, 1.0f),
            Color.cyan,
            new(0.0f, 1.0f, 0.6f),
            new(0.0f, 1.0f, 0.4f),
            Color.green,
            new(0.6f, 1.0f, 0.0f),
            new(0.8f, 1.0f, 0.0f),
            Color.yellow,
            new(1.0f, 0.8f, 0.0f),
            new(1.0f, 0.6f, 0.0f),
            new(1.0f, 0.4f, 0.0f),
            Color.red
        };
    }

    /// <summary>
    /// Gets the color corresponding to the specified value.
    /// </summary>
    /// <param name="value">The value to map to a color.</param>
    /// <returns>The color corresponding to the specified value.</returns>
    public Color GetColorForValue(float value)
    {
        float normalizedValue = Mathf.Clamp01(value);
        float scaledValue = normalizedValue * (_colors.Length - 1); // Scale to the range of the color array

        int index1 = Mathf.FloorToInt(scaledValue); // Get the lower index
        int index2 = Mathf.Clamp(index1 + 1, 0, _colors.Length - 1); // Get the upper index
        float t = scaledValue - index1; // Calculate the interpolation factor

        return Color.Lerp(_colors[index1], _colors[index2], t); // Interpolate between the two colors
    }
}

