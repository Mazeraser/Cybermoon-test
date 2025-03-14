using UnityEngine;

namespace Mechanics.MoveSystem
{
    public class Jump : MovableParent, IMovable
    {
        private float timer;

        [SerializeField]
        private float _jumpPower;

        [SerializeField]
        private bool _ignoreGrounding;

        private bool _isGrounded;
        public bool IsGrounded
        {
            get { return _isGrounded; }
        }

        public float Speed
        {
            get { return _jumpPower; }
        }

        [SerializeField]
        private float _standartGravityScale;
        [SerializeField]
        private float _fallingGravityScale;

        private void Update()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb.velocity.y != 0)
                rb.gravityScale = _fallingGravityScale;
            else
                rb.gravityScale = _standartGravityScale;
        }

        public void Turn(Vector2 direction)
        {
            if (_isGrounded||_ignoreGrounding)
                Move(Vector2.up, _jumpPower);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _isGrounded = collision.gameObject.tag == "Ground";
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            _isGrounded = collision.gameObject.tag != "Ground";
        }
    }
}