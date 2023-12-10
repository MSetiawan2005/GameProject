using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopetManager : MonoBehaviour
{
    CopetBase currentState;
    public CopetWander copetJalan = new CopetWander();
    public CopetSembunyi copethide = new CopetSembunyi();

    // Start is called before the first frame update
    void Start()
    {
        currentState = copetJalan;
        currentState.EnterState(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
    public void PindahState(CopetBase kondisi)
    {
        currentState = kondisi;
        kondisi.EnterState(this);
    }
}
