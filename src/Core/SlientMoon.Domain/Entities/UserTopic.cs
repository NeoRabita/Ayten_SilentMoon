using SlientMoon.Domain.Common;

namespace SlientMoon.Domain.Entities;

public class UserTopic : BaseEntity
{
    public int UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int TopicId { get; set; }
    public Topic Topic { get; set; }
}