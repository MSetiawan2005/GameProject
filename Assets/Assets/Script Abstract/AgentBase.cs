using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentBase
{
    // Start is called before the first frame update
    public abstract void EnterState(AgentManager agen);

    // Update is called once per frame
    public abstract void UpdateState(AgentManager agen); 

}
