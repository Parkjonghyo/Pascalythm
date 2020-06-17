using UnityEngine;
using UnityEngine.UI;

public class select_btn : MonoBehaviour
{
    [SerializeField] private Button m_Bt;
    [SerializeField] private GetSong getSong;

    public void btn_selected()
    {
        getSong.selectDatabase(GetComponentInChildren<Text>().text);
    }
}
