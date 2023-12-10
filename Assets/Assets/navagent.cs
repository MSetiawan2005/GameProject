using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navagent : MonoBehaviour
{
    GameObject obyekmakanan;
    Vector3 targetbaru;
    public float jarak;
    NavMeshAgent agen;
    // Start is called before the first frame update
    void Start()
    {
        agen = this.GetComponent<NavMeshAgent>();
        
        this.agen.speed = 3.0f;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (obyekmakanan == null)
        {
            obyekmakanan = GameObject.FindGameObjectWithTag("makanan");
        }
        if (obyekmakanan != null)
        {
            targetbaru = new Vector3(
               obyekmakanan.transform.position.x,
               obyekmakanan.transform.position.y,
               obyekmakanan.transform.position.z
               );
            agen.SetDestination(targetbaru);
            jarak = Vector3.Distance(this.transform.position, obyekmakanan.transform.position);
            
            if (jarak < 2)
            {
                Destroy(obyekmakanan.gameObject);
            }
        }
    }
}
