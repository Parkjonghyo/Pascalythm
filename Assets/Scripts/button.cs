using System;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    [SerializeField] private RhythmParser _rtp;
    private Animator _anim;
    [SerializeField] private TimeHandler _th;
    [SerializeField] private Audio_Player ap;
    [SerializeField] private Result result;
    private Queue<int> noteQ;

    private float timEqual;
    private float timNormal;
    private float timMiss;
    private float currentPeek;

    /*float _startedTime; // 현재 시간(scene 시작부터 ms초) Time.timeSinceLevelLoad 를 사용하자.
    bool _isMusicStarted = false;*/

    // Start is called before the first frame update
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        timEqual = 75;
        timNormal = 200;
        timMiss = 300;
        currentPeek = 0;
    }

    private void Start()
    {
        noteQ = _rtp.GiveNoteNew(int.Parse(gameObject.name));
    }
    
    public void Panjeong()
    {
        if (currentPeek.Equals(0)) return;
        var nonAbs = 500 - _anim.GetCurrentAnimatorStateInfo(0).normalizedTime * 1000;

        var tim = Math.Abs(nonAbs);
        
        /*var audTime = ap.audioSource.time * 1000;
        float nonAbs = currentPeek - audTime;
        float tim = Math.Abs(nonAbs);
        Debug.Log(_anim.GetCurrentAnimatorStateInfo(0).normalizedTime);*/
        if (tim >= timMiss) return;
        currentPeek = 0;
        if (tim <= timEqual)
        {
            _anim.SetTrigger("Equal");
            result.Touch(Result.Timing.Equal);
            return;
        }
        if (tim <= timNormal)
        {
            _anim.SetTrigger("Normal");
            if (nonAbs > 0) { result.Touch(Result.Timing.Normal_Early); return; }
            result.Touch(Result.Timing.Normal_Late); return;
        }
        result.Touch(Result.Timing.Miss);
    }

    public void AnimationSpeed(float speed)
    {
        /*_anim.SetFloat("Speed", speed);*/
        _anim.speed = speed;
    }
    
    public void MobileTouch(int mode)
    {
        switch (mode)
        {
            case 0: Panjeong(); break;
            case 1: break;
            case 2: break;
        }
    }

    /*public void MusicStart() { _isMusicStarted = true; }*/

    private void Update()
    {
        if (!_th.IsMusicStarted) return;
        while (CreateNote())
        {
            // TODO 살려줘요
        };
        if (currentPeek.Equals(0)) return;
        if (currentPeek - ap.audioSource.time * 1000 < -300) {currentPeek = 0; result.Touch(Result.Timing.Miss);} 
    }

    private bool CreateNote()
    {
        if (noteQ.Count == 0) return false;
        if (_th.StartedTime < noteQ.Peek()) return false;
        currentPeek = noteQ.Dequeue();
        _anim.SetTrigger("NoteCreation");
        return true;
    }

}
