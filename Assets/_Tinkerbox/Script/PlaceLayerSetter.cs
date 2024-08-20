using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class PlaceLayerSetter : MonoBehaviour
{
    public List<DynamicGridObstacle> ObstacleGrids;


    public void SetObstacleComponentEnable(bool isEnable,string toLayer)
    {
        for (int i = 0; i < ObstacleGrids.Count; i++)
        {
            ObstacleGrids[i].gameObject.layer = LayerMask.NameToLayer(toLayer);
            ObstacleGrids[i].enabled = isEnable;
        }
    }

    public void SetStartLayerDisable(GameObject go,bool isEnabled)
    {
        for (int i = 0; i < ObstacleGrids.Count; i++)
        {
            if (ObstacleGrids[i].gameObject == go.gameObject)
            {
                ObstacleGrids[i].gameObject.layer = LayerMask.NameToLayer("Default");
                ObstacleGrids[i].enabled = isEnabled;
            }
           
        }
    }
}
