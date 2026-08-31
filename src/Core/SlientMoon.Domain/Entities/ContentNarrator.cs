using SlientMoon.Domain.Common;
using SlientMoon.Domain.Enums;

namespace SlientMoon.Domain.Entities
{
    public class ContentNarrator : BaseEntity
    {
        public int ContentId { get; set; }

        public Content Content { get; set; }

        public NarratorGender Gender { get; set; }
    }
}