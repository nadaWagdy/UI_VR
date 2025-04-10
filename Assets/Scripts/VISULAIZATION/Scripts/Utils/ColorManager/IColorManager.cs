using UnityEngine;

/// <summary>
/// Interface for color management in visualizations.
/// </summary>
public interface IColorManager
{
    /// <summary>
    /// Retrieves a <see cref="Material"/> color based on the specified value.
    /// </summary>
    /// <param name="value">The value used to determine the color.</param>
    /// <returns>A <see cref="Material"/> representing the color.</returns>
    Material GetColor(float value);

    /// <summary>
    /// Applies a specified color to a GameObject.
    /// </summary>
    /// <param name="parameters">An array of parameters where the first element is the GameObject and the subsequent elements are used as needed for the color application.</param>
    void ApplyColor(params object[] parameters);
}
