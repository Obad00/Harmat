public class Joueur
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Prenom { get; }
    public string Nom { get; }
    public List<Carte> Main { get; }

    public Joueur(string prenom, string nom)
    {
        Prenom = prenom;
        Nom = nom;
        Main = new List<Carte>();
    }

    public override string ToString()
    {
        return $"{Prenom} {Nom}";
    }

    public void AjouterCarte(Carte carte)
    {
        if (Main.Count < 8)
        {
            Main.Add(carte);
        }
    }

    public void RetirerCarte(Carte carte)
    {
        Main.Remove(carte);
    }
}
