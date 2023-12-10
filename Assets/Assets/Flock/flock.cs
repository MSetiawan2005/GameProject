using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour
{
    public flockmanager myManager;
    public float speed;
    bool belok = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        Bounds b = new Bounds(myManager.transform.position, myManager.swimlimits * 2);
        RaycastHit hit = new RaycastHit();
        Vector3 arah = Vector3.zero;

        if (!b.Contains(transform.position))
        {
            belok = true;
            arah = myManager.transform.position - transform.position;
        }
        else

        if (Physics.Raycast(transform.position,
            this.transform.forward * myManager.deteksitabrak, out hit))
        {
            belok = true;
            Debug.DrawRay(this.transform.position, this.transform.forward, Color.red);
            arah = Vector3.Reflect(this.transform.forward, hit.normal);
            Debug.DrawRay(hit.point, arah * 50, Color.blue);
        }


        else { belok = false; }

        if (belok)
        {
            // Vector3 arah = myManager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(arah),
               myManager.rotationSpeed * Time.deltaTime);
        }

        if (Random.Range(0, 100) < 10)
        {
            speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
        }

        if (Random.Range(0, 100) < 10)
        {
            ApplyRules();
        }

        transform.Translate(0, 0,
            Time.deltaTime * speed);
    }



    void ApplyRules()
    {
        GameObject[] gerombol;
        gerombol = myManager.allfish;

        Vector3 ratacentre = Vector3.zero;
        Vector3 ratavoidance = Vector3.zero;
        float Grupspeed = 0.01f;
        float neighbourdistance;
        int groupsize = 0;

        foreach (GameObject go in gerombol)
        {
            if (go != this.gameObject)
            {
                neighbourdistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (neighbourdistance <= myManager.neighbourDistance)
                {
                    ratacentre += go.transform.position;
                    groupsize++;
                    if (neighbourdistance < 1.0f) // jika terlalu dekat dengan ikan lain
                    {

                        ratavoidance = ratavoidance + (this.transform.position - go.transform.position);
                    }
                    flock anotherFlock = go.GetComponent<flock>();
                    Grupspeed = Grupspeed + anotherFlock.speed;
                }
            }
        }
        if (groupsize > 0)
        {
            ratacentre = ratacentre / groupsize + (myManager.goalpos - this.transform.position);
            Grupspeed = Grupspeed / groupsize;

            Vector3 direction = (ratacentre + ratavoidance) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }
    }
}