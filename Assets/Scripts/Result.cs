using UnityEngine;
using static UnityEngine.PlayerPrefs;

public class Result : MonoBehaviour
{
    [SerializeField] private ScoreAvailable sa;
    [SerializeField] private PanAvail pa;
    [SerializeField] private ComboAvail ca;
    [SerializeField] private RhythmParser rp;
    private LoadingSceneManager lsm;

    public int Equal, Normal, Miss;
    public int Score;
    public int MaxCombo, Combo;
    private int NoteCount;

    public enum Timing
    {
        Miss,
        Normal_Early,
        Normal_Late,
        Equal
    }

    public int checkFullCombo()
    {
        if (MaxCombo == NoteCount) return 1;
        return 0;
    }
    
    private void Awake()
    {
        lsm = GameObject.FindWithTag("LoadingSceneManager").GetComponent<LoadingSceneManager>();
        Equal = Normal = Miss = Score = MaxCombo = Combo = 0;
    }

    private void Start()
    {
        NoteCount = rp.noteCount;
    }

    public void Touch(Timing timing)
    {
        if (timing == Timing.Equal)
        {
            Equal++;
            CheckMaxCombo();
            pa.CheckPan(PanAvail.Timing.Equal);
        }
        else if (timing == Timing.Normal_Early)
        {
            Normal++;
            CheckMaxCombo();
            pa.CheckPan(PanAvail.Timing.Normal_Early);
        }else if (timing == Timing.Normal_Late)
        {
            Normal++;
            CheckMaxCombo();
            pa.CheckPan(PanAvail.Timing.Normal_Late);
        }
        else
        {
            Miss++;
            Combo = 0;
            ca.TextChange(Combo);
            pa.CheckPan(PanAvail.Timing.Miss);
            return;
        }
        Score = (1000000 * Equal + 500000 * Normal) / NoteCount;
        sa.TextChange(Score);
        ca.TextChange(Combo);
    }

    private void CheckMaxCombo()
    {
        if (++Combo > MaxCombo) MaxCombo++;
    }

    public void toResult()
    {
        SetInt("rScore",Score);
        SetInt("rEqual", Equal);
        SetInt("rNormal",Normal);
        SetInt("rMiss",Miss);
        SetInt("rMaxCombo",MaxCombo);
        SetInt("rIsFullCombo", checkFullCombo());
        lsm.LoadSceneWithLoadingScene(3);
    }
    
}
