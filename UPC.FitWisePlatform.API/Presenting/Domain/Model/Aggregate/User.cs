using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.Aggregate;

public partial class User
{
    public int Id { get; }
    
    public string FirstName { get; private set; }
    
    public string LastName { get; private set; }
    
    public Email Email { get; private set; }
    
    public BirthDate BirthDate { get; private set; }
    
    public Gender Gender { get; private set; }
    
    public Image Image { get; private set; }
    
    public string AboutMe { get; private set; }
    
    public int ProfileId { get; private set; }
    
    // Relacionando
    
    public ICollection<Follower> FollowerUsers { get; private set; }= new List<Follower>();
    
    public ICollection<Follower> FollowedUsers { get; private set; }= new List<Follower>();

    
    public ICollection<Certificate> Certificates { get; private set; } = new List<Certificate>();
    
    
    public User() { }

    public User(
        string firstName,
        string lastName,
        Email email,
        BirthDate birthDate,
        Gender gender,
        Image image,
        string aboutMe,
        int profileId)
    {
        // Enum validation
        if (!Enum.IsDefined(gender) || gender == Gender.Unknown)
            throw new ArgumentOutOfRangeException(nameof(gender), "The gender is not a valid value.");

        
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentNullException(nameof(firstName), "First name cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentNullException(nameof(lastName), "Last name cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(aboutMe))
            throw new ArgumentNullException(nameof(aboutMe), "AboutMe cannot be null or empty.");
        
        if (profileId <= 0)
            throw new ArgumentOutOfRangeException(nameof(profileId), "Profile ID must be greater than zero.");
        
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        BirthDate = birthDate;
        Gender = gender;
        Image = image;
        AboutMe = aboutMe;
        ProfileId = profileId;
    }

    public void UpdateDetails(string newFirstName, string newLastName, 
        Email newEmail, BirthDate newBirthDate, Gender newGender,
        Image newImage, string newAboutMe)
    {
        // Enum validation
        if (!Enum.IsDefined(newGender) || newGender == Gender.Unknown)
            throw new ArgumentOutOfRangeException(nameof(newGender), "The gender is not a valid value.");
        
        if (string.IsNullOrWhiteSpace(newFirstName))
            throw new ArgumentNullException(nameof(newFirstName), "First name cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(newLastName))
            throw new ArgumentNullException(nameof(newLastName), "Last name cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(newAboutMe))
            throw new ArgumentNullException(nameof(newAboutMe), "AboutMe cannot be null or empty.");
        
        FirstName = newFirstName;
        LastName = newLastName;
        Email = newEmail;
        BirthDate = newBirthDate;
        Gender = newGender;
        Image = newImage;
        AboutMe = newAboutMe;
    }
    
}