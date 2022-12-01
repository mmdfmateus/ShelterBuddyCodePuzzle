namespace ShelterBuddy.CodePuzzle.Core.Entities;

public class Animal : BaseEntity<Guid>
{
    public string? Name { get; set; }
    public string? Colour { get; set; }
    public string? MicrochipNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateInShelter { get; set; }
    public DateTime? DateLost { get; set; }
    public DateTime? DateFound { get; set; }
    public int? AgeYears { get; set; }
    public int? AgeMonths { get; set; }
    public int? AgeWeeks { get; set; }
    public string Species { get; set; }

    public string AgeText => BuildAgeText();

    private string BuildAgeText()
    {
        var years = AgeYears is not null ? $"{AgeYears} years " : string.Empty;
        var months = AgeMonths is not null ? $"{AgeMonths} months " : string.Empty;
        var weeks = AgeWeeks is not null ? $"{AgeWeeks} weeks" : string.Empty;
        return $"{years}{months}{weeks}".Trim();
    }
}