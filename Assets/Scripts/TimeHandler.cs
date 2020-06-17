using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
    [SerializeField] private Audio_Player _ap;
    [SerializeField] private Result result;
    [SerializeField] private Slider progressbar;
    Slider_Ctrl Sctrl;

    public float StartedTime { get; set; } = 0;
    public bool IsMusicStarted = false;

    public void MusicStart()
    {
        IsMusicStarted = true;
        _ap.Audio_Play();
    }

    public void MusicResume()
    {
        IsMusicStarted = true;
        _ap.audioSource.Play();
    }
    
    public void MusicPaused()
    {
        IsMusicStarted = false;
        _ap.audioSource.Pause();
    }

    private void Start()
    {
        Sctrl = progressbar.GetComponent<Slider_Ctrl>();
        MusicStart();
        progressbar.maxValue = _ap.audioSource.clip.length * 1000;
    }

    public void TimeChange(float time)
    {
        _ap.audioSource.time = time * 0.001f;
        progressbar.value = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMusicStarted) 
        { 
            StartedTime += Time.deltaTime * 1000;
        }

        
        
        /*progressbar.value = Mathf.Lerp(progressbar.value, _ap.audioSource.time * 1000, Time.deltaTime);*/
        if(Sctrl.isPressedChanged())
        {
            _ap.audioSource.time = progressbar.value /1000;
        }else if (!Sctrl.isPressed)
        {
            progressbar.value = _ap.audioSource.time * 1000;
        }

        if (_ap.isEdit()) return;
        if (_ap.audioSource.isPlaying) return;

        if (StartedTime >= _ap.audioSource.clip.length * 1000)
        {
            StartedTime = 0;
            result.toResult();
        }
    }
}
