using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Editor_Manager : MonoBehaviour
{
    private List<int[]> chartList = new List<int[]>();

    public void addList(int timing, int btn_num)
    {
        chartList.Add(new []{timing, btn_num});
        chartList = (from num in chartList
            orderby num[0] descending
            select num).ToList();
    }
    
    public void debugList()
    {
        for (int i = 0; i < chartList.Count; i++)
        {
            Debug.Log(chartList[i][0] + " " + chartList[i][1]);   
        }
    }
}
