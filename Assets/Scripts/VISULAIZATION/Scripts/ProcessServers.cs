using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ProcessServers : MonoBehaviour
{
    public string visualizationTypeStr;
    private VisualizationType _visualizationType;
    private IVisualization _visualization;

    void Start()
    {
        _visualizationType = VisualizationTypeExtensions.FromString(visualizationTypeStr); 
        _visualization = new VisualizationFactory().CreateVisualization(_visualizationType);
        StartCoroutine(FetchDataFromServer(_visualization));
    }

    IEnumerator FetchDataFromServer(IVisualization visualization)
    {
        string serverUrl = GetServerUrl();
        if (string.IsNullOrEmpty(serverUrl))
        {
            UnityEngine.Debug.LogError("Invalid server type.");
            yield break;
        }

        UnityWebRequest request = UnityWebRequest.Get(serverUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            UnityEngine.Debug.Log(request.error);
        }
        else
        {
            string jsonData = request.downloadHandler.text;

            // create the appropriate standardizer
            DataStandardizerFactory factory = new DataStandardizerFactory();
            IDataStandardizer standardizer = factory.CreateStandardizer(_visualizationType);

            // standardize data and render the graph (since render is used for all graphs just like in the package)
            IProcessedData standardizedData = standardizer.StandardizeData(jsonData);
            visualization.Render(standardizedData);
        }
    }

    private string GetServerUrl()
    {
        switch (_visualizationType)
        {
            case VisualizationType.ScatterPlot:
                return "http://127.0.0.1:8000/scatterplot/data";  // scatter plot server url
            case VisualizationType.HeatMapUsingBars:
                return "http://127.0.0.1:8001/graph/heatmap-using-bars";  // bar chart server url
            case VisualizationType.PieChart:
                return "http://127.0.0.1:8000/piechart/data";
            default:
                return string.Empty;
        }
    }
}