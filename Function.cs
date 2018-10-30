using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace MachOneSoftware.PizzaBuddy
{
    public class Function
    {
        private const string LaunchMessage = "Welcome to pizza buddy. You can say, give me a pizza, or, give me a pizza with a number of toppings.";
        private const string StopMessage = "bon appétit";
        private const string HelpMessage = "Stuck on what to order? I can give you a random pizza. Just say, give me a pizza, or, give me a pizza with a number of toppings. You can ask for a pizza with up to 10 toppings.";
        private const string HelpReprompt = "You can say, give me a pizza, or, give me a pizza with a number of toppings. For example, give me a pizza with 3 toppings.";

        private const string Error_TooManyToppings = "Sorry, that's too many toppings. You can ask for a pizza with up to 10 toppings.";
        private const string Error_NegativeToppings = "Sorry, I can't make a pizza with negative toppings. You can ask for a pizza with up to 10 toppings.";
        private const string Error_BadSlot = "Sorry, I didn't understand that. Try using a number between 0 and 10 when asking for a pizza with toppings.";
        private const string Error_Unknown = "Sorry, something went wrong. You can say, give me a pizza, or, give me a pizza with a number of toppings.";

        private ILambdaLogger _log;
        private string _requestId;


        private void Log(string msg)
        {
            _log.LogLine(msg);
        }

        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            _requestId = context.AwsRequestId;
            _log = context.Logger;
            var response = new SkillResponse()
            {
                Version = "1.0.0",
                Response = new ResponseBody() { ShouldEndSession = true }
            };
            IOutputSpeech output = null;

            try
            {
                var requestType = input.GetRequestType();
                if (requestType == typeof(LaunchRequest))
                {
                    response.Response.ShouldEndSession = false;
                    output = GetOutput(LaunchMessage);
                }
                else if (requestType == typeof(SessionEndedRequest))
                    output = GetOutput(StopMessage);
                else if (requestType == typeof(IntentRequest))
                {
                    var intentRequest = (IntentRequest)input.Request;
                    switch (intentRequest.Intent.Name)
                    {
                        case "AMAZON.CancelIntent":
                            output = GetOutput(StopMessage);
                            break;
                        case "AMAZON.StopIntent":
                            output = GetOutput(StopMessage);
                            break;
                        case "AMAZON.HelpIntent":
                            response.Response.ShouldEndSession = false;
                            output = GetOutput(HelpMessage);
                            break;
                        case "RandomPizzaIntent":
                            response.Response.ShouldEndSession = HandleRandomPizzaIntent(intentRequest, out output);
                            break;
                        default:
                            response.Response.ShouldEndSession = false;
                            output = GetOutput(HelpReprompt);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"....Error: FunctionHandler\r\nRequestId: {_requestId}\r\n{ex.Message}\r\nParamaters: input = {input}; context = {context}\r\n{ex}");
                output = GetOutput(Error_Unknown);
                response.Response.ShouldEndSession = false;
            }
            finally
            {
                response.Response.OutputSpeech = output;
            }
            return response;
        }

        /// <summary>
        /// Handler for RandomPizzaIntent. Returns a value indicating whether to end the session. Returns the inner response as an out parameter.
        /// </summary>
        /// <param name="intentRequest">IntentRequest to handle.</param>
        /// <param name="output">Response output speech.</param>
        /// <returns></returns>
        private bool HandleRandomPizzaIntent(IntentRequest intentRequest, out IOutputSpeech output)
        {
            try
            {
                if (intentRequest.Intent.Slots["toppingCount"].Value == null)
                {
                    output = GetOutput(GetRandomPizza());
                    return true;
                }

                var valid = int.TryParse(intentRequest.Intent.Slots["toppingCount"].Resolution.Authorities[0].Values[0].Value.Id, out var count);

                if (valid && count <= 10 && count >= 0)
                {
                    output = GetOutput(GetRandomPizza(count));
                    return true;
                }
                else if (count > 10)
                {
                    output = GetOutput(Error_TooManyToppings);
                    return false;
                }
                else if (count < 0)
                {
                    output = GetOutput(Error_NegativeToppings);
                    return false;
                }
                else if (!valid && decimal.TryParse(intentRequest.Intent.Slots["toppingCount"].Value, out var countD))
                {
                    output = GetOutput(GetRandomPizza((int)Math.Floor(countD)));
                    return true;
                }
                else
                {
                    output = GetOutput(Error_BadSlot);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log($"....Error: HandleRandomPizzaIntent\r\nRequestId: {_requestId}\r\n{ex.Message}\r\nParamaters: intentRequest = {intentRequest}\r\n{ex}");
                output = GetOutput(Error_Unknown);
                return false;
            }
        }

        private PlainTextOutputSpeech GetOutput(string text)
        {
            return new PlainTextOutputSpeech() { Text = text };
        }

        private string GetRandomPizza(int toppingCount = -1, PizzaType pizzaType = PizzaType.All)
        {
            try
            {
                if (toppingCount == 0)
                    return "For no toppings, I recommend a cheese pizza.";

                IEnumerable<KeyValuePair<string, string[]>> pizzas = null;
                string[] toppings = null;

                if (pizzaType == PizzaType.Meat)
                {
                    pizzas = Repo.MeatPizzas;
                    toppings = Repo.Meats;
                }
                else if (pizzaType == PizzaType.Veggie)
                {
                    pizzas = Repo.VeggiePizzas;
                    toppings = Repo.Veggies;
                }
                else if (pizzaType == PizzaType.All)
                {
                    pizzas = Repo.AllPizzas;
                    toppings = Repo.AllToppings;
                }

                var r = new Random(DateTime.Now.Millisecond);
                var opt1 = "";
                if (toppingCount > 1)
                {
                    try
                    {
                        var available = pizzas.Where(p => p.Value.Length == toppingCount);
                        var i = new Random(DateTime.Now.Millisecond).Next(available.Count() - 1);
                        opt1 = $"For {toppingCount} toppings, I recommend the {available.ElementAt(i).Key} pizza. Its toppings are {ConcatToppings(available.ElementAt(i).Value)}.";
                    }
                    catch { }
                }
                else if (toppingCount < 1)
                {
                    var i = new Random(DateTime.Now.Millisecond).Next(pizzas.Count() - 1);
                    opt1 = $"I recommend the {pizzas.ElementAt(i).Key} pizza. Its toppings are {ConcatToppings(pizzas.ElementAt(i).Value)}.";
                }

                if (opt1 != "" && r.Next() % 2 == 0)
                    return opt1;
                else
                    return GetRandomizedPizza(toppings, toppingCount);
            }
            catch (Exception ex)
            {
                Log($"....Error: GetRandomPizza\r\nRequestId: {_requestId}\r\n{ex.Message}\r\nParamaters: toppingCount = {toppingCount}; pizzaType = {pizzaType}\r\n{ex}");
                throw;
            }
        }

        private string GetRandomizedPizza(string[] toppings, int toppingCount)
        {
            try
            {
                var r = new Random(DateTime.Now.Millisecond);
                var random = false;
                // Up to 5 toppings if not specified
                if (toppingCount < 1)
                {
                    toppingCount = r.Next(6);
                    random = true;
                }

                var theToppings = toppings;
                var myToppings = new List<string>();
                for (var i = 0; i < toppingCount; i++)
                {
                    while (myToppings.Count == i)
                    {
                        var topping = theToppings[r.Next(theToppings.Length)];
                        if (!myToppings.Contains(topping))
                            myToppings.Add(topping);
                    }
                }

                if (toppingCount == 1)
                    return $"I recommend a {myToppings[0]} pizza.";
                else
                {
                    if (!random)
                        return $"For {toppingCount} toppings, I recommend a pizza with {ConcatToppings(myToppings.ToArray())}";
                    else
                        return $"OK, here's a pizza with {toppingCount} toppings. It has {ConcatToppings(myToppings.ToArray())}";
                }
            }
            catch (Exception ex)
            {
                Log($"....Error: GetRandomizedPizza\r\nRequestId: {_requestId}\r\n{ex.Message}\r\nParamaters: toppings = {toppings}; toppingCount = {toppingCount}\r\n{ex}");
                throw;
            }
        }

        private string ConcatToppings(string[] toppings)
        {
            try
            {
                var sb = new StringBuilder(toppings[0]);
                for (var i = 1; i < toppings.Length - 1; i++)
                {
                    sb.Append($", {toppings[i]}");
                }
                sb.Append($", and {toppings[toppings.Length - 1]}.");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                Log($"....Error: ConcatToppings\r\nRequestId: {_requestId}\r\n{ex.Message}\r\nParamaters: toppings = {toppings}\r\n{ex}");
                throw;
            }
        }
    }
}
