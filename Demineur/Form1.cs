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

        int nbMines = 20;
        EtatCase[,] etats = new EtatCase[10, 10];
        bool[,] mines = new bool[10, 10];
        PictureBox[,] casesPictureBox = new PictureBox[10, 10];

        Image[] imagesCases = new Image[13];

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

        public void PlacerMines()
        {
            void VerifierEtPlacerMine()
            {
                int ligne = rnd.Next(10);
                int colonne = rnd.Next(10);

                if (mines[ligne, colonne]) // mine deja presente
                {
                    VerifierEtPlacerMine(); // Pour placer la mine ailleurs que sur une mine deja a cet endroit
                }
                else
                {
                    mines[ligne, colonne] = true;
                }

            }
            for (int i = 0;i < nbMines; i++)
            {
                VerifierEtPlacerMine();
            }
        }

        public Form1()
        {
            InitializeComponent();
            InitialiserImages();
            for (int ligne = 0; ligne < casesPictureBox.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < casesPictureBox.GetLength(1); colonne++)
                {

                    PictureBox newBox = new PictureBox();
                    /*((System.ComponentModel.ISupportInitialize)newBox).BeginInit();*/
                    newBox.Location = new Point(10 + colonne * 45, 120 + ligne * 45);
                    newBox.Name = $"pictureBox{ligne}_{colonne}";
                    newBox.Size = new Size(40, 40);
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

            PlacerMines();
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
                        if (mines[ligne, colonne])
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
                        AfficherMines();
                    }
                    else
                    {
                        casesPictureBox[ligne, colonne].Image = imagesCases[0];
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
    }
}
