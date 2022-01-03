using System.Linq;

static int[, ] GetCombinations(int[] numbers)
{
    int length = 1;
    for (int i = 2; i <= numbers.Length; i++)
        length *= i;

    int[,] combinations = new int[length, numbers.Length];
    Random random = new Random();
    int current = 0;
    for (int j = 0; j < numbers.Length; j++)
    {
        int previousFactorial = GetFactorial(numbers.Length - j);
        int factorial = GetFactorial(numbers.Length - 1 - j);
        int counter = 0;
        int start = 0;
        while (true)
        {
            current = random.Next(0, numbers.Length);

            List<int> usedHorizontalValues = new List<int>();

            for (int c = 0; c < j; c++)
                usedHorizontalValues.Add(combinations[0, c]);

            if (!usedHorizontalValues.Any(i => i == current))
                break;
        }
        for (int i = 0; i < length; i++)
        {
            if (factorial == counter)
            {     
                if (i % previousFactorial == 0 && i >= previousFactorial)
                {
                    start = i;
                }
                List<int> usedVerticalValues = new List<int>();
                for (int c = start; c < i; c += counter)
                {
                    usedVerticalValues.Add(combinations[c, j]);
                }

                List<int> usedHorizontalValues = new List<int>();
                for (int c = 0; c < j; c++)
                {
                    usedHorizontalValues.Add(combinations[i, c]);
                }

                while (true)
                {
                    current = random.Next(0, numbers.Length);
 
                    if (!usedVerticalValues.Any(i => i == current) && !usedHorizontalValues.Any(i => i == current))
                        break;        
                }
                counter = 0;
            }
            counter++;
            combinations[i, j] = current;
        }
    }

    return combinations;
}

static int GetFactorial(int x)
{
    if (x < 0)
        throw new InvalidOperationException();
    int result = 1;
    for (int i = 2; i <= x; i++)
        result *= i;

    return result;
}

static int GetMaxCoins(int[, ] combinations, int[] numbers)
{
    int max = 0;
    int[] sequence = new int[numbers.Length];
    for (int i = 0; i < GetFactorial(numbers.Length); i++)
    {
        for (int j = 0; j < numbers.Length; j++)
        {
            sequence[j] = numbers[combinations[i, j]];
        }

        int[] copy = new int[numbers.Length];
        Array.Copy(numbers, copy, numbers.Length);
        int countOfCoins = 0;
        for (int j = 0; j < sequence.Length; j++)
        {
            for (int c = 0; c < copy.Length; c++)
            {
                if (sequence[j] == copy[c])
                {
                    if (c == 0 && c + 1 < copy.Length)
                    {
                        countOfCoins += 1 * copy[c] * copy[c + 1];
                    }
                    else if (c > 0 && c + 1 == copy.Length)
                    {
                        countOfCoins += copy[c - 1] * copy[c] * 1;
                    }
                    else if (c > 0 && c + 1 < copy.Length)
                    {
                        countOfCoins += copy[c - 1] * copy[c] * copy[c + 1];
                    }
                    else if (c == 0 && c + 1 == copy.Length)
                    {
                        countOfCoins += 1 * copy[c] * 1;
                    }

                    List<int> helper = copy.ToList();
                    helper.RemoveAt(c);
                    copy = helper.ToArray();
                    break;
                }
            }
        }
        Console.WriteLine(countOfCoins);
        if (max < countOfCoins)
            max = countOfCoins;
    }

    return max;
}

int[] numbers = new int[] { 35, 16, 83, 87, 84, 59, 48, 41, 20, 54 };
int[, ] combinations = GetCombinations(numbers);

int result = GetMaxCoins(combinations, numbers);
Console.WriteLine(result);

