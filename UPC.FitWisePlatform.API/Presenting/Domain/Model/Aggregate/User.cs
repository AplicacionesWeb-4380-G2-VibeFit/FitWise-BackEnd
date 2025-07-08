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
    
    public string Username { get; private set; }
    
    public string Password { get; private set; }
    
    public Image Image { get; private set; }
    
    public string AboutMe { get; private set; }
    
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
        string username,
        string password,
        Image image,
        string aboutMe)
    {
        // Enum validation
        if (!Enum.IsDefined(gender) || gender == Gender.Unknown)
            throw new ArgumentOutOfRangeException(nameof(gender), "The gender is not a valid value.");

        
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentNullException(nameof(firstName), "First name cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentNullException(nameof(lastName), "Last name cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentNullException(nameof(username), "Username cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(aboutMe))
            throw new ArgumentNullException(nameof(aboutMe), "AboutMe cannot be null or empty.");
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        BirthDate = birthDate;
        Gender = gender;
        Username = username;
        Password = password;
        Image = image;
        AboutMe = aboutMe;
    }

    public void UpdateDetails(string newFirstName, string newLastName, 
        Email newEmail, BirthDate newBirthDate, Gender newGender,
        string newUsername, string newPassword, Image newImage, string newAboutMe)
    {
        // Enum validation
        if (!Enum.IsDefined(newGender) || newGender == Gender.Unknown)
            throw new ArgumentOutOfRangeException(nameof(newGender), "The gender is not a valid value.");
        
        if (string.IsNullOrWhiteSpace(newFirstName))
            throw new ArgumentNullException(nameof(newFirstName), "First name cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(newLastName))
            throw new ArgumentNullException(nameof(newLastName), "Last name cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(newUsername))
            throw new ArgumentNullException(nameof(newUsername), "Username cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(newPassword))
            throw new ArgumentNullException(nameof(newPassword), "Password cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(newAboutMe))
            throw new ArgumentNullException(nameof(newAboutMe), "AboutMe cannot be null or empty.");
        
        FirstName = newFirstName;
        LastName = newLastName;
        Email = newEmail;
        BirthDate = newBirthDate;
        Gender = newGender;
        Username = newUsername;
        Password = newPassword;
        Image = newImage;
        AboutMe = newAboutMe;
    }
    
}