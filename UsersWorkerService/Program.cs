using Microsoft.EntityFrameworkCore;
using UsersWorkerService;
using WebApiEntities.Context;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        //Se agrega el dbcontext, en este caso a una base SqLite
        services.AddDbContext<DbUsersContext>(optionsBuilder => optionsBuilder.UseSqlite("Data Source=C:\\DB\\DbUsers.db"));
        //se inyectan las clases de lógica y repositorio con sus respectivas interfaces        
    })
    .Build();

await host.RunAsync();
