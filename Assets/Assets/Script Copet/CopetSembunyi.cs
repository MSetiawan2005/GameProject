using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;

public class CopetSembunyi : CopetBase
{
   
    public NavMeshAgent AgenAI;

    private static GameObject target;
    public Vector3 jarakxyzkeTarget;

    public float SudutkeTarget;
    public float jarakVektorkeTarget;

    public float VisAngle;
    public float VisDistance;
    // Start is called before the first frame update
    public override void EnterState(CopetManager agen)
    {
        Debug.Log("Copet sembunyi start start");
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

    }
    public override void UpdateState(CopetManager agen)
    {
        float dist = Mathf.Infinity;
        Vector3 chosenpot = Vector3.zero;

        for(int i=0; i< World.Instance.GetHidingPos().Length; i++)
        {
            Vector3 hideDir = World.Instance.GetHidingPos()[i].transform.position - target.transform.position;
            Vector3 hidepos = World.Instance.GetHidingPos()[i].transform.position + hideDir.normalized * 3;

            if (Vector3.Distance(agen.transform.position, hidepos) < dist)
            {
                chosenpot = hidepos;
                dist = Vector3.Distance(agen.transform.position, hidepos);
            }
        }
        AgenAI.SetDestination(chosenpot);

        jarakxyzkeTarget = target.transform.position - agen.transform.position;
        SudutkeTarget = Vector3.Angle(jarakxyzkeTarget, agen.transform.forward);
        jarakVektorkeTarget = jarakxyzkeTarget.magnitude;

        if(jarakVektorkeTarget<VisDistance && SudutkeTarget < VisAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(agen.transform.position, jarakxyzkeTarget, out hit)) 
            {
                if (hit.collider.gameObject.tag == "satpam")
                {
                    Debug.DrawRay(agen.transform.position, jarakxyzkeTarget, Color.yellow);
                }
                else
                {
                    agen.PindahState(agen.copethide);
                }
            }
        }
        else
        {
            agen.PindahState(agen.copethide);
        }
    }
}
