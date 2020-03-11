namespace Sku_Lister
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.scrapBtn = new System.Windows.Forms.Button();
            this.pdfBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.skuGrid = new System.Windows.Forms.DataGridView();
            this.PAGE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LINK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RESPONSEURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MATCH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WEBSTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HOSTIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exportBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.throbber = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.skuGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.throbber)).BeginInit();
            this.SuspendLayout();
            // 
            // scrapBtn
            // 
            this.scrapBtn.Location = new System.Drawing.Point(224, 27);
            this.scrapBtn.Name = "scrapBtn";
            this.scrapBtn.Size = new System.Drawing.Size(143, 38);
            this.scrapBtn.TabIndex = 4;
            this.scrapBtn.Text = "Scrape Links";
            this.scrapBtn.UseVisualStyleBackColor = true;
            this.scrapBtn.Click += new System.EventHandler(this.scrapBtn_Click);
            // 
            // pdfBox
            // 
            this.pdfBox.Location = new System.Drawing.Point(229, 82);
            this.pdfBox.Name = "pdfBox";
            this.pdfBox.ReadOnly = true;
            this.pdfBox.Size = new System.Drawing.Size(512, 20);
            this.pdfBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(192, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "PDF:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(53, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 38);
            this.button1.TabIndex = 7;
            this.button1.Text = "Select File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(737, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 38);
            this.button2.TabIndex = 8;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // skuGrid
            // 
            this.skuGrid.AllowUserToAddRows = false;
            this.skuGrid.AllowUserToDeleteRows = false;
            this.skuGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.skuGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PAGE,
            this.SKU,
            this.LINK,
            this.RESPONSEURL,
            this.MATCH,
            this.WEBSTATUS,
            this.HOSTIP});
            this.skuGrid.Location = new System.Drawing.Point(12, 113);
            this.skuGrid.Name = "skuGrid";
            this.skuGrid.ReadOnly = true;
            this.skuGrid.Size = new System.Drawing.Size(908, 365);
            this.skuGrid.TabIndex = 9;
            // 
            // PAGE
            // 
            this.PAGE.HeaderText = "PAGE";
            this.PAGE.Name = "PAGE";
            this.PAGE.ReadOnly = true;
            this.PAGE.Width = 123;
            // 
            // SKU
            // 
            this.SKU.HeaderText = "SKU";
            this.SKU.Name = "SKU";
            this.SKU.ReadOnly = true;
            this.SKU.Width = 123;
            // 
            // LINK
            // 
            this.LINK.HeaderText = "LINK URL";
            this.LINK.Name = "LINK";
            this.LINK.ReadOnly = true;
            this.LINK.Width = 123;
            // 
            // RESPONSEURL
            // 
            this.RESPONSEURL.HeaderText = "RESPONSE URL";
            this.RESPONSEURL.Name = "RESPONSEURL";
            this.RESPONSEURL.ReadOnly = true;
            this.RESPONSEURL.Width = 123;
            // 
            // MATCH
            // 
            this.MATCH.HeaderText = "MATCH";
            this.MATCH.Name = "MATCH";
            this.MATCH.ReadOnly = true;
            this.MATCH.Width = 123;
            // 
            // WEBSTATUS
            // 
            this.WEBSTATUS.HeaderText = "WEB STATUS";
            this.WEBSTATUS.Name = "WEBSTATUS";
            this.WEBSTATUS.ReadOnly = true;
            this.WEBSTATUS.Width = 123;
            // 
            // HOSTIP
            // 
            this.HOSTIP.HeaderText = "HOSTIP";
            this.HOSTIP.Name = "HOSTIP";
            this.HOSTIP.ReadOnly = true;
            this.HOSTIP.Width = 123;
            // 
            // exportBtn
            // 
            this.exportBtn.Location = new System.Drawing.Point(395, 27);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(143, 38);
            this.exportBtn.TabIndex = 10;
            this.exportBtn.Text = "Export to Excel";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(566, 27);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(143, 38);
            this.stopBtn.TabIndex = 11;
            this.stopBtn.Text = "Stop Scrape";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(933, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // throbber
            // 
            this.throbber.Image = global::Sku_Lister.Properties.Resources.ajax_loader_1_;
            this.throbber.Location = new System.Drawing.Point(418, 248);
            this.throbber.Name = "throbber";
            this.throbber.Size = new System.Drawing.Size(102, 101);
            this.throbber.TabIndex = 13;
            this.throbber.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 490);
            this.Controls.Add(this.throbber);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.exportBtn);
            this.Controls.Add(this.skuGrid);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pdfBox);
            this.Controls.Add(this.scrapBtn);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(949, 529);
            this.MinimumSize = new System.Drawing.Size(949, 529);
            this.Name = "Form1";
            this.Text = "Flyer Scraper";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.skuGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.throbber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button scrapBtn;
        private System.Windows.Forms.TextBox pdfBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView skuGrid;
        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PAGE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKU;
        private System.Windows.Forms.DataGridViewTextBoxColumn LINK;
        private System.Windows.Forms.DataGridViewTextBoxColumn RESPONSEURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn MATCH;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEBSTATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn HOSTIP;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.PictureBox throbber;
    }
}

