namespace DomainObjects.Messages.Abstract
{
    public class Message<T>
    {
        public MessageType type { get; set; }
        public T payload { get; set; }
        
        public Message()
        {
        }
    }
    
}