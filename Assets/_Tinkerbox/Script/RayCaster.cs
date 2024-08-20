using System;
using System.Collections;
using System.Collections.Generic;
using Obi;
using Pathfinding;
using UnityEngine;

public class RayCaster : MonoBehaviour
{

    public List<ObiSolver> obiSolver;
    public PathController PController;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private GameObject GetHittedRope()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log("X: " + ray.origin.x + "Y: " + ray.origin.y + "Z: " + ray.origin.z);
        Debug.DrawRay(ray.origin, ray.direction * 1000);

        int filter = ObiUtils.MakeFilter(ObiUtils.CollideWithEverything, 0);

        // perform a raycast, check if it hit anything:
        for (int i = 0; i < obiSolver.Count; i++)
        {
            if (obiSolver[i].Raycast(ray, out QueryResult result, filter, 50, 0.1f))
            {
                //obiSolver.particleToActor
                // get the start and size of the simplex that was hit:
                int simplexStart =
                    obiSolver[i].simplexCounts.GetSimplexStartAndSize(result.simplexIndex, out int simplexSize);

                int particleIndex = obiSolver[i].simplices[simplexStart];

                ObiRope rope = obiSolver[i].particleToActor[particleIndex].actor as ObiRope;

                return rope.gameObject;

            }
        }
    

        return null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hittedSnake = GetHittedRope();
            Debug.Log(hittedSnake.name);
            if (hittedSnake != null)
            {
                SnakePlaceHolder sHolder = hittedSnake.GetComponent<SnakePlaceHolder>();
                
                //Change layer to nonObstacle ---> CURRENT PLACE
                sHolder.CurrentStartLayerChangeTo("Default",false);
                
                //Change layer to nonObstacle ---> NEXT PLACE
                sHolder.ChangeAllNextPlaceLayer(false,"Default");
                //sHolder.ChangeAllCurrentPlaceLayer(false,"Default");
                
                //Get Current-End pos
                var currentStartTransform = sHolder.GetCurrentHolderFirstTransform();
                var nextEndTransform = sHolder.GetNextHolderEntryTransform();

                PController.t1 = currentStartTransform;
                PController.t2 = nextEndTransform;
                
                //Set Seeker
                Seeker s = hittedSnake.GetComponent<Seeker>();
                
                //Set Snake Controller
                SnakeController snk = hittedSnake.GetComponent<SnakeController>();
                
                PController.RequestPath(s,snk);

            }
        }
    }
}
