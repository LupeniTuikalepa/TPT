namespace Helteix.Singletons.Interfaces
{
    public interface ISingletonFactory<out T> where T : ISingleton
    {
        public T CreateSingleton();
    }
}