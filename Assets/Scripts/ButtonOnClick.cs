using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonOnClick : MonoBehaviour
{
    private bool isPaused;
    private LoadingSceneManager lsm;
    
    public TimeHandler _th;
    public touchHandler _touchH;
    public btn_all _btnAll;
    public GameObject panel;
    public GameObject btn_Resume, btn_Restart, btn_Menu;
    private bool isItem, touched = false;

    [SerializeField] private Trace_touch[] TT = new Trace_touch[2];

    private void Awake()
    {
        lsm = GameObject.FindWithTag("LoadingSceneManager").GetComponent<LoadingSceneManager>();
    }

    private void Start()
    {
        isPaused = false;
    }

    public void Restart()
    {
        if (touched) return;
        lsm.LoadSceneWithLoadingScene(SceneManager.GetActiveScene().buildIndex);
        touched = true;
    }

    public void toMenu()
    {
        if (touched) return;
        lsm.LoadSceneWithLoadingScene(1);
        touched = true;
    }

    public void Stop()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            TT[0].Paused();
            TT[1].Paused();
            _th.MusicPaused();
            _btnAll.buttonAnimationSpeed(0);
            OnPausedScreen();
            _touchH.ignore = true;   
        }
        else
        {
            TT[0].Resume();
            TT[1].Resume();
            _th.MusicResume();
            _btnAll.buttonAnimationSpeed(1);
            OffPausedScreen();
            _touchH.ignore = false;
        }
    }

    public void btn_paused()
    {
        TT[0].Paused();
        TT[1].Paused();
        _th.MusicPaused();
        _btnAll.buttonAnimationSpeed(0);
    }

    public void btn_play()
    {
        TT[0].Resume();
        TT[1].Resume();
        _th.MusicResume();
        _btnAll.buttonAnimationSpeed(1);
    }

    private void OnPausedScreen()
    {
        panel.SetActive(true);
        panel.GetComponent<Animation>().Play();
        isItem = true;
        Invoke("OnItems",1f); 
    }

    private void OffPausedScreen()
    {
        panel.SetActive(false);
        isItem = false;
        OnItems(); 
    }

    public void OnItems()
    {
        if (!isItem)
        {
            btn_Menu.SetActive(false);
            btn_Restart.SetActive(false);
            btn_Resume.SetActive(false);
        }
        else
        {
            btn_Menu.SetActive(true);
            btn_Restart.SetActive(true);
            btn_Resume.SetActive(true);   
        }
    }
}
