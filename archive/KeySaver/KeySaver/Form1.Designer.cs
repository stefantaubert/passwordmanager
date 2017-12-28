namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Gmx23333333333333333333333333333333333333333333333",
            "12.01.12"}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.löschenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pwSearchBool = new System.Windows.Forms.ToolStripMenuItem();
            this.alleEinträgeKopiernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einträgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.registryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eintragHinzufügenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eintragAktivierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passwortMaker1 = new WindowsFormsApplication1.PasswortMaker();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(2, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(478, 271);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "Wählen Sie eine Rubrik";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(9, 28);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer1.Size = new System.Drawing.Size(705, 271);
            this.splitContainer1.SplitterDistance = 224;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Margin = new System.Windows.Forms.Padding(2);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(224, 271);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.treeView1_SelectedIndexChanged);
            this.listView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyUp_1);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Rubriken (78)";
            this.columnHeader1.Width = 238;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.löschenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 26);
            // 
            // löschenToolStripMenuItem
            // 
            this.löschenToolStripMenuItem.Name = "löschenToolStripMenuItem";
            this.löschenToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.löschenToolStripMenuItem.Text = "Löschen";
            this.löschenToolStripMenuItem.Click += new System.EventHandler(this.löschenToolStripMenuItem_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Italic);
            this.textBox1.ForeColor = System.Drawing.Color.Gray;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(224, 24);
            this.textBox1.Text = "Suchen";
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter_1);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave_1);
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pwSearchBool,
            this.alleEinträgeKopiernToolStripMenuItem,
            this.einträgeToolStripMenuItem});
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(28, 24);
            // 
            // pwSearchBool
            // 
            this.pwSearchBool.CheckOnClick = true;
            this.pwSearchBool.Name = "pwSearchBool";
            this.pwSearchBool.Size = new System.Drawing.Size(237, 22);
            this.pwSearchBool.Text = "auch nach Passwörtern suchen";
            this.pwSearchBool.Click += new System.EventHandler(this.pwSearchBool_Click);
            // 
            // alleEinträgeKopiernToolStripMenuItem
            // 
            this.alleEinträgeKopiernToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("alleEinträgeKopiernToolStripMenuItem.Image")));
            this.alleEinträgeKopiernToolStripMenuItem.Name = "alleEinträgeKopiernToolStripMenuItem";
            this.alleEinträgeKopiernToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.alleEinträgeKopiernToolStripMenuItem.Text = "alle Einträge kopieren";
            this.alleEinträgeKopiernToolStripMenuItem.Click += new System.EventHandler(this.alleEinträgeKopiernToolStripMenuItem_Click);
            // 
            // einträgeToolStripMenuItem
            // 
            this.einträgeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importierenToolStripMenuItem,
            this.exportierenToolStripMenuItem});
            this.einträgeToolStripMenuItem.Name = "einträgeToolStripMenuItem";
            this.einträgeToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.einträgeToolStripMenuItem.Text = "Einträge";
            // 
            // importierenToolStripMenuItem
            // 
            this.importierenToolStripMenuItem.Name = "importierenToolStripMenuItem";
            this.importierenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importierenToolStripMenuItem.Text = "importieren";
            this.importierenToolStripMenuItem.Click += new System.EventHandler(this.importierenToolStripMenuItem_Click);
            // 
            // exportierenToolStripMenuItem
            // 
            this.exportierenToolStripMenuItem.Name = "exportierenToolStripMenuItem";
            this.exportierenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportierenToolStripMenuItem.Text = "exportieren";
            this.exportierenToolStripMenuItem.Click += new System.EventHandler(this.exportierenToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.addToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addToolStripMenuItem.Image")));
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(100, 24);
            this.addToolStripMenuItem.Text = "Hinzufügen";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // showGeneratorToolStripMenuItem
            // 
            this.showGeneratorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showGeneratorToolStripMenuItem.Image")));
            this.showGeneratorToolStripMenuItem.Name = "showGeneratorToolStripMenuItem";
            this.showGeneratorToolStripMenuItem.Size = new System.Drawing.Size(133, 24);
            this.showGeneratorToolStripMenuItem.Tag = "an";
            this.showGeneratorToolStripMenuItem.Text = "Passwortgenerator";
            this.showGeneratorToolStripMenuItem.Click += new System.EventHandler(this.showGeneratorToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(87, 24);
            this.saveToolStripMenuItem.Text = "Speichern";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            this.saveToolStripMenuItem.EnabledChanged += new System.EventHandler(this.saveToolStripMenuItem_EnabledChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textBox1,
            this.toolStripMenuItem1,
            this.addToolStripMenuItem,
            this.showGeneratorToolStripMenuItem,
            this.registryToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(723, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Tag = "";
            this.menuStrip1.Text = "menuStrip1";
            // 
            // registryToolStripMenuItem
            // 
            this.registryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eintragHinzufügenToolStripMenuItem,
            this.eintragAktivierenToolStripMenuItem});
            this.registryToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("registryToolStripMenuItem.Image")));
            this.registryToolStripMenuItem.Name = "registryToolStripMenuItem";
            this.registryToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.registryToolStripMenuItem.Text = "Registry";
            // 
            // eintragHinzufügenToolStripMenuItem
            // 
            this.eintragHinzufügenToolStripMenuItem.Name = "eintragHinzufügenToolStripMenuItem";
            this.eintragHinzufügenToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.eintragHinzufügenToolStripMenuItem.Text = "Eintrag hinzufügen";
            this.eintragHinzufügenToolStripMenuItem.Click += new System.EventHandler(this.eintragHinzufügenToolStripMenuItem_Click);
            // 
            // eintragAktivierenToolStripMenuItem
            // 
            this.eintragAktivierenToolStripMenuItem.Name = "eintragAktivierenToolStripMenuItem";
            this.eintragAktivierenToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.eintragAktivierenToolStripMenuItem.Text = "Eintrag aktivieren";
            this.eintragAktivierenToolStripMenuItem.Click += new System.EventHandler(this.eintragAktivierenToolStripMenuItem_Click);
            // 
            // passwortMaker1
            // 
            this.passwortMaker1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.passwortMaker1.Location = new System.Drawing.Point(10, 186);
            this.passwortMaker1.Margin = new System.Windows.Forms.Padding(2);
            this.passwortMaker1.Name = "passwortMaker1";
            this.passwortMaker1.Size = new System.Drawing.Size(705, 107);
            this.passwortMaker1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 309);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.passwortMaker1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(460, 238);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Schlüssel- und Passwortverwaltungsprogramm";
            this.TopMost = true;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private PasswortMaker passwortMaker1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem löschenToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripTextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pwSearchBool;
        private System.Windows.Forms.ToolStripMenuItem alleEinträgeKopiernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem einträgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importierenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportierenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem registryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eintragHinzufügenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eintragAktivierenToolStripMenuItem;
    }
}

