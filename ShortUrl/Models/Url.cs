using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.Models
{
    public class Url
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Начальный URL")]
        public string LongUrl { get; set; }

        //[Required]
        [DisplayName("Короткий URL")]
        public string ShortUrl { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Дата создания")]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [DisplayName("Количество переходов")]
        public int Counter { get; set; }
        
    }
}
