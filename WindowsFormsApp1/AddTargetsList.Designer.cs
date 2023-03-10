namespace SniperManagerApp
{
    partial class AddTargetsListForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbPlatforms = new System.Windows.Forms.GroupBox();
            this.rbtnSteam = new System.Windows.Forms.RadioButton();
            this.gbTargetPageOrID = new System.Windows.Forms.GroupBox();
            this.tbProfilePage = new System.Windows.Forms.TextBox();
            this.gbTargetInfo = new System.Windows.Forms.GroupBox();
            this.lTargetID = new System.Windows.Forms.Label();
            this.pbTargetIcon = new System.Windows.Forms.PictureBox();
            this.lTargetNickname = new System.Windows.Forms.Label();
            this.gbGame = new System.Windows.Forms.GroupBox();
            this.clbGames = new System.Windows.Forms.CheckedListBox();
            this.groupBox2.SuspendLayout();
            this.gbPlatforms.SuspendLayout();
            this.gbTargetPageOrID.SuspendLayout();
            this.gbTargetInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTargetIcon)).BeginInit();
            this.gbGame.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBox2.TabIndex = 2;
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
            // gbPlatforms
            // 
            this.gbPlatforms.Controls.Add(this.rbtnSteam);
            this.gbPlatforms.Location = new System.Drawing.Point(12, 12);
            this.gbPlatforms.Name = "gbPlatforms";
            this.gbPlatforms.Size = new System.Drawing.Size(98, 52);
            this.gbPlatforms.TabIndex = 3;
            this.gbPlatforms.TabStop = false;
            this.gbPlatforms.Text = "Платформа";
            // 
            // rbtnSteam
            // 
            this.rbtnSteam.AutoSize = true;
            this.rbtnSteam.Checked = true;
            this.rbtnSteam.Location = new System.Drawing.Point(15, 22);
            this.rbtnSteam.Name = "rbtnSteam";
            this.rbtnSteam.Size = new System.Drawing.Size(55, 17);
            this.rbtnSteam.TabIndex = 0;
            this.rbtnSteam.TabStop = true;
            this.rbtnSteam.Text = "Steam";
            this.rbtnSteam.UseVisualStyleBackColor = true;
            // 
            // gbTargetPageOrID
            // 
            this.gbTargetPageOrID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTargetPageOrID.Controls.Add(this.tbProfilePage);
            this.gbTargetPageOrID.Location = new System.Drawing.Point(116, 12);
            this.gbTargetPageOrID.Name = "gbTargetPageOrID";
            this.gbTargetPageOrID.Size = new System.Drawing.Size(555, 51);
            this.gbTargetPageOrID.TabIndex = 4;
            this.gbTargetPageOrID.TabStop = false;
            this.gbTargetPageOrID.Text = "Адрес страницы / ID";
            // 
            // tbProfilePage
            // 
            this.tbProfilePage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProfilePage.Location = new System.Drawing.Point(12, 19);
            this.tbProfilePage.Name = "tbProfilePage";
            this.tbProfilePage.Size = new System.Drawing.Size(532, 20);
            this.tbProfilePage.TabIndex = 0;
            this.tbProfilePage.TextChanged += new System.EventHandler(this.tbProfilePage_TextChanged);
            // 
            // gbTargetInfo
            // 
            this.gbTargetInfo.Controls.Add(this.lTargetID);
            this.gbTargetInfo.Controls.Add(this.pbTargetIcon);
            this.gbTargetInfo.Controls.Add(this.lTargetNickname);
            this.gbTargetInfo.Location = new System.Drawing.Point(332, 69);
            this.gbTargetInfo.Name = "gbTargetInfo";
            this.gbTargetInfo.Size = new System.Drawing.Size(343, 179);
            this.gbTargetInfo.TabIndex = 6;
            this.gbTargetInfo.TabStop = false;
            this.gbTargetInfo.Text = "Информация";
            // 
            // lTargetID
            // 
            this.lTargetID.AutoSize = true;
            this.lTargetID.Location = new System.Drawing.Point(6, 87);
            this.lTargetID.Name = "lTargetID";
            this.lTargetID.Size = new System.Drawing.Size(0, 13);
            this.lTargetID.TabIndex = 3;
            // 
            // pbTargetIcon
            // 
            this.pbTargetIcon.Location = new System.Drawing.Point(182, 19);
            this.pbTargetIcon.Name = "pbTargetIcon";
            this.pbTargetIcon.Size = new System.Drawing.Size(150, 150);
            this.pbTargetIcon.TabIndex = 2;
            this.pbTargetIcon.TabStop = false;
            // 
            // lTargetNickname
            // 
            this.lTargetNickname.AutoSize = true;
            this.lTargetNickname.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lTargetNickname.Location = new System.Drawing.Point(6, 19);
            this.lTargetNickname.Name = "lTargetNickname";
            this.lTargetNickname.Size = new System.Drawing.Size(0, 37);
            this.lTargetNickname.TabIndex = 1;
            // 
            // gbGame
            // 
            this.gbGame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbGame.Controls.Add(this.clbGames);
            this.gbGame.Location = new System.Drawing.Point(12, 70);
            this.gbGame.Name = "gbGame";
            this.gbGame.Size = new System.Drawing.Size(99, 87);
            this.gbGame.TabIndex = 7;
            this.gbGame.TabStop = false;
            this.gbGame.Text = "Игры";
            // 
            // clbGames
            // 
            this.clbGames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.clbGames.FormattingEnabled = true;
            this.clbGames.Location = new System.Drawing.Point(6, 19);
            this.clbGames.Name = "clbGames";
            this.clbGames.Size = new System.Drawing.Size(87, 49);
            this.clbGames.TabIndex = 0;
            // 
            // AddTargetsListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(687, 561);
            this.Controls.Add(this.gbGame);
            this.Controls.Add(this.gbTargetInfo);
            this.Controls.Add(this.gbTargetPageOrID);
            this.Controls.Add(this.gbPlatforms);
            this.Controls.Add(this.groupBox2);
            this.Name = "AddTargetsListForm";
            this.Text = "Добавить цель";
            this.Load += new System.EventHandler(this.AddTargetsListForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.gbPlatforms.ResumeLayout(false);
            this.gbPlatforms.PerformLayout();
            this.gbTargetPageOrID.ResumeLayout(false);
            this.gbTargetPageOrID.PerformLayout();
            this.gbTargetInfo.ResumeLayout(false);
            this.gbTargetInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTargetIcon)).EndInit();
            this.gbGame.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox gbPlatforms;
        private System.Windows.Forms.RadioButton rbtnSteam;
        private System.Windows.Forms.GroupBox gbTargetPageOrID;
        private System.Windows.Forms.TextBox tbProfilePage;
        private System.Windows.Forms.GroupBox gbTargetInfo;
        private System.Windows.Forms.Label lTargetNickname;
        private System.Windows.Forms.PictureBox pbTargetIcon;
        private System.Windows.Forms.Label lTargetID;
        private System.Windows.Forms.GroupBox gbGame;
        private System.Windows.Forms.CheckedListBox clbGames;
    }
}