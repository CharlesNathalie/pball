namespace pball.Services.Tests;
public partial class ScrambleServiceTests : BaseServiceTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ScrambleService_Scramble_and_Descramble_With_Empty_String_Good_Test(string culture)
    {
        Assert.True(await _ScrambleServiceSetupAsync(culture));

        if (ScrambleService != null)
        {
            string retStr = ScrambleService.Scramble("");
            Assert.Equal("", retStr);

            retStr = ScrambleService.Descramble("");
            Assert.Equal("", retStr);
        }
    }
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task ScrambleService_Scramble_and_Descramble_Good_Test(string culture)
    {
        Assert.True(await _ScrambleServiceSetupAsync(culture));

        Random random = new Random();

        if (ScrambleService != null)
        {
            for (int countWord = 0; countWord < 10000; countWord++)
            {
                string Word = "";
                string ScrambleWord = "";
                string DescrambleWord = "";

                int wordLength = random.Next(1, 250);
                for (int i = 0; i < wordLength; i++)
                {
                    Word += (char)random.Next('0', 'z');
                }


                ScrambleWord = ScrambleService.Scramble(Word);
                Assert.NotEqual(Word, ScrambleWord);

                DescrambleWord = ScrambleService.Descramble(ScrambleWord);
                Assert.Equal(Word, DescrambleWord);
            }
        }
    }
}

