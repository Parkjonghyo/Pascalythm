using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class button_Editor : MonoBehaviour, IPointerDownHandler
{
    private void Start()
    {
        this.enabled = true;
    }

    [SerializeField] private Slider _slider;
    private List<int> a = new List<int>();

    public void sibal_check()
    {
        // TODO - 몰라요
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        a.Add( Mathf.FloorToInt(_slider.value));
        a.Sort();
    }
}
