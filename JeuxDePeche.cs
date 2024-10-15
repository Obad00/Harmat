using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class JeuxDePeche
{
    private List<Joueur> Joueurs { get; }
    private TableDeJeu Table { get; }
    private int TourActuel { get; set; }
    private bool SensHoraire { get; set; }

    public JeuxDePeche(List<Joueur> joueurs)
    {
        Joueurs = joueurs;
        var paireDeCartes = new PaireDeCartes();
        paireDeCartes.Melanger();
        Table = new TableDeJeu(paireDeCartes);
        SensHoraire = true;
        DistribuerCartes();
    }

    private void DistribuerCartes()
    {
        int nbCartes = 5; // nombre initial de cartes à distribuer
        foreach (var joueur in Joueurs)
        {
            for (int i = 0; i < nbCartes; i++)
            {
                joueur.AjouterCarte(Table.Piocher());
            }
        }
    }

    public async Task DemarrerPartie()
    {
        Console.WriteLine("La partie commence !");
        ChoisirPremierJoueur();

        while (true)
        {
            var joueur = Joueurs[TourActuel];
            Console.WriteLine($"C'est au tour de {joueur}");

            if (!joueur.ADesCartes())
            {
                Console.WriteLine($"{joueur} a gagné la partie !");
                break;
            }

            JouerTour(joueur);
            await Task.Delay(1000); // retardement pour simuler le temps

            PasserAuJoueurSuivant();
        }
    }

    private void ChoisirPremierJoueur()
    {
        Random random = new Random();
        TourActuel = random.Next(Joueurs.Count);
    }

    private void JouerTour(Joueur joueur)
    {
        var carte = joueur.Main.FirstOrDefault();
if (joueur.Main.Count > 0)  // Vérifie s'il y a des cartes dans la main
{
    Table.DeposerCarte(carte);
    Console.WriteLine($"{joueur} a joué {carte}");
}
else
{
    Console.WriteLine($"{joueur} n'a pas de carte jouable et pioche !");
    joueur.AjouterCarte(Table.Piocher());
}


        Console.WriteLine($"Dernière carte sur la pile de dépôt : {Table.DerniereCarteDeposee()}");
    }

    private void PasserAuJoueurSuivant()
    {
        if (SensHoraire)
        {
            TourActuel = (TourActuel + 1) % Joueurs.Count;
        }
        else
        {
            TourActuel = (TourActuel - 1 + Joueurs.Count) % Joueurs.Count;
        }
    }
}
