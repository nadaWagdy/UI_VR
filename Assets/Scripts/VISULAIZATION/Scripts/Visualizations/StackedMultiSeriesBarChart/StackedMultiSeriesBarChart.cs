using UnityEngine;

/// <summary>
/// Visualization class for rendering a stacked multi-series bar chart in Unity.
/// </summary>
public class StackedMultiSeriesBarChart : IVisualization, IPrefabLoading
{
    private StackedBar barCreator;
    private readonly BarColorManager colorManager;
    private readonly BoundingShape boundingCube;
    private readonly StackedMultiSeriesBarChartAxes axisLabelsAndLines;
    private GameObject barsParent;
    private const string barPrefabPath = "Prefabs/MultiSeriesBarChart/BarPrefab";

    /// <summary>
    /// Initializes a new instance of the <see cref="StackedMultiSeriesBarChart"/> class.
    /// </summary>
    public StackedMultiSeriesBarChart()
    {
        colorManager = new BarColorManager();
        LoadPrefabs();
        boundingCube = new BoundingShape();
        axisLabelsAndLines = new StackedMultiSeriesBarChartAxes(boundingCube);
    }

    /// <summary>
    /// Loads the bar prefab from the Resources folder.
    /// </summary>
    public void LoadPrefabs()
    {
        GameObject barPrefab = Resources.Load<GameObject>(barPrefabPath);

        if (barPrefab == null)
        {
            Debug.LogError("Bar prefab not found! Ensure it exists in the Prefabs folder at the correct path.");
        }
        else
        {
            this.barCreator = new StackedBar(barPrefab, 0.25f, 1.0f);
        }
    }

    /// <summary>
    /// Renders the stacked multi-series bar chart using the provided processed data.
    /// </summary>
    /// <param name="data">The processed data for the stacked multi-series bar chart, must be of type <see cref="StackedMultiSeriesData"/>.</param>
    public void Render(IProcessedData data)
    {
        if (data == null)
        {
            Debug.LogError("Data is null in StackedMultiSeriesBarChart.Render!");
            return;
        }

        if(data is StackedMultiSeriesData stackedData)
        {
            if (barsParent == null)
            {
                barsParent = new GameObject("Bar Chart");
            }

            for (int i = 0; i < stackedData.StackedSeriesData.Count; i++)
                {
                    var series = stackedData.StackedSeriesData[i];

                    for (int j = 0; j < series.Bars.Count; j++)
                    {
                        StackedBarData barData = series.Bars[j];

                        GameObject stackedBar = barCreator.RenderElement(barsParent.transform, barData.SegmentValues, i, j);
                        colorManager.ApplyColorsToStackedBar(stackedBar, i);
                    }
                }

            boundingCube.Initialize(barsParent, BoundingShape.Shape.Cube);
            boundingCube.Create3DGrid();
            axisLabelsAndLines.AddAxisLines();
            axisLabelsAndLines.AddAxisNumbers(stackedData);
        }
        else
        {
            Debug.LogError("Invalid data type passed to StackedMultiSeriesBarChart. Expected StackedMultiSeriesData.");
        }
    }
}
