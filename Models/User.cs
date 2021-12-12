using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Minimal.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirebaseKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserGoalTier UserGoalTier { get; set; }
        public Int16 TotalItemsOwned { get; set; } = 0;
        public Int16 TotalItemsRemoved { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum UserGoalTier
    {
        [EnumMember(Value = "Beginner Minimalist")]
        Beginner,
        [EnumMember(Value = "Average Person")]
        Average,
        [EnumMember(Value = "Hoarder")]
        Hoarder,
        [EnumMember(Value = "Advanced Minimalist")]
        Minimalist,
        [EnumMember(Value = "Removed")]
        Removed
    }
}
