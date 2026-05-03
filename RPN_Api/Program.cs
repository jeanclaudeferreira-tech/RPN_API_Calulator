using Microsoft.AspNetCore.Mvc;

namespace RPN_Api_V1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddSingleton<RPNStacksService>();
            var app = builder.Build();
            
            app.MapGet("/rpn/op", () => {
                return Results.Ok("List of available operators : + - * /");
                });

            app.MapGet("/rpn/stack", (RPNStacksService service) => {
                string result = service.GetAvailableStacksId();
                return Results.Ok(result);
            });

            app.MapGet("/rpn/stack/{stack_id:int}", (
                [FromRoute(Name ="stack_id") ] int stackId,
                [FromServices] RPNStacksService service) => {
                string result = service.GetStackId(stackId);
                return Results.Ok(result);
            });

            app.Run();

         
        }
    }
}
