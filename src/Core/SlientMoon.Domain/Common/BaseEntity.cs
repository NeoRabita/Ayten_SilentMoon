using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SlientMoon.Domain.Common
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
        //Createdate, IsDeleted, UpdateDate elave et
    }
}
