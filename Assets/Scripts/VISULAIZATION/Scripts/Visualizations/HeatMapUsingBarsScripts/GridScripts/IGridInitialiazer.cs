using UnityEngine;
public interface IGridInitializer
{
    void InitializeGridDimensions(int sampleCount, out int gridWidth, out int gridHeight, out GameObject[,] grid);
}
