using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class cariterdekat : MonoBehaviour
{
    GameObject[] obyekmakanan;
    Vector3 targetterdekat;
    public float jarak;
    NavMeshAgent agen;

    public int carimakanan;
    public float JangkauanPencarian;
    public float batasdekat;
    int posisitarget;

    // Start is called before the first frame update
    void Start()
    {
        agen = this.GetComponent<NavMeshAgent>();

        this.agen.speed = 3.0f;

        carimakanan = 1;
        JangkauanPencarian = 10f;
        batasdekat = 1.2f;
        posisitarget = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (carimakanan == 1)
        {
            carimakananterdekat();
        }
        if (carimakanan == 0)
        {
            if (obyekmakanan[posisitarget].gameObject == null)
            {
                posisitarget = -1;
                carimakanan = 1;
                agen.destination=this.transform.position;

            }
        }
        if (posisitarget != -1)
        {
            jarak = Vector3.Distance(this.transform.position, obyekmakanan[posisitarget].transform.position);
            if (jarak <= batasdekat)
            {
                Destroy(obyekmakanan[posisitarget].gameObject);
                carimakanan = 1;
                posisitarget = -1;
            }
        }
    }

    void carimakananterdekat()
    {
        if(carimakanan == 1)
        {
            if (GameObject.FindGameObjectsWithTag("makanan") != null)
            {
                obyekmakanan = GameObject.FindGameObjectsWithTag("makanan");
                targetterdekat = new Vector3(1000, 1000, 1000);
                for (int i = 0; i < obyekmakanan.Length; i++)
                {
                    Vector3 jarakdarimakan = this.transform.position - obyekmakanan[i].transform.position;
                    if (jarakdarimakan.magnitude < targetterdekat.magnitude)
                    {
                        targetterdekat = jarakdarimakan;
                        posisitarget = i;
                        carimakanan = 0;

                        jarak = Vector3.Distance(this.transform.position, obyekmakanan[i].transform.position);

                        if (jarak > JangkauanPencarian)
                        {
                            targetterdekat = new Vector3(1000, 1000, 1000);
                            carimakanan = 1;
                            posisitarget = -1;
                        }
                    }

                }
            }
            if (carimakanan == 0)
            {
                this.agen.destination = obyekmakanan[posisitarget].transform.position;
            }
        }
    }
}
