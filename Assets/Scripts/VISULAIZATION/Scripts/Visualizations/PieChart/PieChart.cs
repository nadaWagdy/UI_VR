using UnityEngine;

/// <summary>
/// Main class for rendering a 3D pie chart.
/// </summary>
public class PieChart : IVisualization
{
    private PieSlice pieSliceCreator; 
    private PieSliceColorManager colorManager; 
    private float radius; 
    private float height; 
    private GameObject slicesParent;

    /// <summary>
    /// Initializes a new instance of the PieChart class.
    /// </summary>
    /// <param name="radius">The radius of the pie chart.</param>
    /// <param name="height">The height of the pie slices.</param>
    public PieChart(float radius = 0.3f, float height = 0.3f)
    {
        this.radius = radius;
        this.height = height;
        pieSliceCreator = new PieSlice(radius, height);
        colorManager = new PieSliceColorManager();
    }

    /// <summary>
    /// Renders the pie chart based on the provided processed data.
    /// </summary>
    /// <param name="data">The processed data for rendering.</param>
    public void Render(IProcessedData data)
    {
        if (data is not PieChartData pieChartData)
        {
            Debug.LogError("Invalid data type for PieChart rendering.");
            return;
        }

        if (slicesParent == null)
            {
                slicesParent = new GameObject("Pie Chart");
            }

        double totalValue = 0;
        foreach (var slice in pieChartData.PieSliceData)
        {
            totalValue += slice.Value;
        }

        double currentAngle = 0;

        foreach (var sliceData in pieChartData.PieSliceData)
        {
            double angleSize = (sliceData.Value / totalValue) * 360;
            GameObject pieSlice = pieSliceCreator.RenderElement(slicesParent.transform, sliceData, currentAngle, angleSize);
            Material sliceColor = colorManager.GetColor((float)sliceData.Value);
            colorManager.ApplyColor(pieSlice, sliceColor);

            currentAngle += angleSize;
        }
    }

}


