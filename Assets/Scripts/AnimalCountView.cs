using TMPro;
using UnityEngine;

public class AnimalCountView : MonoBehaviour
{
    [SerializeField] private AnimalCounter _animalCounter;
    [SerializeField] private TextMeshProUGUI _counterText;

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
        int animalCount = _animalCounter.AnimalCount;

        if (_counterText != null)
            _counterText.text = $"Количество спасенных животных: {animalCount}";
    }
}
