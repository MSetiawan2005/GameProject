using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;

public class AgentKejar : AgentBase
{
    public NavMeshAgent AgenAI;
    private static GameObject target;//maling
    public Vector3 jarakxyzkeTarget;


    // Start is called before the first frame update
    public override void EnterState(AgentManager agen)
    {
        Debug.Log("agen berlari start");    
        NavMeshAgent comnav = agen.GetComponent<NavMeshAgent>();
        if(comnav == null)
        {
            agen.AddComponent<NavMeshAgent>();
        }
        target = GameObject.FindGameObjectWithTag("copet");
        AgenAI = agen.GetComponent<NavMeshAgent>();
        this.AgenAI.speed = 2f;
        this.AgenAI.angularSpeed = 100f;
        this.AgenAI.acceleration = 5f;


        //    Rigidbody rb = agen.GetComponent<Rigidbody>();
        // if (rb == null)
        //  {
        //        agen.gameObject.AddComponent<Rigidbody>();
        //      }
    }

    // Update is called once per frame
    public override void UpdateState(AgentManager agen)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            agen.PindahState(agen.cubeberjalan);
        }
        if (target != null)
        {
            this.AgenAI.SetDestination(target.transform.position);
            jarakxyzkeTarget = target.transform.position - AgenAI.transform.position;

            RaycastHit hit;
            if (Physics.Raycast(AgenAI.transform.position,
                jarakxyzkeTarget, out hit))
            {
                Debug.DrawRay(AgenAI.transform.position, jarakxyzkeTarget, Color.green);
            }
            if (hit.collider.gameObject.tag != ("copet"))
            {
                agen.PindahState(agen.cubeberjalan);
            }

            if (AgenAI.remainingDistance < 3)
            {
                AgenAI.speed = 0;
            }
            else
            {
                this.AgenAI.speed = 3.5f;
            }
        }
        else
        {
            agen.PindahState(agen.cubeberjalan);
        }

    }

}
