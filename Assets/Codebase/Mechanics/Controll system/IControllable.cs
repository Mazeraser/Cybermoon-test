using UnityEngine;

namespace Mechanics.ControllSystem
{
    public interface IControllable
    {
        void ControllMove(Vector2 direction);
    }
}