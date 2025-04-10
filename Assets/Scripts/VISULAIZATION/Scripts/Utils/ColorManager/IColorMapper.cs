using UnityEngine;

/// <summary>
/// Defines the contract for a color mapper that retrieves colors based on numerical values.
/// </summary>
public interface IColorMapper
{
    /// <summary>
    /// Gets the color corresponding to the specified numerical value.
    /// </summary>
    /// <param name="value">The value to map to a color.</param>
    /// <returns>The color corresponding to the specified value.</returns>
    Color GetColorForValue(float value);
}
