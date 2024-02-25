using System.Collections;
using Config.Level;
using Config.Player;
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
        [SerializeField] private RoadConfig roadConfig;
        [SerializeField] private PlayerConfig playerConfig;
        
        private float _currentSpeed;
        private bool _isChangingSpeed;
        private float _horizontalSpeed;
   
        private void Start()
        {
            player.GetComponent<Rigidbody2D>();
            _currentSpeed = playerConfig.DefaultSpeed;
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
            float leftEdge = -roadConfig.RoadRange;
            float rightEdge = roadConfig.RoadRange;
            
            Vector3 position = player.transform.localPosition;
            position.x = Mathf.Clamp(position.x + _horizontalSpeed * Time.deltaTime, leftEdge, rightEdge);
            
            player.transform.localPosition = new Vector3(position.x, position.y, 0);
        }

        private void ReceivedDirection(Direction direction)
        {
            _horizontalSpeed = direction switch
            {
                Direction.Left => -playerConfig.TurnSpeed,
                Direction.Right => playerConfig.TurnSpeed,
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
                        _currentSpeed -= playerConfig.StepSpeed;
                        yield return new WaitForSeconds(0.1f);
                        break;
                    }
                    case Speed.Gas:
                    {
                        _currentSpeed += playerConfig.StepSpeed;
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
            bool isBreak = (_currentSpeed <= playerConfig.MinSpeed && speed == Speed.Brake) || 
                           (_currentSpeed >= playerConfig.MaxSpeed && speed == Speed.Gas);
            if (isBreak)
            {
                _isChangingSpeed = false;
                _currentSpeed = speed == Speed.Brake ? playerConfig.MinSpeed : playerConfig.MaxSpeed;
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