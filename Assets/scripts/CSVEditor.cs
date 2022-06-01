using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVEditor : Singleton<CSVEditor>
{
    string filepath;
    StreamWriter editor;
    StreamReader reader;
    public List<Response> dataFromFile;
    string path;
        // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath;
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            path += "/../../";
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path += "/../";
        }
        filepath = path + "\\csv.csv";
        //filepath = "D:\\RayQubeProjects\\JigsawPuzzle\\Builds\\V0.1\\csv.csv";
        Debug.Log(filepath);
        readFromFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void writeOnFile(Response[] allUsers)
    {
        Debug.Log("Writing");
        editor = new StreamWriter(filepath, false);
        editor.WriteLine("Name, Phone, Email, Score, Rank");
        editor.Close();

        editor = new StreamWriter(filepath, true);
        for(int i = 0; i < allUsers.Length; i++)
        {
            editor.WriteLine(allUsers[i].name + ", " + allUsers[i].phone + ", " + allUsers[i].email + ", " + allUsers[i].score);
        }

        editor.Close();
        dataFromFile = readFromFile();
    }

    public List<Response> readFromFile()
    {
        Debug.Log("Reading from file");
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
