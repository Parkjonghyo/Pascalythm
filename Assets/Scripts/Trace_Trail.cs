using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace_Trail : MonoBehaviour
{
    [SerializeField] private GameObject[] Trace = new GameObject[7];
    [SerializeField] private Animation _animation;
    [SerializeField] private Transform object_pool;


    public void trail_setOn(Transform spot, float angle)
    {
        _animation.Play();
        transform.parent = spot;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.Euler(0,0,angle);
    }

    public void trail_setOff()
    {
        transform.parent = object_pool;
        gameObject.SetActive(false);
        for(int i = 0;i<7;i++)
        {
            Trace[i].SetActive(true);
        }
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
    }
    
}
