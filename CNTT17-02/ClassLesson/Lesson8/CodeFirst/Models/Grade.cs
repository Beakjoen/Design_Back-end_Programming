namespace CodeFirst.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public string GreadName { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
