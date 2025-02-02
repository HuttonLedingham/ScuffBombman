namespace bombs
{
    partial class SPMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ContinueButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.HighScoreLabel = new System.Windows.Forms.Label();
            this.HighestLevelLabel = new System.Windows.Forms.Label();
            this.CurrentScorelLabel = new System.Windows.Forms.Label();
            this.CurrentLevelLebal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ContinueButton
            // 
            this.ContinueButton.BackColor = System.Drawing.Color.PaleGreen;
            this.ContinueButton.Enabled = false;
            this.ContinueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContinueButton.Location = new System.Drawing.Point(152, 331);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(144, 107);
            this.ContinueButton.TabIndex = 0;
            this.ContinueButton.Text = "Continue";
            this.ContinueButton.UseVisualStyleBackColor = false;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(152, 243);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 74);
            this.button2.TabIndex = 1;
            this.button2.Text = "New Game";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.SteelBlue;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 39);
            this.button3.TabIndex = 2;
            this.button3.Text = "Back";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightCoral;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(96, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 75);
            this.label1.TabIndex = 3;
            this.label1.Text = "Single Player";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HighScoreLabel
            // 
            this.HighScoreLabel.AutoSize = true;
            this.HighScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HighScoreLabel.Location = new System.Drawing.Point(53, 151);
            this.HighScoreLabel.Name = "HighScoreLabel";
            this.HighScoreLabel.Size = new System.Drawing.Size(118, 75);
            this.HighScoreLabel.TabIndex = 5;
            this.HighScoreLabel.Text = "HighScore:\r\n\r\n\r\n";
            this.HighScoreLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // HighestLevelLabel
            // 
            this.HighestLevelLabel.AutoSize = true;
            this.HighestLevelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HighestLevelLabel.Location = new System.Drawing.Point(257, 151);
            this.HighestLevelLabel.Name = "HighestLevelLabel";
            this.HighestLevelLabel.Size = new System.Drawing.Size(149, 25);
            this.HighestLevelLabel.TabIndex = 6;
            this.HighestLevelLabel.Text = "Highest Level:";
            // 
            // CurrentScorelLabel
            // 
            this.CurrentScorelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentScorelLabel.Location = new System.Drawing.Point(12, 331);
            this.CurrentScorelLabel.Name = "CurrentScorelLabel";
            this.CurrentScorelLabel.Size = new System.Drawing.Size(109, 55);
            this.CurrentScorelLabel.TabIndex = 7;
            this.CurrentScorelLabel.Text = "Current Score:";
            // 
            // CurrentLevelLebal
            // 
            this.CurrentLevelLebal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentLevelLebal.Location = new System.Drawing.Point(13, 381);
            this.CurrentLevelLebal.Name = "CurrentLevelLebal";
            this.CurrentLevelLebal.Size = new System.Drawing.Size(100, 48);
            this.CurrentLevelLebal.TabIndex = 8;
            this.CurrentLevelLebal.Text = "Current Level:";
            // 
            // SPMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(471, 450);
            this.Controls.Add(this.CurrentLevelLebal);
            this.Controls.Add(this.CurrentScorelLabel);
            this.Controls.Add(this.HighestLevelLabel);
            this.Controls.Add(this.HighScoreLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ContinueButton);
            this.Name = "SPMenu";
            this.Text = "SPMenu";
            this.Load += new System.EventHandler(this.SPMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label HighScoreLabel;
        private System.Windows.Forms.Label HighestLevelLabel;
        private System.Windows.Forms.Label CurrentScorelLabel;
        private System.Windows.Forms.Label CurrentLevelLebal;
    }
}