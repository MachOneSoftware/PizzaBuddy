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
                { "Beef Lovers",        new[] { "pepperoni", "beef" } },
                { "Breakfast",          new[] { "bacon", "boiled egg" } },
                { "Chicken Feast",      new[] { "chicken", "mushroom", "sweetcorn" } },
                { "Deluxe Hawaiian",    new[] { "ham", "bacon", "pineapple" } },
                { "Diavolo"             new[] { "pepperoni", "salami", "onion" } }
                { "Hawaiian",           new[] { "ham", "pineapple" } },
                { "Hawaiian Elvis",     new[] { "canadian bacon", "pineapple", "sauerkraut" } },
                { "Meat Lovers",        new[] { "pepperoni", "beef", "sausage", "ham" } },
                { "New York Classic",   new[] { "double cheese", "pepperoni", "hot chorizo sausage"} },
                { "Pepperoni Bacon",    new[] { "pepperoni", "bacon" } },
                { "Pepperoni Passion",  new[] { "pepperoni" } },
                { "Pepperoni Veggie",   new[] { "pepperoni", "onion", "green pepper" } },
                { "Ranch BBQ",          new[] { "chicken", "pepperoni", "beef", "bacon"} },
		            { "Seafood",     	      new[] { "shrimp", "anchovy" , "basil" } },
                { "Supreme",            new[] { "pepperoni", "sausage", "mushroom", "green pepper", "onion" } },
                { "Texas BBQ",          new[] { "chicken", "bacon", "onion", "red pepper", "green pepper" } },
                { "Tuna Supreme",       new[] { "tuna", "sweetcorn", "onion" } }
            }
        );

        private static Lazy<Dictionary<string, string[]>> _veggiePizzas = new Lazy<Dictionary<string, string[]>>(() =>
            new Dictionary<string, string[]>()
            {
                { "Balkan",             new[] {"goat cheese", "cherry tomato", "black olive" }},
                { "Mushroom Broccoli",  new[] {"mushroom", "broccoli" } },
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
                "salami",
                "sausage",
                "shrimp",
                "tuna"
            }
        );

        private static Lazy<string[]> _veggies = new Lazy<string[]>(() =>
            new[]
            {
                "banana pepper",
                "basil",
                "black olive",
                "broccoli",
                "cherry tomato",
                "eggplant",
                "goat cheese",
                "green pepper",
                "jalapeño pepper",
                "mushroom",
                "olive",
                "onion",
                "pineapple",
                "red pepper",
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