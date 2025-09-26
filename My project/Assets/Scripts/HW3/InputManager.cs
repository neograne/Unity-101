using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public bool throwAction;
    public bool rebindAction;
    public UnityEvent throwEvent = new();
    public UnityEvent rebindEvent = new();

    public void OnThrow(InputValue value)
    {
        throwAction = value.isPressed;
        throwEvent?.Invoke();
    }

    public void OnRebind(InputValue value)
    {
        rebindAction = value.isPressed;
        rebindEvent?.Invoke();
    }
}
