namespace Gameplay.Behaviours
{
    public interface ITargetContainer<T>
    {
        T Target { get; }
        void ReleaseTarget();
    }
}