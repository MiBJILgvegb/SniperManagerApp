namespace SniperManagerApp
{
    partial class ShowTargetsList
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
            this.lbTargetsList = new System.Windows.Forms.ListBox();
            this.gbTargetInfo = new System.Windows.Forms.GroupBox();
            this.gbGames = new System.Windows.Forms.GroupBox();
            this.clbGames = new System.Windows.Forms.CheckedListBox();
            this.cbIsWatching = new System.Windows.Forms.CheckBox();
            this.lID = new System.Windows.Forms.Label();
            this.pbTargetIcon = new System.Windows.Forms.PictureBox();
            this.lNickname = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbTargetInfo.SuspendLayout();
            this.gbGames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTargetIcon)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTargetsList
            // 
            this.lbTargetsList.FormattingEnabled = true;
            this.lbTargetsList.Location = new System.Drawing.Point(12, 12);
            this.lbTargetsList.Name = "lbTargetsList";
            this.lbTargetsList.Size = new System.Drawing.Size(253, 537);
            this.lbTargetsList.TabIndex = 0;
            this.lbTargetsList.SelectedIndexChanged += new System.EventHandler(this.lbTargetsList_SelectedIndexChanged);
            // 
            // gbTargetInfo
            // 
            this.gbTargetInfo.Controls.Add(this.gbGames);
            this.gbTargetInfo.Controls.Add(this.cbIsWatching);
            this.gbTargetInfo.Controls.Add(this.lID);
            this.gbTargetInfo.Controls.Add(this.pbTargetIcon);
            this.gbTargetInfo.Controls.Add(this.lNickname);
            this.gbTargetInfo.Location = new System.Drawing.Point(275, 12);
            this.gbTargetInfo.Name = "gbTargetInfo";
            this.gbTargetInfo.Size = new System.Drawing.Size(400, 485);
            this.gbTargetInfo.TabIndex = 1;
            this.gbTargetInfo.TabStop = false;
            this.gbTargetInfo.Text = "Описание";
            this.gbTargetInfo.Visible = false;
            // 
            // gbGames
            // 
            this.gbGames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGames.Controls.Add(this.clbGames);
            this.gbGames.Location = new System.Drawing.Point(244, 181);
            this.gbGames.Name = "gbGames";
            this.gbGames.Size = new System.Drawing.Size(149, 69);
            this.gbGames.TabIndex = 6;
            this.gbGames.TabStop = false;
            this.gbGames.Text = "Игры";
            // 
            // clbGames
            // 
            this.clbGames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbGames.FormattingEnabled = true;
            this.clbGames.Location = new System.Drawing.Point(6, 19);
            this.clbGames.Name = "clbGames";
            this.clbGames.Size = new System.Drawing.Size(137, 34);
            this.clbGames.TabIndex = 0;
            // 
            // cbIsWatching
            // 
            this.cbIsWatching.AutoSize = true;
            this.cbIsWatching.Location = new System.Drawing.Point(8, 150);
            this.cbIsWatching.Name = "cbIsWatching";
            this.cbIsWatching.Size = new System.Drawing.Size(94, 17);
            this.cbIsWatching.TabIndex = 5;
            this.cbIsWatching.Text = "Отслеживать";
            this.cbIsWatching.UseVisualStyleBackColor = true;
            // 
            // lID
            // 
            this.lID.AutoSize = true;
            this.lID.Location = new System.Drawing.Point(9, 97);
            this.lID.Name = "lID";
            this.lID.Size = new System.Drawing.Size(0, 13);
            this.lID.TabIndex = 4;
            // 
            // pbTargetIcon
            // 
            this.pbTargetIcon.Location = new System.Drawing.Point(244, 19);
            this.pbTargetIcon.Name = "pbTargetIcon";
            this.pbTargetIcon.Size = new System.Drawing.Size(150, 150);
            this.pbTargetIcon.TabIndex = 3;
            this.pbTargetIcon.TabStop = false;
            // 
            // lNickname
            // 
            this.lNickname.AutoSize = true;
            this.lNickname.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lNickname.Location = new System.Drawing.Point(14, 28);
            this.lNickname.Name = "lNickname";
            this.lNickname.Size = new System.Drawing.Size(0, 37);
            this.lNickname.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnApply);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Location = new System.Drawing.Point(313, 503);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(362, 46);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(5, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(115, 27);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(123, 13);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(115, 27);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Применить";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(241, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(115, 27);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ShowTargetsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 561);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbTargetInfo);
            this.Controls.Add(this.lbTargetsList);
            this.Name = "ShowTargetsList";
            this.Text = "Список целей";
            this.Load += new System.EventHandler(this.ShowTargetsList_Load);
            this.gbTargetInfo.ResumeLayout(false);
            this.gbTargetInfo.PerformLayout();
            this.gbGames.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbTargetIcon)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbTargetsList;
        private System.Windows.Forms.GroupBox gbTargetInfo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lNickname;
        private System.Windows.Forms.PictureBox pbTargetIcon;
        private System.Windows.Forms.CheckBox cbIsWatching;
        private System.Windows.Forms.Label lID;
        private System.Windows.Forms.GroupBox gbGames;
        private System.Windows.Forms.CheckedListBox clbGames;
    }
}