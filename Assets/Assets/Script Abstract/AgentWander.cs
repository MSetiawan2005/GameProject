using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AgentWander : AgentBase
{
    public float Jitter;
    public NavMeshAgent AgenAI;
    private static GameObject target;
    public Vector3 jarakxyzkeTarget;

    public override void EnterState(AgentManager agen)
    {
        Debug.Log("agen berjalan start");
        NavMeshAgent comnav = agen.GetComponent<NavMeshAgent>();
        if (comnav == null)
        {
            agen.AddComponent<NavMeshAgent>();
        }
        target = GameObject.FindGameObjectWithTag("copet");
        AgenAI = agen.GetComponent<NavMeshAgent>();
        AgenAI.speed = 2f;
        AgenAI.angularSpeed = 100f;
        AgenAI.acceleration = 5f;
        Jitter = 0.5f;
        Jalanjalan();
    }

    // Update is called once per frame
    public override void UpdateState(AgentManager agen)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            agen.PindahState(agen.cubeberlari);
        }
        RaycastHit hit;

        if (Physics.Raycast(AgenAI.transform.position,
            jarakxyzkeTarget, out hit))
        {
            if (hit.collider.gameObject.tag != ("copet"))
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
                    if (hit.collider.gameObject.tag == ("copet"))
                    {

                        Debug.DrawRay(AgenAI.transform.position, jarakxyzkeTarget, Color.green);
                        agen.PindahState(agen.cubeberlari);
                    }
                
                }
            }
        }
    void Jalanjalan()
    {
        Vector3 wandertarget = Vector3.zero;
        float wanderradius = 6f;
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
