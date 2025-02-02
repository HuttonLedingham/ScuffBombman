namespace bombs
{
    partial class MPEndScreen
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
            this.winMessage = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // winMessage
            // 
            this.winMessage.BackColor = System.Drawing.Color.LightSalmon;
            this.winMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winMessage.Location = new System.Drawing.Point(12, 37);
            this.winMessage.Name = "winMessage";
            this.winMessage.Size = new System.Drawing.Size(381, 101);
            this.winMessage.TabIndex = 0;
            this.winMessage.Text = "Congrats to Player 1 for Winning!";
            this.winMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SpringGreen;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(72, 178);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(254, 65);
            this.button1.TabIndex = 1;
            this.button1.Text = "Back to Main Menu";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MPEndScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(405, 283);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.winMessage);
            this.Name = "MPEndScreen";
            this.Text = "MPEndScreen";
            this.Load += new System.EventHandler(this.MPEndScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label winMessage;
        private System.Windows.Forms.Button button1;
    }
}