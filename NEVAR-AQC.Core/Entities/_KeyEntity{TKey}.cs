using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Entities
{
    public class KeyEntity<TKey> where TKey : struct
    {
        [Key]
        public TKey Id { get; set; }
    }
}