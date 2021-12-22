namespace PBallServices;

public partial class ContactService : ControllerBase, IContactService
{
    private string GenerateTempCode()
    {
        int UniqueCodeSize = 4;
        string TempCode = "";

        Random r = new Random((int)DateTime.Now.Ticks);

        while (TempCode.Length < UniqueCodeSize)
        {
            TempCode += (r.Next(0, 9)).ToString();
        }

        return TempCode;
    }
}

