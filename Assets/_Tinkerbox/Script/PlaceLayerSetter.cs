using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Sirenix.OdinInspector;
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

    [Button]
    public void GetObstacle()
    {
        ObstacleGrids.Clear();
        foreach (Transform VARIABLE in transform)
        {
            if(VARIABLE.GetComponent<DynamicGridObstacle>()) ObstacleGrids.Add(VARIABLE.GetComponent<DynamicGridObstacle>());
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
