using UnityEngine;

/// <summary>
/// Abstract class providing utility methods for rendering axes, labels, and lines within a 3D bounding cube.
/// </summary>
public abstract class AxesHelper
{
    /// <summary>
    /// The bounding cube around which the axes, labels, and gridlines are rendered.
    /// </summary>
    public BoundingShape boundingCube;

    /// <summary>
    /// Initializes a new instance of the <see cref="AxesHelper"/> class.
    /// </summary>
    /// <param name="boundingCube">The bounding cube to associate with this helper.</param>
    public AxesHelper(BoundingShape boundingCube)
    {
        this.boundingCube = boundingCube;
    }

    /// <summary>
    /// Adds axis numbers to the bounding cube.
    /// This method must be implemented by derived classes.
    /// </summary>
    /// <param name="data">The processed data used to generate axis numbers.</param>
    public abstract void AddAxisNumbers(IProcessedData data);

    /// <summary>
    /// Creates a label at a specified position within the bounding cube.
    /// </summary>
    /// <param name="parent">The parent transform to contain the label.</param>
    /// <param name="position">The 3D position of the label relative to the parent.</param>
    /// <param name="text">The text to display on the label.</param>
    /// <param name="alignment">The alignment of the text within the label.</param>
    public void CreateAxisLabel(Transform parent, Vector3 position, string text, TextAnchor alignment)
    {
        GameObject textObject = new("AxisLabel");
        textObject.transform.SetParent(parent, worldPositionStays: true);
        textObject.transform.position = position;

        TextMesh textMesh = textObject.AddComponent<TextMesh>();
        textMesh.text = text;
        textMesh.anchor = alignment;
        textMesh.fontSize = 8;
        textMesh.characterSize = 0.1f;
        textMesh.alignment = TextAlignment.Center;
        textMesh.color = Color.gray;

        textObject.transform.localScale = Vector3.one * 0.5f;
    }

    /// <summary>
    /// Adds evenly spaced axis labels along an axis in the bounding cube.
    /// </summary>
    /// <param name="steps">The number of labels to generate along the axis.</param>
    /// <param name="parent">The parent transform to contain the labels.</param>
    /// <param name="cubeCenter">The center position of the bounding cube.</param>
    /// <param name="halfScale">Half the scale of the bounding cube.</param>
    /// <param name="isXAxis">Specifies whether the labels are for the X-axis. If false, labels are for the Z-axis.</param>
    public void AddAxisLabels(int steps, Transform parent, Vector3 cubeCenter, Vector3 halfScale, bool isXAxis)
    {
        double spacing = isXAxis
            ? boundingCube.Transform.localScale.x / (steps - 1)
            : boundingCube.Transform.localScale.z / (steps - 1);

        for (int i = 0; i < steps; i++)
        {
            double pos = i * spacing;

            CreateAxisLabel(parent,
                cubeCenter + (isXAxis
                    ? new Vector3((float)(-halfScale.x + pos), -halfScale.y, (float)(-halfScale.z * 1.1f))
                    : new Vector3((float)(-halfScale.x * 1.1f), -halfScale.y, (float)(-halfScale.z + pos))),
                $"S{i + 1}",
                TextAnchor.MiddleCenter);
        }
    }

    /// <summary>
    /// Creates a line along a specified axis in the bounding cube.
    /// </summary>
    /// <param name="parent">The parent transform to contain the line.</param>
    /// <param name="start">The starting position of the line.</param>
    /// <param name="end">The ending position of the line.</param>
    /// <param name="color">The color of the line.</param>
    public void CreateAxisLine(Transform parent, Vector3 start, Vector3 end, Color color)
    {
        GameObject lineObject = new("Axis Line");
        lineObject.transform.SetParent(parent, worldPositionStays: true);

        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(new Vector3[] { start, end });
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.025f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.useWorldSpace = false;
    }

    /// <summary>
    /// Adds gridlines to represent axes within the bounding cube.
    /// </summary>
    public void AddAxisLines()
    {
        GameObject axisLinesParent = new("Axis Lines");
        axisLinesParent.transform.SetParent(boundingCube.Transform, worldPositionStays: true);

        Vector3 cubeCenter = boundingCube.Transform.position;
        Vector3 halfScale = boundingCube.Transform.localScale / 2.0f;

        // Y-axis line
        CreateAxisLine(axisLinesParent.transform,
            cubeCenter + new Vector3(-halfScale.x, -halfScale.y, halfScale.z),
            cubeCenter + new Vector3(-halfScale.x, halfScale.y, halfScale.z), Color.gray);

        // Z-axis line
        CreateAxisLine(axisLinesParent.transform,
            cubeCenter + new Vector3(-halfScale.x, -halfScale.y, -halfScale.z),
            cubeCenter + new Vector3(-halfScale.x, -halfScale.y, halfScale.z), Color.gray);

        // X-axis line
        CreateAxisLine(axisLinesParent.transform,
            cubeCenter + new Vector3(-halfScale.x, -halfScale.y, -halfScale.z),
            cubeCenter + new Vector3(halfScale.x, -halfScale.y, -halfScale.z), Color.gray);
    }
}

