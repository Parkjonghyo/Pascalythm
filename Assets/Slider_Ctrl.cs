using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slider_Ctrl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TextMeshProUGUI Timing;
    [SerializeField] private Slider slider;
    [SerializeField] private bool isEditScreen;
    private bool previousIspressed;
    public bool isPressed;

    public bool isPressedChanged()
    {
        if (!isEditScreen) return false;
        if (previousIspressed.Equals(isPressed))
        {
            return false;
        }
        previousIspressed = false;
        return true;
    }

    private void Update()
    {
        if (!isEditScreen) return;
        Timing.text = Mathf.FloorToInt(slider.value).ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        previousIspressed = true;
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
