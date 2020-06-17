using System;
using UnityEngine;
using UnityEngine.UI;

public class TestDistance : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Editor_Manager edm;
    private int i = 1;

    public void sibalDebugging()
    {
        edm.addList( Mathf.FloorToInt(_slider.value), ++i);
    }

    public void DebugNawara()
    {
        edm.debugList();
    }
    
}
