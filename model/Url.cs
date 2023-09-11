using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace LinkShortener.model
{
    [Table("Url")]
    public class Url
    {
        public Url(string? urls)
        {
            Id = new Guid();
            Urls = urls;
            DateTime date = DateTime.Now;
            CreateAt = date.ToString("dd/MM/yyyy");
        }

        [Display(Name = "Id")]
        [Column("Id")]
        public Guid Id { get; private set; }

        [Display(Name = "Urls")]
        [Column("Urls")]
        public string? Urls { get; private set; }

        [Display(Name = "CreateAt")]
        [Column("CreateAt")]
        public string? CreateAt { get; private set; }
    }
}
