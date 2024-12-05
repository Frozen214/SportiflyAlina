namespace Sportifly.API.Model;

public class UserModel
{
    public int UserId { get; set; }
    public string UserLogin{ get; set; }
    public string UserPassword { get; set; }
    public string UserRole { get; set; }
    public string UserOwner { get; set; }
    public int RoleId { get; set; }
}
