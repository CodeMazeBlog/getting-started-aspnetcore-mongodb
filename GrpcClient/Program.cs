using System;
using System.Threading.Tasks;
using StudentGrpcService;
using Grpc.Net.Client;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new StudentService.StudentServiceClient(channel);
            while(true)
            {
                Console.WriteLine("Enter an argument");
                string line = Console.ReadLine();
                if (line == "exit")
                {
                    break;
                }
                var reply = await client.GetStudentAsync(
                    new GetStudentRequest (){Id = line}
                );
                if(reply.Student != null)
                {
                    Console.WriteLine("Reply: " + reply.Student);
                }
                if(reply.Error != null)
                {
                    Console.WriteLine("Reply: " + reply.Error);
                }
            }
            Console.WriteLine("Exiting...");
        }
    }
}
