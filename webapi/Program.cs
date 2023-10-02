using LottoTicketGenerator.Dal;
using webapi.ApplicationServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<EntityFrameWorkContext>();
builder.Services.AddMvc().AddControllersAsServices().AddJsonOptions(configure =>
{
    configure.JsonSerializerOptions.IncludeFields = true;
});

AddApplicationServicesAsSingleton(builder);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();

static void AddApplicationServicesAsSingleton(WebApplicationBuilder builder)
{
    var serviceProvider = builder.Services.BuildServiceProvider();
    var logger = serviceProvider.GetService<ILogger<ServiceBase>>();
    builder.Services.AddSingleton(typeof(ILogger), logger);

    var serviceTypes = new Type?[]
    {
        typeof(LottoTicketService)
    };

    foreach (var serviceType in serviceTypes)
    {
        ObjectFactory objectFactory = ActivatorUtilities.CreateFactory(serviceType, new Type[0]);
        builder.Services.AddSingleton(serviceType, serviceProvider =>
        {
            var obj = (LottoTicketService)objectFactory(serviceProvider, null);
            obj.logger = serviceProvider.GetRequiredService<ILogger>();
            return obj;
        });
    }
}