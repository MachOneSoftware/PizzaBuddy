﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MachOneSoftware.PizzaBuddy
{
    static class Repo
    {
        private static Lazy<Dictionary<string, string[]>> _meatPizzas = new Lazy<Dictionary<string, string[]>>(() =>
            new Dictionary<string, string[]>()
            {
                { "Meat Lovers",        new[] { "pepperoni", "beef", "sausage", "ham" } },
                { "Hawaiian",           new[] { "ham", "pineapple" } },
                { "Deluxe Hawaiian",    new[] { "ham", "bacon", "pineapple" } },
                { "Supreme",            new[] { "pepperoni", "sausage", "mushroom", "green pepper", "onion" } },
                { "Pepporoni Bacon",    new[] { "pepperoni", "bacon" } },
                { "Beef Lovers",        new[] { "pepperoni", "beef" } },
                { "Pepperoni Veggie",   new[] { "pepperoni", "onion", "green pepper" } }
            }
        );

        private static Lazy<Dictionary<string, string[]>> _veggiePizzas = new Lazy<Dictionary<string, string[]>>(() =>
            new Dictionary<string, string[]>()
            {
                { "Veggie Lovers",      new[] {"tomato", "olive", "spinach", "mushroom", "onion" } },
                { "Spice Garden",       new[] {"green pepper", "banana pepper", "jalapeño pepper" } },
                { "Spinach Delight",    new[] {"spinach", "tomato" } }
            }
        );

        private static Lazy<string[]> _meats = new Lazy<string[]>(() =>
            new[]
            {
                "bacon",
                "beef",
                "chicken",
                "ham",
                "pepperoni",
                "sausage"
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
                "spinach",
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
