# Hangfire

## Paso 1 instalar hangfire

dotnet add package Hangfire 

## Paso 2 Preparar la base de datos SqlServer

Crear la base de datos donde se van registrar los datos de hangfire. Tener la cadena de conexión

## Paso 3 

Agregar la linea "Hangfire":"Information", como se muestra a continuación en el appsettings.json. para visualizar los datos de hangfire
```json
 "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Hangfire":"Information"
    }
  },
```
## Paso 4
Agregar la configuración básica
```cs
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        }));
builder.Services.AddHangfireServer(options=> options.SchedulePollingInterval = TimeSpan.FromSeconds(1));
```
En la linea

```cs
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
```
En <b>DefaultConnection</b> va la cadena de conexión a la misma base de datos, la principal o una dedicada a hangfire


En la linea
```cs
builder.Services.AddHangfireServer(options=> options.SchedulePollingInterval = TimeSpan.FromSeconds(1));
```
Puede ir de esta manera, aui por defecto la consulta de las tareas es cada 7 segundos
```cs
builder.Services.AddHangfireServer();
```

Con esto se agrega el dashboard de hangfire
```cs
app.UseHangfireDashboard();
```
## Paso 5 
Ejecutar la app, para que puedamos ver las tablas relacionadas con hangfire

## Paso 6
Hacer la inyección en el constructor
```cs
private IBackgroundJobClient _backgroundJobClient;

public PersonasController(IBackgroundJobClient backgroundJobClient)
{
  _backgroundJobClient = backgroundJobClient;
}
```
## Paso 7
```cs
_backgroundJobClient.Enqueue(() =>{ 
  Console.WriteLine("Hola mundo hangfire");
});
```
En la linea de hola mundo, se agrega el servicio async

# Referencias

https://www.youtube.com/watch?v=A5TkKShmNO4

https://docs.hangfire.io/en/latest/

---
dotnet tool install --global dotnet-ef



