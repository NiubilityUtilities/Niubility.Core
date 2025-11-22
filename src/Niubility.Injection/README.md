## Declare dependency and specify the lifetime type

```C#
    [Injection(InjectionLifetimeTypes.Singleton)]
    public class ClassA
    {
        ...
    }
``````



## Instantiate into container at once
```C#
    public void ConfigureServices(IServiceCollection services)
    {
        services.RegisterServices(typeof(ClassA).Assembly);
    }
```