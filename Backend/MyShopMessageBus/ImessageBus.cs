namespace MyShopMessageBus;

public interface ImessageBus
{
    Task PublishMessage (object Message, string Topic_Queue_Name);

}
