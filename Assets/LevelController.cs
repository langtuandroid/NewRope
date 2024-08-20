using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int TotalCompleteCount;
    private int currentCount;
   public void SetCompleteCount(int value)
       {
           currentCount += value
        Debug.LogError(currentCount);   
       }
}
