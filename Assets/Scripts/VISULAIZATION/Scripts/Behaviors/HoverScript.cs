using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HoverScript : MonoBehaviour
{
    public TextMeshProUGUI textObject; // Reference to the UI text object

    public void OnHover()
    {
       
        Debug.Log("Heyy" + this.transform.position.x);
        
        //textObject.SetText("oo");
        //textObject.text = "Heyy"; // Update the text when hovering
        //textObject.fontSize = 32;
       
    }
}
