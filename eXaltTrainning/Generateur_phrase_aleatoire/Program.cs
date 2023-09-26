using System.Text;

namespace Generateur_phrase_aleatoire
{
    class Program
    {
        static string ObtenirElementAleatoire(string[] t)
        {
            Random random = new Random();
            return t[random.Next(t.Length)];
        }

        static void CreePhrase(string[] sujets, string[] verbes, string[] complements)
        {
            const int NB_Phrase = 50;
            int doublonsEvites = 0;

            StringBuilder sb = new StringBuilder();
            List<string> PhraseUnique = new List<string>();

            for(int i  = 0; i < NB_Phrase; i++)
            {
                var sujet = ObtenirElementAleatoire(sujets);
                var verbe = ObtenirElementAleatoire(verbes);
                var complement = ObtenirElementAleatoire(complements);

                sb.Append($"{sujet} {verbe} {complement}");
                sb.Replace("à le", "au");
                if(!PhraseUnique.Contains(sb.ToString()))
                    PhraseUnique.Add(sb.ToString());                    
                else
                    CreePhrase(sujets, verbes, complements);
                    doublonsEvites++;
                sb.Clear();
            }
            AfficherPhrases(PhraseUnique);
            Console.WriteLine($"Nombre de doublons évités : {doublonsEvites}");

        }

        static void AfficherPhrases(List<string> listes)
        {
            foreach(string item in  listes)
            {
                Console.WriteLine(item);
            }
        }

        static void Main(string[] args)
        {
            var sujets = new string[]
            {
                "Un lapin",
                "Une grand-mère",
                "Un chat",
                "un bonhomme de neige",
                "Une limance",
                "Une fée",
                "Un magicien",
                "Une tortue"
            };

            var verbes = new string[]
            {
                "mange",
                "écrase",
                "détruit",
                "observe",
                "attrape",
                "regarde",
                "avale",
                "néttoie",
                "se bat avec",
                "s'accroche à"
            };

            var complements = new string[]
            {
                "un arbre",
                "un livre",
                "la lune",
                "le soleil",
                "un serpent",
                "une carte",
                "une girafe",
                "le ciel",
                "une piscine",
                "un gateau",
                "une souris",
                "un crapaud"
            };

            CreePhrase(sujets, verbes, complements);
        }
    }
}