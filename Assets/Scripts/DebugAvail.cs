using System;
using TMPro;
using UnityEngine;

public class DebugAvail : MonoBehaviour
{
    private TextMeshProUGUI tmpgui;


    private void Awake()
    {
        tmpgui = GetComponent<TextMeshProUGUI>();
    }

    public void TextChange(int Equal, int Normal, int Miss, int noteC)
    {
        tmpgui.text = String.Format("{0}:{1}:{2}/{3}", Equal, Normal, Miss, noteC);
    }
}
