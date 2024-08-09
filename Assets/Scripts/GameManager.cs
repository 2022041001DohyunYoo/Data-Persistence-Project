using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static GameManager Instance ;
    public int highScore;

    public string bestPlayerName = " ";

    public string playerName;

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load_Data();
        }  
        else if(Instance != this){
            Destroy(gameObject);
        }
    }

    [System.Serializable]

    class SaveData{ public int highScore; public string bestPlayerName; }

    public void Save_Data(){
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.bestPlayerName = bestPlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load_Data(){
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path)){
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            bestPlayerName = data.bestPlayerName;
        }
    }

}
