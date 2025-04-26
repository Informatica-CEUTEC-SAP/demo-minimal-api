#region Step 1: Configuration Setup
    var builder = WebApplication.CreateBuilder(args);
#endregion Step 1: Configuration Setup
    
#region Step2: Service Registration
#region Step2.1: Add services to the DI container.
    // Addd Swagger services.
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
#endregion Step2.1: Add services to the DI container.
    
#region Step2.2: Add database context
    
#endregion Step2.2: Add database context
#endregion Step2: Service Registration
    
#region Step3: Build the application
    var app = builder.Build();
#endregion Step3: Build the application

#region Step4: Middleware Pipeline Configuration
    #region Step4.1: Configure the HTTP request pipeline.
    
        if (app.Environment.IsDevelopment())
        {
            // middleware for Swagger
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
    
    #endregion Step4.1: Configure the HTTP request pipeline.
    
    app.MapGet("/", () => "Hello World!")
        .WithName("GetHelloWorld")
        .WithDescription("This is a simple hello world endpoint.")
        .WithTags("HelloWorld")
        .WithSummary("Hello World Endpoint")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);
    
#endregion Step4: Middleware Pipeline Configuration

#region Step5: Start the Application
    app.Run();
#endregion Step5: Start the Application