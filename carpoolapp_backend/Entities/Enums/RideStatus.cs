using System.ComponentModel;

namespace carpoolapp_backend.Entities.Enums;

public enum RideStatus
{
    [Description("ACTIVE")]
    Active,
    [Description("FULL")]
    Full,
    [Description("CANCELLED")]
    Cancelled
}