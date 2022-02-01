using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler
{

    [HideInInspector] public bool Pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}
