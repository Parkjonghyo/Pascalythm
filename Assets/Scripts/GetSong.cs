using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class GetSong : MonoBehaviour
{
    [SerializeField] private Transform contents_tr;
    [SerializeField] private TextMeshPro title;
    [SerializeField] private TextMeshPro composer;
    [SerializeField] private TextMeshPro D_G, D_S, D_M;
    /*[SerializeField] private TextMeshPro highScore;*/
    [SerializeField] private TextMeshPro HS_G, HS_S, HS_M;
    [SerializeField] private TextMeshPro bpm;
    [SerializeField] private TextMeshPro chart_designer;
    /*[SerializeField] private Button B_G, B_S, B_M; */
        
    private Music m;
    private LoadingSceneManager lsm;
    
    
    private SqliteConnection con;

    private void Awake()
    {
        m = GameObject.FindWithTag("UniqueGameManager").GetComponent<Music>();
        lsm = GameObject.FindWithTag("LoadingSceneManager").GetComponent<LoadingSceneManager>();
    }

    public static string GetConStr()
    {
        string strCon = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            strCon = "URI=file:" + Application.persistentDataPath + "/Pascalythm_local.db";
        }
        else
        {
            strCon = "URI=file:" + Application.dataPath + "/Pascalythm_local.db";
        }
        return strCon;
    }

    void Start()
    {
        #region GameObject 가져오기
        /*Text[] Main_Title = new Text[8];
        for (int i = 0; i < 8; i++)
        {
            Main_Title[i] = contents_tr.GetChild(i).GetComponentInChildren<Text>();
        }*/
        Text[] Main_Title = new Text[contents_tr.childCount];
        for (int i = 0; i < contents_tr.childCount; i++)
        {
            Main_Title[i] = contents_tr.GetChild(i).GetComponentInChildren<Text>();
        }
        #endregion

        #region 데이터베이스 연결 및 질의
        List<string> Title = new List<string>();

        con = new SqliteConnection(GetConStr());
        con.Open();

        string stm = "SELECT Title FROM Song_Info";

        var cmd = new SqliteCommand(stm, con);

        SqliteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            Title.Add(rdr["Title"].ToString());
        }
        #endregion

        #region 값 넣기
        /*for (int i = 0; i < 8; i++)
        {
            Main_Title[i].text = Title[i];
        }*/
        for (int i = 0; i < Main_Title.Length; i++)
        {
            Main_Title[i].text = Title[i];
        }
        #endregion
        
        con.Close();
        
        selectDatabase(Title[0]);
        /*if (m.difficulty.Equals(0))
        {
            B_G.onClick.Invoke();
            B_G.Select();
        }
        else if (m.difficulty.Equals(1))
        {
            B_S.onClick.Invoke();
            B_S.Select();
        }
        else
        {
            B_M.onClick.Invoke();
            B_M.Select();
        }*/
    }

    void setText(string[] args)
    {
        title.text = args[0];
        composer.text = args[1];
        chart_designer.text = string.Format("Chart_Designer : {0}", args[2]);
        bpm.text = string.Format("BPM : {0}", args[3]);
        m.bpm = int.Parse(args[3]);
        D_G.text = args[4];
        D_S.text = args[5];
        D_M.text = args[6];
    }

    void setScoreText()
    {
        string stm = $"SELECT Difficult, Score FROM Score_Info WHERE title=@title";

        var cmd = new SqliteCommand(stm, con);
        cmd.Parameters.AddWithValue("@title", m.songName);

        SqliteDataReader rdr = cmd.ExecuteReader();

        if (rdr.IsDBNull(0))
        {
            HS_G.text = String.Format("{0,7:D7}", 0);
            HS_S.text = String.Format("{0,7:D7}", 0);
            HS_M.text = String.Format("{0,7:D7}", 0);
        }
        
        while (rdr.Read())
        {
            if(rdr["Difficult"].Equals("G")) HS_G.text = String.Format("{0,7:D7}", rdr["Score"]); 
            else if(rdr["Difficult"].Equals("S")) HS_S.text = String.Format("{0,7:D7}", rdr["Score"]); 
            else if(rdr["Difficult"].Equals("M")) HS_M.text = String.Format("{0,7:D7}", rdr["Score"]); 
        }
    }
    
    public void selectDatabase(string title_sn)
    {
        m.songName = title_sn;
        
        string[] args = new string[7];
        
        con.Open();

        string stm = $"SELECT * FROM Song_Info WHERE title=@title";

        var cmd = new SqliteCommand(stm, con);
        cmd.Parameters.AddWithValue("@title", m.songName);

        SqliteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            for (int i = 0; i < 7; i++)
            {
                args[i] = rdr[i].ToString();   
            }
        }
        setScoreText();
        setText(args);
        con.Close();
    }

    public void Difficulty_Btn_Click(int i)
    {
        m.difficulty = i;
        if (m.isEdit)
        {
            lsm.LoadSceneWithLoadingScene(4);    
        }
        else
        {
            lsm.LoadSceneWithLoadingScene(2);   
        }
    }

    public void Back_Btn_Click()
    {
        lsm.LoadSceneWithLoadingScene(0);
    }
    
}