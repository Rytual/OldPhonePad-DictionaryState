# OldPhonePad - Dictionary State Implementation

```
 ___________________
|  _____________  |
| |             | |
| |   CODING    | |
| |  CHALLENGE  | |
| |_____________| |
|  __ __ __  _    |
| |__|__|__||_|   |
| |__|__|__||_|   |
| |__|__|__||_|   |
|   1   2   3     |
```

## Remember T9 Texting?

This project takes me back to the days when sending "HELLO" required pressing `4433555 555666#`. Before smartphones and autocorrect, we had to manually cycle through letters on numeric keypads. This was the golden age of thumb cramps and accidentally sending "GPPD" instead of "GOOD" because you lost count.

This implementation solves the classic T9-style phone keypad decoder challenge using a **dictionary-based approach with state tracking**.

## The Challenge

Decode old phone keypad input into readable text. The keypad layout:

```
1: &'(        2: abc       3: def
4: ghi        5: jkl       6: mno
7: pqrs       8: tuv       9: wxyz
*: backspace  0: space     #: send
```

### Examples

- `33#` → `E`
- `227*#` → `B` (type CA, backspace)
- `4433555 555666#` → `HELLO`
- `8 88777444666*664#` → `TURING`

## Why Dictionary State?

This approach uses a simple dictionary mapping and state variables to track:
- Which key we're currently pressing
- How many times we've pressed it
- When to commit a character (on space, key change, or send)

**Pros:**
- Straightforward and easy to understand
- Minimal memory overhead
- Fast lookups with O(1) dictionary access
- Natural handling of consecutive presses

**Cons:**
- State management can get complex with edge cases
- Manual tracking of current key and press count
- Not as elegant as some functional approaches

Good for when you need clarity over cleverness. Like explaining code to a teammate at 2 AM before a deployment.

## Getting Started

### Prerequisites

- .NET 8.0 or later
- That's it!

### Running the Code

```bash
# Clone the repository
git clone https://github.com/yourusername/OldPhonePad-DictionaryState.git
cd OldPhonePad-DictionaryState

# Build the project
dotnet build

# Run tests
dotnet test

# For a specific test with verbose output
dotnet test --logger "console;verbosity=detailed"
```

### Using the Decoder

```csharp
using OldPhonePad.DictionaryState;

string result = OldPhonePadDecoder.OldPhonePad("4433555 555666#");
Console.WriteLine(result); // Output: HELLO
```

## Test Coverage

This project includes 40+ unit tests covering:

- All provided examples
- Edge cases (empty input, multiple backspaces, excessive spaces)
- Single character decoding for all keys
- Cycling behavior (pressing a key more times than it has letters)
- Pause handling (spaces between same-key presses)
- Backspace operations (including backspacing empty strings)
- Special keys (symbols on key 1, space on key 0)
- Complex real-world scenarios - SOS, HELLO WORLD and stuff like that
- Error handling (null input, missing send character)
- Stress tests with long inputs and many backspaces

Run the tests and watch them pass. Feels like successfully sending an SMS on a flip phone.

## Project Structure

```
OldPhonePad-DictionaryState/
├── src/
│   ├── OldPhonePad.cs                    # Main decoder implementation
│   └── OldPhonePad.DictionaryState.csproj
├── tests/
│   ├── OldPhonePadTests.cs              # Test suite
│   └── OldPhonePad.DictionaryState.Tests.csproj
├── .github/
│   └── workflows/
│       └── dotnet.yml                    # CI/CD pipeline
├── .gitignore
├── LICENSE
└── README.md
```

## Implementation Details

The algorithm maintains two key pieces of state:
1. `currentKey` - which digit key is being pressed
2. `pressCount` - how many consecutive presses

When processing input:
- **Digit keys**: If same as current, increment count; if different, commit previous and start new
- **Space**: Commit current character and reset state (allows same-key repeats)
- **Backspace (*)**: Commit pending character, then remove last character from result
- **Send (#)**: Commit any pending character and return result

The character lookup uses modulo arithmetic to handle cycling when you press too many times.

## Extensions & Ideas

Want to take this further? Some ideas:

- Add support for punctuation and numbers
- Create a console demo where you can type in real-time
- Implement T9 predictive text with dictionary lookups
- Add a web API wrapper
- Create a reverse encoder (text → key presses)
- Build a GUI that looks like an old Nokia phone (peak nostalgia)

## Alternatives

Check out my other implementations of the same problem:
- **OldPhonePad-FSM**: Uses a finite state machine for more formal state transitions
- **OldPhonePad-Grouping**: Groups consecutive digits before processing
- **OldPhonePad-OOP**: Object-oriented design with separate Keypad and Decoder classes
- **OldPhonePad-RegexStack**: Regex preprocessing with stack-based evaluation

Each one has different trade-offs in readability, performance and extensibility.

## Contributing

Found a bug? Have an improvement? Feel free to open an issue or submit a pull request. This was built as a coding challenge solution, but I'm always happy to make it better.

Please follow standard C# conventions and include tests for any new functionality.

## License

MIT License - see LICENSE file for details. Use it, modify it, share it. Maybe give a nod to the glory days of T9 texting.

## Acknowledgments

- Iron Software for the coding challenge that sparked this trip down memory lane
- Nokia, for teaching an entire generation how to text without looking at the phone
- Anyone who ever sent a text message that took 5 minutes to type because you kept overshooting letters

---

Built with nostalgia and a mild case of RSI from testing on an actual old phone. In the early 2000s we were basically training for software engineering by mastering T9.

*Last updated: October 2025*
