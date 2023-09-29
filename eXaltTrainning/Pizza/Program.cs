using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace Pizza
{
    class PizzaPersonnalise : Pizza
    {
        static int nbPizzasPersonnalisee = 0;

        public PizzaPersonnalise() : base("Personnalisee", 5, false, null) 
        {
            nbPizzasPersonnalisee++;
            nom = "Personnalisée" + nbPizzasPersonnalisee;
            ingredients = new List<string>();
            while (true)
            {
                Console.Write($"Rentrez un ingrédient pour la pizza personnalisée {nbPizzasPersonnalisee} (ENTER pour terminer) : ");
                var ingredient = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ingredient))
                {
                    break;
                }
                if (ingredients.Contains(ingredient))
                {
                    Console.WriteLine("ERREUR : Cet ingrédient est déjà present dans la pizza");
                } else
                {
                    ingredients.Add(ingredient);
                    Console.WriteLine(string.Join(", ", ingredients));
                }                
                Console.WriteLine();
            }
            prix = 5 + ingredients.Count * 1.5f;
        }

    }


    class Pizza
    {
        public string nom {  get; protected set; }
        public float prix { get; protected set; }
        public bool vegetarienne { get; private set; }
        public List<string> ingredients { get; protected set; }

        public Pizza(string nom, float prix, bool vegetarienne, List<string> ingredients)
        {
            this.nom = nom;
            this.prix = prix;
            this.vegetarienne = vegetarienne;
            this.ingredients = ingredients;
        }

        public void Afficher()
        {
            string badgeVegetarienne = vegetarienne ? "(V)": "";
            string nomAffichage = StringFormatter(nom);
            List<string> ingredientsAfficher = ingredients.Select(x => StringFormatter(x)).ToList();

            Console.WriteLine($"{nomAffichage} {badgeVegetarienne} - {prix}€");
            Console.WriteLine(string.Join(", ", ingredientsAfficher));
            Console.WriteLine();
        }

        private static string StringFormatter( string str )
        {
            if (string.IsNullOrEmpty(str))
                return str;
            string majuscule = str.ToUpper();
            string minuscule = str.ToLower();

            return majuscule[0] + minuscule[1..];
        }
    }
    class Program
    {

        static List<Pizza> GetPizzasFromCode()
        {
            var listesPizzas = new List<Pizza>() {
            new Pizza("4 fromages", 11.5f, true, new List<string>{"cantal","mozzarella", "fromage de chèvre", "gruyère" }),
            new Pizza("indienne", 10.5f, false, new List < string > { "curry", "mozzarella", "poulet", "poivron", "oignon", "coriandre" }),
            new Pizza("mexicaine", 13f, false, new List < string > { "boeuf", "mozzarella", "maïs", "tomate", "oignon", "coriandre" }),
            new Pizza("margherita", 8f, true, new List < string > { "sauce tomate", "mozzarella", "basilic" }),
            new Pizza("calzone", 12f, false, new List < string > { "tomate", "jambon", "persil", "oignon" }),
            new Pizza("complète", 9.5f, false, new List < string > { "jambon", "oeuf", "fromage" })
            };
            return listesPizzas;
        }

        static List<Pizza> GetPizzasFromFile(string filename)
        {
            string json = null;

            try
            {
                json = File.ReadAllText(filename);
            }
            catch
            {
                Console.WriteLine($"Erreur de lecture du ficher : {filename}");
                return null;
            }

            List<Pizza> pizzas = null;

            try
            {
                pizzas = JsonConvert.DeserializeObject<List<Pizza>>(json);
            }
            catch
            {
                Console.WriteLine("ERREUR : Les données json ne sont pas valides");
                return null;
            }

            return pizzas;

        }

        static void GenerateJsonFile(List<Pizza> pizzas, string filename)
        {
            string json = JsonConvert.SerializeObject(pizzas);
            File.WriteAllText(filename, json);
        }

        static List<Pizza> GetPizzasFromUrl(string url)
        {
            var webclient = new WebClient();
            string client = null;
            try
            {
                client = webclient.DownloadString(url);
            }
            catch
            {
                Console.WriteLine("Erreur réseau");
                return null;
            }
            List<Pizza> pizzas = null;

            try
            {
                pizzas = JsonConvert.DeserializeObject<List<Pizza>>(client);
            }
            catch
            {
                Console.WriteLine("ERREUR : Les données json ne sont pas valides");
                return null;
            }

            return pizzas;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var filename = "pizza.json";

            //var pizzas = GetPizzasFromCode();
            //GenerateJsonFile(pizzas, filename );

            //var pizzas = GetPizzasFromFile(filename);
            var pizzas = GetPizzasFromUrl("https://codeavecjonathan.com/res/pizzas2.json");

            if(pizzas != null )
            {
                foreach (var pizza in pizzas)
                    pizza.Afficher();
            }            
        }
    }
}