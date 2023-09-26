namespace jeu_du_pendu
{
    class program
    {
        static void Main(string[] args)
        {
            var mots = ChargerLesMots("mots.txt");
            if((mots == null) || (mots.Length == 0))
            {
                Console.WriteLine("Impossible de lancer le jeu.");
            } else
            {
                while(true)
                {
                    Random r = new Random();
                    string mot = mots[r.Next(mots.Length)].Trim().ToUpper();
                    DevinerMot(mot);
                    if (!DemandeDeRejouer())
                    {
                        break;
                    }
                }
                Console.WriteLine("Merci et à bientôt.");
            }
        }

        private static void DevinerMot(string mot)
        {
            List<char> lettresDevinnee = new List<char>();
            List<char> lettresExclue = new List<char>();
            const int NB_VIES = 6;
            int vieRestante = NB_VIES;
            while (vieRestante > 0)
            {
                Console.WriteLine(Ascii.PENDU[NB_VIES-vieRestante]);
                Console.WriteLine();
                AfficherMot(mot, lettresDevinnee);
                Console.WriteLine();
                char lettre = DemanderUneLettre();
                Console.Clear();
                if (mot.Contains(lettre))
                {
                    Console.WriteLine("Cette lettre est dans le mot.");
                    lettresDevinnee.Add(lettre);
                    if(ToutesLettresDevinees(mot, lettresDevinnee))
                    {
                        break;
                    }
                }
                else
                {
                    if (!lettresExclue.Contains(lettre))
                    {
                        vieRestante--;
                        lettresExclue.Add(lettre);
                    }                                   
                    Console.WriteLine($"Vies restantes : {vieRestante}");
                }
                if(lettresExclue.Count > 0)
                    Console.WriteLine($"Le mot ne contient pas les lettres : {String.Join(", ", lettresExclue)}");
                Console.WriteLine();
            }

            Console.WriteLine(Ascii.PENDU[NB_VIES - vieRestante]);

            if (vieRestante == 0)
            {
                Console.WriteLine($"Perdu ! le mot etait : {mot}");
            }
            else
            {
                AfficherMot(mot, lettresDevinnee);
                Console.WriteLine() ;
                Console.WriteLine("Gagner !");
            }
            

            //DemanderUneLettre();
        }

        static char DemanderUneLettre()
        {
            while (true)
            {
                Console.Write("Rentrez une lettre : ");
                string reponse = Console.ReadLine();
                if (reponse != "" || reponse.Length > 1)
                {
                    reponse = reponse.ToUpper();
                    return reponse[0];
                }                    
                Console.WriteLine("ERREUR : Vous devez rentrer une lettre");
            }

        }

        static void AfficherMot(string mot, List<char> lettres)
        {
            for(int i = 0; i < mot.Length; i++)
            {
                char lettre = mot[i];
                if (lettres.Contains(lettre))
                    Console.Write($"{lettre} ");
                else
                    Console.Write("_ ");
            }
            Console.WriteLine();
        }

        static bool ToutesLettresDevinees(string mot, List<char> lettres)
        {
            foreach(char lettre in lettres)
            {
                mot = mot.Replace(lettre.ToString(), "");
            }
            if (mot.Length == 0)
                return true;
            return false;
        }

        static string[] ChargerLesMots(string nomFichier)
        {
            try
            {
                return File.ReadAllLines(nomFichier);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de lecture du fichier : {nomFichier}, message : {ex.Message}");
            }

            return null;
        }

        static bool DemandeDeRejouer()
        {
            Console.WriteLine("Voulez-vous rejouer ? (o/n)");
            char reponse = DemanderUneLettre();
            if (reponse == 'o' || reponse == 'O')
            {
                return true;
            } else if (reponse == 'n' ||  reponse == 'N')
            {
                return false;
            }
            else
            {
                Console.WriteLine("Erreur : vous devez repondre avec o ou n");
                return DemandeDeRejouer();
            }
        }
    }
}