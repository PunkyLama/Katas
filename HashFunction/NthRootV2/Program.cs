class Program
{
    public static void Main(string[] args)
    {
        try
        {
            // Déclaration des variables d'entrée
            double number = new();
            int root = new();
            // Message d'invite pour la saisie de l'utilisateur
            Console.Write("Input an number (double) value: ");
            number = Convert.ToDouble(Console.ReadLine());

            Console.Write("Input an root (integer) value: ");
            root = Convert.ToInt32(Console.ReadLine());
            /*
            On vérifie que le nombre et la racine sont bien supérieurs à 1.
            Si ce n'est pas le cas, on lève une exception ArgumentOutOfRangeException pour indiquer à l'utilisateur que ces valeurs ne sont pas acceptables.
             */
            if (number <= 1.00)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "The number must be positive.");
            }

            if (root <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(root), "The root must be positive.");
            }

            //display the result
            Console.WriteLine("The nth root is : " + GetNthRoot(number, root));
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
    /*
    La méthode GetNthRoot calcule la racine n-ième d'un nombre en utilisant la méthode de Newton-Raphson. 
    On initialise deux variables : currentApproximation et nextApproximation. 
    Puis, on effectue une boucle while pour raffiner l'approximation de la racine. 
    Dans chaque itération, on met à jour currentApproximation à nextApproximation et on calcule la nouvelle approximation de la racine nextApproximation. 
    On continue la boucle tant que la différence entre currentApproximation et nextApproximation est supérieure à double.Epsilon. 
    Finalement, on retourne la valeur de nextApproximation, qui est la racine n-ième approximative du nombre donné.
     */
    public static double GetNthRoot(double number, int root)
    {
        double currentApproximation = number / root;
        double nextApproximation = (1.00 / root) * (((root - 1) * currentApproximation) + (number / Power(currentApproximation, root - 1)));
        while (currentApproximation - nextApproximation > double.Epsilon)
        {
            currentApproximation = nextApproximation;
            nextApproximation = (1.00 / root) * (((root - 1) * currentApproximation) + (number / Power(currentApproximation, root - 1)));
        }
        return nextApproximation;
    }
    /*
    La méthode Power calcule la puissance d'un nombre en multipliant le nombre par lui-même autant de fois que spécifié par la puissance. 
    Elle prend deux arguments : le nombre que l'on veut élever à une certaine puissance et la puissance à laquelle on veut élever le nombre. 
    La méthode utilise une boucle while pour effectuer la multiplication répétée du nombre, en commençant par une valeur initiale de 1 pour la variable "result". 
    À chaque itération de la boucle, la puissance est décrémentée jusqu'à ce qu'elle atteigne 0. 
    Lorsque la puissance atteint 0, la méthode renvoie la valeur finale de "result", qui est le résultat de la multiplication répétée.
     */
    public static double Power(double number, int power)
    {
        double result = 1.00;
        while (power-- > 0)
        {
            result *= number;
        }
        return result;
    }
}