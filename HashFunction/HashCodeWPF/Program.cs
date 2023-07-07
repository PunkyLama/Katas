using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeWPF
{
    public class Program
    {
        private static Random Randomizer = new Random();
        public static ConcurrentDictionary<int, List<string>> hashGroups = new ConcurrentDictionary<int, List<string>>();
        public static int numChar;

        public static async Task FindOccurrencesAsync(ConcurrentDictionary<int, List<string>> hashGroups, int numHashes)
        {
            List<int> hashes = new List<int>(hashGroups.Keys);
            foreach (int hash in hashes)
            {
                if (hashGroups[hash].Count == numHashes)
                {
                    Debug.WriteLine($"{numHashes} chaînes de caractères avec le même code de hachage {hash} ont été trouvées. Voici la liste des chaînes :");
                    foreach (string StringInList in hashGroups[hash])
                    {
                        Debug.WriteLine(StringInList);
                    }
                    Stopwatch timer = Stopwatch.StartNew();
                    TimeSpan timeTaken = timer.Elapsed;
                    timer.Stop();
                    Debug.WriteLine("Time taken : " + timeTaken.ToString(@"m\:ss\.fff"));
                    return;
                }
            }
        }

        public static async Task GenerateHashesAsync(ConcurrentDictionary<int, List<string>> hashGroups, int numHashes)
        {
            while (true)
            {
                byte[] tmpSource;
                var RString = RandomString();
                tmpSource = ASCIIEncoding.ASCII.GetBytes(RString);
                var HashedString = tmpSource.GetHashCode();
                hashGroups.AddOrUpdate(HashedString, new List<string> { RString }, (key, oldValue) =>
                {
                    if (oldValue.Count < numHashes && !oldValue.Contains(RString))
                    {
                        oldValue.Add(RString);
                    }
                    return oldValue;
                });
                if (hashGroups[HashedString].Count == numHashes)
                {
                    break;
                }
            }
        }

        public static string RandomString()
        {
            StringBuilder StringBuild = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                StringBuild.Append((char)Randomizer.Next((int)'a', ((int)'z') + 1));
            }
            return StringBuild.ToString();
        }
    }
}

/*using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeWPF
{
    public class Program
    {
        private static Random Randomizer = new Random();
        public static Dictionary<int, List<string>> hashGroups = new Dictionary<int, List<string>>();
        public static int numChar;

        public static async Task FindOccurrencesAsync(Dictionary<int, List<string>> hashGroups, int numHashes)
        {
            List<int> hashes = new List<int>(hashGroups.Keys);
            foreach (int hash in hashes)
            {
                if (hashGroups[hash].Count == numHashes)
                {
                    Debug.WriteLine($"{numHashes} chaînes de caractères avec le même code de hachage {hash} ont été trouvées. Voici la liste des chaînes :");
                    foreach (string StringInList in hashGroups[hash])
                    {
                        Debug.WriteLine(StringInList);
                    }
                    Stopwatch timer = Stopwatch.StartNew();
                    TimeSpan timeTaken = timer.Elapsed;
                    timer.Stop();
                    Debug.WriteLine("Time taken : " + timeTaken.ToString(@"m\:ss\.fff"));
                    return;
                }
            }
        }

        public static async Task GenerateHashesAsync(Dictionary<int, List<string>> hashGroups, int numHashes)
        {
            while (true)
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
                    if (hashGroups[HashedString].Count < numHashes && !hashGroups[HashedString].Contains(RString))
                    {
                        hashGroups[HashedString].Add(RString);
                    }
                }
                if (hashGroups[HashedString].Count == numHashes)
                {
                    break;
                }
            }
        }

        public static string RandomString()
        {
            StringBuilder StringBuild = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                StringBuild.Append((char)Randomizer.Next((int)'a', ((int)'z') + 1));
            }
            return StringBuild.ToString();
        }
    }
}
*/
/*using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Packaging;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace HashCodeWPF
{
    public class Program
    {
        private static Random Randomizer = new Random(); // Instancie un objet de type Random pour générer des nombres aléatoires.
        public static Dictionary<int, List<string>> hashGroups = new Dictionary<int, List<string>>(); // Initialise un dictionnaire qui stockera les groupes de chaînes de caractères ayant le même code de hachage.
        public static int numChar;
        public static async Task FindOccurrencesAsync(Dictionary<int, List<string>> hashGroups, int numHashes)
        {
            List<int> hashes = new List<int>(hashGroups.Keys);

            foreach (int hash in hashes)
            {
                if (hashGroups[hash].Count == numHashes)
                {
                    Debug.WriteLine($"{numHashes} chaînes de caractères avec le même code de hachage {hash} ont été trouvées. Voici la liste des chaînes :");
                    foreach (string StringInList in hashGroups[hash])
                    {
                        Debug.WriteLine(StringInList);
                    }

                    Stopwatch timer = Stopwatch.StartNew();
                    TimeSpan timeTaken = timer.Elapsed;
                    timer.Stop();

                    Debug.WriteLine("Time taken : " + timeTaken.ToString(@"m\:ss\.fff"));

                    return;
                }
            }
        }
        public static async Task GenerateHashesAsync(Dictionary<int, List<string>> hashGroups, int numHashes)
        {
            while (hashGroups.Count < numHashes)
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
                    if (hashGroups[HashedString].Count < numHashes && !hashGroups[HashedString].Contains(RString))
                    {
                        hashGroups[HashedString].Add(RString);
                    }
                }
            }
        }
        public static string RandomString()
        {
            StringBuilder StringBuild = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                StringBuild.Append((char)Randomizer.Next((int)'a', ((int)'z') + 1));
            }
            return StringBuild.ToString();
        }
    }
}
*/