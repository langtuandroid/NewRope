using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FakeLevelHolder : MonoBehaviour
{
   public TextMeshProUGUI FakeTMP;

   private void Start()
   {
      //var a = PlayerPrefs.GetInt()
      //FakeTMP.text = a.ToString("0");

      if (PlayerPrefs.HasKey("FakeLevel"))
      {
         var a = PlayerPrefs.GetInt("FakeLevel");
         FakeTMP.text = "Level"+" " +a.ToString("0");
      }
      else
      {
         PlayerPrefs.SetInt("FakeLevel",1);
         var a = 1;
         FakeTMP.text = "Level"+" " + a.ToString("0");

      }
   }
}
