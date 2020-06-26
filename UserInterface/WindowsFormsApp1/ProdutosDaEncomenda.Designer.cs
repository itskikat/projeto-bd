namespace WindowsFormsApp1
{
    partial class ProdutosDaEncomenda
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ProdutoListView = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ProdutoListView);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(2, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1066, 660);
            this.panel1.TabIndex = 23;
            // 
            // ProdutoListView
            // 
            this.ProdutoListView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ProdutoListView.FullRowSelect = true;
            this.ProdutoListView.GridLines = true;
            this.ProdutoListView.HideSelection = false;
            this.ProdutoListView.Location = new System.Drawing.Point(-3, 0);
            this.ProdutoListView.MultiSelect = false;
            this.ProdutoListView.Name = "ProdutoListView";
            this.ProdutoListView.Size = new System.Drawing.Size(1049, 611);
            this.ProdutoListView.TabIndex = 23;
            this.ProdutoListView.UseCompatibleStateImageBehavior = false;
            this.ProdutoListView.View = System.Windows.Forms.View.Details;
            // 
            // ProdutosDaEncomenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 613);
            this.Controls.Add(this.panel1);
            this.Name = "ProdutosDaEncomenda";
            this.Text = "ProdutosDaEncomenda";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView ProdutoListView;
    }
}