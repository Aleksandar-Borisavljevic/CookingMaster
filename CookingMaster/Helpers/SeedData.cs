//using System.Collections.ObjectModel;

//namespace CookingMaster.Helpers
//{
//    public static class SeedData
//    {
//        public static IEnumerable<IngredientCategory> GetIngredientCategories()
//        {
//            return new ObservableCollection<IngredientCategory>
//            {
//                new IngredientCategory
//                {
//                    CategoryId = 1,
//                    CategoryName = "All Ingredient",
//                    IconPath = "allingredient"
//                },
//                new IngredientCategory
//                {
//                    CategoryId = 2,
//                    CategoryName = "Fruit",
//                    IconPath = "fruit"
//                },
//                new IngredientCategory
//                {
//                    CategoryId = 3,
//                    CategoryName = "Vegetables",
//                    IconPath = "vegetables"
//                },
//                new IngredientCategory
//                {
//                    CategoryId = 4,
//                    CategoryName = "Spices",
//                    IconPath = "spices"
//                },
//                new IngredientCategory
//                {
//                    CategoryId = 5,
//                    CategoryName = "Dairy product",
//                    IconPath = "dairyproduct"
//                },
//                new IngredientCategory
//                {
//                    CategoryId = 6,
//                    CategoryName = "Meat",
//                    IconPath = "meat"
//                },
//                new IngredientCategory
//                {
//                    CategoryId = 7,
//                    CategoryName = "Seafood",
//                    IconPath = "seafood"
//                },
//                new IngredientCategory
//                {
//                    CategoryId = 8,
//                    CategoryName = "Cereals",
//                    IconPath = "cereals"
//                }
//            };
//        }
//        public static IEnumerable<Ingredient> GetIngredients()
//        {
//            return new ObservableCollection<Ingredient>
//        {
//            new Ingredient
//            {
//                IngredientId= 1,
//                IngredientCategoryId = 2,
//                IngredientName="Banana",
//                IconPath="banana",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 2,
//                IngredientCategoryId = 2,
//                IngredientName="Blackberry",
//                IconPath="blackberry",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 3,
//                IngredientCategoryId = 2,
//                IngredientName="Blue grape",
//                IconPath="bluegrape",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 4,
//                IngredientCategoryId = 2,
//                IngredientName="Green apple",
//                IconPath="greenapple",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 5,
//                IngredientCategoryId = 2,
//                IngredientName="Kiwi",
//                IconPath="kiwi",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 6,
//                IngredientCategoryId = 2,
//                IngredientName="Orange",
//                IconPath="orange",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 7,
//                IngredientCategoryId = 3,
//                IngredientName="Asparagus",
//                IconPath="asparagus",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 8,
//                IngredientCategoryId = 6,
//                IngredientName="Bacon",
//                IconPath="bacon",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 9,
//                IngredientCategoryId = 6,
//                IngredientName="Beef steak",
//                IconPath="beefsteak",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 10,
//                IngredientCategoryId = 3,
//                IngredientName="Black olive",
//                IconPath="blackolive",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 11,
//                IngredientCategoryId = 3,
//                IngredientName="Broccoli",
//                IconPath="broccoli",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 12,
//                IngredientCategoryId = 3,
//                IngredientName="Cabbage",
//                IconPath="cabbage",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 13,
//                IngredientCategoryId = 3,
//                IngredientName="Carrot",
//                IconPath="carrot",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 14,
//                IngredientCategoryId = 3,
//                IngredientName="Celeriac",
//                IconPath="celeriac",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 15,
//                IngredientCategoryId = 2,
//                IngredientName="Cherry",
//                IconPath="cherry",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 16,
//                IngredientCategoryId = 3,
//                IngredientName="Garlic",
//                IconPath="garlic",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 17,
//                IngredientCategoryId = 2,
//                IngredientName="Lemon",
//                IconPath="lemon",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 18,
//                IngredientCategoryId = 2,
//                IngredientName="Lime",
//                IconPath="lime",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 19,
//                IngredientCategoryId = 2,
//                IngredientName="Peach",
//                IconPath="peach",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 20,
//                IngredientCategoryId = 2,
//                IngredientName="Pear",
//                IconPath="pear",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 21,
//                IngredientCategoryId = 2,
//                IngredientName="Pineapple",
//                IconPath="pineapple",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 22,
//                IngredientCategoryId = 3,
//                IngredientName="Potato",
//                IconPath="potato",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 23,
//                IngredientCategoryId = 2,
//                IngredientName="Raspberry",
//                IconPath="raspberry",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 24,
//                IngredientCategoryId = 2,
//                IngredientName="Red apple",
//                IconPath="redapple",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 25,
//                IngredientCategoryId = 3,
//                IngredientName="Red cabbage",
//                IconPath="redcabbage",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 26,
//                IngredientCategoryId = 6,
//                IngredientName="Salami",
//                IconPath="salami",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 27,
//                IngredientCategoryId = 6,
//                IngredientName="Sausage",
//                IconPath="sausage",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 28,
//                IngredientCategoryId = 6,
//                IngredientName="Spicy salami rolls",
//                IconPath="Spicysalamirolls",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 29,
//                IngredientCategoryId = 2,
//                IngredientName="Strawberries",
//                IconPath="strawberries",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 30,
//                IngredientCategoryId = 5,
//                IngredientName="Cheese",
//                IconPath="cheese",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 31,
//                IngredientCategoryId = 5,
//                IngredientName="Milk",
//                IconPath="milk",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 32,
//                IngredientCategoryId = 5,
//                IngredientName="Yogurt",
//                IconPath="yogurt",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 33,
//                IngredientCategoryId = 4,
//                IngredientName="Sugar",
//                IconPath="sugar",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 34,
//                IngredientCategoryId = 4,
//                IngredientName="Oregano",
//                IconPath="oregano",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 35,
//                IngredientCategoryId = 4,
//                IngredientName="Basil",
//                IconPath="basil",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 36,
//                IngredientCategoryId = 7,
//                IngredientName="Salmon",
//                IconPath="salmon",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            },
//            new Ingredient
//            {
//                IngredientId= 37,
//                IngredientCategoryId = 7,
//                IngredientName="Canned Tuna",
//                IconPath="cannedtuna",
//                Priority = Enums.IngredientPriorityEnum.LowPriority
//            }
//        };
//        }

