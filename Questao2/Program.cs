using Newtonsoft.Json;
using Questao2;


public class Program
{
    private static readonly HttpClient Client = new();

    public static void Main()
    {
        var teamName = "Paris Saint-Germain";
        var year = 2013;
        var totalGoals = getTotalScoredGoals(teamName, year).Result;

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year).Result;

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);
     
        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> getTotalScoredGoals(string team, int year)
    {
        var totalPages = 1;
        var actualPage = 1;
        var totalGoals = 0;

        //Matches being Team 1
        while (actualPage <= totalPages)
        {
            var path = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={actualPage}";
            var response = await Client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                //var result = await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadAsAsync<Page>();
                //var page = JsonConvert.DeserializeObject<Page>(result);
                totalPages = result.TotalPages;
                actualPage++;
                totalGoals += result.Matches.Sum(m => m.Team1Goals);
            }
        }

        //Matches being Team 2
        totalPages = 1;
        actualPage = 1;
        while (actualPage <= totalPages)
        {
            var path = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team2={team}&page={actualPage}";
            var response = await Client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                //var result = await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadAsAsync<Page>();
                //var page = JsonConvert.DeserializeObject<Page>(result);
                totalPages = result.TotalPages;
                actualPage++;
                totalGoals += result.Matches.Sum(m => m.Team2Goals);
            }
        }
        
        return totalGoals;
    }

}