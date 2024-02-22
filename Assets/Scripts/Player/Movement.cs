using System;
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
        [SerializeField] private float maxSpeed;
        [SerializeField] private float minSpeed;
        
        private float _currentSpeed;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = player.GetComponentInChildren<Rigidbody2D>();
        }

        private void Move(Direction direction)
        {
            Debug.Log(direction);
            float speed = direction switch
            {
                Direction.Left => - defaultSpeed,
                Direction.Right => defaultSpeed,
                _ => 0f
            };
            
            Vector2 velocity = new Vector2(speed, 0);
            _rigidbody2D.velocity = velocity;
        }

        public void MoveLeft() => Move(Direction.Left);
        public void MoveRight() => Move(Direction.Right);

        private void ChangeSpeed(Speed speed)
        {
            //*крутий код*
        }

        public void Brake() => ChangeSpeed(Speed.Brake);
        public void Gas() => ChangeSpeed(Speed.Gas);
        
    }
}
