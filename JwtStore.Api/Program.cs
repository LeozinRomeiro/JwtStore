using JwtStore.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();
builder.AddAccountContext();
builder.AddMediatR();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

app.MapAccountContext();

app.Run();
