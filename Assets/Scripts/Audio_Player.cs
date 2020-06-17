using UnityEngine;

public class Audio_Player : MonoBehaviour
{
    private Music _m;
    private AudioClip _data;
    [SerializeField] public AudioSource audioSource;
    public int offset;
    
    private void Awake()
    {
        _m = GameObject.FindWithTag("UniqueGameManager").GetComponent<Music>();
        _data = Resources.Load<AudioClip>(_m.songName + "/base");
        offset = 500 + _m.offset;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = _data;
    }

    public void Audio_Play()
    {
        Invoke("DelayPlay", (float)0.001*offset); 
    }
    

    private void DelayPlay()
    {
        audioSource.Play();
    }

    public bool isEdit()
    {
        return _m.isEdit;
    }
}
