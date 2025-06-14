using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotation.Models
{
    [Table("HangHoa")]
    public class HangHoa
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(20)]
        public string? MaHang { get; set; }
        [MaxLength(100)]
        [Required]
        public string? TenHang { get; set; }
        [Column(TypeName = "varchar")]
        public decimal DonGia { get; set; }
        [Column(TypeName = "varchar")]
        [Required]
        [MaxLength(20)]
        public string? MaLoai { get; set; }
        [ForeignKey("MaLoai")]
        public LoaiHang? LoaiHang { get; set; }
    }
}
