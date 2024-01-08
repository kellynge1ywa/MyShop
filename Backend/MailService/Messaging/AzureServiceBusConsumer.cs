
using System.Text;

using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace MailService;

public class AzureServiceBusConsumer : IAzureServiceBusConsumer
{
    private readonly IConfiguration _Iconfiguration;
    private readonly string _ConnectionString;
    private readonly string  _QueueName;
    private readonly ServiceBusProcessor _EmailProcessor;
    private readonly MailsService _MailsService;
    public AzureServiceBusConsumer(IConfiguration configuration)
    {
        _Iconfiguration=configuration;
        _ConnectionString=_Iconfiguration.GetValue<string>("AzureConectionString");
        _QueueName=_Iconfiguration.GetValue<string>("QueueandTopic:registerqueue");
         _MailsService= new MailsService(configuration);

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
            //Sending an email
            //Confirm email is sent
             StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<img src=\"https://cdn.pixabay.com/photo/2019/10/10/08/11/shopping-4538982_640.jpg\" width=\"500\" height=\"250\">");
                stringBuilder.Append("<h1> Hello " + user.Fullname + "</h1>");
                stringBuilder.AppendLine("<br/>Welcome to MyShop Application");

                stringBuilder.Append("<br/>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p>We have amazing products at affordable prices with discounts!!</p>");

                await _MailsService.SendMail(user, stringBuilder.ToString());

            await args.CompleteMessageAsync(args.Message); //We are done delete message from queue

        } catch(Exception ex){
            throw;
            //Send email to admin
        }

        
    }

    
}
