using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine;

public class PathEntryCalculator : MonoBehaviour
{
   public Transform LeftPathStartTransform;
   public Transform RightPathStartTransform;

   public List<Transform> LeftPathTransformList;
   public List<Transform> RightPathTransformList;

   public List<Vector3> LList;
   public List<Vector3> RList;

   private bool _isLeft;
   private bool _isRight;
   public Transform SnakeHead { get; set; }

   private void Start()
   {
   

      for (int i = 0; i < LeftPathTransformList.Count; i++)
      {
         LList[i] = LeftPathTransformList[i].position;
      }

      for (int i = 0; i < RightPathTransformList.Count; i++)
      {
         RList[i] = RightPathTransformList[i].position;
      }
   }

   [Button]
   public void SetAllList()
   {
      LeftPathTransformList.Clear();
      RightPathTransformList.Clear();
      
      for (int i = 0; i < transform.childCount-1; i++)
      {
         LeftPathTransformList.Add(transform.GetChild(i));
      }

      for (int i = LeftPathTransformList.Count-1; 0 <= i; i--)
      {
         RightPathTransformList.Add(LeftPathTransformList[i]);
      }

      LList = new List<Vector3>(LeftPathTransformList.Count);
      RList = new List<Vector3>(RightPathTransformList.Count);
   }

 

   
   
   public Transform GetClosestEntry()
   {
      
      
      
      
      var left = LeftPathStartTransform.position;
      var right = RightPathStartTransform.position;

      left.y = SnakeHead.transform.position.y;
      right.y = left.y;

      var leftDiff = (SnakeHead.transform.position - left).magnitude;
      var rightDiff = (SnakeHead.transform.position - right).magnitude;

      if (leftDiff < rightDiff) 
      {
         
         return LeftPathStartTransform;
      }

      return RightPathStartTransform;
   }
   public bool CalculatePathDirection()
   {
      var left = LeftPathStartTransform.position;
      var right = RightPathStartTransform.position;

      left.y = SnakeHead.transform.position.y;
      right.y = left.y;

      var leftDiff = (SnakeHead.transform.position - left).magnitude;
      var rightDiff = (SnakeHead.transform.position - right).magnitude;

      if (leftDiff < rightDiff) 
      {
         Debug.Log("Going To First Entry Point");
         return true;
      }
     
      Debug.Log("Going To Sec Entry Point");
      return false;

   }

   public List<Vector3> GetEntryPath()
   {
      if (CalculatePathDirection())
      {
         return LList;
         //snake path tanımlaması right
      }
      
  

      return RList;
      
   }
   
   
}
