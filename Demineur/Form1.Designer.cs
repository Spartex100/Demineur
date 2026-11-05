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
            NouvellePartieBoutton = new Button();
            AfficherMineBoutton = new Button();
            MinesLabel = new Label();
            DrapeauRestantLabel = new Label();
            QuitterBoutton = new Button();
            SuspendLayout();
            // 
            // NouvellePartieBoutton
            // 
            NouvellePartieBoutton.Location = new Point(12, 82);
            NouvellePartieBoutton.Name = "NouvellePartieBoutton";
            NouvellePartieBoutton.Size = new Size(438, 29);
            NouvellePartieBoutton.TabIndex = 0;
            NouvellePartieBoutton.Text = "Nouvelle partie";
            NouvellePartieBoutton.UseVisualStyleBackColor = true;
            NouvellePartieBoutton.Click += button1_Click;
            // 
            // AfficherMineBoutton
            // 
            AfficherMineBoutton.Location = new Point(302, 47);
            AfficherMineBoutton.Name = "AfficherMineBoutton";
            AfficherMineBoutton.Size = new Size(148, 29);
            AfficherMineBoutton.TabIndex = 1;
            AfficherMineBoutton.Text = "Afficher les mines";
            AfficherMineBoutton.UseVisualStyleBackColor = true;
            AfficherMineBoutton.Click += button2_Click;
            // 
            // MinesLabel
            // 
            MinesLabel.AutoSize = true;
            MinesLabel.Location = new Point(12, 16);
            MinesLabel.Name = "MinesLabel";
            MinesLabel.Size = new Size(75, 20);
            MinesLabel.TabIndex = 2;
            MinesLabel.Text = "Mines : 00";
            MinesLabel.Click += label1_Click;
            // 
            // DrapeauRestantLabel
            // 
            DrapeauRestantLabel.AutoSize = true;
            DrapeauRestantLabel.Location = new Point(12, 51);
            DrapeauRestantLabel.Name = "DrapeauRestantLabel";
            DrapeauRestantLabel.Size = new Size(155, 20);
            DrapeauRestantLabel.TabIndex = 3;
            DrapeauRestantLabel.Text = "Drapeaux restants : 00";
            DrapeauRestantLabel.Click += label2_Click;
            // 
            // QuitterBoutton
            // 
            QuitterBoutton.BackColor = Color.FromArgb(255, 128, 128);
            QuitterBoutton.ForeColor = Color.AliceBlue;
            QuitterBoutton.Location = new Point(302, 12);
            QuitterBoutton.Name = "QuitterBoutton";
            QuitterBoutton.Size = new Size(148, 29);
            QuitterBoutton.TabIndex = 4;
            QuitterBoutton.Text = "Quitter la partie";
            QuitterBoutton.UseVisualStyleBackColor = false;
            QuitterBoutton.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(462, 572);
            Controls.Add(QuitterBoutton);
            Controls.Add(DrapeauRestantLabel);
            Controls.Add(MinesLabel);
            Controls.Add(AfficherMineBoutton);
            Controls.Add(NouvellePartieBoutton);
            Name = "Form1";
            Text = "Des mineurs";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button NouvellePartieBoutton;
        private Button AfficherMineBoutton;
        private Label MinesLabel;
        private Label DrapeauRestantLabel;
        private Button QuitterBoutton;
    }
}
