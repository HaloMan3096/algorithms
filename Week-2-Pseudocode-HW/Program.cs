namespace Week_2_Pseudocode_HW;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(pow(5, 5));
    }
    
    // This has a Big O of O(n), because the time scales based on how large the exponent is
    static int pow(int baseNum, int exponent) // Identify the base and the power
    {
        // Create a temp var to hold the solution
        var answer = 1;
        
        // for the power we loop through and multiply temp var by the base
        for (int i = 0; i < exponent; i++)
        {
            answer *= baseNum;
        }
        
        // return the temp var
        return answer;
    }
}