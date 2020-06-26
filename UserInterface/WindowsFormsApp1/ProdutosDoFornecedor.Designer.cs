namespace WindowsFormsApp1
{
    partial class ProdutosDoFornecedor
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
            this.ProdutoListView = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Comprar = new System.Windows.Forms.Button();
            this.ProductQuantity = new System.Windows.Forms.NumericUpDown();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // ProdutoListView
            // 
            this.ProdutoListView.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProdutoListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProdutoListView.FullRowSelect = true;
            this.ProdutoListView.GridLines = true;
            this.ProdutoListView.HideSelection = false;
            this.ProdutoListView.Location = new System.Drawing.Point(0, 0);
            this.ProdutoListView.MultiSelect = false;
            this.ProdutoListView.Name = "ProdutoListView";
            this.ProdutoListView.Size = new System.Drawing.Size(980, 565);
            this.ProdutoListView.TabIndex = 24;
            this.ProdutoListView.UseCompatibleStateImageBehavior = false;
            this.ProdutoListView.View = System.Windows.Forms.View.Details;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.Comprar);
            this.panel2.Controls.Add(this.ProductQuantity);
            this.panel2.Location = new System.Drawing.Point(0, 571);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(317, 57);
            this.panel2.TabIndex = 25;
            // 
            // Comprar
            // 
            this.Comprar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Comprar.Location = new System.Drawing.Point(3, 11);
            this.Comprar.Name = "Comprar";
            this.Comprar.Size = new System.Drawing.Size(168, 34);
            this.Comprar.TabIndex = 4;
            this.Comprar.Text = "Comprar";
            this.Comprar.UseVisualStyleBackColor = true;
            this.Comprar.Click += new System.EventHandler(this.Comprar_Click);
            // 
            // ProductQuantity
            // 
            this.ProductQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProductQuantity.Location = new System.Drawing.Point(194, 14);
            this.ProductQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ProductQuantity.Name = "ProductQuantity";
            this.ProductQuantity.Size = new System.Drawing.Size(120, 30);
            this.ProductQuantity.TabIndex = 5;
            this.ProductQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ProdutosDoFornecedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(980, 629);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ProdutoListView);
            this.Name = "ProdutosDoFornecedor";
            this.Text = "ProdutosDaFornecedor";
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProductQuantity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ProdutoListView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Comprar;
        private System.Windows.Forms.NumericUpDown ProductQuantity;
    }
}