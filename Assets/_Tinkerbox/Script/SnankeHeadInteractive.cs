using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SnankeHeadInteractive : MonoBehaviour
{
    public MeshRenderer Mr;
    private AudioSource _aSource;
    private bool _effectActive = false;
    private Color c;
    private void Start()
    {
        _aSource = GetComponent<AudioSource>();
       c = Mr.material.color;

    }

    public void SetColorFlash()
    {
        
        if(_effectActive) return;

        _effectActive = true;
        if(!_aSource.isPlaying)   _aSource.Play();
        Mr.material.DOColor(Color.red, 0.15f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            Mr.material.color = c;

        });
        Mr.transform.DOScale(170f, 0.075f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
        Mr.transform.DOShakeRotation(0.1f,45, 5, 15f, false, ShakeRandomnessMode.Harmonic).SetLoops(2, LoopType.Yoyo).OnComplete(
            () =>
            {
                _effectActive = false;
            });
    }
}
