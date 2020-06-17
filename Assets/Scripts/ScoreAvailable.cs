using System;
using TMPro;
using UnityEngine;

public class ScoreAvailable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpgui;
    [SerializeField] private Animation anim;

    public void TextChange(int Score)
    {
        tmpgui.text = String.Format("{0,7:D7}", Score);
        anim.Play();
    }
}
