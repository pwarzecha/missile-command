using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadManager
{
    public static void SaveScores(List<HighScoreEntry> highScoreList){
        BinaryFormatter myFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        myFormatter.Serialize(stream, highScoreList);
        stream.Close();
    }

    public static List<HighScoreEntry> LoadScores(){
        string path = Application.persistentDataPath + "/data.dat";

        if(File.Exists(path)){
            BinaryFormatter myFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<HighScoreEntry> myScores = myFormatter.Deserialize(stream) as List<HighScoreEntry>;
            stream.Close();
            return myScores;
        } else {
            Debug.LogError("Highscore saves file not found");
            return null;
        }
    }
}
