namespace Dinota.Domain
{
    /// <summary>
    /// Activities which is done by users 
    /// </summary>
    public enum UserActivityTypeEnum
    {
        SignOff = 0,
        SignIn = 1,
    }

    /// <summary>
    /// State changes of entities
    /// </summary>
    public enum EntityTraceTypeEnum
    {
        Added = 2,
        Modified = 3,
        Deleted = 4
    }

    public enum MarkStatusEnum
    {
        Procurement = 1,
        Fabrication = 2,
        Transport = 3,
        Erection = 4,
    }

    public enum StatusReferenceEnum
    {
        Procurement = 1,
        Fabrication = 2,
        Packaging = 3,
        Transport = 4,
        Erection = 5,
    }
}
