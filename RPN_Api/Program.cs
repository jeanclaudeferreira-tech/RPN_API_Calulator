using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace RPN_Api_V1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Logging.ClearProviders();
            var loggerConfiguration = new LoggerConfiguration()
                .WriteTo.Console();
            var log = loggerConfiguration.CreateLogger();
            builder.Logging.AddSerilog(log);
            builder.Services.AddSingleton<RPNStacksService>();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();
            
            app.MapGet("/rpn/op", async (
                [FromServices]RPNStacksService service,
                [FromServices] ILogger<Program> logger) => 
            {
                logger.LogInformation("Call GET /rpn/op method");
                return Results.Ok($"List of available operators : {service.GetOperators()}");
            });

            app.MapGet("/rpn/stack", (
                [FromServices] RPNStacksService service,
                [FromServices] ILogger<Program> logger) => {
                logger.LogInformation("Call GET /rpn/stack method");
                string result = service.GetAvailableStacksId();
                return Results.Ok(result);
            });

            app.MapGet("/rpn/stack/{stack_id:int}", (
                [FromRoute(Name ="stack_id") ] int stackId,
                [FromServices] RPNStacksService service,
                [FromServices] ILogger<Program> logger) => 
            {
                logger.LogInformation("Call GET /rpn/stack method with stack_id= {stackId}", stackId);
                if (!service.StackIdExists(stackId)) return Results.NotFound();

                string result = service.GetStackId(stackId);
                return Results.Ok(result);
            });

            app.MapPost("/rpn/stack", (RPNStacksService service,
                [FromServices] ILogger<Program> logger) => 
            {
                logger.LogInformation("Call POST /rpn/stack method");
                int stackId = service.CreateNewStack();
                return Results.Ok(stackId); //todo Results.Created
            });

            app.MapDelete("/rpn/stack/{stack_id:int}", (
                 [FromRoute(Name ="stack_id")] int stackId, 
                 [FromServices] RPNStacksService service,
                 [FromServices] ILogger<Program> logger) => 
            {
                logger.LogInformation("Call DELETE /rpn/stack method with stack_id= {stackId}", stackId);
                var result = service.DeleteStack(stackId);
                if(result) return Results.Ok($"Stack_id {stackId} deleted successfully");
                return Results.NotFound(stackId); 
            });

            app.MapPost("/rpn/stack/{stack_id:int}/{value:int}", (
                [FromRoute(Name = "stack_id")] int stackId,
                [FromRoute] int value,
                [FromServices] RPNStacksService service,
                [FromServices] ILogger<Program> logger) =>
            {
                logger.LogInformation("Call POST /rpn/stack method with stack_id= {stackId} and value= {value}", stackId, value);
                if (!service.StackIdExists(stackId)) return Results.NotFound();
                if (!service.AddValueToStack(stackId, value)) Results.NotFound(); 
                return Results.Ok();
            });

            app.MapPost("/rpn/op/{op}/stack/{stack_id:int}", (
                [FromRoute] string op,
                [FromRoute(Name="stack_id")] int stackId,
                [FromServices] RPNStacksService service,
                [FromServices] ILogger<Program> logger) => 
            {
                logger.LogInformation("Call POST /rpn/stack method with stack_id= {stackId} and op= {op}", stackId, op);
                if (!service.StackIdExists(stackId)) return Results.NotFound($"Stack {stackId} doesn't exist.");
                if (!service.GetOperators().Contains(op)) return Results.BadRequest($"Unauthorised operator {op}.");
                if (!service.ApplyOperator(op, stackId)) return Results.BadRequest("An error occurred when applying the operator.");
                return Results.Ok();
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Run();

        }
    }
}
