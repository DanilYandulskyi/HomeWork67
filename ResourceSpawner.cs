using UnityEngine;
using System.Collections;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource[] _resource;
    [SerializeField] private Map _map;
    [SerializeField] private float _interval;

    private void Awake()
    {
        StartCoroutine(SpawnResource());
    }

    private IEnumerator SpawnResource()
    {
        while (enabled)
        {
            Spawn();
            yield return new WaitForSeconds(_interval);
        }
    }

    private void Spawn()
    {
        int minValue = -5;
        int maxValue = 5;

        Resource resource = Instantiate(_resource[Random.Range(0, _resource.Length)],
            new Vector2(Random.Range(minValue, maxValue), Random.Range(minValue, maxValue)), Quaternion.identity);

        _map.AddResource(resource);
    }
}
