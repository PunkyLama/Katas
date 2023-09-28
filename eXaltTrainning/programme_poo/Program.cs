namespace programme_poo
{
    class Enfant : Etudiant
    {
        string classeEcole;
        Dictionary<string, float> notes;
        public Enfant(string nom, int age, string classeEcole, Dictionary<string, float> notes) : base(nom, age, null)
        {
            this.classeEcole = classeEcole;
            this.notes = notes;
        }

        public override void Afficher()
        {
            AfficherNomEtAge();
            Console.WriteLine(" Enfant en classe de : " + classeEcole);
            if((notes != null)&&(notes.Count != 0))
            {
                Console.WriteLine(" Notes moyennes :");
                foreach (var note in notes)
                {
                    Console.WriteLine($"    {note.Key} : {note.Value} / 10");
                }
            }            
            AfficherProfesseurPrincipale();
        }

    }

    class Etudiant : Personne
    {
        string infoEtude;
        public Personne professeurPrincipale;
        public Etudiant(string nom, int age, string infoEtude = null) : base(nom, age, null)
        {
            this.infoEtude = infoEtude;
        }

        public override void Afficher()
        {
            AfficherNomEtAge();
            Console.WriteLine(" Etudie en : " + infoEtude);
            AfficherProfesseurPrincipale();
        }
        
        protected void AfficherProfesseurPrincipale()
        {
            if (professeurPrincipale != null)
            {
                Console.WriteLine(" Sont proffesseur principale est : ");
                professeurPrincipale.Afficher();
            }
        }
    }

    class Personne : IAffichable
    {
        static int nombreDePersonnes = 0;

        protected string nom;
        protected int age;
        string emploi;
        protected int numeroPersonne;

        public Personne(string nom, int age, string emploi = null)
        {
            this.nom = nom;
            this.age = age;
            this.emploi = emploi;
            nombreDePersonnes++;
            this.numeroPersonne = nombreDePersonnes;
        }

        public virtual void Afficher()
        {
            AfficherNomEtAge();
            if (emploi != null)
                Console.WriteLine(" Emploie : " + emploi);
            else
                Console.WriteLine(" Aucun emploie spéficié");

        }
        protected void AfficherNomEtAge()
        {
            Console.WriteLine("Personne N°" + numeroPersonne);
            Console.WriteLine("Nom : " + nom);
            Console.WriteLine(" Age : " + age);
        }

        public static void AfficherNBPersonne()
        {
            Console.WriteLine($"Nombre total de personnes : {nombreDePersonnes}");
        }
    }

    interface IAffichable
    {
        void Afficher();
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var personnes = new List<IAffichable> { 
                new Personne("Pierre", 43, "Chômeur"),
                new Personne("Jacque", 35, "Professeur"),
                new Etudiant("David", 20, "Ingénieur Informatique"),
                new Enfant("Juliette", 8, "CP", new Dictionary<string, float>{
                    {"Math", 5f },
                    {"Geo", 8.5f } 
                }),
            };

            foreach (var person in personnes)
            {
                person.Afficher();
            }
        }
    }    
}