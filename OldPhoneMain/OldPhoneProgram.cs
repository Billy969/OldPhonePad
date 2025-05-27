using System.Text;

public class OldPhoneProgram
{
    public static void Main(string[] args)
    {
        //First message when it's run
        Console.WriteLine("OldPhonePad (end with #):");

        // This StringBuilder stores the user input character by character
        StringBuilder inputBuilder = new StringBuilder();

        // Loop to read each character until '#' is entered
        while (true)
        {
            // Read a key from the console without displaying it automatically
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char ch = keyInfo.KeyChar;

            // If the '#' key is pressed, simulate pressing Enter and exit the loop
            if (ch == '#')
            {
                Console.WriteLine(); // Simulate "Enter"
                break;
            }

            Console.Write(ch); // Display the character in the console
            inputBuilder.Append(ch); // Add the character to the input
        }

        // Convert the StringBuilder to a string
        string input = inputBuilder.ToString();

        // If the input is empty, display a message and exit
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Nothing was entered.");
            return;
        }

        // Process the input and get the output
        string output = OldPhonePad(input);

        // Display the final output
        Console.WriteLine("Output: " + output);
    }

    // This function converts the input based on old phone keypad logic
    public static string OldPhonePad(string input)
    {
        if (string.IsNullOrEmpty(input)) return "";

        // Dictionary that maps numeric keys to letters like on an old phone
        Dictionary<char, string> keypad = new Dictionary<char, string>
        {
            {'1', "&'("}, {'2', "ABC"}, {'3', "DEF"}, {'4', "GHI"},
            {'5', "JKL"}, {'6', "MNO"}, {'7', "PQRS"},
            {'8', "TUV"}, {'9', "WXYZ"}, {'0', " "}
        };

        // Result string where the final text will be built
        StringBuilder result = new StringBuilder();
        // Temporary storage for repeated key presses
        StringBuilder currentGroup = new StringBuilder();

        // Loop through each character in the input
        for (int i = 0; i < input.Length; i++)
        {
            char ch = input[i];

            // If a new key is pressed (different from the previous), process the group
            if (currentGroup.Length > 0 && currentGroup[0] != ch)
            {
                ProcessGroup(currentGroup, keypad, result);
                currentGroup.Clear(); // Reset the group
            }

            // Add the current character to the group
            currentGroup.Append(ch);

            // If the character is '*', remove the last character from the result (like backspace)
            if (ch == '*')
            {
                if (result.Length > 0)
                    result.Length--;
            }
        }

        // Process the last group after the loop ends
        ProcessGroup(currentGroup, keypad, result);

        return result.ToString();
    }

    // This function translates a group of key presses into a single letter
    private static void ProcessGroup(StringBuilder group, Dictionary<char, string> keypad, StringBuilder result)
    {
        if (group.Length == 0) return;

        char key = group[0];

        // If the key does not exist in the keypad, exit
        if (!keypad.ContainsKey(key)) return;

        string letters = keypad[key];

        if (letters.Length == 0) return;

        // Determine which letter to use based on how many times the key was pressed
        int index = (group.Length - 1) % letters.Length;

        // Add the corresponding letter to the result
        result.Append(letters[index]);
    }
}