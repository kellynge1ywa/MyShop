
using System.Text;
using System.Text.Json.Serialization;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace MailService;

public class AzureServiceBusConsumer : IAzureServiceBusConsumer
{
    private readonly IConfiguration _Iconfiguration;
    private readonly string _ConnectionString;
    private readonly string  _QueueName;
    private readonly ServiceBusProcessor _EmailProcessor;
    public AzureServiceBusConsumer(IConfiguration configuration)
    {
        _Iconfiguration=configuration;
        _ConnectionString=_Iconfiguration.GetValue<string>("AzureConectionString")!;
        _QueueName=_Iconfiguration.GetValue<string>("QueueandTopic:registerqueue")!;

        var client= new ServiceBusClient(_ConnectionString);
        _EmailProcessor= client.CreateProcessor(_QueueName);
        
    }
    public async  Task Start()
    {
        _EmailProcessor.ProcessMessageAsync+=OnRegisterUser;
        _EmailProcessor.ProcessErrorAsync+=ErroHandler;
        await _EmailProcessor.StartProcessingAsync();
    }

    public async  Task Stop()
    {
        await _EmailProcessor.StopProcessingAsync();
        await _EmailProcessor.DisposeAsync();
    }

    private Task ErroHandler(ProcessErrorEventArgs args)
    {
        //Send email to admin
        return Task.CompletedTask;
    }

    private async  Task OnRegisterUser(ProcessMessageEventArgs args)
    {
        var message=args.Message;
        var body=Encoding.UTF8.GetString(message.Body); //read message as string
        var user=JsonConvert.DeserializeObject<ShopUserMessageDto>(body); //string to ChatUserMessageDto

        try{
            //To send email
            //Say that you are done 
            await args.CompleteMessageAsync(args.Message); //We are done delete message from queue

        } catch(Exception ex){
            Console.WriteLine(ex.Message);
            //Send email to admin
        }

        
    }

    
}
