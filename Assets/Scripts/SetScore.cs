using UnityEngine;
using Mono.Data.Sqlite;
using TMPro;
using static UnityEngine.PlayerPrefs;

public class SetScore : MonoBehaviour
{
    private Music m;
    private SqliteConnection con;

    private int dScore;
    private string diff;

    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private GameObject new_record;
    

    private void Awake()
    {
        m = GameObject.FindWithTag("UniqueGameManager").GetComponent<Music>();
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
        if (m.difficulty.Equals(0)) diff = "G";
        else if (m.difficulty.Equals(1)) diff = "S";
        else diff = "M";
        
        #region 데이터베이스 연결 및 질의
        
        con = new SqliteConnection(GetConStr());
        con.Open();

        string stm = "SELECT Score FROM Score_Info WHERE Title=@title AND Difficult=@difficult";

        var cmd = new SqliteCommand(stm, con);

        cmd.Parameters.AddWithValue("@title", m.songName);
        cmd.Parameters.AddWithValue("@difficult", diff);
        
        SqliteDataReader rdr = cmd.ExecuteReader();

        if (rdr.IsDBNull(0))
        {
            Debug.Log("Inserted");
            insertDatabase();
            con.Close();
            bestScore.text = GetInt("rScore").ToString();
            new_record.SetActive(true);
            return;
        } 
        
        while (rdr.Read())
        {
            Debug.Log("Red");
            dScore = int.Parse(rdr["Score"].ToString());
            bestScore.text = dScore.ToString();
        }
        #endregion

        if (dScore < GetInt("rScore"))
        {
            Debug.Log("Updated");
            new_record.SetActive(true);
            updateDatabase();
            bestScore.text = GetInt("rScore").ToString();
        }

        con.Close();
    }

    public void insertDatabase()
    {
        string stm = "INSERT INTO Score_Info VALUES(@title, @difficult, @score)";

        var cmd = new SqliteCommand(stm, con);

        cmd.Parameters.AddWithValue("@title", m.songName);
        cmd.Parameters.AddWithValue("@difficult", diff);
        cmd.Parameters.AddWithValue("@score", GetInt("rScore"));
        
        cmd.ExecuteNonQuery();
    }

    public void updateDatabase()
    {
        string stm = "UPDATE Score_Info SET Score=@score WHERE Title=@title AND Difficult=@difficult";

        var cmd = new SqliteCommand(stm, con);

        cmd.Parameters.AddWithValue("@title", m.songName);
        cmd.Parameters.AddWithValue("@difficult", diff);
        cmd.Parameters.AddWithValue("@score", GetInt("rScore"));
        
        cmd.ExecuteNonQuery();
    }
    
}