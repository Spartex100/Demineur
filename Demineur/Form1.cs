// Justin Vachon & Marc-Antoine Plante

using System.Diagnostics.Eventing.Reader;

enum EtatCase
{
    Cachee,
    Revelee,
    Drapeau
}

namespace Demineur
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();

        int nbMines = 15;
        EtatCase[,] etats = new EtatCase[10, 10]; 
        bool[,] mines = new bool[10, 10];
        PictureBox[,] casesPictureBox = new PictureBox[10, 10]; //Création des picturebox

        Image[] imagesCases = new Image[13];

        bool finDePartie = false;

        public void InitialiserImages() // Pré-chargement des images par numéro suivi des autres images
        {

            string dossier = "ImagesDemineur"; 

            for (int i = 0; i <= 8; i++)
            {
                imagesCases[i] = Image.FromFile($"{dossier}/{i}.png");
            }

            imagesCases[9] = Image.FromFile($"{dossier}/cachee.png");
            imagesCases[10] = Image.FromFile($"{dossier}/drapeau.png");
            imagesCases[11] = Image.FromFile($"{dossier}/mine.png");
            imagesCases[12] = Image.FromFile($"{dossier}/mineExplosee.png");
        }

        public void InitialiserNouvellePartie() //Débuter la partie, Supression des mines, Cacher les cases, Validation du nombre de mines, check de fin de partie
        {

            int nouvNbMines;

            bool estUnEntier = int.TryParse(NbMinesSelecteur.Text, out nouvNbMines);

            if (estUnEntier) // Vérification du nombre de mines séléctionnés
            {
                if (nouvNbMines >= 5 && nouvNbMines <= 30) // Vérification du nombre de mines séléctionnés
                {
                    nbMines = nouvNbMines;
                    DrapeauRestantLabel.Text = $"Drapeaux restants : {nbMines}";

                    for (int ligne = 0; ligne < etats.GetLength(0); ligne++)
                    {
                        for (int colonne = 0; colonne < etats.GetLength(1); colonne++) 
                        {
                            // réinitialiser toute les cases
                            mines[ligne, colonne] = false;
                            etats[ligne, colonne] = EtatCase.Cachee; 
                            casesPictureBox[ligne, colonne].Image = imagesCases[9];
                        }
                    }

                    finDePartie = false;
                }
                else
                {
                    NbMinesSelecteur.Text = "15";
                    MessageBox.Show("Le nombre de mines doit être un \nnombre entier entre 5 et 30 inclus.");
                }
            }
            else
            {
                NbMinesSelecteur.Text = "15";
                MessageBox.Show("Le nombre de mines doit être un \nnombre entier entre 5 et 30 inclus.");
            }

            

        }

        public void PlacerMines(int[] CaseInitiale) // Placement des mines après le premier clic pour éviter de perdre au premier tour.

        {
            void VerifierEtPlacerMine() //Défini l'emplacement des mines après le premier clic
            {
                int ligne = rnd.Next(10);
                int colonne = rnd.Next(10);

                if (mines[ligne, colonne] || (ligne == CaseInitiale[0] && colonne == CaseInitiale[1])) // mine deja presente ou case initiale cliquée
                {
                    VerifierEtPlacerMine(); // Pour placer la mine ailleurs que sur une mine deja a cet endroit jusqu a succès
                }
                else
                {
                    mines[ligne, colonne] = true; 
                }

            }

            for (int i = 0; i < nbMines; i++)
            {
                VerifierEtPlacerMine();
            }
        }

        void VerifierAlentours(int ligne, int colonne) // Verifier les 8 cases autour de la case cliquée pour compter les mines
        {

            int ligneInitiale = ligne - 1;
            int colonneInitiale = colonne - 1; // commencer par le coin superieur gauche

            int NbMinesEntourantes = 0; // compte de mines

            for (int i = ligneInitiale; i < ligneInitiale + 3; i++) 
            {
                for (int j = colonneInitiale; j < colonneInitiale + 3; j++)
                {
                    if (i >= 0 && i <= 9 && j >= 0 && j <= 9)
                    {
                        if (mines[i, j])
                        {
                            NbMinesEntourantes++;

                        }
                    }
                }
            }

            casesPictureBox[ligne, colonne].Image = imagesCases[NbMinesEntourantes]; // Afficher le nombre de mines entourantes sur la case

            /*if (NbMinesEntourantes == 0) // Bonus non fonctionnel
            {
                for (int i = ligneInitiale; i < ligneInitiale + 3; i++)
                {
                    for (int j = colonneInitiale; j < colonneInitiale + 3; j++)
                    {
                        if (i >= 0 && i <= 9 && j >= 0 && j <= 9)
                        {
                            VerifierAlentours(i, j);
                        }
                    }
                }
            }*/

        }

        void AfficherMines(int[] CaseCliquee, bool forcerAffichage) // Trouver toute les mines et les afficher ou cacher (toggle)
        {
            int ligneCliquee = CaseCliquee[0];
            int colonneCliquee = CaseCliquee[1];

            for (int ligne = 0; ligne < casesPictureBox.GetLength(0); ligne++) // Vérifier chaques lignes
            {
                for (int colonne = 0; colonne < casesPictureBox.GetLength(1); colonne++) // Vérifier chaques colonne
                {
                    if (mines[ligne, colonne] && etats[ligne, colonne] != EtatCase.Drapeau) // on montre pas si il y a deja un drapeau dessus
                    {
                        if (casesPictureBox[ligne, colonne].Image == imagesCases[9] || (forcerAffichage && casesPictureBox[ligne, colonne].Image == imagesCases[11])) // Si les mines sont cachées OU Révélés mais la partie est finie
                        {
                            if (ligne == ligneCliquee && colonne == colonneCliquee) // mine rouge si s'agit de la case cliquée
                            {
                                casesPictureBox[ligne, colonne].Image = imagesCases[12]; 
                            }
                            else
                            {
                                casesPictureBox[ligne, colonne].Image = imagesCases[11]; // sinon mine normale
                            }
                        }
                        else if (!forcerAffichage && (casesPictureBox[ligne, colonne].Image == imagesCases[12] || casesPictureBox[ligne, colonne].Image == imagesCases[11])) // Si les mines sont déja révélés
                        {
                            casesPictureBox[ligne, colonne].Image = imagesCases[9];
                        }
                    }
                }
            }
        }

        private void PictureBox_Click(object sender, MouseEventArgs e)
        {
            PictureBox caseCliquee = (PictureBox)sender;

            string tag = (string)caseCliquee.Tag;
            if (tag == null)
            {
                MessageBox.Show("Vous avez oublié un tag. Le programme va planté sous peu.");
            }

            string[] coordonnees = tag.Split(',');
            int ligne = int.Parse(coordonnees[0]);
            int colonne = int.Parse(coordonnees[1]);

            int[] CaseCliqueeCoordonnee = { ligne, colonne };

            void SurClicGauche() // Logique sur clic gauche...
            {

                bool minesPlacees = false;
                for (int ligne = 0; ligne < casesPictureBox.GetLength(0); ligne++)
                {
                    for (int colonne = 0; colonne < casesPictureBox.GetLength(1); colonne++)
                    {
                        // Trouver si les mines ont été placées
                        if (mines[ligne, colonne]) 
                        {
                            minesPlacees = true;
                        }
                    }
                }
                if (!minesPlacees)
                {
                    PlacerMines(CaseCliqueeCoordonnee);
                }

                if (etats[ligne, colonne] == EtatCase.Cachee)
                {
                    etats[ligne, colonne] = EtatCase.Revelee;

                    if (mines[ligne, colonne]) // mine presente
                    {
                        AfficherMines(CaseCliqueeCoordonnee, true);
                        MessageBox.Show("Oupelai... :/");
                        finDePartie = true;

                    }
                    else
                    {

                        VerifierAlentours(ligne, colonne);

                        int CalculerCasesRevelee() // Retourne le nombre de cases révélés (Pour détérminer une victoire)
                        {
                            int Revelee = 0;

                            for (int ligne = 0; ligne < casesPictureBox.GetLength(0); ligne++)
                            {
                                for (int colonne = 0; colonne < casesPictureBox.GetLength(1); colonne++)
                                {
                                    // compter chaques cases cliquées
                                    if (etats[ligne, colonne] == EtatCase.Revelee) 
                                    {
                                        Revelee++;
                                    }
                                }
                            }
                            return Revelee;
                        }
                        if (CalculerCasesRevelee() + nbMines == 100)
                        {
                            MessageBox.Show("Yippie! :D");
                            finDePartie = true;
                        }

                    }
                }
            }

            void SurClicDroit()  // Logique sur clic droit!?!
            {

                int CalculerDrapeauxRestants() // Compter le nombre de drapeux placés
                {
                    int drapeaux = 0;

                    for (int ligne = 0; ligne < casesPictureBox.GetLength(0); ligne++) 
                    { 
                        for (int colonne = 0; colonne < casesPictureBox.GetLength(1); colonne++) // Compter les drapeaux sur chaques cases
                        {
                            if (etats[ligne, colonne] == EtatCase.Drapeau)
                            {
                                drapeaux++;
                            }
                        }
                    }

                    int drapeauRestants = nbMines - drapeaux;
                    return drapeauRestants;
                }

                if (etats[ligne, colonne] == EtatCase.Cachee && CalculerDrapeauxRestants() > 0)
                {
                    casesPictureBox[ligne, colonne].Image = imagesCases[10];
                    etats[ligne, colonne] = EtatCase.Drapeau;
                }
                else if (etats[ligne, colonne] == EtatCase.Drapeau)
                {
                    casesPictureBox[ligne, colonne].Image = imagesCases[9];
                    etats[ligne, colonne] = EtatCase.Cachee;

                }
                DrapeauRestantLabel.Text = $"Drapeaux restants : {CalculerDrapeauxRestants()}";

            }

            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (!finDePartie)
                    {
                        SurClicGauche();
                    }

                    break;
                case MouseButtons.Right:
                    if (!finDePartie)
                    {
                        SurClicDroit();
                    }
                    break;
                default:
                    break;
            }
        }

        public Form1()
        {

            InitializeComponent();
            InitialiserImages();
            int TailleCases = 50;
            for (int ligne = 0; ligne < casesPictureBox.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < casesPictureBox.GetLength(1); colonne++) // Générer chaques cases du jeu et initialiser chaques valeurs
                {

                    PictureBox newBox = new PictureBox();
                    newBox.Location = new Point(10 + colonne * TailleCases, 120 + ligne * TailleCases);
                    newBox.Name = $"pictureBox{ligne}_{colonne}";
                    newBox.Size = new Size(TailleCases, TailleCases);
                    newBox.TabIndex = 0;
                    newBox.TabStop = false;
                    newBox.BackColor = System.Drawing.Color.LightGray;
                    newBox.Image = imagesCases[9];
                    newBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    newBox.Tag = $"{ligne},{colonne}";
                    Controls.Add(newBox);
                    casesPictureBox[ligne, colonne] = newBox;
                    etats[ligne, colonne] = EtatCase.Cachee;

                    newBox.MouseClick += PictureBox_Click;
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Quit_Button(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void AfficherMine_Button(object sender, EventArgs e)
        {
            if (!finDePartie)
            {
                int[] fausseCase = { 10, 10 };
                AfficherMines(fausseCase, false);
            }
        }

        private void NouvellePartie_Button(object sender, EventArgs e)
        {
            InitialiserNouvellePartie();
        }
    }
}
