using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotation.Models
{
    [Table("LoaiHang")]
    public class LoaiHang
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(10)]
        public string? MaLoai { get; set; }
        [MaxLength(100)]
        [Required]
        public string? TenLoai { get; set; }
        public ICollection<HangHoa>? HangHoa { get; set; }
    }
}
