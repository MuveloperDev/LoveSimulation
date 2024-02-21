using Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private Scenes _currentScene;
    public Scenes currentScene { get { return _currentScene; }}

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += LoadedScene;
        SceneLoader.Instance.onCompleteLoad += LoadComplete;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIManager_Out.Instance.Dispose();
            Debug.Log("�ΰ��Ӿ� �ε� ����");
            SceneLoader.Instance.LoadSceneAsync("InGameScene");
        }
    }

    private void LoadComplete()
    {
        Debug.Log("�� ���Ӿ� �ε� ��");
    }

    private void LoadedScene(Scene scene, LoadSceneMode mode)
    {
       switch (scene.name)
       {
            case nameof(Scenes.TitleScene):
                {
                    _currentScene = Scenes.TitleScene;
                     UIManager_Out.Instance.Initialize();
                    TitleSequence();
                }
                break;
            case nameof(Scenes.InGameScene):
                {
                    Debug.Log("�ΰ��Ӿ� �ε� ��");
                    _currentScene = Scenes.InGameScene;
                    UIManager_In.Instance.Initialize();
                    InGameSequence();
                }
                break;
       }
    }

    private void TitleSequence()
    { 
        
    }
    private void InGameSequence()
    { 
        
    }
}
