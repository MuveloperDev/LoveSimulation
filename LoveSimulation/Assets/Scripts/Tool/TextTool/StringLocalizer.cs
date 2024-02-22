using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StringLocalizer : MonoBehaviour
{
    [Header("[ DEPENDENCE ]")]
    [SerializeField] private TextMeshProUGUI tmp;
    [Header("[ INFORMATION ]")]
    [SerializeField] private int _currentId = -1;
    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        if (null == tmp)
        {
            Debug.LogError($"{GetType()} tmp is null.");
            return;
        }

        StringLocalizerManager.Instance.onChangeLanguage += UpdateLanguage;
    }
    void Start()
    {
        
    }

    public void UpdateString(int id)
    {
        _currentId = id;
        switch (StringLocalizerManager.Instance.currentLanguage)
        {
            case ELanguage.En:
                { 
                }
                break;
            case ELanguage.Kr:
                {
                }
                break;
            default:
                Debug.LogError("Invaild language..");
                break;
        }
    }

    public void UpdateLanguage(ELanguage eLanguage)
    {
        switch (eLanguage)
        {
            case ELanguage.En:
                {
                }
                break;
            case ELanguage.Kr:
                {
                }
                break;
            default:
                Debug.LogError("Invaild language..");
                break;
        }
        tmp.font = StringLocalizerManager.Instance.fontAsset;
    }
}
