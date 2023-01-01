using Grpc.Net.Client;
using GrpcService;
using Google.Protobuf.WellKnownTypes;

namespace GrpcClient;

public class Program
{
   public static async Task Main()
   {
      System.Console.WriteLine("UTILIZING SERVICE [1]");
      // the port of the grpc service project
      var channel = GrpcChannel.ForAddress("http://localhost:5061");

      // define the client for our sample service
      var client = new Sample.SampleClient(channel);

      // send the request and receive the reponse
      var response =  await client.GetFullNameAsync(new SampleRequest{FirstName="Fady", LastName="Gamil"});

      System.Console.WriteLine(response.FullName);

      System.Console.WriteLine("UTILIZING SERVICE [2]");

      // define another client for another service
      var ProductServiceClient = new Product.ProductClient(channel);

      var stockDate = DateTime.SpecifyKind(new DateTime(2022, 2, 1), DateTimeKind.Utc);
      var productServiceResponse = await  ProductServiceClient.CreateNewProductAsync(
         new ProductRequest{
            ProductName="Iphone Pro Max", 
            Price=13000, 
            ProductCode="531IPM", 
            StockDate=Timestamp.FromDateTime(stockDate)});

      System.Console.WriteLine(productServiceResponse.StatusCode);

      System.Console.WriteLine("UTILIZING SERVICE [3]");
      var products = await ProductServiceClient.GetAllProductsAsync(new Google.Protobuf.WellKnownTypes.Empty());
      foreach(var product in products.Products)
      {
         // convert the stock data from timestamp format to date time format
         var stock_date = product.StockDate.ToDateTime();
         System.Console.WriteLine($"{product.ProductName} || {product.ProductCode} || $ {product.Price} || {stock_date.ToString("dd-MM-yyyy")}");
      }



      // shutdown the channel 
      await channel.ShutdownAsync();



      Console.ReadKey();
   }
}