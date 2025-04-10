using UnityEngine;
using TMPro;


public class HeatMapUsingBarsUIManager : MonoBehaviour
{
    private GameObject _textPrefab;
    [SerializeField] private Vector3 offset = new(0, 1f, 0);
    [SerializeField] private Vector3 scale = new(0.01f, 0.001f, 0.01f);

    public void SetTextPrefab(GameObject prefab)
    {
        _textPrefab = prefab;
    }

    public void AttachTooltip(GameObject cell, HeatMapUsingBarsCellData barData, Vector2 gridPosition)
    {
        GameObject textCanvas = Instantiate(_textPrefab, cell.transform.position + offset, Quaternion.identity);
        textCanvas.transform.SetParent(cell.transform);
        textCanvas.transform.localScale = scale;
        textCanvas.transform.localPosition = new Vector3(0, 0.7f, 0);

        TextMeshProUGUI heightText = textCanvas.transform.Find("tooltip/Height").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI positionText = textCanvas.transform.Find("tooltip/Position").GetComponent<TextMeshProUGUI>();

        heightText.text = $"Intensity: {barData.Intensity}";
        positionText.text = $"Position: ({gridPosition.x}, {gridPosition.y})";

        var interactable = cell.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.hoverEntered.AddListener((args) => textCanvas.SetActive(true));
            interactable.hoverExited.AddListener((args) => textCanvas.SetActive(false));
        }

        textCanvas.SetActive(false);
    }

}

