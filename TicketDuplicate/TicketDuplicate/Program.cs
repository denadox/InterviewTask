using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            
            using (StreamReader reader = new StreamReader("TextFile1.txt"))
            {
                
                string content = reader.ReadToEnd();

                string pattern = @"No. : (\d+).*?PRICE\s+(\d+\.\d+)";

                Regex regex = new Regex(pattern, RegexOptions.Singleline);
                MatchCollection matches = regex.Matches(content);

                var matchesList = new List<(string, decimal)>();

                foreach (Match match in matches)
                {
                    var ticketNumber = match.Groups[1].Value;
                    var price = decimal.Parse(match.Groups[2].Value);

                    matchesList.Add((ticketNumber, price));                   
                }

                decimal totalSum = matchesList.Sum(match => match.Item2);
                //Console.WriteLine(matchesList.Count);
                //Console.WriteLine(totalSum);

                var uniqueMatchesList = matchesList.Distinct().ToList();
                var uniquieTotalSum = uniqueMatchesList.Sum(match => match.Item2);
                //Console.WriteLine(uniqueMatchesList.Count);
                //Console.WriteLine(uniquieTotalSum);

                Console.WriteLine($"The amount of duplicated tickets is {matchesList.Count - uniqueMatchesList.Count} and the amount of money that was lost is {totalSum - uniquieTotalSum} $.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}