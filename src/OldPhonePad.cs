using System;
using System.Text;

namespace OldPhonePad.DictionaryState
{
    /// <summary>
    /// Decodes old T9-style phone keypad input into text output.
    /// This implementation uses a dictionary-based approach with state tracking.
    /// </summary>
    public static class OldPhonePadDecoder
    {
        // Keypad mapping - just like the good old days of flip phones
        private static readonly Dictionary<char, string> KeypadMap = new()
        {
            { '1', "&'(" },
            { '2', "abc" },
            { '3', "def" },
            { '4', "ghi" },
            { '5', "jkl" },
            { '6', "mno" },
            { '7', "pqrs" },
            { '8', "tuv" },
            { '9', "wxyz" },
            { '0', " " }
        };

        /// <summary>
        /// Decodes a T9-style phone keypad input string into readable text.
        /// </summary>
        /// <param name="input">The input string containing digits, spaces, '*' (backspace), and '#' (send).</param>
        /// <returns>The decoded text string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when input is null.</exception>
        /// <exception cref="ArgumentException">Thrown when input doesn't contain the send character '#'.</exception>
        public static string OldPhonePad(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");
            }

            if (!input.Contains('#'))
            {
                throw new ArgumentException("Input must contain the send character '#'.", nameof(input));
            }

            var result = new StringBuilder();
            char currentKey = '\0';  // tracks which key we're pressing
            int pressCount = 0;       // how many times we've hit it

            foreach (char c in input)
            {
                // Hit send? We're done here
                if (c == '#')
                {
                    // Flush any pending character before exiting
                    if (currentKey != '\0' && pressCount > 0)
                    {
                        result.Append(GetCharacterFromKey(currentKey, pressCount));
                    }
                    break;
                }

                // Backspace - delete the last character if there is one
                if (c == '*')
                {
                    // First, commit any pending key presses
                    if (currentKey != '\0' && pressCount > 0)
                    {
                        result.Append(GetCharacterFromKey(currentKey, pressCount));
                        currentKey = '\0';
                        pressCount = 0;
                    }

                    // Now backspace the output
                    if (result.Length > 0)
                    {
                        result.Length--;
                    }
                    continue;
                }

                // Space means we're pausing - commit current key and reset
                if (c == ' ')
                {
                    if (currentKey != '\0' && pressCount > 0)
                    {
                        result.Append(GetCharacterFromKey(currentKey, pressCount));
                    }
                    currentKey = '\0';
                    pressCount = 0;
                    continue;
                }

                // If it's a digit key
                if (char.IsDigit(c))
                {
                    // Same key as before? Increment the press count
                    if (c == currentKey)
                    {
                        pressCount++;
                    }
                    else
                    {
                        // Different key - commit the previous one and start fresh
                        if (currentKey != '\0' && pressCount > 0)
                        {
                            result.Append(GetCharacterFromKey(currentKey, pressCount));
                        }
                        currentKey = c;
                        pressCount = 1;
                    }
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Gets the character from a given key based on the number of presses.
        /// Cycles through available characters if presses exceed the count.
        /// </summary>
        private static char GetCharacterFromKey(char key, int presses)
        {
            if (!KeypadMap.TryGetValue(key, out string? chars))
            {
                // Invalid key - just return empty (could throw instead)
                return '\0';
            }

            if (string.IsNullOrEmpty(chars))
            {
                return '\0';
            }

            // Cycle through characters - if you press too many times, it wraps around
            // Just like when you'd overshoot the letter you wanted!
            int index = (presses - 1) % chars.Length;
            return char.ToUpper(chars[index]);
        }
    }
}
