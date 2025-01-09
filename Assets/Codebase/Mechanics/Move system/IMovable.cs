using UnityEngine;

namespace Mechanics.MoveSystem
{
    public interface IMovable
    {
        float Speed { get; }
        void Turn(Vector2 direction);
    }
}