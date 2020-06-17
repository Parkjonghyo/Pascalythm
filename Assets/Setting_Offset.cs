using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Setting_Offset : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI offset_tmp;
    private Music _m;

    private void Start()
    {
        _m = GameObject.FindWithTag("UniqueGameManager").GetComponent<Music>();
        offset_tmp.text = _m.offset.ToString();
    }

    public void click_offset_Button(bool is_Up)
    {
        int offset = int.Parse(offset_tmp.text);
        if (is_Up)
        {
            offset++;
            offset_tmp.text = offset.ToString();
        }
        else
        {
            offset--;
            offset_tmp.text = offset.ToString();
        }

        _m.offset = offset;
    }
}
