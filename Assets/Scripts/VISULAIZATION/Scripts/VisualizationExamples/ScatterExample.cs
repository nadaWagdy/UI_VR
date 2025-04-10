using System.Collections;
using UnityEngine;

/// <summary>
/// Demonstrates the creation and rendering of a scatter plot visualization using directly created JSON data.
/// </summary>
public class ScatterExample : MonoBehaviour
{
    private IVisualization _scatterPlot;

    /// <summary>
    /// Initializes the scatter plot visualization and renders it with example JSON data.
    /// </summary>
    void Start()
    {
        VisualizationFactory factory = new();
        string jsonData = @"
        {
            ""x_values"": [20, 40, 60, 80, 100],
            ""y_values"": [40000, 60000, 80000, 100000, 120000],
            ""z_values"": [50, 30, 70, 90, 110]
        }";

        ScatterPlotStandardizer standardizer = new();
        IProcessedData data = standardizer.StandardizeData(jsonData);
        _scatterPlot = factory.CreateVisualization(VisualizationType.ScatterPlot);
        _scatterPlot.Render(data);


        // StartCoroutine(WaitRealtimeAndDoSomething());
    }

    /// <summary>
    /// Updates the scatter plot visualization every frame.
    /// Ensures axis labels face the camera.
    /// </summary>
    private void Update()
    {
        ((ScatterPlot)_scatterPlot).Update();
    }

    /// <summary>
    /// Waits for a specified time in real-time and then updates the scatter plot with new JSON data.
    /// </summary>
    IEnumerator WaitRealtimeAndDoSomething()
    {
        Debug.Log("Waiting started");

        yield return new WaitForSecondsRealtime(5f);

        string updatedJsonData = @"
        {
            ""x_values"": [30, 70, 90],
            ""y_values"": [50000, 90000, 110000],
            ""z_values"": [20, 60, 80]
        }";

        ScatterPlotStandardizer standardizer = new();
        IProcessedData data = standardizer.StandardizeData(updatedJsonData);

        _scatterPlot.Render(data);

        Debug.Log("Waiting finished and scatter plot updated");
    }
}
