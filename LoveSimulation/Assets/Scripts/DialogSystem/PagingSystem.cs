using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#pragma warning disable CS1998 
public class PagingSystem : UIBase, IPointerDownHandler
{
    List<string> textList = new List<string> {
        "Hello Everyone! my name is jeonmuhyuk",
        "Hello Everyone! my name is AAAAAAAAA",
        "Hello Everyone! my name is BBBBBBBBB"
    };
    int i = 0;

    [Header("DEPENDENCE")]
    [SerializeField] private Dialog _dialog;
    [SerializeField] private CanvasGroup _canvasGroup;
    [Header("INFORMATION")]
    [SerializeField] private bool killProcess = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        _dialog.Play(textList[i], 3.0f);
        i++;
    }

    protected async override UniTask BeforeShow()=> _dialog.ClearText();
    protected override void AfterShow()
    {
        base.AfterShow();
        _canvasGroup.DOFade(1, 1f).OnComplete(() =>
        {
            _dialog.Play(textList[i], 3.0f);
            i++;
        });
    }

    protected async override UniTask BeforeHide()
    {
        killProcess = true;
        _canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            _dialog.Kill();
            killProcess = false;
        });
        await UniTask.WaitUntil(() => false == killProcess);
    }
    protected override void AfterHide()
    {
        base.AfterHide();
        _dialog.ClearText();
        i = 0;
    }
}
