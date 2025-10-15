namespace Helteix.Singletons.Interfaces
{
    public interface ISingletonFinder<T> where T : ISingleton
    {
        public bool TryFindExistingInstance(out T instance);
    }
}