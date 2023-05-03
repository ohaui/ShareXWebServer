using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ShareXWebClient.Models;

public class Link
{
    [Key]
    public int Id { get; set; }
    public string? Value { get; set; }
    public string? ReferenceLink { get; set; }
    public DateTime CreateTime = DateTime.Now;
}
