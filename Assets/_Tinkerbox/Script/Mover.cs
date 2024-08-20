using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
   private List<Vector3> PathList = new List<Vector3>();
   private int currentPathIndex = 0;

   private void Update()
   {
      if (PathList.Count>0)
      {
         MoveToPath();
      }
   }

   public void MoveToPath()
   {
      var a = (PathList[currentPathIndex] - transform.position).magnitude;
      
      if (a>0.1f)
      {
         transform.position = Vector3.Lerp(transform.position, PathList[currentPathIndex], Time.deltaTime * 20f);
      }else if (a <= 0.1f)
      {
         currentPathIndex++;
      }

      if (currentPathIndex>=PathList.Count)
      {
         PathList.Clear();
         currentPathIndex = 0;
         Debug.Log("Movement DONE!");
      }
   }

   public void SetPath(List<Vector3> path)
   {
      PathList = new List<Vector3>();
      for (int i = 0; i < path.Count; i++)
      {
         PathList.Add(path[i]);
      }
   }
}
