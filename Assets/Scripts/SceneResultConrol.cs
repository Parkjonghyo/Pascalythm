using TMPro;
using UnityEngine;
using static UnityEngine.PlayerPrefs;

public class SceneResultConrol : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rSongName, rScore, rEqual, rNormal, rMiss, rRank, rMaxCombo; 
    [SerializeField] GameObject rFullCombo;

    private Music _m;
    private LoadingSceneManager lsm;
    private bool touched = false;
    private void Awake()
    {
        lsm = GameObject.FindWithTag("LoadingSceneManager").GetComponent<LoadingSceneManager>();
        _m = GameObject.FindWithTag("UniqueGameManager").GetComponent<Music>();
    }

    private void Start()
    {
        rSongName.text = _m.songName;
        rScore.text = GetInt("rScore").ToString();
        rEqual.text = GetInt("rEqual").ToString();
        rNormal.text = GetInt("rNormal").ToString();
        rMiss.text = GetInt("rMiss").ToString();
        rRank.text = checkRank();
        rMaxCombo.text = GetInt("rMaxCombo").ToString();
        rFullCombo.SetActive(GetInt("rIsFullCombo").Equals(1));
    }

    string checkRank()
    {
        int score = GetInt("rScore");
        if (score == 1000000) return "EQT";
        if (score >= 980000) return "IST";
        if (score >= 950000) return "S";
        if (score >= 920000) return "AA";
        if (score >= 900000) return "A";
        if (score >= 850000) return "B";
        if (score >= 800000) return "C";
        if (score >= 750000) return "D";
        if (score >= 700000) return "E";
        return "F";
    }

    public void btnRestart()
    {
        if (touched) return;
        lsm.LoadSceneWithLoadingScene(2);
        touched = true;
    }

    public void btnMenu()
    {
        if (touched) return;
        lsm.LoadSceneWithLoadingScene(1);
        touched = true;
    }
    
    
}
