using Common.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public abstract class BaseEntity<TModelID>
    {
        public TModelID Id { get; set; }
        public DateTime DateTimeOfCreation { get; private set; } = DateTime.Now;
        public DateTime DateTimeOfUpdate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        public override string ToString() =>
            $"[Base-Info:" +
            $"-Create:{DateTimeOfCreation.GetPersianDateTime()}" +
            $"-Update:{DateTimeOfUpdate.GetPersianDateTime()}" +
            $"-IsDeleted:{IsDeleted}" +
            $"]";
    }
}
