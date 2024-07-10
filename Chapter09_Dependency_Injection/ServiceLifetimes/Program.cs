using System.Runtime.CompilerServices;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddSingleton<DBContext>();
//builder.Services.AddSingleton<Repository>();
builder.Services.AddTransient<DBContext>();
builder.Services.AddSingleton<Repository>();
var app = builder.Build();


app.MapGet("/", () => "Hello World!");
app.MapGet("/test", (ReturnRowCount));

app.Run();

static string ReturnRowCount(DBContext dBContext, Repository repository)
{
    return $"DBContext: {dBContext.Rowcount} Repository: {repository.RowCount}";
} 
class Repository
{
    private readonly DBContext _dbContext;
    public int RowCount1 => _dbContext.Rowcount;
    public Repository(DBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public int RowCount => _dbContext.Rowcount;
}

class DBContext
{
    public int Rowcount { get; }
        = Random.Shared.Next(1, 1000000000);
}