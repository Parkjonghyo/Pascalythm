using System.IO;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private LoadingSceneManager lsm;
    private Music m;
    private bool touched;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        CopyDB();
    }
    
    void CopyDB()
    {
        string filepath = string.Empty;
        if (Application.platform == RuntimePlatform.Android)
        {
            filepath = Application.persistentDataPath + "/Pascalythm_local.db";
            if (!File.Exists(filepath))
            {
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/Pascalythm_local.db");
                loadDB.bytesDownloaded.ToString();
                while (!loadDB.isDone) { }
                File.WriteAllBytes(filepath, loadDB.bytes);
            }
        }
        else
        {
            filepath = Application.dataPath + "/Pascalythm_local.db";
            if (!File.Exists(filepath)) {
                File.Copy(Application.streamingAssetsPath + "/Pascalythm_local.db", filepath);
            }
        }
    }

    private void Start()
    {
        lsm = GameObject.FindWithTag("LoadingSceneManager").GetComponent<LoadingSceneManager>();
        m = GameObject.FindWithTag("UniqueGameManager").GetComponent<Music>();
        touched = false;
    }

    public void ToPlayScreen()
    {
        if (touched) return;
        m.isEdit = false;
        lsm.LoadSceneWithLoadingScene(1);
        touched = true;
    }
    
    public void ToEditorScreen()
    {
        if (touched) return;
        m.isEdit = true;
        lsm.LoadSceneWithLoadingScene(1);
        touched = true;
    }
}
