namespace DomainObjects.Interfaces.Messages
{
    public interface IMessage<T>
    {
        T GetPayload();
    }
}