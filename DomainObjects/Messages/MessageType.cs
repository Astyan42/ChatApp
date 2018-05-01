namespace DomainObjects.Messages
{
    public enum MessageType
    {
        ConnectionEvent,
        UserJoined,
        UserLeft,
        JoinRequested,
        UsersRequested,
        UserStartedTyping,
        UserStoppedTyping,
        MessageAdded,
        MessageHistory,
        UserRefreshe,
    }
}