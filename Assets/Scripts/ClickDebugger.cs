using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClickDebugger : MonoBehaviour
{
    //void OnEnable()
    //{
    //    InputManager.Instance.UI.Click.performed += ctx => DebugClick();
    //}

    //void OnDisable()
    //{
    //    InputManager.Instance.UI.Click.performed -= ctx => DebugClick();
    //}

    //void DebugClick()
    //{
    //    PointerEventData pointerData = new PointerEventData(EventSystem.current);
    //    pointerData.position = Mouse.current.position.ReadValue();

    //    List<RaycastResult> results = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(pointerData, results);

    //    Debug.Log($"=== CLICK at {pointerData.position} ===");
    //    if (results.Count == 0)
    //    {
    //        Debug.Log("NOTHING HIT");
    //    }
    //    else
    //    {
    //        foreach (RaycastResult hit in results)
    //        {
    //            Debug.Log($"HIT: {hit.gameObject.name} | Distance: {hit.distance}");
    //        }
    //    }
    //}
}
