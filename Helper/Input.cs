
namespace Helper
{
    class Input
    {
        public static char GetCharWithPrompt(string prompt, string errorMessage)
        {
            char userCharInput = ' ';
            bool isChar = false;

            if(!String.IsNullOrEmpty(prompt))
            {
                Console.WriteLine(prompt);
            }

            do
            {
                string input = Console.ReadLine();

                if(!String.IsNullOrEmpty(input) && input.Length == 1)
                {
                    userCharInput = input[0];
                    isChar = true;
                } else
                {
                    Console.WriteLine(errorMessage);
                }
            } while (!isChar);

            return userCharInput;
        }

        public static int GetIntWithPrompt(string prompt, string errorMessage)
        {
            bool conversionSuccessful = false;
            int userIntInput = 0;


            do
            {
                Console.Write(prompt);
                string? userInput = Console.ReadLine();

                if (userInput == null || userInput == "")
                {
                    Console.WriteLine();
                    Console.WriteLine(errorMessage);
                    continue;
                }

                conversionSuccessful = Int32.TryParse(userInput, out userIntInput);

                if (!conversionSuccessful)
                {
                    Console.WriteLine();
                    Console.WriteLine(errorMessage);
                }

            } while (!conversionSuccessful);


            return userIntInput;
        }

        public static string GetStringWithPrompt(string? prompt, string? errorMessage)
        {
            if (!String.IsNullOrEmpty(prompt))
                Console.Write(prompt);
            

            do
            {
                string? userInput = Console.ReadLine();

                if (!String.IsNullOrEmpty(userInput))
                {
                    return userInput;
                }
                else
                {
                    if (!String.IsNullOrEmpty(errorMessage))
                        Console.Write(errorMessage);
                }
            } while (true);
        }
    }
}