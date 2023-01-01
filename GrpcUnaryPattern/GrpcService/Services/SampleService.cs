using Grpc.Core;

namespace GrpcService.Services;

// [serviceName].[serviceName]Base
public class SampleService : Sample.SampleBase
{
   public override Task<SampleResponse> GetFullName(SampleRequest request, ServerCallContext context)
   {
      var fullNameRes = request.FirstName + " " + request.LastName;

      return Task.FromResult(new SampleResponse{FullName = fullNameRes});
   }
}