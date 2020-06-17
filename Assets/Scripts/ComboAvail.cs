using TMPro;
using UnityEngine;

public class ComboAvail : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpgui;
    [SerializeField] private Animation anim;

    public void TextChange(int Combo)
    {
        tmpgui.text = Combo.ToString();
        anim.Play();
    }
}
