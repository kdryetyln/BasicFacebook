using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Models.BaseEntity
{
    public interface IBaseEntity
    {
        int ID { get; set; }
        DateTime? CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
