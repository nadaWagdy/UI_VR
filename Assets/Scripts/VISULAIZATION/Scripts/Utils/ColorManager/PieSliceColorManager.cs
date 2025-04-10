using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages the colors for pie slices in visualizations.
/// </summary>
public class PieSliceColorManager : IColorManager
{
    private readonly List<Material> materialPalette;

    /// <summary>
    /// Initializes a new instance of the <see cref="PieSliceColorManager"/> class with a default material palette.
    /// </summary>
    public PieSliceColorManager()
    {
        materialPalette = new List<Material>
        {
            Resources.Load<Material>("Materials/BlueGradientMat"),
            Resources.Load<Material>("Materials/TurqouiseGradientMat"),
            Resources.Load<Material>("Materials/GreenGradientMat"),
            Resources.Load<Material>("Materials/OrangeGradientMat"),
            Resources.Load<Material>("Materials/RedGradientMat"),
            // Add more materials as needed
        };
    }

    /// <summary>
    /// Retrieves the appropriate color material for the given value.
    /// </summary>
    /// <param name="value">The value used to determine the color.</param>
    /// <returns>The corresponding <see cref="Material"/>.</returns>
    public Material GetColor(float value)
    {
        int index = (int)value % materialPalette.Count;
        return materialPalette[index];
    }

    /// <summary>
    /// Applies the specified color material to the given pie slice GameObject.
    /// </summary>
    /// <param name="parameters">An array where the first element is the GameObject to color and the second is the material to apply.</param>
    public void ApplyColor(params object[] parameters)
    {
        // Validate parameters
        if (parameters.Length < 2 ||
            parameters[0] is not GameObject pieSlice ||
            parameters[1] is not Material color)
        {
            Debug.LogError("Invalid parameters for applying color.");
            return;
        }

        Renderer renderer = pieSlice.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = color;
        }
        else
        {
            Debug.LogWarning("The specified pie slice does not have a Renderer component.");
        }
    }
}
