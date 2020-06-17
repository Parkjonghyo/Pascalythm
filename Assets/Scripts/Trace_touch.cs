using System;
using System.Collections;
using System.Timers;
using TMPro;
using UnityEngine;

public class Trace_touch : MonoBehaviour
{
    private bool firstFlag;
    private float BasicSpeed;
    [SerializeField] Animation animation;
    [SerializeField] private float BPM;
    [SerializeField] private float beat;
    [SerializeField] private Transform object_pool;
    [SerializeField] private Trace_Pad Trace_pad;
    [SerializeField] private Result result;
    private bool stop;
    private float timer = 700;
    private bool pause;

    private Music m;

    public void Paused()
    {
        pause = true;
    }

    public void Resume()
    {
        pause = false;
    }
    
    private void Start()
    {
        m = GameObject.FindWithTag("UniqueGameManager").GetComponent<Music>();
        BPM = m.bpm;
    }

    public void touched_Trace()
    {
        timer = 700;
    }
    
    public void set_Beat(float Beat) { beat = Beat; }
    
    public void touch_setOn(Transform spot)
    {
        transform.parent = spot;
        transform.localPosition = Vector3.zero;
        animation.Play("Trace_Available");
        firstFlag = stop = false;
        StartCoroutine(Spin());
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        while (timer > 0)
        {
            while (pause)
            {
                yield return null;                
            }
            timer -= Time.deltaTime * 1000;
            yield return null;
        }

        if (!stop)
        {
            result.Touch(Result.Timing.Miss);
            stop = true;
            transform.parent = object_pool;
            transform.localPosition = Vector3.zero;
            Trace_pad.panjeongMiss();
        }
    }

    IEnumerator Spin()
    {
        while (!stop)
        {
            while (pause)
            {
                yield return null;                
            }
            transform.Rotate(new Vector3(0,0,-5));
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.CompareTag(transform.tag)) return;

        if (firstFlag) return;
        transform.parent = other.transform;
        firstFlag = true;
        StartCoroutine(Move());
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Trace_End") && other.transform.parent.Equals(transform.parent))
        {
            if (Vector2.Distance(transform.localPosition,other.transform.localPosition) <= 0.2f)
            {
                animation.Play("Trace_DeAvail");
                stop = true;
                transform.parent = object_pool;
                transform.localPosition = Vector3.zero;
                other.transform.parent.GetComponent<Trace_Trail>().trail_setOff();
                result.Touch(Result.Timing.Equal);
            }
        }
        
        if (!other.transform.CompareTag(transform.tag)) return;
        if (transform.parent == other.transform) return;
        if (transform.localPosition.x < 5) return;
        transform.parent = other.transform;
        transform.localPosition = new Vector3(0,0,0);
    }

    IEnumerator Move()
    {
        while (true)
        {
            // 5*Time.deltaTime 이 1초에 건너가게 하는거임
            // 0.5초만에 건너가게 하고 싶다면 2 * 5 * Time.deltaTime
            // 1/x = y / x초만에 건너가게 하고 싶다면 y를 곱한다.
            if (stop) break;
            BasicSpeed = 5 * Time.deltaTime / 60;

            BasicSpeed *= BPM * (beat/4);
            while (pause)
            {
                yield return null;                
            }
            transform.localPosition = new Vector2(transform.localPosition.x + BasicSpeed, transform.localPosition.y);
            yield return null;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag(transform.tag))
        {
            if (stop) return;
            other.GetComponent<Trace_Trail>().trail_setOff();
        }
        else if (other.transform.parent.Equals(transform.parent))
        {
            if (other.transform.CompareTag("Trace"))
            {
                StartCoroutine(Erase(other));
            }
        }
    }

    IEnumerator Erase(Collider2D other)
    {
        yield return null;
        other.gameObject.SetActive(false);
    }
}
