using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService.Services;


public class ProductService : Product.ProductBase
{
   public override Task<ProductResponse> CreateNewProduct(ProductRequest request, ServerCallContext context)
   {
      System.Console.WriteLine($"[Received From Client] : {request.ProductName} || {request.ProductCode} || ${request.Price}");
      var response = new ProductResponse{
         IsError = false,
         StatusCode = 200
      };
      return Task.FromResult(response);
   }

   public override Task<ProductList> GetAllProducts(Empty request, ServerCallContext context)
   {
      var stockDate = DateTime.SpecifyKind(new DateTime(2023, 1, 1), DateTimeKind.Utc);
      var response = new ProductList();
      // get these products from database ... 
      response.Products.AddRange(
         new List<ProductRequest>{
            new ProductRequest{
               ProductName="Iphone",
               ProductCode="IPM123",
               Price=13570,
               StockDate=Timestamp.FromDateTime(stockDate)
            },
            new ProductRequest{
               ProductName="Playstation 3",
               ProductCode="Ps3_1095",
               Price=9500,
               StockDate=Timestamp.FromDateTime(stockDate)
            }
         }
      );
      return Task.FromResult(response);
   }
}