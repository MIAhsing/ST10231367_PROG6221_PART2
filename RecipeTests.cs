using NUnit.Framework;

[TestFixture]
public class RecipeTests
{
    [Test]
    public void CalculateTotalCalories_ShouldReturnCorrectSum()
    {
        // Arrange
        Recipe recipe = new Recipe();
        List<object> recipeInfo = new List<object>();
        List<List<string>> ingredients = new List<List<string>>();
        List<string> steps = new List<string>();

        // Create a sample recipe with known calorie values
        ingredients.Add(new List<string> { "Ingredient 1", "1", "unit", "100" });
        ingredients.Add(new List<string> { "Ingredient 2", "2", "unit", "200" });
        steps.Add("Step 1");
        steps.Add("Step 2");

        recipeInfo.Add("Test Recipe");
        recipeInfo.Add(ingredients);
        recipeInfo.Add(steps);

        recipe.AddRecipe(); // Add the recipe to the mainList

        // Act
        int totalCalories = recipe.CalculateTotalCalories(recipeInfo);

        // Assert
        Assert.AreEqual(300, totalCalories);
    }
    private static void Main()
    {
        

        RecipeTests runner = new RecipeTests();
        runner.CalculateTotalCalories_ShouldReturnCorrectSum();
    }
    
}
