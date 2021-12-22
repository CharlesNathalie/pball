namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<Contact>> LoginAsync(LoginModel loginModel)
    {
        // no need
        //if (LoggedInService.LoggedInContactInfo == null && LoggedInService.LoggedInContactInfo?.LoggedInContact == null)
        //{
        //    return await Task.FromResult(BadRequest(PBallRes.YouDoNotHaveAuthorization));
        //}

        if (string.IsNullOrWhiteSpace(loginModel.LoginEmail))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "LoginEmail")));
        }

        if (!string.IsNullOrWhiteSpace(loginModel.LoginEmail) && (loginModel.LoginEmail.Length < 5 || loginModel.LoginEmail.Length > 100))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._LengthShouldBeBetween_And_, "LoginEmail", "5", "100")));
        }

        if (string.IsNullOrWhiteSpace(loginModel.Password))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._IsRequired, "Password")));
        }

        if (!string.IsNullOrWhiteSpace(loginModel.Password) && (loginModel.Password.Length < 5 || loginModel.Password.Length > 50))
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes._LengthShouldBeBetween_And_, "Password", "5", "50")));
        }

        try
        {
            Contact? contact = (from c in db.Contacts
                               where c.LoginEmail == loginModel.LoginEmail
                               select c).FirstOrDefault();

            if (contact == null)
            {
                return await Task.FromResult(BadRequest(String.Format(PBallRes.UnableToLoginAs_WithProvidedPassword, loginModel.LoginEmail)));
            }

            if (loginModel.Password == ScrambleService.Descramble($"{ contact.PasswordHash }"))
            {

                byte[] key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("APISecret"));

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                            new Claim(ClaimTypes.Name, $"{ contact.LoginEmail }")
                    }),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                contact.Token = tokenHandler.WriteToken(token);

                contact.PasswordHash = "";
                contact.ResetPasswordTempCode = "";

                return await Task.FromResult(Ok(contact));
            }
        }
        catch (Exception ex)
        {
            return await Task.FromResult(BadRequest(string.Format(PBallRes.Error_, ex.Message)));
        }

        return await Task.FromResult(BadRequest(string.Format(PBallRes.UnableToLoginAs_WithProvidedPassword, loginModel.LoginEmail)));
    }
}

