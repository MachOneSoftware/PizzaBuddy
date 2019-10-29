using System;
using System.Collections.Generic;
using System.Linq;

namespace MachOneSoftware.PizzaBuddy
{
    static class Repo
    {
        private static Lazy<Dictionary<string, string[]>> _meatPizzas = new Lazy<Dictionary<string, string[]>>(() =>
            new Dictionary<string, string[]>()
            {
                { "Barbeque Chicken",   new[] { "onion", "chicken", "bbq sauce"}},
                { "Beef Lovers",        new[] { "pepperoni", "beef" } },
                { "Breakfast",          new[] { "bacon", "boiled egg" } },
                { "Buffalo Chicken",    new[] { "chicken", "buffalo sauce" } },
                { "Cheesesteak",        new[] { "beef", "green pepper", "onion", "mushroom" } },
                { "Chicken Bacon Alfredo",      new[] { "mushroom", "onion", "chicken", "bacon"} },
                { "Chicken Feast",      new[] { "chicken", "mushroom", "sweetcorn" } },
                { "Deluxe Hawaiian",    new[] { "ham", "bacon", "pineapple" } },
                { "Diavolo",             new[] { "pepperoni", "salami", "onion" } },
                { "Emiliana",           new[] { "tomato", "mozzarella cheese", "eggplant", "potato", "sausage" }},
                { "Four Seasons",       new[] {"tomato", "mozzarella cheese", "basil leaves", "artichoke",
                                               "fontina cheese", "mushroom", "red pepper", "provolone cheese", "potato",
                                               "pecorino cheese" } },
                { "Hawaiian",           new[] { "ham", "pineapple" } },
                { "Hawaiian Elvis",     new[] { "canadian bacon", "pineapple", "sauerkraut" } },
                { "Meat Lovers",        new[] { "pepperoni", "beef", "sausage", "ham" } },
                { "New York Classic",   new[] { "double cheese", "pepperoni", "hot chorizo sausage"} },
                { "Pepperoni Bacon",    new[] { "pepperoni", "bacon" } },
                { "Pepperoni Passion",  new[] { "pepperoni" } },
                { "Pepperoni Veggie",   new[] { "pepperoni", "onion", "green pepper" } },
                { "Philly Cheesesteak",     new[] { "mozzarella cheese", "seasoned steak", "bell peppers"} },
                { "Prosciutto and Arugula",   new[] { "mozzarella chesse", "parmesan", "prosciutto", "arugula" } },
                { "Ranch BBQ",          new[] { "chicken", "pepperoni", "beef", "bacon"} },
                { "Regina",             new[] { "ham", "artichoke", "mozzarella", "olive" } },
                { "Seafood",            new[] { "shrimp", "anchovy" , "basil" } },
                { "Supreme",            new[] { "pepperoni", "sausage", "mushroom", "green pepper", "onion" } },
                { "Texas BBQ",          new[] { "chicken", "bacon", "onion", "red pepper", "green pepper" } },
                { "Tuna Supreme",       new[] { "tuna", "sweetcorn", "onion" } }
            }
        );

        private static Lazy<Dictionary<string, string[]>> _veggiePizzas = new Lazy<Dictionary<string, string[]>>(() =>
            new Dictionary<string, string[]>()
            {
                { "Balkan",             new[] {"goat cheese", "cherry tomato", "black olive" }},
                { "Four cheese",        new[] {"riccota", "parmigian", "mozzarella","gorgonzola" } },
                { "Margherita",         new[] {"basil", "tomato", "garlic" } },
                { "Mushroom Broccoli",  new[] {"mushroom", "broccoli" } },
                { "Neopolitan",         new[] { "basil leaves", "tomato", "mozarella cheese"} },
                { "Parmigiana",         new[] {"tomato", "eggplant" } },
                { "Spice Garden",       new[] {"green pepper", "banana pepper", "jalapeño pepper" } },
                { "Spinach Delight",    new[] {"spinach", "tomato" } },
                { "Veggie Lovers",      new[] {"tomato", "olive", "spinach", "mushroom", "onion" } }
            }
        );

        private static Lazy<string[]> _meats = new Lazy<string[]>(() =>
            new[]
            {
                "anchovy",
                "bacon",
                "beef",
                "boiled egg",
                "canadian bacon",
                "chicken",
                "ham",
                "hot chorizo sausage",
                "pepperoni",
                "prosciutto",
                "salami",
                "sausage",
                "shrimp",
                "steak",
                "tuna"
            }
        );

        private static Lazy<string[]> _veggies = new Lazy<string[]>(() =>
            new[]
            {
                "artichoke",
                "arugula",
                "banana pepper",
                "basil",
                "basil leaves",
                "bell peppers",
                "black olive",
                "broccoli",
                "cherry tomato",
                "eggplant",
                "garlic",
                "goat cheese",
                "gorgonzola",
                "green pepper",
                "jalapeño pepper",
                "mozzarella",
                "mushroom",
                "olive",
                "onion",
                "parmigian",
                "pineapple",
                "potato",
                "red onion",
                "red pepper",
                "riccota",
                "sauerkraut",
                "spinach",
                "sweetcorn",
                "tomato"
            }
        );

        public static IEnumerable<KeyValuePair<string, string[]>> MeatPizzas { get => _meatPizzas.Value; }
        public static IEnumerable<KeyValuePair<string, string[]>> VeggiePizzas { get => _veggiePizzas.Value; }
        public static IEnumerable<KeyValuePair<string, string[]>> AllPizzas { get => _meatPizzas.Value.Concat(_veggiePizzas.Value); }
        public static string[] Meats { get => _meats.Value; }
        public static string[] Veggies { get => _veggies.Value; }
        public static string[] AllToppings { get => _veggies.Value.Concat(_meats.Value).ToArray(); }
    }

    enum PizzaType
    {
        Meat, Veggie, All
    }
}
