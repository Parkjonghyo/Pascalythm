/*using UnityEngine;
using System;
using MySql.Data.MySqlClient;

public class DatabaseConnection : MonoBehaviour
{
    private MySqlConnection connection;
    private string server;
    private string database;
    private string uid;
    private string password;

    private void DBStartUp()
    {
        string connStr = "server=pascal.ccjs8el6oepb.us-east-1.rds.amazonaws.com;port=3306;database=Pascal;uid=chrina;pwd=Aa101023535.;charset=utf8;";

        connection = new MySqlConnection(connStr);
        connection.Open();
    }

    void Start()
    {
        try{
            DBStartUp();
        }catch(Exception ex)
        {
            Debug.Log(ex);
        }

        string query = "SELECT * FROM Account";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        Debug.Log(cmd.CommandTimeout);
        MySqlDataReader dataReader = cmd.ExecuteReader();

        Debug.Log(dataReader.GetString(0));
        #region ㅁㄴㅇㄹ
        //using (MySqlConnection connection = new MySqlConnection(connectionString))
        //{
        //    connection.Open();
        //    Debug.Log("Success");

        //    string query = "SELECT * FROM Pascal.Account";

        //    MySqlCommand cmd = new MySqlCommand("set net_write_timeout=99999; set net_read_timeout=99999;", connection);
        //    cmd.ExecuteNonQuery();

        //    cmd.CommandText = query;
        //    cmd.ExecuteNonQuery();

        //    Debug.Log(cmd);
        //}
        #endregion
    }
}*/