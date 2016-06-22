using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Seminar.Model
{
    public abstract  class EntityBase
    {
        [Key]
        public int ID { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string CreatedByUsername { get; set; }
        public string UpdatedByUsername { get; set; }
    }
}
