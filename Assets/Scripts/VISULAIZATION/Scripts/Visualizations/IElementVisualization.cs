using UnityEngine;

/// <summary>
/// Defines the contract for visual elements that can be rendered in a visualization.
/// </summary>
public interface IElementVisualization
{
    /// <summary>
    /// Renders a visual element based on the provided parameters.
    /// </summary>
    /// <param name="parentTransform">The parent transform under which the element will be instantiated.</param>
    /// <param name="parameters">An array of parameters required for rendering the element.</param>
    /// <returns>A <see cref="GameObject"/> representing the rendered element, or null if rendering fails.</returns>
    GameObject RenderElement(Transform parentTransform, params object[] parameters); 
}
