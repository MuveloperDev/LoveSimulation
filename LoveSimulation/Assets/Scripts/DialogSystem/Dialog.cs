using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Cysharp.Threading.Tasks;

public class Dialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    Tween tween;

    public void Play(string argText, float duration)
    {
        if (null != tween && true == tween.IsPlaying())
            tween.Kill();

        tmp.text = string.Empty;
        tween = tmp.DOText(argText, duration);
    }

    public void Kill()
    {
        if (null == tween || false == tween.IsPlaying())
            return;

        tween.Kill();
    }
    public void ClearText() => tmp.text = string.Empty;
}
