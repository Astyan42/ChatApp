namespace DomainObjects.Messages
{
    /// <summary>
    /// This is an enum containing the event to dispatch. These event are used only from api to client.
    /// </summary>
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