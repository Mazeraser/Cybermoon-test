namespace Mechanics.SpellSystem
{
    public interface ISpell
    {
        public float ReloadTime { get; }
        public void Cast();
    }
}