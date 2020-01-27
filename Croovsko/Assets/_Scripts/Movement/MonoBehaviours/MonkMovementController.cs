using _Scripts.State;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Movement.MonoBehaviours
{
    public class MonkMovementController : MonoBehaviour
    {

        [SerializeField] private int _health;
        [SerializeField] private float _firerate;
    
        [SerializeField] private Transform[] _patrolPoints;
        private Transform _currentPatrolPoint;

        private BulletSpawner _bulletSpawner;
        private PlayerDetector _playerDetector;

        private Animator _animator;

        private float _timer;
        void Start()
        {
            _playerDetector = GetComponentInChildren<PlayerDetector>();
            _bulletSpawner = GetComponentInChildren<BulletSpawner>();
            
            if(_patrolPoints.Length > 0)
                _currentPatrolPoint = _patrolPoints[Random.Range(0, _patrolPoints.Length)];
            
            _animator = GetComponent<Animator>();
            _timer = 1 / _firerate;
        }
    
        void Update()
        {
            if (_playerDetector.playerInRange)
            {
                if (_timer > 0)
                {
                    _timer -= Time.deltaTime;
                }
                else
                {
                    _timer = 100;
                    _animator.SetTrigger("Shoot");
                }
            } else
            {
                _timer = 1 / _firerate;
                Patrol();
            }
        
            if(_health <= 0)
                Destroy(gameObject);
        }

        private void Shoot()
        {
            _bulletSpawner.Shoot(-(transform.position - _playerDetector.playerPosition));
            _timer = 1 / _firerate;
        }

        private void Patrol()
        {
            if (_currentPatrolPoint == null) return;
            
            if (Vector2.Distance(transform.position, _currentPatrolPoint.position) > 0.1f)
                transform.position = Vector2.Lerp(transform.position, _currentPatrolPoint.position, Time.deltaTime);
            else
                _currentPatrolPoint = _patrolPoints[Random.Range(0, _patrolPoints.Length)];
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
           if(other.gameObject.layer == 12) 
               _health--;
        }
    }
}
