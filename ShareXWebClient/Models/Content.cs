using System.ComponentModel.DataAnnotations;

namespace ShareXWebClient.Models;

public class Content //might be not that good class naming
{
    [Key]
    public int Id { get; set; }
    public string? PublicId { get; set; } //name of the file
    public string? Path { get; set; } //full path (includes name)
    public DateTime CreateTime = DateTime.Now;
}