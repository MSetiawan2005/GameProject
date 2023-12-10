using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.AI;

public class CopetWander : CopetBase
{
    public float Jitter;
    public NavMeshAgent AgenAI;

    private static GameObject target;
    public Vector3 jarakxyzkeTarget;
    public override void EnterState(CopetManager agen) 
    {
        Debug.Log("Copet berjalan start");
        NavMeshAgent comnav = agen.GetComponent<NavMeshAgent>();
        if (comnav == null)
        {
            agen.AddComponent<NavMeshAgent>();
        }
        target = GameObject.FindGameObjectWithTag("satpam");
        AgenAI = agen.GetComponent<NavMeshAgent>();
        AgenAI.speed = 10f;
        AgenAI.angularSpeed = 100f;
        AgenAI.acceleration = 10f;
        Jitter = 0.5f;
        Jalanjalan();
    }
    public override void UpdateState(CopetManager agen)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            agen.PindahState(agen.copethide);
        }
        RaycastHit hit;

        if (Physics.Raycast(AgenAI.transform.position,
            jarakxyzkeTarget, out hit))
        {
            if (hit.collider.gameObject.tag != ("satpam"))
            {
                if (AgenAI.remainingDistance < 2)
                {
                    Jalanjalan();
                }
            }
        }
        else
        {
            if (AgenAI.remainingDistance < 2)
            {
                Jalanjalan();
            }
        }
        if (target != null)
        {
            jarakxyzkeTarget = target.transform.position -
                    AgenAI.transform.position;


            if (Physics.Raycast(AgenAI.transform.position,
                jarakxyzkeTarget, out hit))
            {
                if (hit.collider.gameObject.tag == ("satpam"))
                {

                    Debug.DrawRay(AgenAI.transform.position, jarakxyzkeTarget, Color.green);
                    agen.PindahState(agen.copethide);
                }

            }
        }
    }
    void Jalanjalan()
    {
        Vector3 wandertarget = Vector3.zero;
        float wanderradius = 3f;
        float wanderdistance = 9;

        wandertarget += new Vector3(
            Random.Range(-1.0F, 1.0F) * Jitter,
            0,
            Random.Range(-1.0F, 1.0F) * Jitter
            );

        wandertarget.Normalize();
        wandertarget *= wanderradius;

        Vector3 targetlokal = wandertarget +
            new Vector3(0, 0, wanderdistance);

        Vector3 targetworld =
            AgenAI.transform.InverseTransformVector(targetlokal);
        AgenAI.SetDestination(targetworld);
    }


}
