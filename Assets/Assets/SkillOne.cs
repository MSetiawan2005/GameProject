using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SkillOne : MonoBehaviour,
     IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image Bgimage;
    private Image JoystickImage;
    public Vector2 InputDirSkill;

    public GameObject Skillmajumundur;
    public GameObject Skillrangeobject;

    public float offset = 2f;
    public float minimumoffset = 0.03f;
    public float offsetskill = 5f;

    public float magnitudo;
    public float Zaxisskill;
    static int aktif;

    public GameObject objprefab;
    // Start is called before the first frame update
    void Start()
    {
        Bgimage = GetComponent<Image>();
        JoystickImage = transform.GetChild(0).GetComponent<Image>();
        InputDirSkill = Vector2.zero;
        Skillrangeobject.GetComponent<Renderer>().enabled = false;
        Skillmajumundur.GetComponent<Renderer>().enabled = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Skillmajumundur.GetComponent<Renderer>().enabled = true;

        Vector2 posskill1 = Vector2.zero;
        float bgimagesizeX = Bgimage.rectTransform.sizeDelta.x;
        float bgImagesizeY = Bgimage.rectTransform.sizeDelta.y;


        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Bgimage.rectTransform, eventData.position, eventData.pressEventCamera, out posskill1))
        {
            posskill1.x /= bgimagesizeX;
            posskill1.y /= bgImagesizeY;

            InputDirSkill = new Vector2(posskill1.x, posskill1.y);
            InputDirSkill = InputDirSkill.magnitude > 1 ? InputDirSkill.normalized : InputDirSkill;

            JoystickImage.rectTransform.anchoredPosition =
           new Vector2(InputDirSkill.x * (bgimagesizeX / offset),
                       InputDirSkill.y * (bgimagesizeX / offset)
                       );
            Zaxisskill = Mathf.Atan2(InputDirSkill.x, InputDirSkill.y) * Mathf.Rad2Deg;
            Skillrangeobject.transform.eulerAngles = new Vector3(0, Zaxisskill, 0);
            magnitudo = Vector2.SqrMagnitude(InputDirSkill);
            if(magnitudo > minimumoffset)
            {
                Skillmajumundur.transform.localPosition = new Vector3(0, 0, offsetskill * magnitudo);
                aktif = 1;
            }
            else
            {
                aktif = 0;
            }
        }

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Skillrangeobject.GetComponent<Renderer>().enabled = true;
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Skillrangeobject.GetComponent<Renderer>().enabled = false;
        Skillmajumundur.GetComponent<Renderer>().enabled = false;
        InputDirSkill = Vector2.zero;
        JoystickImage.rectTransform.anchoredPosition = Vector2.zero;
        if (aktif==1)
        {
            skillB();
        }
    }

    void skill1()
    {
        aktif = 0;
        Vector3 posisiskill;
        posisiskill = Skillmajumundur.transform.position;
        GameObject molina = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        molina.transform.position = posisiskill;
        Color warnaacak = new Color(
            Random.Range(0f,1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
            );
        molina.GetComponent<Renderer>().material.color = warnaacak;
    }

    void skillB()
    {
        aktif = 0;
        Vector3 posisiskill;
        posisiskill = Skillmajumundur.transform.position;

        GameObject molina = (GameObject)Instantiate(objprefab, posisiskill, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
