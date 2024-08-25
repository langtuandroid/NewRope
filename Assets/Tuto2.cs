using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tuto2 : MonoBehaviour
{
    public List<GameObject> Walls;
    public List<GameObject> Hands;
    private int a = 0;

    private void Start()
    {
        Walls[0].gameObject.SetActive(true);
        Hands[0].gameObject.SetActive(true);
        Hands[0].transform.DOScale(1.05f, .2f).SetEase(Ease.Linear).SetLoops(-2, LoopType.Yoyo);
    }

    public void RaiseIndex()
    {
        Debug.Log(a);
        Walls[a].gameObject.SetActive(false);
        Hands[a].gameObject.SetActive(false);
        a++;
        Debug.Log(a);
        
        for (int i = 0; i < 3; i++)
        {
            if (i == a) 
            {
                Walls[i].gameObject.SetActive(true);
                Hands[i].gameObject.SetActive(true);

                Hands[i].transform.DOScale(1.05f, .2f).SetEase(Ease.Linear).SetLoops(-2, LoopType.Yoyo);
            }
        }
    }
}
