using Grpc.Net.Client;
using GrpcService;

namespace GrpcClient;

public class program
{
   public static async Task Main()
   {
      await ServerStreamingAPI();
   }


   // method to show the server-streaming pattern
   private static async Task ServerStreamingAPI()
   {
      // first create a channel
      var channel = GrpcChannel.ForAddress("http://localhost:5043");

      // create a client to communicate through tihs channel
      var client = new Streamdemo.StreamdemoClient(channel);

      var response = client.ServerStreamingAPI(
         new test1{Msg="testing the server-streaming api"}
      );

      while(await response.ResponseStream.MoveNext(CancellationToken.None)){
         var resValue = response.ResponseStream.Current.Msg;
         System.Console.WriteLine(resValue);
      }

      System.Console.WriteLine("Server streaming is completed");

      await channel.ShutdownAsync();
   }

   // method to test the client-streaming api
   private static async Task ClientStreamingAPI()
   {
      var channel = GrpcChannel.ForAddress("http://localhost:5043");

      var client = new Streamdemo.StreamdemoClient(channel);

      var response = client.ClientStreamingAPI();
   }
}