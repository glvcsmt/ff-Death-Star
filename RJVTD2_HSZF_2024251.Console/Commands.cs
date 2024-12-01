using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJVTD2_HSZF_2024251.Console;

// The Commands class provides helper methods to interact with the user
// and prompt for input through the Spectre.Console library.
// It simplifies collecting integer and string input from the user.
public static class Commands
{
    // Prompts the user to enter an integer value.
    // Displays the provided prompt message and returns the user's input as an integer.
    public static int GetInt(string p)
    {
        // Use the Spectre.Console library to create a prompt for an integer input.
        int n = AnsiConsole.Prompt(new TextPrompt<int>(p));
        // Print a blank line after receiving the input to enhance user experience.
        System.Console.WriteLine();
        // Return the parsed integer input.
        return n;
    }
    
    // Prompts the user to enter a string value.
    // Displays the provided prompt message and returns the user's input as a string.
    public static string GetString(string p)
    {
        // Use the Spectre.Console library to create a prompt for a string input.
        string s = AnsiConsole.Prompt(new TextPrompt<string>(p));
        // Print a blank line after receiving the input to enhance user experience.
        System.Console.WriteLine();
        // Return the string input.
        return s;
    }
}