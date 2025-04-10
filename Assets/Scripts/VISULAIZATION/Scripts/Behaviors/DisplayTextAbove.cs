using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DisplayTextAbove : MonoBehaviour
{
    public string labelText = "Hello, VR!"; // The text to display
    public float heightAboveObject = 2f; // Height at which the text should be displayed above the GameObject
    
    private GameObject textObject;
    private TextMeshPro textMeshPro;
    
    void Start()
    {
        Debug.Log("size is " + PointData.nodePositions.Count);
        foreach (KeyValuePair<Transform, string> entry in PointData.nodePositions)
        {
            Transform positionss = entry.Key;
            string values = entry.Value;

            // Display the key (position) and value in the console
            Debug.Log("Position: " + positionss + " Value: " + values);
        }

        // Trying to get the value associated with a specific position
        
       
        if (PointData.nodePositions.TryGetValue(transform, out string value))
        {
            Debug.Log("Value found: " + value); // Prints "Value found: Node1"
            labelText = value;
        }
        else
        {
            Debug.Log("Position not found in the dictionary.");
        }

        // Create a new GameObject to hold the TextMeshPro component
        textObject = new GameObject("TextLabel");

        // Add the TextMeshPro component
        textMeshPro = textObject.AddComponent<TextMeshPro>();

        // Set the text and other properties
        textMeshPro.text = labelText;
        textMeshPro.fontSize = 0.5f; // Adjust the size as needed
        textMeshPro.alignment = TextAlignmentOptions.Center;
        textMeshPro.color = Color.white;

        // Set the text object to be a child of this GameObject
        textObject.transform.SetParent(transform);

        // Adjust position so that the text appears above the GameObject
        textObject.transform.localPosition = new Vector3(0, heightAboveObject, 0); // Adjust the height as needed

        // Make sure the text object is facing the camera
        textObject.transform.LookAt(Camera.main.transform);
        textObject.transform.Rotate(0, 180, 0); // Rotate to face the camera properly
        if (textObject != null)
        {
            textObject.SetActive(false);
        }
    }

    public void OnHover()
    {
        // Show the text when the object is hovered over
        if (textObject != null)
        {
            textObject.transform.LookAt(Camera.main.transform);
            textObject.transform.Rotate(0, 180, 0); // Rotate to face the camera properly
            textObject.SetActive(true);
        }
    }

    public void OnHoverEnd()
    {
        // Hide the text when the hover ends
        if (textObject != null)
        {
            textObject.SetActive(false);
        }
    }
}
