using ConsoleClient;

using Google.Protobuf.WellKnownTypes;

using Grpc.Net.Client;

var uri = new Uri(@"https://localhost:7299", UriKind.Absolute);

Console.Write("Введите приветствие или погода: ");
var input = Console.ReadLine();

if (string.IsNullOrWhiteSpace(input))
{
    Console.WriteLine("Ошибка, введите команду");
    return;
}

switch (input.ToLower())
{
    case "приветствие":
        Console.Write("Введите имя: ");
        var inputName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(inputName))
        {
            Console.WriteLine("Введите имя!");
            break;
        }
        using (var channel = GrpcChannel.ForAddress(uri))
        {
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = inputName });
            Console.WriteLine(reply.Message);
        }
        break;

    case "погода":
        using (var channel = GrpcChannel.ForAddress(uri))
        {
            var client = new Weather.WeatherClient(channel);
            var reply = await client.GetTodayWeatherAsyncAsync(new Empty());
            Console.WriteLine($"{reply.Temp}\n{reply.Desc}");
        }
        break;
    default:
        Console.WriteLine("Такой команды нет");
        break;
}