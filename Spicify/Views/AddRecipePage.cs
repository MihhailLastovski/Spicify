using Spicify.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Spicify.Views
{
    public class AddRecipePage : ContentPage
    {
        private Entry recipeTitleEntry;
        private Button selectImageButton;
        private Label selectedImageLabel;
        private Entry descriptionEntry;
        private Entry ingredientsEntry;
        private Entry instructionsEntry;
        private Button addButton;
        private Database database = new Database("database.db3");

        private string selectedImagePath;

        public AddRecipePage()
        {
            Title = "Add Recipe";

            recipeTitleEntry = new Entry
            {
                Placeholder = "Recipe Title"
            };

            selectImageButton = new Button
            {
                Text = "Select Image",
                Command = new Command(SelectImage)
            };

            selectedImageLabel = new Label();

            descriptionEntry = new Entry
            {
                Placeholder = "Description"
            };

            ingredientsEntry = new Entry
            {
                Placeholder = "Ingredients *please separate ingredients with commas*"
            };

            instructionsEntry = new Entry
            {
                Placeholder = "Cooking Instruction"
            };
            addButton = new Button
            {
                Text = "Add Recipe",
                Command = new Command(AddRecipe)
            };

            Content = new StackLayout
            {
                Margin = new Thickness(20),
                Children = {
                recipeTitleEntry,
                selectImageButton,
                selectedImageLabel,
                descriptionEntry,
                ingredientsEntry,
                instructionsEntry,
                addButton
            }
            };
        }

        private async void SelectImage()
        {
            try
            {
                FileResult result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Select an Image"
                });

                if (result != null)
                {
                    selectedImagePath = result.FullPath;
                    selectedImageLabel.Text = selectedImagePath;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void AddRecipe()
        {
            if (string.IsNullOrWhiteSpace(recipeTitleEntry.Text))
            {
                await DisplayAlert("Error", "Recipe title cannot be empty", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(selectedImagePath))
            {
                await DisplayAlert("Error", "Please select an image", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(descriptionEntry.Text))
            {
                await DisplayAlert("Error", "Description cannot be empty", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(ingredientsEntry.Text))
            {
                await DisplayAlert("Error", "Ingredients cannot be empty", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(instructionsEntry.Text))
            {
                await DisplayAlert("Error", "Cooking Instruction cannot be empty", "OK");
                return;
            }

            string localFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string localImagePath = Path.Combine(localFolderPath, Path.GetFileName(selectedImagePath));
            File.Copy(selectedImagePath, localImagePath, true);

            Models.Recipe newRecipe = new Models.Recipe
            {
                RecipeTitle = recipeTitleEntry.Text,
                ImageSource = localImagePath,
                Description = descriptionEntry.Text,
                Ingredients = ingredientsEntry.Text,
                CookingInstruction = instructionsEntry.Text,
                UserID = Database.CurrentUser.Id
            };

            await database.CreateRecipeAsync(newRecipe);

            await DisplayAlert("Success", "Recipe added successfully", "OK");

            recipeTitleEntry.Text = string.Empty;
            selectedImagePath = null;
            selectedImageLabel.Text = string.Empty;
            descriptionEntry.Text = string.Empty;
            ingredientsEntry.Text = string.Empty;
            instructionsEntry.Text = string.Empty;
        }
    }



}