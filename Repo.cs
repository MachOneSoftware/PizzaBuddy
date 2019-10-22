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
                { "Chicken Feast",      new[] { "chicken", "mushroom", "sweetcorn" } },
                { "Deluxe Hawaiian",    new[] { "ham", "bacon", "pineapple" } },
                { "Hawaiian",           new[] { "ham", "pineapple" } },
                { "Hawaiian Elvis",     new[] { "canadian bacon", "pineapple", "saurkraut" } },
                { "Meat Lovers",        new[] { "pepperoni", "beef", "sausage", "ham" } },
                { "Pepperoni Bacon",    new[] { "pepperoni", "bacon" } },
                { "Pepperoni Passion",  new[] { "pepperoni" } },
                { "Pepperoni Veggie",   new[] { "pepperoni", "onion", "green pepper" } },
                { "Ranch BBQ",          new[] { "chicken", "pepperoni", "beef", "bacon"} },
                { "Supreme",            new[] { "pepperoni", "sausage", "mushroom", "green pepper", "onion" } },
                { "Texas BBQ",          new[] { "chicken", "bacon", "onion", "red pepper", "green pepper" } },
                { "Tuna Supreme",       new[] { "tuna", "sweetcorn", "onion" } }
            }
        );

        private static Lazy<Dictionary<string, string[]>> _veggiePizzas = new Lazy<Dictionary<string, string[]>>(() =>
            new Dictionary<string, string[]>()
            {
                { "Spice Garden",       new[] {"green pepper", "banana pepper", "jalapeño pepper" } },
                { "Spinach Delight",    new[] {"spinach", "tomato" } },
                { "Veggie Lovers",      new[] {"tomato", "olive", "spinach", "mushroom", "onion" } }
            }
        );

        private static Lazy<string[]> _meats = new Lazy<string[]>(() =>
            new[]
            {
                "bacon",
                "beef",
                "canadian bacon",
                "chicken",
                "ham",
                "pepperoni",
                "sausage",
                "tuna"
            }
        );

        private static Lazy<string[]> _veggies = new Lazy<string[]>(() =>
            new[]
            {
                "banana pepper",
                "green pepper",
                "jalapeño pepper",
                "mushroom",
                "olive",
                "onion",
                "pineapple",
                "red pepper",
                "saurkraut",
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
