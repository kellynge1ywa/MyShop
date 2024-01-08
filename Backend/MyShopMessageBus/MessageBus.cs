

using System.Text;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace MyShopMessageBus;

public class MessageBus : ImessageBus
{
    private readonly string connectionstring="Endpoint=sb://myshopbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ufGVZTi7J6lUFjmSHU/xxX/HzQbrdHyos+ASbCDULUk=";
    public async Task PublishMessage(object Message, string Topic_Queue_Name)
    {
        //Create a client to connect us to service bus
        var client =new ServiceBusClient(connectionstring);

        //Create a sender that will send message to queue or topic name
        ServiceBusSender sender=client.CreateSender(Topic_Queue_Name);
        //Convert message to json
        var body=JsonConvert.SerializeObject(Message);
        
        ServiceBusMessage theMessage= new ServiceBusMessage(Encoding.UTF8.GetBytes(body)){
            CorrelationId=Guid.NewGuid().ToString(), //Give each message a unique identifier
        };

        //Send the message
        await sender.SendMessageAsync(theMessage);

        //Free the resource
        await sender.DisposeAsync();

    }
}
