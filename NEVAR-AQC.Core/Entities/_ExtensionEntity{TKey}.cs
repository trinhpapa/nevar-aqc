using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Entities
{
    public class ExtensionEntity<TKey> : LogEntity<long> where TKey : struct
    {
        [Key]
        public TKey Id { get; set; }

        [StringLength(100)]
        public string Note { get; set; }
    }
}