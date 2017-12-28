namespace KeySaver
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.mySeachTextBox1 = new KeySaver.SeachTextBox();
            this.myTextBox1 = new KeySaver.CryptoTextBox();
            this.myListView2 = new KeySaver.CryptoListView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.myTextBox1);
            this.panel1.Controls.Add(this.myListView2);
            this.panel1.Location = new System.Drawing.Point(12, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 306);
            this.panel1.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(207, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 306);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // mySeachTextBox1
            // 
            this.mySeachTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mySeachTextBox1.Location = new System.Drawing.Point(12, 12);
            this.mySeachTextBox1.Name = "mySeachTextBox1";
            this.mySeachTextBox1.Size = new System.Drawing.Size(627, 20);
            this.mySeachTextBox1.TabIndex = 1;
            // 
            // myTextBox1
            // 
            this.myTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTextBox1.Location = new System.Drawing.Point(207, 0);
            this.myTextBox1.Name = "myTextBox1";
            this.myTextBox1.Size = new System.Drawing.Size(420, 306);
            this.myTextBox1.TabIndex = 3;
            this.myTextBox1.Text = "Please choose an item!";
            // 
            // myListView2
            // 
            this.myListView2.Dock = System.Windows.Forms.DockStyle.Left;
            this.myListView2.FullRowSelect = true;
            this.myListView2.LabelEdit = true;
            this.myListView2.LabelWrap = false;
            this.myListView2.Location = new System.Drawing.Point(0, 0);
            this.myListView2.MultiSelect = false;
            this.myListView2.Name = "myListView2";
            this.myListView2.Size = new System.Drawing.Size(207, 306);
            this.myListView2.TabIndex = 2;
            this.myListView2.UseCompatibleStateImageBehavior = false;
            this.myListView2.View = System.Windows.Forms.View.List;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 356);
            this.Controls.Add(this.mySeachTextBox1);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Key Saver 2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private CryptoTextBox myTextBox1;
        private CryptoListView myListView2;
        private SeachTextBox mySeachTextBox1;
    }
}