using UnityEngine;

/// <summary>
/// Represents a multi-series bar chart visualization.
/// </summary>
public class MultiSeriesBarChart : MonoBehaviour, IVisualization
{
    private GameObject barPrefab;
    private const string barPrefabPath = "Prefabs/MultiSeriesBarChart/BarPrefab";
    private readonly float barWidth;
    private readonly float spacing;
    private readonly Bar seriesBar;
    private readonly BarColorManager colorManager;
    private readonly BoundingShape boundingCube;
    private MultiSeriesBarChartAxes axisLabelsAndLines;
    private GameObject barsParent;

    /// <summary>
    /// Constructor for MultiSeriesBarChart.
    /// </summary>
    /// <param name="barWidth">The width of each bar (default is 0.5).</param>.
    /// <param name="spacing">The spacing between bars (default is 1.0).</param>
    public MultiSeriesBarChart(float barWidth = 0.5f, float spacing = 1.0f)
    {
        this.barWidth = barWidth;
        this.spacing = spacing;

        LoadPrefabs();

        boundingCube = new BoundingShape();

        axisLabelsAndLines = new MultiSeriesBarChartAxes(boundingCube);
        seriesBar = new Bar(barPrefab, barWidth, spacing);
        colorManager = new BarColorManager();
    }

    /// <summary>
    /// Loads the bar prefab from the Resources folder.
    /// </summary>
    private void LoadPrefabs()
    {
        barPrefab = Resources.Load<GameObject>(barPrefabPath);

        if (barPrefab == null)
        {
            Debug.LogError("Bar prefab not found! Ensure it's in the Prefabs folder.");
        }
    }

    /// <summary>
    /// Renders the multi-series bar chart with a bounding cube.
    /// </summary>
    /// <param name="data">The processed data to render the chart.</param>
    public void Render(IProcessedData data)
    {
        if (data == null)
        {
            Debug.LogError("Data is null in MultiSeriesBarChart.Render!");
            return;
        }

        if (data is MultiSeriesData barChartData)
        {
            Debug.Log("Rendering MultiSeriesBarChart...");

            if (barsParent == null)
            {
                barsParent = new GameObject("Bar Chart");
            }

            for (int seriesIndex = 0; seriesIndex < barChartData.SeriesData.Count; seriesIndex++)
            {
                var series = barChartData.SeriesData[seriesIndex];
                if (series == null)
                {
                    Debug.LogError($"Series data is null at index {seriesIndex}");
                    continue;
                }

                Material seriesColor = colorManager.GetColor(seriesIndex);
                RenderSeries(series, seriesIndex, seriesColor);
            }

            boundingCube.Initialize(barsParent, BoundingShape.Shape.Cube);
            boundingCube.Create3DGrid();
            axisLabelsAndLines.AddAxisLines();
            axisLabelsAndLines.AddAxisNumbers(barChartData);
        }
        else
        {
            Debug.LogError("Invalid data type passed to MultiSeriesBarChart. Expected MultiSeriesData.");
        }
    }

    /// <summary>
    /// Renders a specific series of bars.
    /// </summary>
    /// <param name="series">The series data to render.</param>
    /// <param name="seriesIndex">The index of the series.</param>
    /// <param name="seriesColor">The color to apply to the series bars.</param>
    private void RenderSeries(SeriesData series, int seriesIndex, Material seriesColor)
    {
        for (int valueIndex = 0; valueIndex < series.Values.Count; valueIndex++)
        {
            GameObject bar = seriesBar.RenderElement(barsParent.transform, series.Values[valueIndex], valueIndex, seriesIndex);
            if (bar != null)
            {
                colorManager.ApplyColor(bar, seriesColor);
            }
        }
    }



}
