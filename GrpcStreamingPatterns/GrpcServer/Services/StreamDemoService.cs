using System;
using Grpc.Core;
using GrpcService;

namespace GrpcStreamingPatterns.Services;

public class StreamDemoService : Streamdemo.StreamdemoBase
{
   private Random random = new Random();
   public override async Task ServerStreamingAPI(
      testingMsg request, 
      IServerStreamWriter<testingMsg> responseStream, 
      ServerCallContext context)
   {
      for (int i = 1; i <=20; i++){
         await responseStream.WriteAsync(new testingMsg{Msg=$"Message Number {i}"});
         // => doing some time 
         var randomNum = random.Next(1, 3);
         await Task.Delay(randomNum * 1000);
      }
   }
}