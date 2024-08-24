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
   private CompletePanel _cPanel;
   //public GameObject CloakImage;
   public GameObject HandObject;

   public bool IsTutorial;
   private void Awake()
   {
      TapToStartTMP.transform.DOScale(1.05f, 0.25f).SetEase(Ease.Linear).SetLoops(-2, LoopType.Yoyo);
      
      TopCounterImage.SetActive(false);
      TapToStart.SetActive(true);
      currentMinute = Minute;
      currentSecond = Second;
      
      TimerText.text = currentMinute.ToString("00") + ":" + currentSecond.ToString("00");
      FakeTimerText.text = currentMinute.ToString("00") + ":" + currentSecond.ToString("00");
      _cPanel = FindObjectOfType<CompletePanel>();
      
      if (IsTutorial)
      {
         FakeTimerText.gameObject.SetActive(false);
         TimerText.gameObject.SetActive(false);
         //CloakImage.gameObject.SetActive(false);
         TapToStart.gameObject.SetActive(false);
         HandObject.SetActive(true);
         HandObject.transform.DOScale(1.15f, .2f).SetEase(Ease.Linear).SetLoops(-2,LoopType.Yoyo);
      }
   }

   private void Update()
   {
    
      if (Input.GetMouseButtonDown(0) && !_tapControl)
      {
         if(!IsTutorial)TopCounterImage.SetActive(true);
         if(!IsTutorial)TapToStart.SetActive(false);
         _timerOn = true;
         _tapControl = true;
         if(IsTutorial) HandObject.SetActive(false);

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
         _cPanel.FailPanelActive();
         return;
      }
      
      
   }
   
   public double ClaculateTime()
   {
      var m = (Minute*60) + Second;
      var c = (currentMinute * 60) + currentSecond;
      var total = (m - c);
      
      Debug.LogError($"minute : {m} , secc: {c} , total:{total}");
      return total;
   }
}
