using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RhythmParser : MonoBehaviour
{
    Music m;
    private TextAsset data, data_T;
    public int noteCount = 0;

    private void LoadTrace()
    {
        data_T = Resources.Load(m.songName + "/1" + m.difficulty, typeof(TextAsset)) as TextAsset;
        
        StringReader sr = new StringReader(data_T.text);
        var line = sr.ReadLine();
        while (line != null) // 끝날때까지 돌린다
        {
            noteCount++;
            //다음 줄을 읽는다.
            line = sr.ReadLine();
        }
    }

    private void LoadNote()
    {
        data = Resources.Load(m.songName + "/0" + m.difficulty, typeof(TextAsset)) as TextAsset;
        
        StringReader sr = new StringReader(data.text);
        var line = sr.ReadLine();
        while (line != null) // 끝날때까지 돌린다
        {
            noteCount++;
            //다음 줄을 읽는다.
            line = sr.ReadLine();
        }
    }
    
    private void Awake()
    {
        m = GameObject.FindWithTag("UniqueGameManager").GetComponent<Music>();
        LoadNote();
        LoadTrace();
    }

    public Queue<int> GiveNoteNew(int btnNumber)
    {
        Queue<int> note2 = new Queue<int>();
        if (data != null)
        {
            StringReader sr = new StringReader(data.text);
            
            //읽어올 스트링
            // sr, 즉 받아온 텍스트파일을 한줄씩 읽는다.
            var line = sr.ReadLine();

            while (line != null) // 끝날때까지 돌린다
            {
                //괄호를 없애고 공백을 지운다.
                line = line.Replace("(", "").Replace(")", "").Replace(";", "");

                // 스트링을 스플릿할 배열
                // 쉼표를 기준으로 스플릿한다.
                var lineArray = line.Split(',');

                if (btnNumber == int.Parse(lineArray[1]))
                {
                    note2.Enqueue(int.Parse(lineArray[0]));
                }

                //다음 줄을 읽는다.
                line = sr.ReadLine();
            }
        }
        return note2;
    }
    
    public Queue<string[]> GiveNoteNew_T()
    {
        Queue<string[]> note2 = new Queue<string[]>();
        if (data_T != null)
        {
            StringReader sr = new StringReader(data_T.text);
            
            //읽어올 스트링
            // sr, 즉 받아온 텍스트파일을 한줄씩 읽는다.
            var line = sr.ReadLine();

            while (line != null) // 끝날때까지 돌린다
            {
                //괄호를 없애고 공백을 지운다.
                line = line.Replace("(", "").Replace(")", "").Replace(";", "").Trim();

                // 스트링을 스플릿할 배열
                // 쉼표를 기준으로 스플릿한다.
                string[] lineArray = line.Split(',');
                
                note2.Enqueue(lineArray);

                //다음 줄을 읽는다.
                line = sr.ReadLine();
            }
        }
        return note2;
    }
}
