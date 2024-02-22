using System.Collections;
using UnityEngine;

namespace Player
{
    public enum Direction
    {
       Left,
       Right
    }

    public enum Speed
    {
        Brake,
        Gas
    }
    public class Movement : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private float defaultSpeed;
        [SerializeField] private float turnSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float minSpeed;
        [SerializeField] private float stepSpeed;
        
        private float _currentSpeed;
        private Rigidbody2D _rigidbody2D;
        private bool _isChangingSpeed;
        
        private void Awake()
        {
            _rigidbody2D = player.GetComponentInChildren<Rigidbody2D>();
            _currentSpeed = defaultSpeed;
            _isChangingSpeed = false;
        }

        private void FixedUpdate()
        {
            Vector2 forwardVelocity = new Vector2(_rigidbody2D.velocity.x, _currentSpeed);
            _rigidbody2D.velocity = forwardVelocity;
        }

        private void Move(Direction direction)
        {
            float speed = direction switch
            {
                Direction.Left => - turnSpeed,
                Direction.Right => turnSpeed,
                _ => 0f
            };
            
            Vector2 velocity = new Vector2(speed, _rigidbody2D.velocity.y);
            _rigidbody2D.velocity = velocity;
        }

        public void MoveLeft() => Move(Direction.Left);
        public void MoveRight() => Move(Direction.Right);
        public void StopMove() => _rigidbody2D.velocity = Vector2.zero;
        
        private IEnumerator ChangeSpeedCoroutine(Speed speed)
        {
            _isChangingSpeed = true;
            
            while (_isChangingSpeed)
            {
                if (AdjustSpeed(speed)) 
                    break;
                
                switch (speed)
                {
                    case Speed.Brake:
                    {
                        _currentSpeed -= stepSpeed;
                        yield return new WaitForSeconds(0.1f);
                        break;
                    }
                    case Speed.Gas:
                    {
                        _currentSpeed += stepSpeed;
                        yield return new WaitForSeconds(0.1f);
                        break;
                    }
                    default:
                        _currentSpeed = 0f;
                        break;
                }
            }
        }
        
        private bool AdjustSpeed(Speed speed)
        {
            bool isBreak = (_currentSpeed <= minSpeed && speed == Speed.Brake) || 
                           (_currentSpeed >= maxSpeed && speed == Speed.Gas);
            if (isBreak)
            {
                _isChangingSpeed = false;
                _currentSpeed = speed == Speed.Brake ? minSpeed : maxSpeed;
            }
            return isBreak;
        }
        
        public void Brake() => StartCoroutine(ChangeSpeedCoroutine(Speed.Brake));

        public void Gas() => StartCoroutine(ChangeSpeedCoroutine(Speed.Gas));
        
        public void StopChangeSpeed()
        {
            _isChangingSpeed = false;
            StopCoroutine(nameof(ChangeSpeedCoroutine));
        }
    }
}