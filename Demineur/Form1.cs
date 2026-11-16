// Justin Vachon & Marc-Antoine Plante

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

        int nbMines = 30;
        EtatCase[,] etats = new EtatCase[10, 10];
        bool[,] mines = new bool[10, 10];
        PictureBox[,] casesPictureBox = new PictureBox[10, 10];

        Image[] imagesCases = new Image[13];

        bool finDePartie = true;

        public void InitialiserImages()
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

        public void InitialiserNouvellePartie()
        {
            nbMines = 20; // A CHANGER (Selectionner par le joueur)
            MinesLabel.Text = $"Mines : {nbMines}";
            DrapeauRestantLabel.Text = $"Drapeaux restants : {nbMines}";

            for (int ligne = 0; ligne < mines.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < mines.GetLength(1); colonne++)
                {
                    mines[ligne, colonne] = false;
                }
            }

            for (int ligne = 0; ligne < etats.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < etats.GetLength(1); colonne++)
                {
                    etats[ligne, colonne] = EtatCase.Cachee;
                    casesPictureBox[ligne, colonne].Image = imagesCases[9];
                }
            }

            finDePartie = true;

        }


        public void PlacerMines(int[] CaseInitiale)

        {
            void VerifierEtPlacerMine()
            {
                int ligne = rnd.Next(10);
                int colonne = rnd.Next(10);

                if (mines[ligne, colonne] || (ligne == CaseInitiale[0] && colonne == CaseInitiale[1])) // mine deja presente
                {
                    VerifierEtPlacerMine(); // Pour placer la mine ailleurs que sur une mine deja a cet endroit
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

        public Form1()
        {

            InitializeComponent();
            InitialiserImages();
            int TailleCases = 50;
            for (int ligne = 0; ligne < casesPictureBox.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < casesPictureBox.GetLength(1); colonne++)
                {


                    PictureBox newBox = new PictureBox();
                    /*((System.ComponentModel.ISupportInitialize)newBox).BeginInit();*/
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

                    /*((System.ComponentModel.ISupportInitialize)newBox).EndInit();*/

                    casesPictureBox[ligne, colonne] = newBox;
                    etats[ligne, colonne] = EtatCase.Cachee;

                    newBox.MouseClick += PictureBox_Click;
                }
            }
        }


        void VerifierAlentours(int ligne, int colonne)
        {
            //casesPictureBox[ligne, colonne].Image = imagesCases[0];

            int ligneInitiale = ligne - 1;
            int colonneInitiale = colonne - 1;

            int NbMinesEntourantes = 0;

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

            casesPictureBox[ligne, colonne].Image = imagesCases[NbMinesEntourantes];


            /*if (NbMinesEntourantes == 0)
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

        void AfficherMines(int[] CaseCliquee)
        {
            int ligneCliquee = CaseCliquee[0];
            int colonneCliquee = CaseCliquee[1];

            for (int ligne = 0; ligne < casesPictureBox.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < casesPictureBox.GetLength(1); colonne++)
                {
                    if (mines[ligne, colonne] && etats[ligne, colonne] != EtatCase.Drapeau) // on montre pas si ya deja un drapeau dessus
                    {
                        if (casesPictureBox[ligne, colonne].Image == imagesCases[9])
                        {
                            if (ligne == ligneCliquee && colonne == colonneCliquee)
                            {
                                casesPictureBox[ligne, colonne].Image = imagesCases[12];
                            }
                            else
                            {
                                casesPictureBox[ligne, colonne].Image = imagesCases[11];
                            }

                        }
                        else if (casesPictureBox[ligne, colonne].Image == imagesCases[12] || casesPictureBox[ligne, colonne].Image == imagesCases[11])
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

            if (finDePartie)
            {

            }
            else
            {

            }

            void SurClickGauche()
            {
                if (etats[ligne, colonne] == EtatCase.Cachee)
                {
                    etats[ligne, colonne] = EtatCase.Revelee;

                    if (mines[ligne, colonne]) // mine presente
                    {
                        AfficherMines(CaseCliqueeCoordonnee);
                        finDePartie = true;

                    }
                    else
                    {

                        VerifierAlentours(ligne, colonne); // Logique principale

                        int CalculerCasesRevelee()
                        {
                            int Revelee = 0;

                            for (int ligne = 0; ligne < casesPictureBox.GetLength(0); ligne++)
                            {
                                for (int colonne = 0; colonne < casesPictureBox.GetLength(1); colonne++)
                                {
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
                            MessageBox.Show("Yippie");
                            finDePartie = true;
                        }

                    }
                }
            }

            void SurClickDroit()
            {

                int CalculerDrapeauxRestants()
                {
                    int drapeaux = 0;

                    for (int ligne = 0; ligne < casesPictureBox.GetLength(0); ligne++)
                    {
                        for (int colonne = 0; colonne < casesPictureBox.GetLength(1); colonne++)
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
                    if (finDePartie)
                    {
                        finDePartie = false;
                        PlacerMines(CaseCliqueeCoordonnee);
                    }

                    if (!finDePartie)
                    {
                        SurClickGauche();
                    }


                    break;
                case MouseButtons.Right:
                    if (!finDePartie)
                    {
                        SurClickDroit();
                    }
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

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
                AfficherMines(fausseCase);
            }
        }

        private void NouvellePartie_Button(object sender, EventArgs e)
        {
            InitialiserNouvellePartie();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox caseSelectionMines = (TextBox)sender;

            
        }
    }
}
