using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;

public partial class Exercise
{
    public int Id { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Uri ImageUri { get; private set; }
    
    public ICollection<HealthPlanExercise> HealthPlanExercises { get; private set; } = new List<HealthPlanExercise>();

    public Exercise() { }
    
    public Exercise(string name, string description, Uri imageUri)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Exercise name cannot be null or empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Exercise description cannot be null or empty.", nameof(description));
        ArgumentNullException.ThrowIfNull(imageUri);

        this.Name = name;
        this.Description = description;
        this.ImageUri = imageUri;
    }
    
    public void UpdateDetails(string newName, string newDescription, Uri newImageUri)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Exercise new name cannot be null or empty.", nameof(newName));
        if (string.IsNullOrWhiteSpace(newDescription))
            throw new ArgumentException("Exercise new description cannot be null or empty.", nameof(newDescription));
        ArgumentNullException.ThrowIfNull(newImageUri);

        this.Name = newName;
        this.Description = newDescription;
        this.ImageUri = newImageUri;
    }
}