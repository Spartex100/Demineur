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
            int TailleCases = 60;
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

            int ligneInitiale = ligne -  1;
            int colonneInitiale = colonne - 1;

            int NbMinesEntourantes = 0;
            
            for (int i = ligneInitiale; i < ligneInitiale+3; i++)
            {
                for (int j = colonneInitiale; j < colonneInitiale+3; j++)
                {
                    if (i >= 0 && i <=9 && j >= 0 && j <= 9)
                    {
                        if (mines[i,j])
                        {
                            NbMinesEntourantes++;
                            
                        }
                    }
                }
            }

            casesPictureBox[ligne, colonne].Image = imagesCases[NbMinesEntourantes];
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

            void AfficherMines()
            {
                for (int ligne = 0; ligne < casesPictureBox.GetLength(0); ligne++)
                {
                    for (int colonne = 0; colonne < casesPictureBox.GetLength(1); colonne++)
                    {
                        if (mines[ligne, colonne] && etats[ligne, colonne] != EtatCase.Drapeau) // on montre pas si ya deja un drapeau dessus
                        {
                            casesPictureBox[ligne, colonne].Image = imagesCases[12];
                        }
                    }
                }
            }

            void SurClickGauche()
            {
                if (etats[ligne, colonne] == EtatCase.Cachee)
                {
                    etats[ligne, colonne] = EtatCase.Revelee;

                    if (mines[ligne, colonne]) // mine presente
                    {
                        //finDePartie = true;
                        AfficherMines();
                    }
                    else
                    {
                        if (finDePartie) // Placer les mines apres le premier clic pour pas mourrir au premier tour :)
                        {
                            finDePartie = false;
                            int[] CaseInitiale = { ligne, colonne };
                            PlacerMines(CaseInitiale);
                        }
                        
                        //PlacerMines(); // passer la case initiale mais juste une fois
                        //casesPictureBox[ligne, colonne].Image = imagesCases[0]; 
                        VerifierAlentours(ligne, colonne); // Logique principale
                        
                    }
                }
                else if (etats[ligne, colonne] == EtatCase.Revelee)
                {
                    //Rien
                }
                else if (etats[ligne, colonne] == EtatCase.Drapeau)
                {
                    //Rien
                }
            }

            void SurClickDroit()
            {
                if (etats[ligne, colonne] == EtatCase.Cachee)
                {
                    casesPictureBox[ligne, colonne].Image = imagesCases[10];
                    etats[ligne, colonne] = EtatCase.Drapeau;
                }
                else if (etats[ligne, colonne] == EtatCase.Revelee)
                {
                    //Rien
                }
                else if (etats[ligne, colonne] == EtatCase.Drapeau)
                {
                    casesPictureBox[ligne, colonne].Image = imagesCases[9];
                    etats[ligne, colonne] = EtatCase.Cachee;
                }
            }

            switch (e.Button)
            {
                case MouseButtons.Left:
                    SurClickGauche();
                    break;
                case MouseButtons.Right:
                    SurClickDroit();
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
    }
}
