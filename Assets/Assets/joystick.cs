using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class joystick : MonoBehaviour,
    IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Objekyangdigerakan; //empty root karakter
    public GameObject Objekyangdiputar; // objek karakter
    public float SpeedMax = 5f;
    public float Zaxis;

    private Image Bgimage;
    private Image JoystickImage;
    public Vector2 InputDir;
    public float offset = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        Bgimage = GetComponent<Image>();
        JoystickImage = transform.GetChild(0).GetComponent<Image>();
        InputDir = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;
        float bgimagesizeX = Bgimage.rectTransform.sizeDelta.x;
        float bgImagesizeY = Bgimage.rectTransform.sizeDelta.y;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(Bgimage.rectTransform,eventData.position,eventData.pressEventCamera, out pos))
        {
            pos.x /= bgimagesizeX;
            pos.y /= bgImagesizeY;

            InputDir = new Vector2(pos.x, pos.y);
            InputDir = InputDir.magnitude > 1 ? InputDir.normalized : InputDir;

            JoystickImage.rectTransform.anchoredPosition =
                new Vector2(InputDir.x * (bgimagesizeX / offset),
                            InputDir.y * (bgimagesizeX / offset)
                            );
            Zaxis = Mathf.Atan2(InputDir.x, InputDir.y) * Mathf.Rad2Deg;
            Objekyangdiputar.transform.eulerAngles = new Vector3(0, Zaxis, 0);
        }

    }
    private void LateUpdate()
    {
        Objekyangdigerakan.transform.Translate(
            InputDir.x * SpeedMax * Time.deltaTime, 0,
            InputDir.y * SpeedMax * Time.deltaTime
            );
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        InputDir = Vector2.zero;
        JoystickImage.rectTransform.anchoredPosition = Vector2.zero;
    }
}
