using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVEditor : Singleton<CSVEditor>
{
    public string filepath;
    StreamWriter editor;
    StreamReader reader;
    public List<Response> dataFromFile;
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void writeOnFile(Response[] allUsers)
    {
        editor = new StreamWriter(filepath, false);
        editor.WriteLine("Name, Phone, Email, Score, Rank");
        editor.Close();

        editor = new StreamWriter(filepath, true);
        for(int i = 0; i < allUsers.Length; i++)
        {
            editor.WriteLine(allUsers[i].name + ", " + allUsers[i].phone + ", " + allUsers[i].email + ", " + allUsers[i].score);
        }

        editor.Close();
    }

    public List<Response> readFromFile()
    {
        dataFromFile = new List<Response>();
        reader = new StreamReader(filepath);
        bool endOfFile = false;
        string line = reader.ReadLine();
        if (line == null)
        {
            return null;
        }
        while (!endOfFile)
        {
            string Data = reader.ReadLine();
            if (Data == null)
            {
                endOfFile = true;
                break;
            }
            var data_Values = Data.Split(',');
            Response temp = new Response();
            temp.name = data_Values[0].ToString();
            temp.phone = data_Values[1].ToString();
            temp.email = data_Values[2].ToString();
            temp.score = int.Parse(data_Values[3]);
            dataFromFile.Add(temp);
        }
        return dataFromFile;
    }


}
