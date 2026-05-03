namespace RPN_Api
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();
            app.MapGet("/rpn/op", () => {
                return Results.Ok("List of available operators : + - * /");
                });
            app.Run();


        }
    }
}
