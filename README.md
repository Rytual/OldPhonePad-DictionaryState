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

## The Challenge

Diving into this keypad thing was a fun puzzle. The goal is to decode old T9-style phone input like `4433555 555666#` into `HELLO`. Before smartphones, this is how we texted - pressing number keys multiple times to cycle through letters.

The keypad layout:

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

## My Approach

I went with a dictionary-based approach with state tracking. Seemed straightforward enough - keep track of which key you're pressing and how many times, then commit the character when something changes (new key, space, or send).

The dictionary maps each digit to its letters:
- Track `currentKey` - which digit is being pressed
- Track `pressCount` - how many consecutive times
- When the key changes or we hit space/send, commit the character

**What works well:**
- Easy to understand and debug
- Fast lookups with the dictionary
- Handles consecutive presses naturally

**What's a bit clunky:**
- State management gets tricky with edge cases
- Manual tracking of current key and press count
- Not as clean as some other approaches

Good for when you need something clear and practical. The FSM version takes this further if you're curious about more formal state transitions.

## Getting Started

### Prerequisites

- .NET 8.0 or later

### Running the Code

```bash
# Clone the repository
git clone https://github.com/yourusername/OldPhonePad-DictionaryState.git
cd OldPhonePad-DictionaryState

# Build and test
dotnet build
dotnet test

# For verbose test output
dotnet test --logger "console;verbosity=detailed"
```

### Using the Decoder

```csharp
using OldPhonePad.DictionaryState;

string result = OldPhonePadDecoder.OldPhonePad("4433555 555666#");
Console.WriteLine(result); // Output: HELLO
```

## Test Coverage

The project has 40+ tests covering:
- All provided examples
- Edge cases (empty input, multiple backspaces, excessive spaces)
- Single character decoding for all keys
- Cycling behavior (pressing a key more times than it has letters)
- Pause handling (spaces between same-key presses)
- Backspace operations
- Special keys (symbols on key 1, space on key 0)
- Complex scenarios like SOS and HELLO WORLD
- Error handling (null input, missing send character)
- Stress tests with long inputs

## Implementation Details

The algorithm is pretty simple:
- **Digit keys**: If same as current, increment count; if different, commit previous and start new
- **Space**: Commit current character and reset (allows same-key repeats)
- **Backspace (*)**: Commit pending character, then remove last character
- **Send (#)**: Commit any pending character and return result

The character lookup uses modulo arithmetic to handle cycling when you press a key too many times.

## Project Structure

```
OldPhonePad-DictionaryState/
├── src/
│   ├── OldPhonePad.cs                    # Main decoder
│   └── OldPhonePad.DictionaryState.csproj
├── tests/
│   ├── OldPhonePadTests.cs              # Test suite
│   └── OldPhonePad.DictionaryState.Tests.csproj
├── .github/
│   └── workflows/
│       └── dotnet.yml                    # CI/CD
├── .gitignore
├── LICENSE
└── README.md
```

## Other Implementations

Check out the other approaches to this problem:
- **OldPhonePad-FSM**: Uses a finite state machine for formal state transitions
- **OldPhonePad-Grouping**: Groups consecutive digits before processing
- **OldPhonePad-OOP**: Object-oriented design with separate Keypad and Decoder classes
- **OldPhonePad-RegexStack**: Regex preprocessing with stack-based evaluation

Each has different tradeoffs in readability and maintainability.

## Fun Note

This kinda reminds me of those old phone games - simple but tricky to get right. The cycling logic with modulo took a few tries to nail down. At first I tried tracking everything manually, but the modulo approach cleaned it up nicely.

## License

MIT License - see LICENSE file for details.

---

*Built for the Iron Software coding challenge - October 2025*
