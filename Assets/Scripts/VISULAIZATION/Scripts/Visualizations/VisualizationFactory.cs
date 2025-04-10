using System;


/// <summary>
/// Factory class for creating different types of visualizations.
/// </summary>
/// 
public class VisualizationFactory
{
    /// <summary>
    /// Creates a visualization based on the specified type.
    /// </summary>
    /// <param name="visualizationType">The type of visualization to create.</param>
    /// <returns>An instance of <see cref="IVisualization"/> representing the specified type.</returns>
    /// <exception cref="ArgumentException">Thrown when the visualization type is not recognized.</exception>
    public IVisualization CreateVisualization(VisualizationType visualizationType)
    {
        return visualizationType switch
        {
            VisualizationType.ScatterPlot => new ScatterPlot(),
            VisualizationType.HeatMapUsingBars => new HeatMapUsingBars(),
            VisualizationType.MultiSeriesBarChart => new MultiSeriesBarChart(),
            VisualizationType.StackedMultiSeriesBarChart => new StackedMultiSeriesBarChart(),
            VisualizationType.PieChart => new PieChart(),
            //VisualizationType.ForceDirectedGraph => new ForceDirectedGraph(),
            
            _ => throw new ArgumentException("Invalid visualization type", nameof(visualizationType))
        };
    }
}



