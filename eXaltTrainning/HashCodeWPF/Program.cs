using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeWPF
{
    public class Program
    {
        private static Random Randomizer = new Random();
        public static ConcurrentDictionary<int, List<string>> hashGroups = new ConcurrentDictionary<int, List<string>>();
        public static int numChar;

        public static async Task FindOccurrencesAsync(ConcurrentDictionary<int, List<string>> hashGroups, int numHashes, MainWindow mainWindow)
        {

            /*
            List<int> hashes = new List<int>(hashGroups.Keys);
            Parallel.ForEach (hashes, hash) =>
            {
                if (hashGroups[hash].Count == numHashes)
                {
                    Debug.WriteLine($"{numHashes} chaînes de caractères avec le même code de hachage {hash} ont été trouvées. Voici la liste des chaînes :");
                    foreach (string StringInList in hashGroups[hash])
                    {
                        Debug.WriteLine(StringInList);
                    }
                    return;
                }
            };*/
            try
            {
                Parallel.ForEach(hashGroups.Keys, hash =>
                {
                    if (hashGroups[hash].Count == numHashes)
                    {
                        mainWindow.tb_affichage.Text += $"Hash: {hashGroups[hash]}\n";
                        foreach (string StringInList in hashGroups[hash])
                        {
                            mainWindow.tb_affichage.Text += $"{StringInList}\n";
                        }
                        mainWindow.tb_affichage.Text += "\n";
                        return;
                        /*
                        Debug.WriteLine($"{numHashes} chaînes de caractères avec le même code de hachage {hash} ont été trouvées. Voici la liste des chaînes :");
                        foreach (string StringInList in hashGroups[hash])
                        {
                            Debug.WriteLine(StringInList);
                        }
                        return;*/
                    }
                });
            } catch (Exception e)
            {
                Debug.WriteLine(e);
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