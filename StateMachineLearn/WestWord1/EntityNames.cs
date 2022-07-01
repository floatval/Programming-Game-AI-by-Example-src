namespace StateMachineLearn;

public enum EntityName
{
    EntityMinerBob,
    EntityElsa,
}

public static class EnumExtensions
{
    public static string GetName(this EntityName entityName)
    {
        return entityName switch
        {
            EntityName.EntityMinerBob => "Miner Bob",
            EntityName.EntityElsa => "Elsa",
            _ => "Unknown"
        };
    }
}

