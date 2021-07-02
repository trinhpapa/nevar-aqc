using System;
using System.ComponentModel;

namespace NEVAR_AQC.Core.Entities
{
    public class LogEntity<TKey> where TKey : struct
    {
        public TKey? CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public TKey? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [DefaultValue(false)]
        public bool? IsDeleted { get; set; } = false;

        public TKey? DeletedBy { get; set; }

        public DateTime? DeletedTime { get; set; }
    }
}