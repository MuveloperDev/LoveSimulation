using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : UIBase
{
    [Header("[ DEPENDENCE ]")]
    [SerializeField] private Button _newGameBtn;
    [SerializeField] private Button _loadBtn;
    [SerializeField] private Button _optionBtn;
    [SerializeField] private Button _quitBtn;

    [Header("[ INFORMATION ]")]
    [SerializeField] private int _titleStringId = 1;
    [SerializeField] private int _newGameStringId = 2;
    [SerializeField] private int _loadStringId = 3;
    [SerializeField] private int _optionStringId = 4;
    [SerializeField] private int _quitStringId = 5;


    void Start()
    {
        _newGameBtn.onClick.AddListener(() => { NewGameOnClickEvent(); });
        _loadBtn.onClick.AddListener(() => { LoadOnClickEvent(); });
        _optionBtn.onClick.AddListener(() => { OptionOnClickEvent(); });
        _quitBtn.onClick.AddListener(() => { QuitOnClickEvent(); });
    }

    private void NewGameOnClickEvent()
    {
        Debug.Log("NewGameOnClickEvent");
    }
    private void LoadOnClickEvent()
    { 
        Debug.Log("LoadOnClickEvent");
    }
    private void OptionOnClickEvent()
    { 
        Debug.Log("OptionOnClickEvent");
    }
    private void QuitOnClickEvent()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
