using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
   public int Minute;
   public int Second;

   private float currentMinute;
   private float currentSecond;

   public TextMeshProUGUI FakeTimerText;
   public TextMeshProUGUI TimerText;
   
   public GameObject TapToStart;
   
   private bool _timerOn = false;
   private bool _tapControl = false;

   public GameObject TopCounterImage;
   public GameObject TapToStartTMP;
   private void Awake()
   {
      TapToStartTMP.transform.DOScale(1.05f, 0.25f).SetEase(Ease.Linear).SetLoops(-2, LoopType.Yoyo);
      
      TopCounterImage.SetActive(false);
      TapToStart.SetActive(true);
      currentMinute = Minute;
      currentSecond = Second;
      
      TimerText.text = currentMinute.ToString("00") + ":" + currentSecond.ToString("00");
      FakeTimerText = TimerText;

   }

   private void Update()
   {
      if (Input.GetMouseButtonDown(0) && !_tapControl)
      {
         TopCounterImage.SetActive(true);
         TapToStart.SetActive(false);
         _timerOn = true;
         _tapControl = true;
      }
      
      if(_timerOn) StartTimer();
   }

   private void StartTimer()
   {
      if(currentSecond>0)currentSecond -= Time.deltaTime;
      if (currentMinute > 0 && currentSecond <= 0)
      {
         currentMinute--;
         currentSecond = 60;
      }
      
      TimerText.text = currentMinute.ToString("00") + ":" + currentSecond.ToString("00");
      
      if (currentMinute == 0 && currentSecond <= 0)
      {
         Debug.LogError("Game Over");
         _timerOn = false;
         return;
      }
      
      
   }
}
