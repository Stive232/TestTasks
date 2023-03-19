namespace TestProject.Core.Infrastructure.Exceptions;

public class NotFoundException : Exception
{
    public Type EntityType { get; }
    public object Argument { get; }

    public NotFoundException(Type entityType, object argument)
        : base($"The object of type {entityType.Name} is not found with argument = {argument}")
    {
        EntityType = entityType;
        Argument = argument;
    }
}
