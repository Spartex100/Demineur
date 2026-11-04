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
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
