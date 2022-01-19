using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Add this component to a Button if you want to call a method continuously
/// while the Button is being held down.
/// </summary>
public class ButtonHeld : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent onButtonHeld = null;

    private bool canInvokeEvent = false;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

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
        while (canInvokeEvent && button.IsInteractable())
        {
            onButtonHeld?.Invoke();
            yield return null;
        }

        yield return null;
    }
}
