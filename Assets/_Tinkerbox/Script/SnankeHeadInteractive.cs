using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SnankeHeadInteractive : MonoBehaviour
{
    public MeshRenderer Mr;

    public void SetColorFlash()
    {
        Mr.material.DOColor(Color.red, 0.15f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
        Mr.transform.DOScale(170f, 0.075f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
        Mr.transform.DOShakeRotation(0.1f,45, 5, 15f, false, ShakeRandomnessMode.Harmonic).SetLoops(2, LoopType.Yoyo);
    }
}
