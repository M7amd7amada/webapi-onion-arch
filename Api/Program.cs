using Api.Extensions;

var app = WebApplication.CreateBuilder().ConfigureServices().Build();

app.Use(async (context, next) =>
   {
       if (context.Request.Path == "/favicon.ico")
       {
           context.Response.StatusCode = 204; // No content
           return;
       }

       await next();
   });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();