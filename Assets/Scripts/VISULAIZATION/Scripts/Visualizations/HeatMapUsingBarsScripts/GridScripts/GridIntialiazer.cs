/* using UnityEngine;

public class GridInitializer : IGridInitializer
{
    public void InitializeGridDimensions(int sampleCount, out int gridWidth, out int gridHeight, out GameObject[,] grid)
    {
        gridWidth = gridHeight = Mathf.CeilToInt(Mathf.Sqrt(sampleCount));
        grid = new GameObject[gridWidth, gridHeight];
    }

    public void Create3DGrid(Transform parent, int gridWidth, int gridHeight, float spacing)
    {
        GameObject gridParent = new GameObject("Grid");
        gridParent.transform.SetParent(parent);

        // Create planes (or modify as per requirement)
        CreateGridPlane(Vector3.right, Vector3.up, gridWidth, spacing, "X-Y Plane", gridParent);
        CreateGridPlane(Vector3.up, Vector3.forward, gridWidth, spacing, "Y-Z Plane", gridParent);
        CreateGridPlane(Vector3.right, Vector3.forward, gridHeight, spacing, "X-Z Plane", gridParent);
    }

    private void CreateGridPlane(Vector3 axis1, Vector3 axis2, int size, float space, string planeName, GameObject parent)
    {
        for (int i = 0; i <= size; i++)
        {
            Vector3 start1 = axis1 * i * space;
            Vector3 end1 = start1 + axis2 * size * space;
            DrawLine(start1, end1, planeName, i, 1, parent);

            Vector3 start2 = axis2 * i * space;
            Vector3 end2 = start2 + axis1 * size * space;
            DrawLine(start2, end2, planeName, i, 2, parent);
        }
    }

    private void DrawLine(Vector3 start, Vector3 end, string parentName, int index, int direction, int lineWidth, GameObject parent)
    {
        string lineName = $"{parentName} Line {direction}-{index}";
        GameObject line = new GameObject(lineName);
        LineRenderer lr = line.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.startWidth = lr.endWidth = lineWidth; 
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lr.endColor = Color.gray;
        Vector3 offset = new Vector3(0.1f, 0.1f, 0f); 
        line.transform.SetParent(parent.transform);
    }

} */
