using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public event Action Clicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Clicked?.Invoke();
        }
    }
}
