using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages the colors for bars in visualizations.
/// </summary>
public class BarColorManager : IColorManager
{
    private readonly List<Material> materialPalette;

    /// <summary>
    /// Initializes a new instance of the <see cref="BarColorManager"/> class with a default material palette.
    /// </summary>
    public BarColorManager()
    {
        materialPalette = new List<Material>
        {
            Resources.Load<Material>("Materials/BlueGradientMat"),
            Resources.Load<Material>("Materials/TurqouiseGradientMat"),
            Resources.Load<Material>("Materials/GreenGradientMat"),
            Resources.Load<Material>("Materials/OrangeGradientMat"),
            Resources.Load<Material>("Materials/RedGradientMat")
            // Add more materials as needed
        };
    }

    /// <summary>
    /// Gets the material color for the specified series index.
    /// </summary>
    /// <param name="seriesIndex">The index of the series.</param>
    /// <returns>A <see cref="Material"/> from the palette based on the series index.</returns>
    public Material GetColor(float seriesIndex)
    {
        return materialPalette[(int)seriesIndex % materialPalette.Count];
    }

    /// <summary>
    /// Applies the specified material color to the given bar GameObject.
    /// </summary>
    /// <param name="parameters">An array of parameters where the first element is the bar GameObject and the second is the material color.</param>
    public void ApplyColor(params object[] parameters)
    {
        // Validate the parameters
        if (parameters.Length < 2 || 
            parameters[0] is not GameObject bar || 
            parameters[1] is not Material color)
        {
            Debug.LogWarning("Invalid parameters provided to ApplyColor method.");
            return;
        }

        Renderer renderer = bar.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = color; 
        }
        else
        {
            Debug.LogWarning("The specified bar does not have a Renderer component.");
        }
    }

    /// <summary>
    /// Applies colors to each segment of the stacked bar based on their index.
    /// </summary>
    /// <param name="stackedBar">The parent GameObject of the stacked bar segments.</param>
    /// <param name="seriesIndex">The index of the series, used to determine the color palette.</param>
    public void ApplyColorsToStackedBar(GameObject stackedBar, int seriesIndex)
    {
        int segmentIndex = 0;

        foreach (Transform segment in stackedBar.transform)
        {
            Material segmentColor = GetColor(segmentIndex);
            ApplyColor(segment.gameObject, segmentColor);
            segmentIndex++;
        }
    }
}

