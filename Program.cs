// the ourAnimals array will store the following: 
using System.Globalization;

string animalSpecies = "";
string animalID = "";
string animalAge = "";
string animalPhysicalDescription = "";
string animalPersonalityDescription = "";
string animalNickname = "";
bool exit = false;
// variables that support data entry
int maxPets = 8;
string? readResult;
string menuSelection = "";
// array used to store runtime data, there is no persisted data
string[,] ourAnimals = new string[maxPets, 6];

// TODO: Convert the if-elseif-else construct to a switch statement

// create some initial ourAnimals array entries
for (int i = 0; i < maxPets; i++)
{
    switch (i)
    {
        case 0:
            animalSpecies = "dog";
            animalID = "d1";
            animalAge = "2";
            animalPhysicalDescription = "medium sized cream colored female golden retriever weighing about 65 pounds. housebroken.";
            animalPersonalityDescription = "loves to have her belly rubbed and likes to chase her tail. gives lots of kisses.";
            animalNickname = "lola";
            break;
        case 1:
            animalSpecies = "dog";
            animalID = "d2";
            animalAge = "9";
            animalPhysicalDescription = "large reddish-brown male golden retriever weighing about 85 pounds. housebroken.";
            animalPersonalityDescription = "loves to have his ears rubbed when he greets you at the door, or at any time! loves to lean-in and give doggy hugs.";
            animalNickname = "loki";
            break;
        case 2:
            animalSpecies = "cat";
            animalID = "c3";
            animalAge = "1";
            animalPhysicalDescription = "small white female weighing about 8 pounds. litter box trained.";
            animalPersonalityDescription = "friendly";
            animalNickname = "Puss";
            break;
        case 3:
            animalSpecies = "cat";
            animalID = "c4";
            animalAge = "?";
            animalPhysicalDescription = "";
            animalPersonalityDescription = "";
            animalNickname = "";
            break;
        default:
            animalSpecies = "";
            animalID = "";
            animalAge = "";
            animalPhysicalDescription = "";
            animalPersonalityDescription = "";
            animalNickname = "";
            break;
    }

    ourAnimals[i, 0] = "ID #: " + animalID;
    ourAnimals[i, 1] = "Species: " + animalSpecies;
    ourAnimals[i, 2] = "Age: " + animalAge;
    ourAnimals[i, 3] = "Nickname: " + animalNickname;
    ourAnimals[i, 4] = "Physical description: " + animalPhysicalDescription;
    ourAnimals[i, 5] = "Personality: " + animalPersonalityDescription;
}

// display the top-level menu options

Console.Clear();

Console.WriteLine("Welcome to the Contoso PetFriends app. Your main menu options are:");
Console.WriteLine(" 1. List all of our current pet information");
Console.WriteLine(" 2. Add a new animal friend to the ourAnimals array");
Console.WriteLine(" 3. Ensure animal ages and physical descriptions are complete");
Console.WriteLine(" 4. Ensure animal nicknames and personality descriptions are complete");
Console.WriteLine(" 5. Edit an animal’s age");
Console.WriteLine(" 6. Edit an animal’s personality description");
Console.WriteLine(" 7. Display all cats with a specified characteristic");
Console.WriteLine(" 8. Display all dogs with a specified characteristic");

