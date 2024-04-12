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

