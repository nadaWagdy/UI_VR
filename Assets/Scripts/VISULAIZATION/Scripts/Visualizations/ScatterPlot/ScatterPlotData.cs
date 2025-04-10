using System.Collections.Generic;
using UnityEngine;

class ScatterPlotData : IProcessedData
{
    public List<Vector3> OriginalPositions { get; private set; }
    public List<Vector3> NormalizedPositions { get; private set; }
    public float MinX { get; private set; }
    public float MaxX { get; private set; }
    public float MinY { get; private set; }
    public float MaxY { get; private set; }
    public float MinZ { get; private set; }
    public float MaxZ { get; private set; }

    public ScatterPlotData(List<Vector3> originalPositions, List<Vector3> normalizedPositions,
        float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        OriginalPositions = originalPositions;
        NormalizedPositions = normalizedPositions;
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;
        MinZ = minZ;
        MaxZ = maxZ;
    }
}