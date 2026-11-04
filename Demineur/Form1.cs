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
        EtatCase[,] etats = new EtatCase[10, 10];
        bool[,] mines = new bool[10, 10];
        PictureBox[,] casesPictureBox = new PictureBox[10, 10];

        Image[] imagesCases = new Image[13];

        public void InitialiserImages()
        {
            
            string dossier = "ImagesDemineur";

            for (int i = 0; i<=8 ; i++)
            {
                imagesCases[i] = Image.FromFile($"{dossier}/{i}.png");
            }

            imagesCases[9] = Image.FromFile($"{dossier}/cachee.png");
            imagesCases[10] = Image.FromFile($"{dossier}/drapeau.png");
            imagesCases[11] = Image.FromFile($"{dossier}/mine.png");
            imagesCases[12] = Image.FromFile($"{dossier}/mineExplosee.png");
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
                    newBox.Location = new Point(10 + colonne * 45, 70 + ligne * 45);
                    newBox.Name = $"pictureBox{ligne}_{colonne}";
                    newBox.Size = new Size(40, 40);
                    newBox.TabIndex = 0;
                    newBox.TabStop = false;
                    newBox.BackColor = System.Drawing.Color.LightGray;
                    newBox.Image = imagesCases[9];
                    newBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    Controls.Add(newBox);
                    /*((System.ComponentModel.ISupportInitialize)newBox).EndInit();*/
                }

            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
