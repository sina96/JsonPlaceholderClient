namespace JsonPlaceholderClient.Utilities;

public static class ConsoleUtil
{
    // Input helpers
    public static string ReadRequired(string prompt)
    {
        string input;

        do
        {
            Console.Write(prompt);
            input = (Console.ReadLine() ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(input))
                Console.WriteLine("Input cannot be empty. Please try again.");

        } while (string.IsNullOrWhiteSpace(input));

        return input;
    }

    public static int ReadIntInRange(string prompt, int min, int max)
    {
        int result;
        string input;

        while (true)
        {
            Console.Write(prompt);
            input = (Console.ReadLine() ?? string.Empty).Trim();

            if (int.TryParse(input, out result) && result >= min && result <= max)
                return result;

            Console.WriteLine($"Please enter a valid number between {min} and {max}.");
        }
    }

    // Output helpers
    public static void WriteHeader(string title)
    {
        Console.WriteLine($"\n=== {title} ===");
    }

    public static void WriteSuccess(string message)
    {
        Console.WriteLine($"{message}");
    }

    public static void WriteError(string message)
    {
        Console.WriteLine($"{message}");
    }

    public static void WriteWarning(string message)
    {
        Console.WriteLine($"{message}");
    }
}
