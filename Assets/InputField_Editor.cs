using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InputField_Editor : MonoBehaviour
{
    [SerializeField] private TimeHandler th;
    [SerializeField] private TMP_InputField _inputField;
    private float outputTime;

    public void changeTime()
    {
        if (float.TryParse(_inputField.text, out outputTime))
        {
            th.TimeChange(outputTime);   
        }
    }
}
