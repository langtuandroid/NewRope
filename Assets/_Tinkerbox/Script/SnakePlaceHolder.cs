using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SnakePlaceHolder : MonoBehaviour
{
   public  GameObject CurrentPlaceHolderObject;
   public  GameObject NextPlaceHolderObject;

   private SnakeController sController;
   private bool _shake = false;
   private void Start()
   {
      sController = GetComponent<SnakeController>();
      CurrentPlaceHolderObject.GetComponent<PlaceLayerSetter>().SetObstacleComponentEnable(true,"SnakePlace");
      NextPlaceHolderObject.GetComponent<PlaceLayerSetter>().SetObstacleComponentEnable(false,"Default");
      NextPlaceHolderObject.GetComponent<PlaceImageSetter>().SetCanvasEnabled(true);
      CurrentPlaceHolderObject.GetComponent<PlaceImageSetter>().SetCanvasEnabled(false);
   }
   
   
   public void NextImageShake()
   {
      if(_shake) return;
      _shake = true;

      GameObject go = null;
      
      if (!NextPlaceHolderObject.GetComponent<PlaceImageSetter>().IsUp)
      {
         go = NextPlaceHolderObject.GetComponent<PlaceImageSetter>().ImageCanvas.gameObject;
      }

      if (!CurrentPlaceHolderObject.GetComponent<PlaceImageSetter>().IsUp)
      {
         go = CurrentPlaceHolderObject.GetComponent<PlaceImageSetter>().ImageCanvas.gameObject;

      }

     

        go.SetActive(true);
        
         go.GetComponent<RectTransform>()
            .DOScale(.75f, .175f).SetEase(Ease.Linear).SetLoops(6, LoopType.Yoyo).OnComplete(
               () =>
               {
                  go.SetActive(false);
                  _shake = false;
               });
   
      


   }

   public void SwapHolders()
   {
      (CurrentPlaceHolderObject, NextPlaceHolderObject) = (NextPlaceHolderObject, CurrentPlaceHolderObject);
     // Debug.Log("Place Swapped!");
   }

   public Transform GetCurrentHolderFirstTransform()
   {
      //snake head e esitlenmeli
      CurrentPlaceHolderObject.GetComponent<PathEntryCalculator>().SnakeHead = sController.SnakeHead;
      //Debug.Log("Current start place alindi");
      return CurrentPlaceHolderObject.GetComponent<PathEntryCalculator>().GetClosestEntry();
   }

   public void CurrentStartLayerChangeTo(string layerName,bool obstacleEnabled)
   {
      var place = GetCurrentHolderFirstTransform();
      place.transform.parent.GetComponent<PlaceLayerSetter>().SetStartLayerDisable(place.gameObject,obstacleEnabled);
   }

   public Transform GetNextHolderEntryTransform()
   {
      //snake head e esitlenmeli
      NextPlaceHolderObject.GetComponent<PathEntryCalculator>().SnakeHead = sController.SnakeHead;
      //Debug.Log("Next start place alindi");
      return NextPlaceHolderObject.GetComponent<PathEntryCalculator>().GetClosestEntry();
   }

   public void ChangeAllNextPlaceLayer(bool isEnabled,string layer)
   {
      var place = NextPlaceHolderObject;
      place.GetComponent<PlaceLayerSetter>().SetObstacleComponentEnable(isEnabled,layer);
   }

   public void ChangeAllCurrentPlaceLayer(bool isEnabled, string layer)
   {
      var place = CurrentPlaceHolderObject;
      place.GetComponent<PlaceLayerSetter>().SetObstacleComponentEnable(isEnabled,layer);
   }
   

}
