using Grpc.Core;

namespace Server.Services;
public sealed class GreeterService : Greeter.GreeterBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context) => 
        Task.FromResult(new HelloReply
    {
        Message = $"Привет {request.Name}"
    });
}
