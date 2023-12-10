using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeKlik : MonoBehaviour
{
    public Vector3 targetputarA; //xyz
    public Vector3 targetputarB;
    
    public Quaternion LerpTargetA;
    public Quaternion LerpTargetB;
    public bool prosesLerp;
    public int sudahdiklik = 0;
    public int nomorkotak;
    public GameObject gamemanager;
    public GameObject camera;
    public int playercam = 1;

    // Start is called before the first frame update
    void Start()
    {
        LerpTargetA = Quaternion.Euler(targetputarA.x, targetputarA.y, targetputarA.z);
        LerpTargetB = Quaternion.Euler(targetputarB.x, targetputarB.y, targetputarB.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(prosesLerp)
        {
            if (gamemanager.GetComponent<GimManager>().pemain == 1)
            {
                if (this.transform.rotation == LerpTargetA)
                {
                    prosesLerp = false;
                    sudahdiklik = 1;
                    gamemanager.GetComponent<GimManager>().tukarpemain();
                }
                else
                {
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, LerpTargetA, 9.8f * Time.deltaTime);
                }
            }
            else 
                if (gamemanager.GetComponent<GimManager>().pemain == 2) 
                {
                if (this.transform.rotation == LerpTargetB)
                {
                    prosesLerp = false;
                    sudahdiklik = 1;
                    gamemanager.GetComponent<GimManager>().tukarpemain();
                }
                else
                {
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, LerpTargetB, 9.8f * Time.deltaTime);
                }

            }
           
        }
    }
 
    private void OnMouseDown()
    {
        Camera camera;
        Camera[] allCameras = Object.FindObjectsOfType(typeof(Camera)) as Camera[];
        foreach (Camera kamera in allCameras)
        {
            Vector3 point = kamera.ScreenToViewportPoint(Input.mousePosition);
            if (point.x >= 0 && point.x <= 1 && point.y >=0 && point.y <=1)
            {
                if (kamera.tag == "MainCamera") { playercam = 1; }
                if (kamera.tag == "Player") { playercam = 2; }
            }
        }
        if (playercam == 1 && gamemanager.GetComponent<GimManager>().pemain == 1)
        {
            if (prosesLerp == false && sudahdiklik == 0)
            {
                Debug.Log("cude di klik");
                prosesLerp = true;
                gamemanager.GetComponent<GimManager>().arrayttt[nomorkotak] = gamemanager.GetComponent<GimManager>().pemain;
            } 
        }
        if (playercam == 2 && gamemanager.GetComponent<GimManager>().pemain == 2)
        {
            if (prosesLerp == false && sudahdiklik == 0)
            {
                Debug.Log("cude di klik");
                prosesLerp = true;
                gamemanager.GetComponent<GimManager>().arrayttt[nomorkotak] = gamemanager.GetComponent<GimManager>().pemain;
            }
        }


    }
}
