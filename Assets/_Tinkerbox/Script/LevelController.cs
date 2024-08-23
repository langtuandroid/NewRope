using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int TotalCompleteCount;
    private int currentCount;
   public void SetCompleteCount(int value)
       {
           currentCount += value;
        Debug.LogError(currentCount);

        if (currentCount == TotalCompleteCount)
        {
            float f = .1f;
            DOTween.To(x => f = x, 0, 1, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                FindObjectOfType<CompletePanel>().PanelActive();

            });
        }
       }
}
