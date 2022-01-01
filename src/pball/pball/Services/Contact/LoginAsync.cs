namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    public async Task<ActionResult<Contact>> LoginAsync(LoginModel loginModel)
    {
        ErrRes errRes = new ErrRes();

        if (string.IsNullOrWhiteSpace(loginModel.LoginEmail))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "LoginEmail"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (loginModel.LoginEmail.Length < 6 || loginModel.LoginEmail.Length > 100)
        {
            errRes.ErrList.Add(string.Format(PBallRes._LengthShouldBeBetween_And_, "LoginEmail", "6", "100"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (string.IsNullOrWhiteSpace(loginModel.Password))
        {
            errRes.ErrList.Add(string.Format(PBallRes._IsRequired, "Password"));
            return await Task.FromResult(BadRequest(errRes));
        }

        if (loginModel.Password.Length > 50)
        {
            errRes.ErrList.Add(string.Format(PBallRes._MaxLengthIs_, "Password", "50"));
            return await Task.FromResult(BadRequest(errRes));
        }

        try
        {
            Contact? contact = (from c in db.Contacts
                               where c.LoginEmail == loginModel.LoginEmail
                               select c).FirstOrDefault();

            if (contact == null)
            {
                errRes.ErrList.Add(string.Format(PBallRes.UnableToLoginAs_WithProvidedPassword, loginModel.LoginEmail));
                return await Task.FromResult(BadRequest(errRes));
            }

            if (loginModel.Password == ScrambleService.Descramble($"{ contact.PasswordHash }"))
            {
                byte[] key = Encoding.ASCII.GetBytes(Configuration["APISecret"]);

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

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
                    return await Task.FromResult(BadRequest(errRes));
                }

                contact.PasswordHash = "";
                contact.ResetPasswordTempCode = "";

                LoggedInService.LoggedInContactList.Add(contact);

                return await Task.FromResult(Ok(contact));
            }
        }
        catch (Exception ex)
        {
            errRes.ErrList.Add(string.Format(PBallRes.Error_, ex.Message));
            return await Task.FromResult(BadRequest(errRes));
        }

        errRes.ErrList.Add(string.Format(PBallRes.UnableToLoginAs_WithProvidedPassword, loginModel.LoginEmail));
        return await Task.FromResult(BadRequest(errRes));
    }
}

