using System.Collections.Generic;
using UnityEngine;

public class BarChartStandardizer : IDataStandardizer
{
    public IProcessedData StandardizeData(string jsonData)
{
    List<HeatMapUsingBarsCellData> barDataList = null;
 
    return new HeatMapUsingBarsData(barDataList);
}

    private List<HeatMapUsingBarsCellData> ConvertToBarData(List<Vector3> nodePositions)
    {
        List<HeatMapUsingBarsCellData> barDataList = new();
        foreach (var position in nodePositions)
        {
            barDataList.Add(new HeatMapUsingBarsCellData(position.x, position.y, position.z));
        }

        return barDataList;
    }
} 