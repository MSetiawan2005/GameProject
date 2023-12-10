using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimManager : MonoBehaviour
{
    public int[] arrayttt = new int[10];
    //jadinya 0-9 yang dipakai hanya 1 sampai 9 , 0 nya abaikan

    public int pemain;
    


    // Start is called before the first frame update
    void Start()
    {
        pemain = 1;    
    }

    public void tukarpemain() 
    {
        if(pemain == 1) { pemain = 2; } else { pemain = 1; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
