using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalSpawner : MonoBehaviour
{
    public event Action Spawned;

    [SerializeField] private Canvas _screen;
    [SerializeField] private AnimalCounter _animalCounter;
    [SerializeField] private List<Image> _animals;

    [SerializeField] private Image _animalZone;

    private Image[] _preDisplayedImages;

    private void Awake()
    {
        _preDisplayedImages = _screen.GetComponentsInChildren<Image>();
    }

    private void OnEnable()
    {
        _animalCounter.Increased += Spawn;
    }

    private void OnDisable()
    {
        _animalCounter.Increased -= Spawn;
    }

    [ContextMenu("Spawn")]
    private void Spawn()
    {
        ClearAnimalZone();
        int animalIndex = UnityEngine.Random.Range(0, _animals.Count);
        Image newAnimal = Instantiate(_animals[animalIndex], _animalZone.transform);

        RectTransform newAnimalRectTransform = newAnimal.rectTransform;
        RectTransform animalZoneRectTransform = _animalZone.GetComponent<RectTransform>();

        Vector2 animalZonePivot = animalZoneRectTransform.pivot;

        Vector2 position = GetFreePosition(newAnimal);

        float xShift = animalZoneRectTransform.rect.width * animalZonePivot.x;
        float yShift = animalZoneRectTransform.rect.height * animalZonePivot.y;

        Vector2 shift = new Vector2(-xShift, -yShift);
        position += shift;

        newAnimalRectTransform.anchoredPosition = position;

        Spawned?.Invoke();
    }

    private void ClearAnimalZone()
    {
        for (int i = _animalZone.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(_animalZone.transform.GetChild(i).gameObject);
        }
    }

    private Vector2 GetFreePosition(Image animal)
    {
        RectTransform animalRectTransform = animal.GetComponent<RectTransform>();
        RectTransform animalZoneRectTransform = _animalZone.GetComponent<RectTransform>();

        float animalZoneWidth = animalZoneRectTransform.rect.width;
        float animalZoneHeight = animalZoneRectTransform.rect.height;

        float minXSpawnCoordinate = animalRectTransform.rect.width;
        float maxXSpawnCoordinate = animalZoneWidth - animalRectTransform.rect.width;

        float minYSpawnCoordinate = animalRectTransform.rect.height;
        float maxYSpawnCoordinate = animalZoneHeight - animalRectTransform.rect.height;

        float xSpawnCoordinate = UnityEngine.Random.Range(minXSpawnCoordinate, maxXSpawnCoordinate);
        float ySpawnCoordinate = UnityEngine.Random.Range(minYSpawnCoordinate, maxYSpawnCoordinate);

        return new Vector2(xSpawnCoordinate, ySpawnCoordinate);
    }
}
