namespace programme_fichiers
{
    class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllText("monFichier.txt", "Voici le contenu que j'écris dans le fichier texte");
            try
            {
                string resultat = File.ReadAllText("monFichier2.txt");
                Console.WriteLine(resultat);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("ERREUR : Ce fichier n'existe pas");
            }
            catch
            {
                Console.WriteLine("Une erreur inconnue est arrivée");
            }
        }
    }
}