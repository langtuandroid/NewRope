using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class HapticManager : MonoBehaviour
{
  public MMF_Player HeavyHaptics;
  public MMF_Player LightHaptics;

  public void SetVibration(bool isHeavy)
  {
    if (isHeavy)
    {
      HeavyHaptics.PlayFeedbacks();
    }
    else
    {
      LightHaptics.PlayFeedbacks();
    }
  }
}
