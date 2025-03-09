using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private int _leftMouseButton = 0;

    public event Action Clicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            Clicked?.Invoke();
        }
    }
}
