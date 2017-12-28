namespace PM
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cryptoListView2 = new PM.CryptoListView();
            this.cryptoTextBox1 = new PM.CryptoTextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(460, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // cryptoListView2
            // 
            this.cryptoListView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cryptoListView2.FullRowSelect = true;
            this.cryptoListView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.cryptoListView2.LabelEdit = true;
            this.cryptoListView2.LabelWrap = false;
            this.cryptoListView2.Location = new System.Drawing.Point(12, 38);
            this.cryptoListView2.MultiSelect = false;
            this.cryptoListView2.Name = "cryptoListView2";
            this.cryptoListView2.Size = new System.Drawing.Size(163, 231);
            this.cryptoListView2.TabIndex = 4;
            this.cryptoListView2.UseCompatibleStateImageBehavior = false;
            this.cryptoListView2.View = System.Windows.Forms.View.Details;
            this.cryptoListView2.SelectedEntryChanged += new System.EventHandler<PM.SelectionChangedEventArgs>(this.cryptoListView1_SelectedEntryChanged);
            // 
            // cryptoTextBox1
            // 
            this.cryptoTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cryptoTextBox1.Location = new System.Drawing.Point(181, 38);
            this.cryptoTextBox1.Name = "cryptoTextBox1";
            this.cryptoTextBox1.Size = new System.Drawing.Size(291, 231);
            this.cryptoTextBox1.TabIndex = 3;
            this.cryptoTextBox1.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 281);
            this.Controls.Add(this.cryptoListView2);
            this.Controls.Add(this.cryptoTextBox1);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 320);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PM16";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private CryptoTextBox cryptoTextBox1;
        private CryptoListView cryptoListView2;
    }
}