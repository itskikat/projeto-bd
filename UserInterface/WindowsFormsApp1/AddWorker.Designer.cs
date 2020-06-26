namespace WindowsFormsApp1
{
    partial class AddWorker
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
            this.GeneroBox = new System.Windows.Forms.ComboBox();
            this.IdadeBox = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.telefoneTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nomeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cartaTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.salarioTextBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.IdadeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.salarioTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GeneroBox
            // 
            this.GeneroBox.FormattingEnabled = true;
            this.GeneroBox.Items.AddRange(new object[] {
            "M",
            "F"});
            this.GeneroBox.Location = new System.Drawing.Point(147, 238);
            this.GeneroBox.Name = "GeneroBox";
            this.GeneroBox.Size = new System.Drawing.Size(121, 24);
            this.GeneroBox.TabIndex = 27;
            // 
            // IdadeBox
            // 
            this.IdadeBox.Location = new System.Drawing.Point(421, 238);
            this.IdadeBox.Name = "IdadeBox";
            this.IdadeBox.Size = new System.Drawing.Size(107, 22);
            this.IdadeBox.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(347, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 24);
            this.label7.TabIndex = 25;
            this.label7.Text = "Idade";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(41, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 24);
            this.label4.TabIndex = 24;
            this.label4.Text = "Genero";
            // 
            // telefoneTextBox
            // 
            this.telefoneTextBox.Location = new System.Drawing.Point(147, 161);
            this.telefoneTextBox.Multiline = true;
            this.telefoneTextBox.Name = "telefoneTextBox";
            this.telefoneTextBox.Size = new System.Drawing.Size(381, 24);
            this.telefoneTextBox.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(42, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 24);
            this.label3.TabIndex = 22;
            this.label3.Text = "Telefone";
            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(147, 101);
            this.emailTextBox.Multiline = true;
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(381, 24);
            this.emailTextBox.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(42, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 24);
            this.label2.TabIndex = 20;
            this.label2.Text = "Email";
            // 
            // nomeTextBox
            // 
            this.nomeTextBox.Location = new System.Drawing.Point(147, 46);
            this.nomeTextBox.Multiline = true;
            this.nomeTextBox.Name = "nomeTextBox";
            this.nomeTextBox.Size = new System.Drawing.Size(381, 24);
            this.nomeTextBox.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 24);
            this.label1.TabIndex = 18;
            this.label1.Text = "Nome";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(42, 317);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 24);
            this.label5.TabIndex = 28;
            this.label5.Text = "Salario";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(221, 455);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 50);
            this.button1.TabIndex = 30;
            this.button1.Text = "Adicionar Funcionário";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.AdicionarFuncionario);
            // 
            // cartaTextBox
            // 
            this.cartaTextBox.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.cartaTextBox.Location = new System.Drawing.Point(193, 382);
            this.cartaTextBox.Multiline = true;
            this.cartaTextBox.Name = "cartaTextBox";
            this.cartaTextBox.Size = new System.Drawing.Size(335, 24);
            this.cartaTextBox.TabIndex = 32;
            this.cartaTextBox.Text = "(opcional)";
            this.cartaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cartaTextBox.TextChanged += new System.EventHandler(this.cartaTextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(41, 380);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 24);
            this.label6.TabIndex = 31;
            this.label6.Text = "Carta Conducao";
            // 
            // salarioTextBox
            // 
            this.salarioTextBox.Location = new System.Drawing.Point(147, 320);
            this.salarioTextBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.salarioTextBox.Name = "salarioTextBox";
            this.salarioTextBox.Size = new System.Drawing.Size(381, 22);
            this.salarioTextBox.TabIndex = 33;
            // 
            // AddWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 547);
            this.Controls.Add(this.salarioTextBox);
            this.Controls.Add(this.cartaTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.GeneroBox);
            this.Controls.Add(this.IdadeBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.telefoneTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nomeTextBox);
            this.Controls.Add(this.label1);
            this.Name = "AddWorker";
            this.Text = "AddWorker";
            ((System.ComponentModel.ISupportInitialize)(this.IdadeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.salarioTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox GeneroBox;
        private System.Windows.Forms.NumericUpDown IdadeBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox telefoneTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nomeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox cartaTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown salarioTextBox;
    }
}