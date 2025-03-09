using System;
using System.Collections;
using UnityEngine;

public class AnimalCounter : MonoBehaviour
{
    [SerializeField] private InputSystem _inputSystem;

    [SerializeField] private float _delay;

    private bool _isCounting;
    private int _animalCount;
    private Coroutine _animalCounterCoroutine;
    private WaitForSeconds _waitForSeconds;

    public event Action Increased;

    public int AnimalCount => _animalCount;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_delay);
    }

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
            yield return _waitForSeconds;

            _animalCount++;
            Increased?.Invoke();
        }
    }
}
