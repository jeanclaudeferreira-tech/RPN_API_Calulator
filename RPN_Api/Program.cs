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
                [FromServices] RPNStacksService service) => 
            {
                if(!service.StackIdExists(stackId)) return Results.NotFound();

                string result = service.GetStackId(stackId);
                return Results.Ok(result);
            });

            app.MapPost("/rpn/stack", (RPNStacksService service) => 
            {
                int stackId = service.CreateNewStack();
                return Results.Ok(stackId); //toto Results.Created
            });

            app.MapDelete("/rpn/stack/{stack_id:int}", (
                 [FromRoute(Name ="stack_id")] int stackId, 
                 [FromServices] RPNStacksService service) => 
            { 
                var result = service.DeleteStack(stackId);
                if(result) return Results.Ok($"Stack_id {stackId} deleted successfully");
                return Results.NotFound(stackId); 
            });
            
            app.Run();

         
        }
    }
}