//        public static IngredientDescription GetDescription(int ingredientId)
//        {
//            var descriptions = new List<IngredientDescription>()
//            {
//                new IngredientDescription
//                {
//                 Ingredient = new Ingredient
//                    {
//                        IngredientId = 13,
//                        IngredientName = "Carrot",
//                        IconPath = "carrot"
//                    },
//                 Description = "Carrots are a root vegetable that are widely cultivated and consumed around the world. Here is some information about carrots:\r\n\r\nAppearance: " +
//                 "Carrots are typically long and slender with a tapered, cone-shaped end. " +
//                 "They come in a range of colors, including orange, purple, yellow, red, and white.\r\n\r\nNutrition: Carrots are a rich source of beta-carotene, " +
//                 "a pigment that the body converts into vitamin A. They are also a good source of fiber, vitamin K, potassium, and antioxidants.\r\n\r\nCulinary uses: " +
//                 "Carrots can be eaten raw or cooked, and are a common ingredient in many dishes. They can be roasted, boiled, steamed, or stir-fried, and are often added to soups, stews, " +
//                 "and salads.\r\n\r\nHistory: Carrots are thought to have originated in the Middle East and were originally cultivated for their aromatic leaves and seeds. " +
//                 "The modern carrot that we are familiar with today was developed in the Netherlands in the 17th century.\r\n\r\nCultivation: " +
//                 "Carrots are typically grown in cool to temperate climates and prefer well-drained soil with a pH of 6.0 to 6.8. " +
//                 "They can be grown from seed and typically take 70 to 80 days to mature.\r\n\r\nHealth benefits: " +
//                 "Carrots are known for their potential health benefits, including improved vision, reduced risk of heart disease, and improved immune function. " +
//                 "The high levels of antioxidants in carrots may also help to protect against cancer.\r\n\r\nIn conclusion, carrots are a versatile and nutritious vegetable that have been enjoyed for centuries. " +
//                 "They can be eaten in a variety of ways and offer a range of health benefits."
//                }
//            };

