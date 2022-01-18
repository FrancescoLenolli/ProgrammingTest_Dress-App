using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Add this component to a Button if you want to call a method continuously
/// while the Button is being held down.
/// </summary>
public class ButtonHeld : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent onButtonHeld = null;
    private bool canInvokeEvent = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        canInvokeEvent = true;
        StartCoroutine(ButtonHeldRoutine());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canInvokeEvent = false;
    }

    public IEnumerator ButtonHeldRoutine()
    {
        while (canInvokeEvent)
        {
            onButtonHeld?.Invoke();
            yield return null;
        }

        yield return null;
    }
}
