using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public enum JoystickDirection
    {
        Horizontal,
        Vertical,
        Both
    };

    public RectTransform background;
    public RectTransform handle;
    [Range(0, 2.0f)] public float handleLimit = 1.0f;
    private Vector2 input = new Vector2(0, 0);

    public JoystickDirection direction = JoystickDirection.Both;

    public float Vertical
    {
        get { return input.y; }
    }

    public float Horizontal
    {
        get { return input.x; }
    }

    Vector2 joystickPosition = new Vector2(0, 0);

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joyDirection = eventData.position -
                               RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, background.position);

        input = joyDirection.magnitude > background.sizeDelta.x / 2.0f
            ? joyDirection.normalized
            : joyDirection / background.sizeDelta.x / 2.0f;

        if (direction == JoystickDirection.Horizontal)
            input = new Vector2(input.x, 0);
        if (direction == JoystickDirection.Vertical)
            input = new Vector2(0, input.y);

        handle.anchoredPosition = input * background.sizeDelta.x / 2.0f * handleLimit;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
    }
}

