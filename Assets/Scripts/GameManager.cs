using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private InputField _playerName;

    public string name = "";
    public string bestName;
    public int bestScore;

    public static GameManager Instance;

    private void Start()
    {
        if (bestName != "")
        {
            _bestScoreText.text = "Best Score : " + bestName + " : " + bestScore;
        }
    }

    public void SetBestScore(int score)
    {
        if(score > bestScore) 
        {
            bestScore = score;
            bestName = name;
            SaveGameInfo();
            MainManager.Instance.BestScoreText.text = "Best Score : " + bestName + " : " + bestScore; 
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadGameInfo();
    }

    public void StartNew()
    {
        if (_playerName.text != "") 
        {
            name = _playerName.text;
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("Player Must Enter A Name To Play");
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int bestScore;
    }

    public void SaveGameInfo()
    {
        SaveData data = new SaveData();
        data.name = name;
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
     
    }

    public void LoadGameInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestName = data.name;
            bestScore = data.bestScore;
        }
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
}
