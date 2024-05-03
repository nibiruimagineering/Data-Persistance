using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private InputField _playerName;

    public string name = "";
    public string bestName;
    public int bestScore;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
}
