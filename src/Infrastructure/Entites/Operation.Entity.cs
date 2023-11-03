using System.Runtime.Serialization;

namespace Infrastructure.Entities
{
    public enum OperationEntity
    {
        [EnumMember(Value = "Deposit")]
        Deposit = 0,
        [EnumMember(Value = "Withdraw")]
        Withdraw = 1
    }
}
