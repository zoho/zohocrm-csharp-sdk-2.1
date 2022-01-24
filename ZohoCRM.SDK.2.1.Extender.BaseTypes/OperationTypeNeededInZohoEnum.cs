namespace ZohoCRM.SDK_2_1.Extender.BaseTypes;

public enum OperationTypeNeededInZohoEnum
{
    Create = 0,
    Update = 1,
    LeaveUnchanged = 2,
    IgnoreDueToError = 3
}

public interface ILogger
{
    public void Log(string? message, params string?[] args);
}