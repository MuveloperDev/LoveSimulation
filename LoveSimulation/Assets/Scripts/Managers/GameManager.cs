using Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private Scenes _currentScene;
    public Scenes currentScene { get { return _currentScene; } set { } }


    private void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += LoadedScene;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
    private void LoadedScene(Scene scene, LoadSceneMode mode)
    {
       switch (scene.name)
       {
            case nameof(Scenes.TitleScene):
                {
                    _currentScene = Scenes.TitleScene;
                }
                break;
            case nameof(Scenes.InGameScene):
                {
                    _currentScene = Scenes.InGameScene;

                }
                break;
       }
    }

}
