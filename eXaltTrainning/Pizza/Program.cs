using System.Text;

namespace Pizza
{
    class PizzaPersonnalise : Pizza
    {
        PizzaPersonnalise() : base("Personnalisee", 5, false, null) 
        {
            ingredients = new List<string>();
        }

    }


    class Pizza
    {
        public string nom {  get; private set; }
        public float prix { get; private set; }
        public bool vegetarienne { get; private set; }
        public List<string> ingredients { get; set; }

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
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var listesPizzas = new List<Pizza>() {
            new Pizza("4 fromages", 11.5f, true, new List<string>{"cantal","mozzarella", "fromage de chèvre", "gruyère" }),
            new Pizza("indienne", 10.5f, false, new List < string > { "curry", "mozzarella", "poulet", "poivron", "oignon", "coriandre" }),
            new Pizza("mexicaine", 13f, false, new List < string > { "boeuf", "mozzarella", "maïs", "tomate", "oignon", "coriandre" }),
            new Pizza("margherita", 8f, true, new List < string > { "sauce tomate", "mozzarella", "basilic" }),
            new Pizza("calzone", 12f, false, new List < string > { "tomate", "jambon", "persil", "oignon" }),
            new Pizza("complète", 9.5f, false, new List < string > { "jambon", "oeuf", "fromage" })
            };
            foreach (var pizza in listesPizzas)
                pizza.Afficher();
        }
    }
}