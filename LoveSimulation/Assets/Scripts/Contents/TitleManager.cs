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
    // Start is called before the first frame update
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