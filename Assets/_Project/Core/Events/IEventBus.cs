namespace _Project.Core
{
    public interface IEventBus {
        void Subscribe<T>(System.Action<T> handler);
        void Unsubscribe<T>(System.Action<T> handler);
        void Publish<T>(T evt);
    }
}