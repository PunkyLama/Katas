using System.Collections;

namespace programme_collections
{
    class Program
    {
        
        static int SommeTableaux(int[] t)
        {
            int somme = 0;
            for (int i = 0; i < t.Length; i++)
            {
                somme += t[i];
            }
            return somme;
        }

        static void AfficherTableau(int[] tableau)
        {
            for(int i = 0;i < tableau.Length;i++)
            {
                Console.WriteLine($"[{i}] {tableau[i]}");
            }
        }

        static void AfficherValeurMaximal(int[] tableau)
        {
            Console.WriteLine($"la valeur max du tableau est {tableau.Max()}");

            // Deuxieme options

            int max = tableau[0];
            for(int i = 0; i < tableau.Length; i++)
            {
                if (tableau[i] > max)
                {
                    max = tableau[i];
                }
            }
            Console.WriteLine($"la valeur max du tableau est {max}");
        }

        static void AfficherValeurMinimale(int[] tableau)
        {
            Console.WriteLine($"la valeur minimale du tableau est {tableau.Min()}");

            // Deuxieme options

            int min = tableau[0];
            for (int i = 0; i < tableau.Length; i++)
            {
                if (tableau[i] < min)
                {
                    min = tableau[i];
                }
            }
            Console.WriteLine($"la valeur minimale du tableau est {min}");
        }

        static void Tableaux()
        {
            //int[] t = { 200, 40, 15, 5, 12 };
            Random Random = new Random();
            const int TAILLE_TABLEAU = 20;
            int[] t= new int[TAILLE_TABLEAU];

            for(int i = 0; i < t.Length; i++)
            {
                t[i] = Random.Next(0, 101);
            }

            AfficherTableau(t);
            AfficherValeurMaximal(t);
            AfficherValeurMinimale(t);
        }

        static void AfficherList(List<string> liste, bool ordreDescendant = false)
        {
            if (ordreDescendant)
            {
                liste.Reverse();
            }
            for(int i = 0; i < liste.Count; i++)
            {
                Console.WriteLine(liste[i]);
            }

            // Autre méthode 
            Console.WriteLine();

            if (!ordreDescendant)
            {
                for (int i = 0; i < liste.Count; i++)
                {
                    Console.WriteLine(liste[i]);
                }
            }
            else
            {
                for (int i = liste.Count-1; i >= 0; i--)
                {
                    Console.WriteLine(liste[i]);
                }
            }

            // Nom le plus long

            string NomLePlusLong = "";

            for (int i = 0; i < liste.Count; i++)
            {
                if (liste[i].Length > NomLePlusLong.Length)
                    NomLePlusLong = liste[i];                
            }
            Console.WriteLine($"Le nom le plus long est {NomLePlusLong}");
        }

        static void Listes()
        {
            /*
            List<string> noms = new List<string> (); 
            
            while (true)
            {
                Console.WriteLine("Rentrez un nom (ENTER pour finir):");
                string nom = Console.ReadLine();

                if(nom == "")
                {
                    break;
                }

                if (noms.Contains(nom))
                {
                    Console.WriteLine("Erreur : ce nom est déjà dans la liste");
                    Console.WriteLine();
                } else
                    noms.Add(nom);
            }

            // Filtrer : Supprimer tous les noms qui termine par "e";
            for (int i = noms.Count - 1; i >= 0; i--)
            {
                string nom = noms[i];
                if (nom[nom.Length - 1] == 'e')
                {
                    noms.RemoveAt(i);
                }
            }

            Console.WriteLine("Voulez vous afficher la listes dans l'ordre inverse ? (OUI ou NON)");
            string order =  Console.ReadLine();

            if(order == "OUI")
            {
                AfficherList(noms, true);
            } else
                AfficherList(noms);  
            */

            var liste1 = new List<string>() { "paul", "jean", "pierre", "emilie", "martin"};
            var liste2 = new List<string>() { "shopie", "jean", "toto", "titi", "martin" };

            AfficherElementCommun(liste1, liste2);

        }

        static void AfficherElementCommun(List<string> liste1, List<string> liste2)
        {
            foreach(var item in liste1)
            {
                if (liste2.Contains(item))
                    Console.WriteLine(item);
            }
        }

        static void Main(string[] args)
        {
            //Tableaux();
            //Listes();
        }
    }
}