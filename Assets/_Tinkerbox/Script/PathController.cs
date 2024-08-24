using System;
using System.Collections;
using System.Collections.Generic;
using Obi;
using Pathfinding;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public Seeker skr { get; set; }
    public Transform t1 { get; set; }
    public Transform t2 { get; set; }
    public SnakeController sController { get; set; }
    private AudioSource _aSource;

    private void Start()
    {
        skr = GetComponent<Seeker>();
        _aSource = GetComponent<AudioSource>();
        Debug.unityLogger.logEnabled = false;
    }

    private void Update()
    {
     /*   if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                t2.position = hit.point;
            RequestPath(t2.position);
        }*/

   
    }

   /* public void RequestPath(Vector3 targetPosition)
    {
        skr.StartPath(t1.position, targetPosition, OnPathComplete);
    }*/
   
   public void RequestPath(Seeker _skr,SnakeController sC)
   {
      skr = _skr;
       sController = sC;
       if(!skr.gameObject.GetComponent<SnakeController>().canMove) return;
       skr.StartPath(t1.position, t2.position, OnPathComplete);
   }
    
    private void OnPathComplete(Path path)
    {
        if (path.error)
        {
            Debug.LogError("Pathfinding error: " + path.errorLog);
            return;
        }

        if (path.vectorPath.Count > 0)
        {
            Vector3 lastPoint = path.vectorPath[path.vectorPath.Count - 1];
            Vector3 firstPoint = path.vectorPath[0];

            var lastPathPos = lastPoint;
            var neededLastPos = t2.position;

            neededLastPos.y = lastPathPos.y;

           var snakeStartPos = t1.position;
            var snakePathFirstPos = firstPoint;
            snakeStartPos.y = snakePathFirstPos.y;

            var startDiff = (snakePathFirstPos - snakeStartPos).magnitude;
            var endDiff = (neededLastPos - lastPathPos).magnitude;

           /* var aa = new GameObject("LastPathPos");
            var bb = new GameObject("NeededLastPathPos");
            
            var cc = new GameObject("SnakeStartPos");
            var dd = new GameObject("NeededSnakeStartPos");


            aa.transform.position = lastPoint;
            bb.transform.position = neededLastPos;

            cc.transform.position = snakeStartPos;
            dd.transform.position = snakePathFirstPos;*/
            
//            Debug.Log($"End Diff: {endDiff} , Start Diff: {startDiff}");
            SnakePlaceHolder a = null;
            if (!sController.GetComponent<SnakePlaceHolder>().CurrentPlaceHolderObject.GetComponent<PlaceImageSetter>()
                .IsUp)
            {
                a = sController.GetComponent<SnakePlaceHolder>();
            }
            else
            {
                a = sController.GetComponent<SnakePlaceHolder>();
            }
            
            
            if (endDiff <= .75f && startDiff<=1.1f)
            {
                _aSource.Play();
               // seeker.GetComponent<asd>().MakePath(path.vectorPath);
                Debug.Log("CAN MOVE");
              //  FindObjectOfType<SnakeController>().SetPath(path.vectorPath);
              sController.SetPath(path.vectorPath,false);
              
              sController.GetComponent<SnakePlaceHolder>().SwapHolders();
              sController.EndMovement = true;
              sController.GetComponent<SnakePlaceHolder>().ChangeAllNextPlaceLayer(false,"Default");
              sController.GetComponent<SnakePlaceHolder>().ChangeAllCurrentPlaceLayer(true,"SnakePlace");
              
              
              

            }
            else
            {
               // Debug.Log($"CANT MOVE, start diff: {startDiff} , end diff:{endDiff}");
                sController.GetComponent<SnankeHeadInteractive>().SetColorFlash();
                sController.GetComponent<SnakePlaceHolder>().ChangeAllCurrentPlaceLayer(true,"SnakePlace");
                //a.NextImageShake();
                //a.ImageShake2();
                sController.GetComponent<SnakePlaceHolder>().ImageShake2();
            }

        }
        else
        {
            //Debug.Log("No path found.");
        }

        // Yolun çizilmesi
        //DrawPath(path);
    }

    private void DrawPath(Path path)
    {
        if (path == null || path.vectorPath.Count == 0) return;

        for (int i = 0; i < path.vectorPath.Count - 1; i++)
        {
            Debug.DrawLine(path.vectorPath[i], path.vectorPath[i + 1], Color.red, 2f);
        }
    }
}
