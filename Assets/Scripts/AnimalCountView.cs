using UnityEngine;

public class AnimalCountView : MonoBehaviour
{
    [SerializeField] private AnimalCounter _animalCounter;

    private void OnEnable()
    {
        _animalCounter.Increased += Show;
    }

    private void OnDisable()
    {
        _animalCounter.Increased -= Show;
    }

    private void Show()
    {
        Debug.Log("Количество спасенных собак: " + _animalCounter.AnimalCount);
    }
}
