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



        public Form1()
        {
            InitializeComponent();
            /*for (int i = 0; i < etats.GetLength(0); i++)
            {
                newBox = new PictureBox;
                newBox.Location = new Point(12, 90);
                newBox.Name = "pictureBox0_0";
                newBox.Size = new Size(40, 40);
                newBox.TabIndex = 0;
                newBox.TabStop = false;
            }*/
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox46_Click(object sender, EventArgs e)
        {

        }
    }
}
