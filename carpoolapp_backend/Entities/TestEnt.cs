using System.ComponentModel.DataAnnotations;

namespace carpoolapp_backend.Entities;

public class TestEnt
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string Surname { get; set; }
}