using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    Vector2 joystickPosition = Vector2.zero;

    void Start()
    {
        //小心坑：1.这个handle的位置要根据预制体的实际坐标来调整。
        joystickPosition =new Vector2(200,200); //RectTransformUtility.WorldToScreenPoint(Main.UiCamera, background.position);
    }

    public override void OnDrag(PointerEventData eventData)
    {
//        Debug.LogError("eventData.position"+eventData.position);
//        Debug.LogError("joystickPosition"+joystickPosition);
        Vector2 direction = eventData.position - joystickPosition;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        ClampJoystick();
//        Debug.LogError("inputVector"+inputVector);
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}