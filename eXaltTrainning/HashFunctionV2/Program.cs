using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
class Program
{
    static Random Randomizer = new Random(); // Instancie un objet de type Random pour générer des nombres aléatoires.
    static Dictionary<int, List<string>> hashGroups = new Dictionary<int, List<string>>(); // Initialise un dictionnaire qui stockera les groupes de chaînes de caractères ayant le même code de hachage.
    static int numChar, numStrings; // Initialise les variables qui détermineront la longueur des chaînes de caractères et le nombre de chaînes dans chaque groupe.
    
    static void Main(string[] args)
    {
        Console.Write("Input number of char in string you want (integer type): "); // Invite l'utilisateur à saisir la longueur des chaînes de caractères.
        numChar = Convert.ToInt32(Console.ReadLine()); // Lit la saisie utilisateur et stocke la valeur dans la variable numChar.
        if (numChar <= 1)
        {
            Console.WriteLine("Error: The number of char must be positive."); // Vérifie si la longueur des chaînes de caractères est valide, sinon affiche un message d'erreur et termine le programme.
            return;
        }
        Console.Write("Input number of string with the same hashcode you want (integer type): "); // Invite l'utilisateur à saisir le nombre de chaînes de caractères à générer pour chaque groupe.
        numStrings = Convert.ToInt32(Console.ReadLine()); // Lit la saisie utilisateur et stocke la valeur dans la variable numStrings.
        if (numStrings <= 1)
        {
            Console.WriteLine("Error: The number of string must be positive."); // Vérifie si le nombre de chaînes de caractères est valide, sinon affiche un message d'erreur et termine le programme.
            return;
        }

        Task.Run(() => HashStrings()); // Démarre une tâche qui exécutera la méthode HashStrings en arrière-plan.
        Console.ReadLine(); // Attends que l'utilisateur appuie sur une touche pour terminer le programme.
    }
    static void HashStrings()
    {
        Stopwatch timer = new Stopwatch();
        timer.Start();
        try
        {
            while (true) // Boucle infinie pour générer et hacher des chaînes de caractères.
            {
                byte[] tmpSource; // Initialise un tableau d'octets qui stockera la chaîne de caractères à hacher.
                var RString = RandomString(); // Génère une nouvelle chaîne de caractères aléatoire.
                tmpSource = ASCIIEncoding.ASCII.GetBytes(RString); // Convertit la chaîne de caractères en tableau d'octets à l'aide de la classe ASCIIEncoding.
                var HashedString = tmpSource.GetHashCode(); // Génére le code de hachage de la chaîne de caractères.

                lock (hashGroups) // Verrouille le dictionnaire pour éviter les accès concurrents.
                {
                    // Si le code de hachage n'est pas présent dans le dictionnaire, ajoute une nouvelle entrée.
                    if (!hashGroups.ContainsKey(HashedString))
                    {
                        hashGroups[HashedString] = new List<string>();
                    }
                    // Si la liste de chaînes de caractères associée au code de hachage ne contient pas déjà la chaîne générée et qu'il y a moins de chaînes que le nombre souhaité, ajoute la chaîne à la liste.
                    if (hashGroups[HashedString].Count < numStrings && !hashGroups[HashedString].Contains(RString))
                    {
                        hashGroups[HashedString].Add(RString);
                    }
                    // Si le nombre de chaînes associées au code de hachage est atteint, affiche les chaînes trouvées et arrête la fonction.
                    if (hashGroups[HashedString].Count == numStrings)
                    {
                        Console.WriteLine( $"{numStrings} strings with the same hash code {HashedString} have been found. Here is the string list :");
                        // Boucle qui permet de parcourir la liste de string présent dans le dictionnaire hashGroups
                        foreach (string StringInList in hashGroups[HashedString])
                        {
                            Console.WriteLine(StringInList);
                        }
                        timer.Stop();
                        TimeSpan timeTaken = timer.Elapsed;
                        Console.WriteLine("Time taken for DVTraitement: " + timeTaken.ToString(@"m\:ss\.fff"));
                        return;
                    }
                }
                // Affiche le nombre de codes de hachage différents stockés dans le dictionnaire toutes les 1000 execution
                if (hashGroups.Count % 1000 == 0)
                {
                    Console.WriteLine(hashGroups.Count);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    static string RandomString()
    {
        StringBuilder StringBuild = new StringBuilder(); // Crée une variable StringBuilder
        for (int i = 0; i < numChar; i++)
        {
            StringBuild.Append((char)Randomizer.Next((int)'a', ((int)'z') + 1)); // Ajoute au le charactère au StringBuilder
        }
        return StringBuild.ToString(); // Retourne le StringBuilder au format sstring
    }

}