using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    AgentBase currentState;

    public AgentWander cubeberjalan = new AgentWander();
    public AgentKejar cubeberlari= new AgentKejar();

    // Start is called before the first frame update
    void Start()
    {
        currentState = cubeberjalan;
        currentState.EnterState(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
    public void PindahState(AgentBase StateTujuan)
    {
        currentState = StateTujuan;
        StateTujuan.EnterState(this);
    }
}
