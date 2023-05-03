using System.ComponentModel.DataAnnotations;

namespace ShareXWebClient.Models;

public class Text
{
    public int Id { get; set; }
    public string? PublicId { get; set; }
    public string? Value { get; set; }
    public DateTime CreateTime = DateTime.Now;
}