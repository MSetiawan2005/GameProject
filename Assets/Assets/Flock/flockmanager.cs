using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flockmanager : MonoBehaviour
{
    public GameObject fishprefab;
    public int numfish = 20;
    
    public Vector3 swimlimits = new Vector3(50, 50, 50);

    [Header("setting ikan")]
    [Range(0.0f, 300.0f)]
    public float minSpeed;
    [Range(0.0f, 300.0f)]
    public float maxSpeed;

    [Range(0.0f, 100.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;

    [Range(1.0f, 20.0f)]
    public float jarakdenganteman;

    [Range(0.1f, 2.0f)]
    public float deteksitabrak;

    public Vector3 goalpos;
    public GameObject[] allfish;

    // Start is called before the first frame update
    void Start()
    {
        allfish = new GameObject[numfish];
        for (int i = 0; i < numfish; i++)
        {
            Vector3 pos = this.transform.position +

                new Vector3(
                    Random.Range(-swimlimits.x, swimlimits.x),  // X
                    Random.Range(-swimlimits.y, swimlimits.y),  // Y
                    Random.Range(-swimlimits.z, swimlimits.z)   // Z
                    );
            allfish[i] =
             (GameObject)Instantiate(fishprefab, pos, Quaternion.identity);

            allfish[i].GetComponent<flock>().myManager = this;
        }

        goalpos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) < 10)
            goalpos = this.transform.position + new Vector3(Random.Range(-swimlimits.x, swimlimits.x),
                                                                Random.Range(-swimlimits.y, swimlimits.y),
                                                                Random.Range(-swimlimits.z, swimlimits.z));
    }
}