using System;
using UnityEngine;

namespace Mechanics.SpellSystem
{
    public class UISpellView : MonoBehaviour
    {
        public static event Action<int> CastSpellEvent;

        public void CastSpell(int index) => CastSpellEvent?.Invoke(index);
    }
}