using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DiaryWebAPI.Model
{
    public class DiaryEntry
    {
        [Key]
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
