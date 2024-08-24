using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Obi;
using Pathfinding;
using TMPro;
using UnityEngine;

[System.Serializable]
public class RayCaster : MonoBehaviour
{

    public List<ObiSolver> obiSolver;
    public PathController PController;
    public LayerMask M;

    public TextMeshProUGUI CounterTMP;
    public int TotalCount;
    public GameObject Glass;
    private bool _shake = true;

    public AudioSource GlassClick;
    public AudioSource GlassDestroy;

    public List<Glasses> GList;
    public int ClickCount { get; set; }

    private HapticManager _hM;
    private void Start()
    {
        Application.targetFrameRate = 60;
        
        for (int i = 0; i < GList.Count; i++)
        {
            
            GList[i].GlassTMP.text = GList[i].GlassCount.ToString("0");
        }

        float f = 0;
        DOTween.To(x => f = x, 0, 1, 0.1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            AstarPath.active.Scan();
        });

        _hM = FindObjectOfType<HapticManager>();
    }


    public void FindSolvers()
    {
        foreach (var VARIABLE in FindObjectsOfType<ObiSolver>())
        {
            obiSolver.Clear();
            obiSolver.Add(VARIABLE);
        }
    }

    public RaycastHit CheckGlass()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100f, M)) return hit;
            
        
        
            return hit;
    }

    private GameObject GetHittedRope()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       // Debug.Log("X: " + ray.origin.x + "Y: " + ray.origin.y + "Z: " + ray.origin.z);
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
        if (GList.Count > 0)
        {
            for (int i = 0; i < GList.Count; i++)
            {
                GList[i].GlassTMP.text = GList[i].GlassCount.ToString("0");

                if (GList[i].GlassCount <= 0)
                {
                    GlassDestroy.Play();
                    GList[i].GlassObject.SetActive(false);
                    GList.RemoveAt(i);
                }
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            var c = CheckGlass();

            if (c.collider != null)
            {
                GlassClick.Play();
                _hM.SetVibration(true);
                ShakeGlass(c.collider.gameObject);
                return;
            }
            Debug.Log(c);
            var hittedSnake = GetHittedRope();
            //Debug.Log(hittedSnake.name);

            //Debug.Log(hittedSnake.name);
            if (hittedSnake != null)
            {
                _hM.SetVibration(false);

                //Debug.Log("DDD");
                SnakePlaceHolder sHolder = hittedSnake.GetComponent<SnakePlaceHolder>();
                ClickCount++;
                //Change layer to nonObstacle ---> CURRENT PLACE
                sHolder.CurrentStartLayerChangeTo("Default",false);
                
                //Change layer to nonObstacle ---> NEXT PLACE
                //sHolder.ChangeAllNextPlaceLayer(false,"Default");
                sHolder.ChangeAllCurrentPlaceLayer(false,"Default");
                
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

    public void DecreaseCount()
    {
        if (GList.Count==0) return;
       // TotalCount--;

        for (int i = 0; i < GList.Count; i++)
        {
            GList[i].GlassCount--;
            GList[i].GlassTMP.text = GList[i].GlassCount.ToString("0");
        }
        //CounterTMP.text = TotalCount.ToString("0");

      
        if (TotalCount <= 0)
        {
           // GlassDestroy.Play();
            //Glass.SetActive(false);
            
        }
    }

    public void ShakeGlass(GameObject go)
    {
        if(!_shake) return;
        _shake = false;
        go.transform.DOShakeScale(.1f, 1f, 1, .25f, false, ShakeRandomnessMode.Harmonic).SetEase(Ease.Linear).SetLoops(2,LoopType.Yoyo)
            .OnComplete(
                () =>
                {
                    _shake = true;
                });
    }
}

[System.Serializable]
public class Glasses
{
    public GameObject GlassObject;
    public TextMeshProUGUI GlassTMP;
    public int GlassCount;
}
