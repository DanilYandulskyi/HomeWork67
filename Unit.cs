using UnityEngine;
using System;

[RequireComponent(typeof(Movement))]
public class Unit : MonoBehaviour
{
    [SerializeField] private Resource _resource;
    [SerializeField] private Vector3 _initialPosition;
    [SerializeField] private Vector2 _moveDirection;

    private Movement _movement;
    private float _distanceMapToStop = 0.2f;

    public event Action<Resource> CollectedResource;

    public bool IsResourceCollected { get; private set; } = false;
    public bool IsStanding { get; private set; } = true;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _initialPosition = transform.position;
    }

    private void Update()
    {
        if (_resource != null)
        {
            _movement.Move(_moveDirection);
            IsStanding = false;
        }

        if (IsResourceCollected & Vector2.SqrMagnitude(transform.position - _initialPosition) < _distanceMapToStop)
        {
            OnCollectedResourse(ref _resource);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Resource resource))
        {
            if (resource == _resource)
            {
                IsResourceCollected = true;
                ChangeMoveDirection();
                resource.StartFollow(transform);
            }
        }
    }

    public void StartMovingToResource(Resource resource)
    {
        _resource = resource;
        _moveDirection = _resource.transform.position - transform.position;
    }

    private void ChangeMoveDirection()
    {
        _moveDirection = _initialPosition - transform.position;
    }

    private void OnCollectedResourse(ref Resource resource)
    {
        CollectedResource?.Invoke(resource);
        _movement.Stop();
        IsResourceCollected = false;
        IsStanding = true;
        resource.StopFollowing();
        resource.Disable();
        resource = null;
    }
}
