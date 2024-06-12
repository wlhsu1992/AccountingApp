using System.ComponentModel.DataAnnotations;

public enum ExpenditureType
{
    食,
    衣,
    住,
    行
}

public class Expenditure
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Title { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Amount { get; set; }

    [Required]
    public DateTime CreatedTime { get; set; }

    [Required]
    public ExpenditureType ExpenditureType { get; set; }
}
