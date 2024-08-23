using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Obi;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class PlaceImageSetter : MonoBehaviour
{
  public GameObject ImageCanvas;

  public GameObject Rope;
  public Material M;
  private Image _im;
  public bool IsUp;
  public float Y;
  private void Start()
  {
      M = Rope.GetComponent<MeshRenderer>().material;
      _im = ImageCanvas.transform.GetChild(0).GetComponent<Image>();
      _im.color = M.color;
      _im.DOFade(0.5f, 0.01f);
      ImageCanvas.GetComponent<RectTransform>().DOAnchorPosY(Y,0);
  }

  [Button]
  public void FindM()
  {

      ImageCanvas.transform.GetChild(0).GetComponent<Image>().color = M.color;

  }

  public void SetCanvasEnabled(bool enabled) => ImageCanvas.SetActive(enabled);
}
