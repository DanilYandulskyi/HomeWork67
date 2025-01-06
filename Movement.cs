using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private float _initialSpeed;
    
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _initialSpeed = _speed;
    }

    public void Move(Vector2 direction)
    {
        _speed = _initialSpeed;
        Vector2 offset = direction.normalized * (_speed * Time.deltaTime);
        _rigidbody.MovePosition(_rigidbody.position + offset);
    }

    public void Stop()
    {
        _speed = 0;
    }
}
