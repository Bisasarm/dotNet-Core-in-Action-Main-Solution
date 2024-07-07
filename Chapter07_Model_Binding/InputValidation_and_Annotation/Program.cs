using System.ComponentModel.DataAnnotations;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
//Using the Usermodel with annotations for creating it
//Withparametervalidation is a nuget package to validate using data annotation
app.MapPost("/users", (CreateUserModel user) => user.ToString()).WithParameterValidation();
//to add a range to id it is easiest to use the AsParameters annotation to add DataAnnotation to the int in the provided data model
app.MapGet("users/{id}",
    ([AsParameters]GetUserModel userModel) => $"Received {userModel.Id}")
    .WithParameterValidation();


app.Run();

public record GetUserModel
{
    [Range(1, 10)]
    public int Id { get; set; }
}
public record CreateUserModel : IValidatableObject
{
    [Required]
    [StringLength(15)]
    [Display(Name = "Your Name")]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(10)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    [Phone]
    public string PhoneNo { get; set; }
    [EmailAddress]
    public string EMail { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(EMail) && string.IsNullOrEmpty(PhoneNo))
        {
            yield return new ValidationResult("You must include either a MailAdr or a PhoneNo", new[] { nameof(EMail), nameof(PhoneNo) });
        }
    }

}
