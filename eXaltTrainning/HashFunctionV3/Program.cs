using System.Diagnostics;
using System.Text;
class Program
{
    static Random Randomizer = new Random(); // Instancie un objet de type Random pour générer des nombres aléatoires.
    static Dictionary<int, List<string>> hashGroups = new Dictionary<int, List<string>>(); // Initialise un dictionnaire qui stockera les groupes de chaînes de caractères ayant le même code de hachage.
    static int numChar, numStrings; // Initialise les variables qui détermineront la longueur des chaînes de caractères et le nombre de chaînes dans chaque groupe.

    static void Main(string[] args)
    {
        // Definie par defaut a 10.
        Console.Write("Entrez la longueur des chaînes de caractères (entier) : ");
        numChar = Convert.ToInt32(Console.ReadLine());
        if (numChar <= 1)
        {
            Console.WriteLine("Erreur : la longueur doit être un entier positif.");
            return;
        }
        Console.Write("Entrez le nombre de chaînes de caractères avec le même code de hachage que vous voulez (entier) : ");
        numStrings = Convert.ToInt32(Console.ReadLine());
        if (numStrings <= 1)
        {
            Console.WriteLine("Erreur : le nombre de chaînes de caractères doit être un entier positif.");
            return;
        }

        Task.Run(() => HashStrings());
        Console.ReadLine();
    }

    static void HashStrings()
    {
        
        Stopwatch timer = new Stopwatch();
        timer.Start();
        try
        {
            Parallel.ForEach(Enumerable.Range(0, hashGroups.Count), new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                (index, loopState) =>
                {
                    byte[] tmpSource;
                    var RString = RandomString();
                    tmpSource = ASCIIEncoding.ASCII.GetBytes(RString);
                    var HashedString = tmpSource.GetHashCode();

                    lock (hashGroups)
                    {
                        if (!hashGroups.ContainsKey(HashedString))
                        {
                            hashGroups[HashedString] = new List<string>();
                        }
                        if (hashGroups[HashedString].Count < numStrings && !hashGroups[HashedString].Contains(RString))
                        {
                            hashGroups[HashedString].Add(RString);
                        }
                        if (hashGroups[HashedString].Count == numStrings)
                        {
                            Console.WriteLine($"{numStrings} chaînes de caractères avec le même code de hachage {HashedString} ont été trouvées. Voici la liste des chaînes :");
                            foreach (string StringInList in hashGroups[HashedString])
                            {
                                Console.WriteLine(StringInList);
                            }
                            timer.Stop();
                            TimeSpan timeTaken = timer.Elapsed;
                            timer = null;
                            Console.WriteLine("Time taken for DVTraitement: " + timeTaken.ToString(@"m\:ss\.fff"));
                            loopState.Stop();
                        }
                    }
                    if (hashGroups.Count % 1000 == 0)
                    {
                        Console.WriteLine(hashGroups.Count);
                    }
                });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    static string RandomString()
    {
        StringBuilder StringBuild = new StringBuilder();
        for (int i = 0; i < numChar; i++)
        {
            StringBuild.Append((char)Randomizer.Next((int)'a', ((int)'z') + 1));
        }
        return StringBuild.ToString();
    }
}