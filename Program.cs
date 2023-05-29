using System;
using System.Collections.Generic;
using System.Linq;

public class Recipe
{
    private List<List<object>> mainList = new List<List<object>>();
    public RecipeNotificationDelegate recipeNotificationDelegate; // Delegate variable

    private void Menu()
    {
        while (true)
        {
            Console.WriteLine("\nCHOOSE AN OPTION");
            Console.WriteLine("1. ADD A RECIPE");
            Console.WriteLine("2. SCALE RECIPE");
            Console.WriteLine("3. PRINT RECIPE");
            Console.WriteLine("4. REMOVE ALL STORED RECIPES");
            Console.WriteLine("5. EXIT");

            string ans = Console.ReadLine();

            if (ans == "1")
            {
                AddRecipe();
            }
            else if (ans == "2")
            {
                ScaleRecipe();
            }
            else if (ans == "3")
            {
                PrintRecipes();
            }
            else if (ans == "4")
            {
                RemoveAllRecipes();
            }
            else if (ans == "5")
            {
                Console.WriteLine("EXITING PROGRAM");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("INVALID OPTION. PLEASE TRY AGAIN.");
            }
        }
    }

    // Method to add a recipe
    private void AddRecipe()
    {
        Console.WriteLine("\nHow many recipes would you like to add?");
        int numRtoAdd;

        while (!int.TryParse(Console.ReadLine(), out numRtoAdd))
        {
            Console.WriteLine("Invalid input. Please enter a valid number:");
        }

        for (int i = 0; i < numRtoAdd; i++)
        {
            Console.WriteLine($"\nEnter the name of recipe {i + 1}:");
            string recName = Console.ReadLine();

            Console.WriteLine($"\nHow many ingredients would you like to add to {recName}:");
            int ingTAdd;

            while (!int.TryParse(Console.ReadLine(), out ingTAdd))
            {
                Console.WriteLine("Invalid input. Please enter a valid number:");
            }

            List<object> recInfo = new List<object>();
            List<List<string>> ingredients = new List<List<string>>();

            Console.WriteLine("\nEnter the name, quantity, unit of measurement, and number of calories for each ingredient:");

            for (int j = 0; j < ingTAdd; j++)
            {
                Console.WriteLine($"\nName of ingredient {j + 1}:");
                string name = Console.ReadLine();

                Console.WriteLine($"\nQuantity of {name}:");
                string quantity = Console.ReadLine();

                Console.WriteLine($"\nUnit of measurement for {name}:");
                string measurement = Console.ReadLine();

                Console.WriteLine($"\nNumber of calories for {name}:");
                string calories = Console.ReadLine();

                string foodGroup = "";
                bool validFoodGroup = false;

                while (!validFoodGroup)
                {
                    Console.WriteLine($"\nEnter the food group for {name}:");
                    Console.WriteLine("Food groups:" + "\n" + "1. Fruits" + "\n" + "2. Vegetables" + "\n" + "3. Grains" + "\n" + "4. Proteins" + "\n" + "5. Dairy," + "\n" + "6. Fats/Oils" + "\n" + "7. Other");
                    foodGroup = Console.ReadLine();

                    if (foodGroup == "1" || foodGroup == "2" || foodGroup == "3" || foodGroup == "4" || foodGroup == "5" || foodGroup == "6" || foodGroup == "7")
                    {
                        validFoodGroup = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid food group option chosen. Please try again.");
                    }
                }

                List<string> ingredient = new List<string> { name, quantity, measurement, calories, foodGroup };
                ingredients.Add(ingredient);
            }

            List<string> steps = new List<string>();

            Console.WriteLine("\nEnter the step details (or enter '0' to finish):");

            int stepNumber = 1;

            while (true)
            {
                Console.WriteLine($"Step {stepNumber}:");
                string details = Console.ReadLine();

                if (details == "0")
                    break;

                steps.Add(details);
                stepNumber++;
            }

            recInfo.Add(recName);
            recInfo.Add(ingredients);
            recInfo.Add(steps);

            mainList.Add(recInfo);
        }

        Console.WriteLine("\nRECIPES ADDED SUCCESSFULLY!");
        Console.WriteLine("=======================================================================================");
    }

    // Method to print recipes
    private void PrintRecipes()
    {
        Console.WriteLine("\n=======================================================================================");
        Console.WriteLine("RECIPE REPORT");
        Console.WriteLine("=======================================================================================");

        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Print recipes in the order they were added");
        Console.WriteLine("2. Print recipes in alphabetical order by recipe name");

        string ans = Console.ReadLine();

        if (ans == "1")
        {
            Console.WriteLine("Printing recipes in the order they were added:\n");
            PrintRecipesInOrder();
        }
        else if (ans == "2")
        {
            Console.WriteLine("Printing recipes in alphabetical order by recipe name:\n");
            SortRecipes();
            PrintRecipesInOrder();
        }
        else
        {
            Console.WriteLine("Invalid option. Returning to main menu.");
        }
    }

    // Method to print recipes in the order they were added
    private void PrintRecipesInOrder()
    {
        foreach (List<object> rec in mainList)
        {
            string rname = (string)rec[0];
            List<List<string>> ings = (List<List<string>>)rec[1];
            List<string> stps = (List<string>)rec[2];

            Console.WriteLine($"\nRECIPE NAME: {rname}");
            Console.WriteLine("\nINGREDIENTS:");

            int totalCalories = 0;

            foreach (List<string> ing in ings)
            {
                Console.WriteLine($"Ingredient name: {ing[0]}");
                Console.WriteLine($"Quantity: {ing[1]}");
                Console.WriteLine($"Unit of measurement: {ing[2]}");
                Console.WriteLine($"Calories: {ing[3]}");
                Console.WriteLine($"Food Group: {GetFoodGroup(ing[4])}");
                Console.WriteLine();

                if (int.TryParse(ing[3], out int calories))
                {
                    totalCalories += calories;
                }
            }

            Console.WriteLine("RECIPE STEPS: ");

            for (int i = 0; i < stps.Count; i++)
            {
                string stepNumber = $"Step {i + 1}";
                string step = stps[i];
                Console.WriteLine($"{stepNumber}: {step}");
            }

            Console.WriteLine("\nTotal Calories: " + totalCalories);

            if (totalCalories > 300)
            {
                NotifyRecipeExceedsCalories(rname);
            }

            Console.WriteLine("\n=======================================================================================");
        }
    }

