<!-- TITLE: Coding Conventions -->
<!-- SUBTITLE: A Collection of Coding Conventions for Clever Crafting of Classic Code -->

# C#
- using directives
	- Each directive shall be on its own line
	- All directives (including `System` and `Microsoft` namespaces) shall be sorted in alphabetical order with no blank lines between them
	- Unless a name must be fully qualified to avoid ambiguity, `using` directives shall be used for all referenced namespaces
- Blank lines
	- There shall be two blank lines between the `using` directives and the rest of the code
	- There shall be two blank lines between each of the following and any surrounding code (except an opening or closing curly bracket)
		- Property (unless uncommented in an interface)
		- Method (unless uncommented in an interface)
		- Event (unless uncommented)
		- Constructor
		- Type (e.g. class, structure, delegate, enum)
	- Concerning fields, constants, and uncommented events
		- There shall be two blank lines before them unless they are immediately preceded by an opening curly bracket
		- There shall be two blank lines after them unless they are immediately followed by a closing curly bracket
		- There may be single blank lines between them to separate them into logical groups
	- Within a method, there may be single blank lines to separate the code into logical blocks, though this should should be kept to a minimum, as it is often the sign of code that should be split into multiple smaller methods.
- Member Order
	- The members within a type shall be arranged in the following order
		- Constructors
		- Events
		- Public properties
		- Public methods
		- Non-public event handlers
		- Non-public vanilla callbacks
		- Other methods and properties
		- Fields
		- Constants
		- Nested Types
	- Within each of the sections, where applicable, the members shall be ordered by dependency, with the outermost entry members listed first and the innermost helper method listed last.
- Comments
	- Text shall be wrapped at 100 characters from the first position after the initial slashes and indent spaces (e.g. if the overall comment text starts at column 13, it will wrap at 113)
	- Concerning double-slash comments 
		- There shall be a single space between the slashes and the text
	- Concerning triple-slash (XML) comments
		- All empty or useless top-level XML elements shall be removed
		- All opening and closing tags for top-level XML elements shall be on their own line
		- All content shall be on its own line(s), between the opening and closing tag lines, indented with four spaces (from the position of the enclosing XML tags)
- Names
	- Except for the following exceptions, the [Microsoft-recommended naming conventions](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines) should be followed
		- Note: As the MS guidelines state, all methods should be PascalCase, even private ones
	- Two-letter (and longer) acronyms and abbreviations (e.g. `Ui`, `Io`, `Id`) should be treated as a single word (i.e. the second letter should be lower-case when using camelCasing or PascalCasing)
	- Fields should have camelCase names with an underscore as a prefix (e.g. `_myField`)
	- Enumerations should have a suffix of `Enum` or `Flags` on their type name and the name should follow singular form (e.g. `ColorEnum` and `FormatFlags` instead of `Colors` and `Formats`)
	- For namespaces, see the [Project and Namespace Guidelines](project-and-namespace-guidelines)
- Brackets
	- Curly brackets should be used on all code blocks, even when the block contains only one statement
	- Expression-bodied syntax (i.e. `=>`) should only be used for properties
- Accessibility
	- The accessibility should always be explicitly declared on all types and members, even when it is the default
	- All non-nested types should be `internal` unless they are needed by other projects

## Example

```csharp
using IDN.Example;
using System;


namespace CodingConventions
{
    public class MyClass
    {
        public MyClass(int value)
        {
            _field1 = value;
        }


        public event EventHandler Event1;
        public event EventHandler Event2;


        public int Property1 => _const1;


        public int Property2
        {
            get => _field1;
            set
            {
                if(ValueIsValid(value))
                {
                    _field1 = value;
                }
            }
        }


        /// <summary>
        ///     Does the thing
        /// </summary>
        /// <remarks>
        ///     This comment should wrap at column 117, since the "T" in the word "This", at the beginning of the 
        ///     comment, is in column 17.
        /// </remarks>
        public void DoIt()
        {
            // Prepare
            Prepare1();
            Prepare2();

            // Do it
            DoSomething1();
            DoSomething2();
        }


        private bool ValueIsValid(int value)
        {
            return (value > 0) && (ValueIsSmall(value));
        }


        private bool ValueIsSmall(int value)
        {
            return value < 100;
        }


        private int _field1;
        private MyEnum _field2;
        private const int _const1 = 1;
        private const int _const2 = 2;


        private enum MyEnum
        {
            One,
            Two
        }
    }
}
```
