using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Trace_Pad : MonoBehaviour
{
    [SerializeField] private RhythmParser _rp;

    [SerializeField] private Transform[] spot = new Transform[12];

    [SerializeField] private Transform objectPool_object;
    
    [SerializeField] private TimeHandler _th;
    [SerializeField] private Trace_touch Touch_Red, Touch_Blue;
    
    [SerializeField] private bool isEditor;
    
    private Queue<string[]> Trace_Note;
    private string[] current_TN;
    
    

    private void Start()
    {
        if (!isEditor)
        {
            Trace_Note = _rp.GiveNoteNew_T();

            StartCoroutine(CreateTrace());
        }else
        {
            
        }
    }

    public void panjeongMiss()
    {
        for (int i = 0; i < 12; i++)
        {
            var ttAll = spot[i].GetComponentsInChildren<Trace_Trail>();
            foreach (var omona in ttAll)
            {
                omona.trail_setOff();
            }
        }
    }

    IEnumerator CreateTrace()
    {
        while (Trace_Note.Count != 0)
        {
            if (_th.StartedTime >= int.Parse(Trace_Note.Peek()[0]) - 500)
            {
                current_TN = Trace_Note.Dequeue();
                int spot_a, spot_b;
                for (int i = 3; i < current_TN.Length - 1; i++)
                {
                    spot_a = Convert.ToInt16(Char.Parse(current_TN[i])) - 65;
                    spot_b = Convert.ToInt16(Char.Parse(current_TN[i + 1])) - 65;
                    setTrace(current_TN[2], spot_a, GetAngle(spot[spot_a].position, spot[spot_b].position), i);
                }
            }
            yield return null;
        }
    }

    private bool setTrace(string color, int spot_a, float angle, int first)
    {
        if (color.Equals("R"))
        {
            if (first.Equals(3))
            {
                Touch_Red.touch_setOn(spot[spot_a]);
            }
            
            var object_R = objectPool_object.GetChild(3).GetChild(0);
            
            if (first.Equals(current_TN.Length - 2))
            {
                /*var end_object = objectPool_object.GetChild(3).childCount;  
                object_R = objectPool_object.GetChild(3).GetChild(end_object - 1);*/
                object_R = objectPool_object.GetChild(3).Find("Traces_End 2");
            }
            var TTR = object_R.GetComponent<Trace_Trail>();
            TTR.trail_setOn(spot[spot_a], angle);
        }else if (color.Equals("B"))
        {
            if (first.Equals(3))
            {
                Touch_Blue.touch_setOn(spot[spot_a]);
            }
            var object_B = objectPool_object.GetChild(2).GetChild(0);
            
            if (first.Equals(current_TN.Length - 2))
            {
                /*var end_object = objectPool_object.GetChild(2).childCount;  
                object_B = objectPool_object.GetChild(2).GetChild(end_object - 1);*/
                object_B = objectPool_object.GetChild(2).Find("Traces_End");
            }
            var TTB = object_B.GetComponent<Trace_Trail>();
            TTB.trail_setOn(spot[spot_a], angle);
        }
        return false;
    }
    
    public static float GetAngle (Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;
        return Mathf.Round(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
    }

    
}
