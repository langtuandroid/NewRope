using System.Collections.Generic;
using UnityEngine;
using Obi;

public class SnakeController : MonoBehaviour
{

    public float headSpeed = 20;
    public float slitherSpeed = 10;

    private ObiRope rope;
    private ObiSolver solver;
    private float[] traction;
    private Vector3[] surfaceNormal;
  
    public float Range;
    
    public List<Vector3> PathList = new List<Vector3>();
    private int currentPathIndex = 0;

    public Transform SnakeHead;
    private bool isEnd = false;
    public bool EndMovement { get; set; }
    public bool canMove { get; set; } 

    
    private int index =1;

    private void Start()
    {
        canMove = true;
        rope = GetComponent<ObiRope>();
        solver = rope.solver;

        // initialize traction array:
        traction = new float[rope.activeParticleCount];
        surfaceNormal = new Vector3[rope.activeParticleCount];
        //GetComponent<ObiRopeExtrudedRenderer>().thicknessScale = 2f;
        // subscribe to solver events (to update surface information)
        //solver.OnBeginStep += ResetSurfaceInfo;
        // solver.OnCollision += AnalyzeContacts;
        //solver.OnParticleCollision += AnalyzeContacts;

    }


    private void OnDestroy()
    {
       // solver.OnBeginStep -= ResetSurfaceInfo;
       // solver.OnCollision -= AnalyzeContacts;
       // solver.OnParticleCollision -= AnalyzeContacts;
    }

    private void ResetSurfaceInfo(ObiSolver s, float stepTime)
    {
        // reset surface info:
        for (int i = 0; i < traction.Length; ++i)
        {
            traction[i] = 0;
            surfaceNormal[i] = Vector3.zero;
        }
    }

    private void AnalyzeContacts(object sender, ObiSolver.ObiCollisionEventArgs e)
    {
        // iterate trough all contacts:
        for (int i = 0; i < e.contacts.Count; ++i)
        {
            var contact = e.contacts.Data[i];
            if (contact.distance < 0.005f)
            {
                int simplexIndex = solver.simplices[contact.bodyA];
                var particleInActor = solver.particleToActor[simplexIndex];

                if (particleInActor.actor == rope)
                {
                    // using 1 here, could calculate a traction value based on the type of terrain, friction, etc.
                    traction[particleInActor.indexInActor] = 1;

                    // accumulate surface normal:
                    surfaceNormal[particleInActor.indexInActor] += (Vector3)contact.normal;
                }
            }
        }
    }

    public void LateUpdate()
    {
        if (PathList.Count>0)
        {
           
                MoveSnake();

        }

      
//        var targetRotation = Quaternion.LookRotation(TT.transform.position - headReferenceFrame.position);

// Smoothly rotate towards the target point.
        //headReferenceFrame.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 999 * Time.deltaTime);


    }
    
    public void SetPath(List<Vector3> path,bool isEndPath)
    {
        if(!canMove) return;
        canMove = false;
        isEnd = isEndPath;
        PathList = new List<Vector3>();
        for (int i = 0; i < path.Count; i++)
        {
            PathList.Add(path[i]);
            PathList[i] = new Vector3(PathList[i].x, solver.positions[0].y, PathList[i].z);
        }
    }

    #region Example Movement
    /*
     *
     * private void MoveSnake()
       {
           var dire = (TT.transform.position - (Vector3)solver.positions[0]).normalized;
           var c = (TT.transform.position - (Vector3)solver.positions[0]);
           c.y = 0;
           var cc = c.magnitude;
           dire.y = 0;

           Debug.Log(cc);
           if (cc>0.2f)
           {
               solver.velocities[0] = Vector3.Lerp(solver.velocities[0],dire , Time.deltaTime * headSpeed);
               
               for (int i = 1; i < rope.activeParticleCount; ++i)
               {
                   int solverIndex = rope.solverIndices[i];
                   int prevSolverIndex = rope.solverIndices[i - 1];

                   // direction from current particle to previous one, projected on the contact surface:
                   Vector4 dir = Vector3.ProjectOnPlane(solver.positions[prevSolverIndex] - solver.positions[solverIndex], surfaceNormal[i]).normalized;

                   // move in that direction:
                   //solver.velocities[solverIndex] = dir  * slitherSpeed * Time.deltaTime;
                   var a = dir;
                   solver.velocities[solverIndex] =
                       Vector3.Lerp(solver.velocities[solverIndex], dir, slitherSpeed * Time.deltaTime);
               }
           }
       }
     */
    

