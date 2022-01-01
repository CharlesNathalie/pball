namespace pball.Controllers.Tests;

public partial class FallBackToFileControllerTests
{
    [Theory]
    [InlineData("en-CA")]
    [InlineData("fr-CA")]
    public async Task Constructor_Good_Test(string culture)
    {
        Assert.True(await FallBackToFileControllerSetup(culture));

        using (HttpClient httpClient = new HttpClient())
        {
            List<string> EndOfUrlList = new List<string>
            {
                "", "anythingThatDoesNotStartWith_api_culture_ShouldDefaultTo", "index.html"
            };

            foreach (string endOfUrl in EndOfUrlList)
            {
                string url = $"{ Configuration?["pballurl"] }{ endOfUrl }";
                var response = await httpClient.GetAsync(url);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                string responseContent = await response.Content.ReadAsStringAsync();
                Assert.Contains("<app-root></app-root>", responseContent);
            }
        }
    }
}