//            return descriptions.SingleOrDefault(x => x.Ingredient.IngredientId == ingredientId);
//        }

//        public static UserKitchen LoginSeed(UserLogin userLogin)
//        {
//            if (userLogin.EmailAddress != "drpetar91@gmail.com" && userLogin.Password != "Jokekoke1!")
//            {
//                return null;
//            }
//            else
//            {
//                return new UserKitchen
//                {
//                    Id = 8,
//                    UserName = "Cikastole",
//                    EmailAddress = userLogin.EmailAddress,
//                    Password = userLogin.Password,
//                    Ingredients = new List<Ingredient>
//                    {
//                        new Ingredient
//                        {
//                            IngredientId = 8,
//                            IngredientCategoryId = 6,
//                            IngredientName = "Bacon",
//                            IngredientNameEng = "Bacon",
//                            IconPath = "bacon",
//                            Priority = IngredientPriorityEnum.MediumPriority
//                        },
//                        new Ingredient
//                        {
//                            IngredientId = 11,
//                            IngredientCategoryId = 3,
//                            IngredientName = "Broccoli",
//                            IngredientNameEng = "Broccoli",
//                            IconPath = "broccoli",
//                            Priority = IngredientPriorityEnum.LowPriority
//                        },
//                        new Ingredient
//                        {
//                            IngredientId = 16,
//                            IngredientCategoryId = 3,
//                            IngredientName = "Garlic",
//                            IngredientNameEng = "Garlic",
//                            IconPath = "garlic",
//                            Priority = IngredientPriorityEnum.HighPriority
//                        },
//                        new Ingredient
//                        {
//                            IngredientId = 22,
//                            IngredientCategoryId = 3,
//                            IngredientName = "Potato",
//                            IngredientNameEng = "Potato",
//                            IconPath = "potato",
//                            Priority = IngredientPriorityEnum.HighPriority
//                        },
//                    }
//                };
//            }
//        }

//        public static List<CulinaryRecipe> GetCulinaryRecipes()
//        {
//            return new List<CulinaryRecipe>
//            {
//               new CulinaryRecipe
//               {
//                   CulinaryRecipeId = 1,
//                   UserId = 8,
//                   UserName = "Cikastole",
//                   Title = "Hash Browns With Beef Steak, Peaches, Bacon and Orange",
//                   Ingredients = new List<string>
//                   {
//                       "potato",
//                       "beefsteak",
//                       "peach",
//                       "garlic",
//                       "bacon",
//                       "redcabbage"
//                   },
//                   TypeOfMeal = TypeOfMeal.Italian,
//                   Description = "\r\n\r\nIngredients: \r\n\r\n-1 large potato, VERY thinly sliced\r\n-1 lb beef steak, cubed\r\n-1 large peach, cubed\r\n-2 cloves garlic, minced\r\n-4 strips bacon, chopped\r\n-1/3 cup red cabbage, finely chopped\r\n-1/3 cup carrot, julienned\r\n-1/3 cup cabbage, shredded\r\n-1 orange, peeled and segmented\r\n-2/3 cup blue grapes, halved\r\n-1 cup broccoli, trimmed and cut into florets \r\n-Salt and pepper, to taste\r\n\r\nInstructions: \r\n\r\n1. Preheat oven to 450 degrees F.\r\n\r\n2. Line a rimmed baking sheet with parchment paper and evenly spread the potato slices on top.\r\n\r\n3. Drizzle with olive oil, salt and pepper and bake for 10 minutes.\r\n\r\n4. Heat a large skillet over medium heat and add the beef steak cubes. Cook until browned, about 8-10 minutes.\r\n\r\n5. Add the garlic and bacon and cook for another 2 minutes.\r\n\r\n6. Add the peach cubes, red cabbage, carrot, cabbage, orange segments, grapes and broccoli and cook for 3-4 minutes or until the vegetables are tender.\r\n\r\n7. Remove the hash browns from the oven and top with the beef steak and vegetables.\r\n\r\n8. Bake for 8-10 minutes or until the hash browns are golden and crispy.\r\n\r\n9. Serve with salt and pepper to taste. Enjoy!,",
//                   IsPrivate = false

