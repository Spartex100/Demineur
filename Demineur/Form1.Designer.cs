namespace Demineur
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            NouvellePartieBoutton = new Button();
            AfficherMineBoutton = new Button();
            MinesLabel = new Label();
            DrapeauRestantLabel = new Label();
            QuitterBoutton = new Button();
            NbMinesSelecteur = new TextBox();
            SuspendLayout();
            // 
            // NouvellePartieBoutton
            // 
            NouvellePartieBoutton.Location = new Point(12, 77);
            NouvellePartieBoutton.Name = "NouvellePartieBoutton";
            NouvellePartieBoutton.Size = new Size(496, 29);
            NouvellePartieBoutton.TabIndex = 0;
            NouvellePartieBoutton.Text = "Nouvelle partie";
            NouvellePartieBoutton.UseVisualStyleBackColor = true;
            NouvellePartieBoutton.Click += NouvellePartie_Button;
            // 
            // AfficherMineBoutton
            // 
            AfficherMineBoutton.Location = new Point(360, 42);
            AfficherMineBoutton.Name = "AfficherMineBoutton";
            AfficherMineBoutton.Size = new Size(148, 29);
            AfficherMineBoutton.TabIndex = 1;
            AfficherMineBoutton.Text = "Afficher les mines";
            AfficherMineBoutton.UseVisualStyleBackColor = true;
            AfficherMineBoutton.Click += AfficherMine_Button;
            // 
            // MinesLabel
            // 
            MinesLabel.AutoSize = true;
            MinesLabel.Location = new Point(12, 16);
            MinesLabel.Name = "MinesLabel";
            MinesLabel.Size = new Size(115, 20);
            MinesLabel.TabIndex = 2;
            MinesLabel.Text = "Mines cachées : ";
            // 
            // DrapeauRestantLabel
            // 
            DrapeauRestantLabel.AutoSize = true;
            DrapeauRestantLabel.Location = new Point(12, 46);
            DrapeauRestantLabel.Name = "DrapeauRestantLabel";
            DrapeauRestantLabel.Size = new Size(155, 20);
            DrapeauRestantLabel.TabIndex = 3;
            DrapeauRestantLabel.Text = "Drapeaux restants : 15";
            // 
            // QuitterBoutton
            // 
            QuitterBoutton.BackColor = Color.FromArgb(255, 128, 128);
            QuitterBoutton.ForeColor = Color.AliceBlue;
            QuitterBoutton.Location = new Point(360, 7);
            QuitterBoutton.Name = "QuitterBoutton";
            QuitterBoutton.Size = new Size(148, 29);
            QuitterBoutton.TabIndex = 4;
            QuitterBoutton.Text = "Quitter la partie";
            QuitterBoutton.UseVisualStyleBackColor = false;
            QuitterBoutton.Click += Quit_Button;
            // 
            // NbMinesSelecteur
            // 
            NbMinesSelecteur.Location = new Point(126, 13);
            NbMinesSelecteur.Name = "NbMinesSelecteur";
            NbMinesSelecteur.Size = new Size(46, 27);
            NbMinesSelecteur.TabIndex = 6;
            NbMinesSelecteur.Text = "15";
            NbMinesSelecteur.TextAlign = HorizontalAlignment.Center;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(126, 217, 87);
            ClientSize = new Size(520, 624);
            Controls.Add(NbMinesSelecteur);
            Controls.Add(QuitterBoutton);
            Controls.Add(DrapeauRestantLabel);
            Controls.Add(MinesLabel);
            Controls.Add(AfficherMineBoutton);
            Controls.Add(NouvellePartieBoutton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "D-Mineur";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button NouvellePartieBoutton;
        private Button AfficherMineBoutton;
        private Label MinesLabel;
        private Label DrapeauRestantLabel;
        private Button QuitterBoutton;
        private TextBox NbMinesSelecteur;
    }
}
