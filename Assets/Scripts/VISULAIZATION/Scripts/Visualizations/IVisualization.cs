/// <summary>
/// Interface for visualizations that can render processed data.
/// </summary>
public interface IVisualization
{
    /// <summary>
    /// Renders the visualization using the provided processed data.
    /// </summary>
    /// <param name="data">The processed data to render.</param>
    void Render(IProcessedData data);


}

