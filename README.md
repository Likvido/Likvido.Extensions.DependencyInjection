# Likvido.Extensions.DependencyInjection
Helper extension methods for registering types for dependency injection

## AddImplementations

The `AddImplementations` extension method is a helper method for registering types for dependency injection in .NET. It scans the assembly of a given base type and registers all non-abstract classes that implement the base type. The method supports filtering of types to be registered based on the `AddImplementationFilter` enum.

It will find the types that implement the given interface, and then find all of the other interfaces that the type implements. It will then register the type with all of the interfaces that it implements (depending on the value of the `AddImplementationFilter` enum).

### Example Use-Cases

#### Register All Implementations

```csharp
services.AddImplementations<IMyInterface>();
```

In this example, all non-abstract classes in the assembly of `IMyInterface` that implement `IMyInterface` will be registered with the `ServiceLifetime.Scoped` lifetime.

#### Register Only Base Implementations

```csharp
services.AddImplementations<IMyInterface>(AddImplementationFilter.OnlyBase);
```

In this example, only non-abstract classes in the assembly of `IMyInterface` that directly implement `IMyInterface` will be registered.

#### Register All Except Base Implementations

```csharp
services.AddImplementations<IMyInterface>(AddImplementationFilter.ExcludeBase);
```

In this example, all non-abstract classes in the assembly of `IMyInterface` that implement `IMyInterface` will be registered, except those that directly implement `IMyInterface`.

## AddOptions

The `AddOptions` extension method is a utility method for registering and validating options in .NET. It binds a configuration section to an options object and optionally validates the options using data annotations. If validation fails, it logs a critical error and throws an exception.

### Example Use-Cases

#### Register and Bind Options

```csharp
services.AddOptions<MyOptions>(configuration.GetSection("MyOptions"));
```

In this example, the `MyOptions` class is registered and bound to the "MyOptions" configuration section. If the configuration section is not found or cannot be bound to `MyOptions`, an exception will be thrown when `MyOptions` is resolved from the service provider.

#### Register, Bind, and Validate Options

```csharp
services.AddOptions<MyOptions>(configuration.GetSection("MyOptions"), validate: true);
```

In this example, the `MyOptions` class is registered, bound to the "MyOptions" configuration section, and validated using data annotations. If the configuration section is not found, cannot be bound to `MyOptions`, or fails validation, an exception will be thrown when `MyOptions` is resolved from the service provider.
