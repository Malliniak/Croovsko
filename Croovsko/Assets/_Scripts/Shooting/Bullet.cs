using DefaultNamespace;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 _direction;
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    public void AddForce(Vector3 force)
    {
        transform.LookAt2d(force);
        _rigidbody.AddForce(force * _speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInputResponder player = other.GetComponent<IInputResponder>();
        if (player != null)
            return;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        Destroy(gameObject, 0.4f);
    }
}