    #endregion
    
    private void MoveSnake()
    {
        var direction = (PathList[currentPathIndex] - (Vector3)solver.positions[0]).normalized;
        var diffrence = (PathList[currentPathIndex] - (Vector3)solver.positions[0]);
        diffrence.y = 0;
        var cc = diffrence.magnitude;
        direction.y = 0;
        //Debug.Log(currentPathIndex);
        //Debug.Log(cc + " " + currentPathIndex);
        //Debug.Log(direction + " " + diffrence + " " + PathList[currentPathIndex] + " " + solver.positions[0]);
       //Debug.Log(cc);
      
        if (cc>Range)
        {
            solver.velocities[0] = Vector3.Lerp(solver.velocities[0],direction*headSpeed , Time.deltaTime * 30);
          // solver.velocities[0] = direction*headSpeed;

            for (int i = 1; i < rope.activeParticleCount; ++i)
            {
                int solverIndex = rope.solverIndices[i];
                int prevSolverIndex = rope.solverIndices[i - 1];

                // direction from current particle to previous one, projected on the contact surface:
                //Vector4 dir = Vector3.ProjectOnPlane(solver.positions[prevSolverIndex] - solver.positions[solverIndex], surfaceNormal[i]).normalized;
                Vector4 dir = (solver.positions[prevSolverIndex] - solver.positions[solverIndex]).normalized;

                // move in that direction:
                //solver.velocities[solverIndex] = dir  * slitherSpeed ;
                var a = dir;
                solver.velocities[solverIndex] =
                  Vector3.Lerp(solver.velocities[solverIndex], dir * slitherSpeed , 30*Time.deltaTime);
            }
        }
        else if (cc<=Range)
        {
            if(!isEnd)currentPathIndex+=5;
            if (isEnd) currentPathIndex++;
            
            for (int i = 1; i < rope.activeParticleCount; ++i)
            {
                int solverIndex = rope.solverIndices[i];
                int prevSolverIndex = rope.solverIndices[i - 1];

                // direction from current particle to previous one, projected on the contact surface:
                Vector4 dir = Vector3.ProjectOnPlane(solver.positions[prevSolverIndex] - solver.positions[solverIndex], surfaceNormal[i]).normalized;

                // move in that direction:
                solver.velocities[solverIndex] = Vector3.zero;
                var a = dir;
                //   solver.velocities[solverIndex] =
                //     Vector3.MoveTowards(solver.velocities[solverIndex], dir, slitherSpeed);
            }
        }
        
        if (currentPathIndex>=PathList.Count)
        {
            Range = 0.3f;
            PathList.Clear();
            currentPathIndex = 0;
            Debug.Log("Movement DONE!");
            var a = (GetComponent<SnakePlaceHolder>().GetNextHolderEntryTransform().position - transform.position)
                .magnitude;
            //Debug.Log(a);
            //Swap kalkacak.
            canMove = true;

           

            if (EndMovement)
            {
                EndMovement = false;
                
                SetPath(GetComponent<SnakePlaceHolder>().CurrentPlaceHolderObject.GetComponent<PathEntryCalculator>()
                    .GetEntryPath(),true);
                //var diff = (PathList[0] - SnakeHead.transform.position).magnitude;
                //Debug.Log(diff);
                //if(diff<=0.75f) PathList.RemoveAt(0);
                //PathList.Insert(0,SnakeHead.transform.forward*0.1f);
                //GetComponent<SnakePlaceHolder>().SwapHolders();
                FindObjectOfType<LevelController>().SetCompleteCount(index);
                index*=-1;
            };
           
            
        }
    }

      
    }