do
{
    Console.WriteLine("\nEnter your selection number (or type Exit to exit the program)");
    readResult = Console.ReadLine();

    if (readResult != null)
    {
        menuSelection = readResult.ToLower();
    }
    exit = String.Equals(readResult.Trim(), "exit", StringComparison.OrdinalIgnoreCase) ? true : false;
    if (exit)
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }
    Console.WriteLine($"\nYou selected {menuSelection} from the menu.\n");

    int GetAnimalCount()
    {
        int animalCount = 0;
        for (int i = 0; i < ourAnimals.GetLength(0); i++)
        {
            if (ourAnimals[i, 0] != "ID #: ")
            {
                animalCount++;
            }
        }
        return animalCount;
    }

    string PluralizeSpecies(string species)
         //I've written this method as an expression-bodied method, which's syntax is simply that instead of wrapping the entire code block with {} and writing "return" I use a single arrow at the start. This only works for methods that consist of a single expression
         => species switch
         {
             "cat" => "cats",
             "dog" => "dogs",
             "rabbit" => "rabbits",
             "sheep" => "sheep",
             "fox" => "foxes",
             _ => species + "s"  // Default rule (may not work for all words)
         };

    string GetFormattedSpeciesList(string[] species)
    {
        //Select() grabs each element of the array and applies an action to it. In this case it calls the PluralizeSpecies() method with it as an argument
        string[] pluralSpecies = species.Select(PluralizeSpecies).ToArray();

        return pluralSpecies.Length switch
        {
            1 => pluralSpecies[0],  // "cats"
            2 => $"{pluralSpecies[0]} and {pluralSpecies[1]}",  // "cats and dogs"
            _ => string.Join(", ", pluralSpecies[..^1]) + $", and {pluralSpecies[^1]}"  // "cats, dogs, and rabbits"
        };
    }

    bool ValidEntryCheck(string[] validValues, string newEntryValue)
    {
        bool isValidEntry = Array.Exists(validValues, value => value.Equals(newEntryValue.Trim(), StringComparison.OrdinalIgnoreCase));
        return isValidEntry;
    }

    string CapitalizeNickname(string nickname, CultureInfo culture = null)
    {
        if (string.IsNullOrWhiteSpace(nickname))
        {
            return nickname;
        }

        //taking into account culture differences (capitalized i for example in Turkish). ??= is the null-coalescing assignment operator and it only assigns the right-hand value to the variable if it's null (if method caller doesn't pass a culture argument)
        culture ??= CultureInfo.CurrentCulture;
        //nickname[1..] is the same as nickname.substring(1);. Aside from that I'm just grabbing the first character from the nickname string, capitalizing it while taking into account the culture passed to the function or the current if nothing's passed and then concatenating it with the rest of the nickname
        return char.ToUpper(nickname[0], culture) + nickname[1..];
    }

    void InsertValues()
    {
        string[] labels = { "ID #: ", "Species: ", "Age: ", "Nickname: ", "Physical description: ", "Personality: " };
        string[] values = { animalID, animalSpecies, animalAge, animalNickname, animalPhysicalDescription, animalPersonalityDescription };

        int row = GetAnimalCount();
        for (int column = 0; column < labels.Length; column++)
        {
            ourAnimals[row, column] = labels[column] + values[column];
        }

        Console.Clear();
        Console.WriteLine("Your pet has been successfully registered!");
    }

    switch (readResult)
    {
        case "1":
            for (int i = 0; i < ourAnimals.GetLength(0); i++)
            {
                for (int j = 0; j < ourAnimals.GetLength(1); j++)
                {
                    if (ourAnimals[i, 0] != "ID #: ")
                    {
                        if (j == ourAnimals.GetLength(1) - 1)
                        {
                            Console.WriteLine($"{ourAnimals[i, j]}\n");
                        }
                        else
                        {
                            Console.WriteLine(ourAnimals[i, j]);
                        }
                    }
                }
            }
            Console.WriteLine($"There are {GetAnimalCount()} animals in our care!");
            if (GetAnimalCount() < maxPets)
            {
                Console.WriteLine($"We have room for {maxPets - GetAnimalCount()} more and all are welcome");
            }

            break;
        case "2":
            bool addPet = true;
            while (addPet)
            {
                if (GetAnimalCount() < maxPets)
                {
                    string[] positiveAnswers = { "yes", "y", "yeah", "yup" };
                    string[] negativeAnswers = { "no", "n", "nope", "nop" };
                    string[] acceptedSpecies = { "cat", "dog" };
                    string userAnswer = "";


                    //get animal species
                    Console.WriteLine($"We have room for {maxPets - GetAnimalCount()} more and we're currently accepting {GetFormattedSpeciesList(acceptedSpecies)}. What species is the lovable creature we're to tend to?");
                    string newEntrySpecies = Console.ReadLine();
                    if (ValidEntryCheck(acceptedSpecies, newEntrySpecies))
                    {

                        animalSpecies = newEntrySpecies;
                        animalID = animalSpecies.Substring(0, 1) + (GetAnimalCount() + 1).ToString();
                    }
                    else
                    {
                        Console.WriteLine($"\nWe only accept {GetFormattedSpeciesList(acceptedSpecies)}, for now, sorry.");
                        break;
                    }

                    //get animal age
                    Console.WriteLine("\nWhat the new animal's age? Enter a number or leave blank and press Enter if you aren't sure:");
                    string newEntryAge = Console.ReadLine().Trim();

                    if (!String.IsNullOrWhiteSpace(newEntryAge))
                    {
                        int parsedAge;
                        bool isValidEntry = int.TryParse(newEntryAge, out parsedAge);

                        while (!isValidEntry)
                        {
                            Console.WriteLine("\nPlease enter a number or leave blank if age is unknown and press Enter:");
                            newEntryAge = Console.ReadLine().Trim();
                            if (String.IsNullOrWhiteSpace(newEntryAge))
                            {
                                animalAge = "?";
                                break;
                            }
                            isValidEntry = int.TryParse(newEntryAge, out parsedAge);
                        }
                        if (isValidEntry)
                        {
                            animalAge = parsedAge.ToString();
                        }
                    }
                    else
                    {
                        animalAge = "?";
                    }

                    //get animal physical description - can be blank
                    Console.WriteLine("\nEnter a physical description of the pet (size, color, gender, weight, housebroken). You can leave it blank and press Enter if you aren't sure:");
                    string newEntryPhysicalDescription = Console.ReadLine().Trim().ToLower();
                    //the following if statement along with its replacement null-coalescing expression is wrongfully assuming that if the user enters blank, newEntryPhysicalDescription will be null. It will be "" (empry string, not null)
                    //if (newEntryPhysicalDescription == null)
                    //{
                    //    animalPhysicalDescription = "tbd";
                    //}
                    //else
                    //{
                    //    animalPhysicalDescription = newEntryPhysicalDescription;
                    //}
                    //The ?? operator is the null-coalescing operator. It checks if newEntryPhysicalDescription is null, and if it is, it assigns "tbd" to animalPhysicalDescription. Otherwise, it assigns newEntryPhysicalDescription
                    //animalPhysicalDescription = newEntryPhysicalDescription ?? "tbd";

                    //this correctly checks for empty strings and performs what I wanted to do with the above block
                    animalPhysicalDescription = string.IsNullOrWhiteSpace(newEntryPhysicalDescription) ? "tbd" : newEntryPhysicalDescription;

                    //get a description of the pet's personality - can be blank.
                    Console.WriteLine("\nEnter a description of the pet's personality (likes or dislikes, tricks, energy level). You can leave it blank and press Enter if you aren't sure:");
                    string newEntryPersonality = Console.ReadLine().Trim();
                    animalPersonalityDescription = string.IsNullOrWhiteSpace(newEntryPersonality) ? "tbd" : newEntryPersonality;

                    //get the pet's nickname - can be blank.
                    Console.WriteLine("\nEnter a nickname for the pet:");
                    string newEntryNickname = Console.ReadLine().Trim().ToLower();
                    string capitalizedNickname = CapitalizeNickname(newEntryNickname);
                    animalNickname = string.IsNullOrWhiteSpace(capitalizedNickname) ? "tbd" : capitalizedNickname;

                    //store the pet information in the ourAnimals array (zero based)
                    //this block was included in the tutorial. I've commented it because I can use the few lines after it to not have to hard code each 2nd dimension value and automate the process a bit on top of that
                    //ourAnimals[GetAnimalCount(), 0] = "ID #: " + animalID;
                    //ourAnimals[GetAnimalCount(), 1] = "Species: " + animalSpecies;
                    //ourAnimals[GetAnimalCount(), 2] = "Age: " + animalAge;
                    //ourAnimals[GetAnimalCount(), 3] = "Nickname: " + animalNickname;
                    //ourAnimals[GetAnimalCount(), 4] = "Physical description: " + animalPhysicalDescription;
                    //ourAnimals[GetAnimalCount(), 5] = "Personality: " + animalPersonalityDescription;

                    InsertValues();

                    bool additionalEntryFirstAsk = true;
                    do
                    {
                        if (additionalEntryFirstAsk)
                        {
                            Console.WriteLine("Would you like to enter an additional furry friend? (y/n)\n");
                        }
                        else
                        {
                            Console.WriteLine("Would you like to enter an additional furry friend? Please answer some common form of yes or no:\n");
                        }

                        userAnswer = Console.ReadLine();
                        additionalEntryFirstAsk = false;

                    } while (!Array.Exists(positiveAnswers, ans => ans.Equals(userAnswer.Trim(), StringComparison.OrdinalIgnoreCase)) && (!Array.Exists(negativeAnswers, ans => ans.Equals(userAnswer.Trim(), StringComparison.OrdinalIgnoreCase))));

                    addPet = Array.Exists(positiveAnswers, ans => ans.Equals(userAnswer.Trim(), StringComparison.OrdinalIgnoreCase)) ? true : false;
                }
                else if (GetAnimalCount() >= maxPets)
                {
                    Console.WriteLine("We're currently at full capacity and not accepting new arrivals.\n");
                    break;
                }
            }
            break;
        case "3":
            Console.WriteLine("3. Ensure animal ages and physical descriptions are complete\n");
            break;
        case "4":
            Console.WriteLine("4. Ensure animal nicknames and personality descriptions are complete\n");
            break;
        case "5":
            Console.WriteLine("5. Edit an animal’s age\n");
            break;
        case "6":
            Console.WriteLine("6. Edit an animal’s personality description\n");
            break;
        case "7":
            Console.WriteLine("7. Display all cats with a specified characteristic\n");
            break;
        case "8":
            Console.WriteLine("8. Display all dogs with a specified characteristic\n");
            break;
    }
}
while (!exit);


// pause code execution
readResult = Console.ReadLine();
