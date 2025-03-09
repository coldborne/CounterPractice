using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class AnimalCounter : MonoBehaviour
{
    public event Action Increased;

    [SerializeField] private InputSystem _inputSystem;
    
    [SerializeField] private float _delay;

    private bool _isCounting;
    private int _animalCount;
    private Coroutine _animalCounterCoroutine;

    public int AnimalCount => _animalCount;

    private void OnEnable()
    {
        _inputSystem.Clicked += ToggleCounter;
    }

    private void OnDisable()
    {
        _inputSystem.Clicked -= ToggleCounter;
    }

    private void ToggleCounter()
    {
        if (_isCounting)
        {
            StopCoroutine(_animalCounterCoroutine);
        }
        else
        {
            _animalCounterCoroutine = StartCoroutine(IncreaseAnimalCount());
        }

        _isCounting = !_isCounting;
    }

    private IEnumerator IncreaseAnimalCount()
    {
        while (_animalCount < int.MaxValue)
        {
            yield return new WaitForSeconds(_delay);

            _animalCount++;
            Increased?.Invoke();
        }
    }
}
