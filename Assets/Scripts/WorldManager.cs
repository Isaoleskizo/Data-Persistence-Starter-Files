using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class WorldManager : MonoBehaviour
{
    private static WorldManager instance = null;
    public string playerName;
    public GameObject nom;
    public GameObject score;

    public string highscoreName;
    public int highscore;

    public static WorldManager Instance => instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null && instance != this) 
        { 
            Destroy(instance);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        LoadData();


        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        score.GetComponent<TextMeshProUGUI>().text = highscoreName + " | "+ highscore;
    }
    public void StartGame()
    {
        playerName = nom.GetComponent<TextMeshProUGUI>().text;
        SceneManager.LoadScene("main");
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE
        Application.Quit();
#endif
    }


    public void SaveData()
    {
        Data data = new();
        data.name = highscoreName;
        data.score = highscore;
        WorldManager.Instance.highscore = highscore;
        WorldManager.Instance.highscoreName = highscoreName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Data data = JsonUtility.FromJson<Data>(json);
            highscoreName = data.name;
            highscore = data.score;
        }
    }
}