    // Method to get the food group name based on the given food group code
    private string GetFoodGroup(string foodGroupCode)
    {
        if (foodGroupCode == "1")
        {
            return "Fruits";
        }
        else if (foodGroupCode == "2")
        {
            return "Vegetables";
        }
        else if (foodGroupCode == "3")
        {
            return "Grains";
        }
        else if (foodGroupCode == "4")
        {
            return "Proteins";
        }
        else if (foodGroupCode == "5")
        {
            return "Dairy";
        }
        else if (foodGroupCode == "6")
        {
            return "Fats/Oils";
        }
        else if (foodGroupCode == "7")
        {
            return "Other";
        }
        else
        {
            return "Unknown";
        }
    }


    // Method to sort recipes alphabetically by recipe name
    private void SortRecipes()
    {
        mainList = mainList.OrderBy(rec => (string)rec[0]).ToList();
    }

    // Method to remove all recipes
    private void RemoveAllRecipes()
    {
        mainList.Clear();
        Console.WriteLine("ALL RECIPES HAVE BEEN REMOVED SUCCESSFULLY");
    }

    // Method to scale a recipe
    private void ScaleRecipe()
    {
        if (mainList.Count == 0)
        {
            Console.WriteLine("No recipes available to scale. Please add recipes first.");
            return;
        }

        Console.WriteLine("SELECT THE RECIPE YOU WOULD LIKE TO SCALE");

        for (int i = 0; i < mainList.Count; i++)
        {
            List<object> r = mainList[i];
            Console.WriteLine($"{i}. {r[0]}");
        }

        int chosenR;

        do
        {
            Console.WriteLine("Enter the number corresponding to the chosen recipe:");
        } while (!int.TryParse(Console.ReadLine(), out chosenR) || chosenR < 0 || chosenR >= mainList.Count);

        int ans;

        Console.WriteLine("SCALE BY: ");
        Console.WriteLine("1. Half (1/2)");
        Console.WriteLine("2. Double (2)");
        Console.WriteLine("3. Triple (3)");

        do
        {
            Console.WriteLine("Enter the number corresponding to the chosen scale option:");
        } while (!int.TryParse(Console.ReadLine(), out ans) || ans < 1 || ans > 3);

        List<object> selectedRecipe = mainList[chosenR];
        string rname = (string)selectedRecipe[0];
        List<List<string>> ingredients = (List<List<string>>)selectedRecipe[1];
        List<string> steps = (List<string>)selectedRecipe[2];

        Console.WriteLine("\n=======================================================================================");
        Console.WriteLine("RECIPE SCALED");
        Console.WriteLine("=======================================================================================");

        Console.WriteLine($"\nRECIPE NAME: {rname}");
        Console.WriteLine("\nINGREDIENTS:");

        foreach (List<string> ing in ingredients)
        {
            double originalQuantity = double.Parse(ing[1]);
            double scaledQuantity = 0;

            if (ans == 1)
            {
                scaledQuantity = originalQuantity / 2;
            }
            else if (ans == 2)
            {
                scaledQuantity = originalQuantity * 2;
            }
            else if (ans == 3)
            {
                scaledQuantity = originalQuantity * 3;
            }

            Console.WriteLine($"Ingredient name: {ing[0]}");
            Console.WriteLine($"Original quantity: {ing[1]}");
            Console.WriteLine($"Scaled quantity: {scaledQuantity}");
            Console.WriteLine($"Unit of measurement: {ing[2]}");
            Console.WriteLine($"Calories: {ing[3]}");
            Console.WriteLine($"Food Group: {GetFoodGroup(ing[4])}");
            Console.WriteLine();
        }

        Console.WriteLine("RECIPE STEPS: ");

        for (int i = 0; i < steps.Count; i++)
        {
            string stepNumber = $"Step {i + 1}";
            string step = steps[i];
            Console.WriteLine($"{stepNumber}: {step}");
        }

        Console.WriteLine("\n=======================================================================================");
    }

    // Method to notify the user when a recipe exceeds 300 calories
    private void NotifyRecipeExceedsCalories(string recipeName)
    {
        if (recipeNotificationDelegate != null)
        {
            recipeNotificationDelegate(recipeName);
        }
    }

    // Method to start the program
    public void StartProgram()
    {
        Console.WriteLine("=======================================================================================");
        Console.WriteLine("WELCOME TO RECIPE PRODUCTIONS!");
        Console.WriteLine("=======================================================================================");

        // Start the program by calling the Menu method
        Menu();
    }

    // Delegate definition
    public delegate void RecipeNotificationDelegate(string recipeName);

    // Notification callback method
    public void NotifyUser(string recipeName)
    {
        Console.WriteLine($"The recipe '{recipeName}' exceeds 300 calories!");
    }


}

public class Program
{
    public void Start()
    {
        Recipe recipe = new Recipe();

        // Set up the delegate to handle the notification callback
        recipe.recipeNotificationDelegate = recipe.NotifyUser;

        // Start the program
        recipe.StartProgram();
    }

    

    public static void Main(string[] args)
    {
        Program program = new Program();
        program.Start();
    }
}

