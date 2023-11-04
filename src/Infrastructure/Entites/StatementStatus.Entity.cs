using System.Runtime.Serialization;

namespace Infrastructure.Entities
{
    public enum StatementSatusEntity
    {
        [EnumMember(Value = "Approved")]
        Approved = 0,
        [EnumMember(Value = "Rejected")]
        Rejected = 1
    }
}