//               },
//               new CulinaryRecipe
//               {
//                   CulinaryRecipeId = 2,
//                   UserId = 8,
//                   UserName = "Cikastole",
//                   Title = "Garlic Beef Steak & Cabbage Stir Fry",
//                   Ingredients = new List<string>
//                   {
//                       "garlic",
//                       "beefsteak",
//                       "redcabbage",
//                       "carrot"
//                   },
//                   TypeOfMeal = TypeOfMeal.Mexican,
//                   Description = "\r\n\r\nIngredients\r\n\r\n• 2 tablespoons almond oil\r\n\r\n• 2 cloves garlic, minced\r\n\r\n• 1 medium onion, chopped\r\n• 1 beef steak, sliced into strips\r\n• 1 red cabbage, shredded\r\n• 1 carrot, sliced into thin coins\r\n• 1 head of broccoli, cut into florets\r\n• 1 potato, cubed\r\n• 4 slices of bacon, chopped\r\n• 1 peach, cut into wedges\r\n• 1 orange, cut into wedges\r\n• 1/2 cup of blue grapes, halved\r\n\r\nInstructions\r\n\r\n1. In a large skillet, heat the almond oil over medium-high heat.\r\n\r\n2. Add the garlic and onion and sauté until lightly golden.\r\n\r\n3. Add the beef steak and cook until browned, about 4 minutes.\r\n\r\n4. Add the red cabbage, carrots, and broccoli and stir-fry until lightly golden, about 4 minutes.\r\n\r\n5. Add the potato and bacon, stir-fry for an additional 5 minutes.\r\n\r\n6. Add the peach, orange, and blue grapes and stir-fry for an additional 2 minutes.\r\n\r\n7. Serve hot and enjoy!",
//                   IsPrivate = false

//               },
//               new CulinaryRecipe
//               {
//                   CulinaryRecipeId = 3,
//                   UserId = 8,
//                   UserName = "Cikastole",
//                   Title = "Roasted Potato & Steak Sheetpan Dinner ",
//                   Ingredients = new List<string>
//                   {
//                       "beefsteak",
//                       "potato",
//                       "bacon",
//                       "garlic",
//                       "carrot",
//                       "redcabbage",
//                       "bluegrapes",
//                       "orange",
//                       "peach",
//                       "broccoli"
//                   },
//                   TypeOfMeal = TypeOfMeal.Mexican,
//                   Description = "\r\n\r\nIngredients:  \r\n-1 lb. beef steak \r\n-6 small potatoes, quartered\r\n-6 slices of bacon, chopped\r\n-2 cloves garlic, minced\r\n-2 carrots, chopped\r\n-1 red cabbage, chopped\r\n-1/2 head of green cabbage, chopped\r\n-1 cup of blue grapes\r\n-1 cup of orange, chopped\r\n-1 peach, sliced\r\n-1 cup of broccoli florets\r\n-3 tablespoons extra-virgin olive oil\r\n-Sea salt, to taste\r\n-Ground black pepper, to taste\r\n\r\nInstructions:\r\n\r\n1. Preheat oven to 400 degrees Fahrenheit.\r\n\r\n2. Line a baking sheet with parchment paper then add the steak, potatoes, bacon, garlic, carrots, red cabbage, green cabbage, blue grapes, orange, peach, and broccoli to the sheet pan.\r\n\r\n3. Drizzle with extra-virgin olive oil, sea salt and black pepper, then toss to combine.\r\n\r\n4. Bake in the preheated oven for 25–30 minutes, stirring halfway through, until steak is cooked through and vegetables are tender.\r\n\r\n5. Serve warm. Enjoy!",
//                   IsPrivate = false

//               },
//            };
//        }
//    }
//}
