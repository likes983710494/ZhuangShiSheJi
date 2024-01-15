using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text buttonText;
    public Color defaultColor;
    public Color hoverColor;

    private void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        defaultColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = defaultColor;
    }
}