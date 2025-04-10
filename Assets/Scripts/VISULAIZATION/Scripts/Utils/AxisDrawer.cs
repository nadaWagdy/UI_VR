using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AxisDrawer
{
    private const float LineWidth = 0.01f;
    private const float LabelScale = 0.1f;
    private const int LabelFontSize = 11;

    private readonly List<GameObject> _labels = new List<GameObject>();
    private readonly GameObject _targetObject;
    
    public AxisDrawer(GameObject targetObject)
    {
        _targetObject = targetObject;
    }

    public List<GameObject> DrawAxes(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        Renderer renderer = _targetObject.GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("The target object must have a Renderer component.");
            return null;
        }

        Vector3 size = renderer.bounds.size;
        Vector3 center = _targetObject.transform.position;
        Vector3 lowestCorner = center - new Vector3(size.x / 2, size.y / 2, size.z / 2);

        // Draw each axis
        DrawAxis(lowestCorner, size.x, Vector3.right, Color.red, minX, maxX);
        DrawAxis(lowestCorner, size.y, Vector3.up, Color.green, minY, maxY);
        DrawAxis(lowestCorner, size.z, Vector3.forward, Color.blue, minZ, maxZ);

        return _labels;
    }

    private void DrawAxis(Vector3 start, float length, Vector3 direction, Color color, float min, float max)
    {
        Vector3 end = start + direction * length;
        DrawLine(start, end, color);

        // Place labels at start, mid-point, and end
        PlaceLabel((start + end) / 2, ((min + max) / 2.0f).ToString("N0"), Color.white);
        PlaceLabel(end, max.ToString("N0"), Color.white);
    }

    private void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        GameObject lineObj = new GameObject("Line");
        lineObj.transform.SetParent(_targetObject.transform);
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.material.color = color;
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = LineWidth;
        lr.endWidth = LineWidth;
        lr.SetPositions(new []{ start, end });
    }

    private void PlaceLabel(Vector3 position, string labelText, Color color)
    {
        GameObject labelObj = new GameObject("Label");
        TextMeshPro textMesh = labelObj.AddComponent<TextMeshPro>();
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.text = labelText;
        textMesh.color = color;
        textMesh.fontSize = LabelFontSize;
        labelObj.transform.localScale = Vector3.one * LabelScale;
        labelObj.transform.SetParent(_targetObject.transform);  // Set the label's parent to the bounding cube
        labelObj.transform.position = position;
    
        _labels.Add(labelObj);
    }
}
