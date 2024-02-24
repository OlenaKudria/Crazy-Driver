using System.Collections;
using Config.Level;
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
        [SerializeField] private RoadConfig road;
        
        private float _currentSpeed;
        private bool _isChangingSpeed;
        private float _horizontalSpeed;
   
        private void Awake()
        {
            player.GetComponentInChildren<Rigidbody2D>();
            _currentSpeed = defaultSpeed;
            _isChangingSpeed = false;
            _horizontalSpeed = 0;
        }

        private void Update()
        { 
            MoveForward();
            UpdatePosition();
        }

        private void MoveForward()
        {
           Vector3 forward = new Vector3(0, _currentSpeed, 0);
           player.transform.localPosition += forward * Time.deltaTime;
        }

        private void UpdatePosition()
        {
            float leftEdge = -road.roadRange;
            float rightEdge = road.roadRange;
            
            Vector3 position = player.transform.localPosition;
            position.x = Mathf.Clamp(position.x + _horizontalSpeed * Time.deltaTime, leftEdge, rightEdge);
            
            player.transform.localPosition = new Vector3(position.x, position.y, 0);
        }

        private void ReceivedDirection(Direction direction)
        {
            _horizontalSpeed = direction switch
            {
                Direction.Left => -turnSpeed,
                Direction.Right => turnSpeed,
                _ => 0f
            };
        }

        public void MovedLeft() => ReceivedDirection(Direction.Left);
        
        public void MovedRight() => ReceivedDirection(Direction.Right);
        
        public void StopMove() => _horizontalSpeed = 0f;
        
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