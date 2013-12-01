namespace InterfaceTest
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.sendButton = new System.Windows.Forms.Button();
            this.trainRadioButton = new System.Windows.Forms.RadioButton();
            this.analysisRadioButton = new System.Windows.Forms.RadioButton();
            this.modeGroupBox = new System.Windows.Forms.GroupBox();
            this.infoTextBox = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.clearButton = new System.Windows.Forms.Button();
            this.IP_TextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.IP_Label = new System.Windows.Forms.Label();
            this.port_Label = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.modeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(532, 193);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(79, 23);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "OK";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // trainRadioButton
            // 
            this.trainRadioButton.AutoSize = true;
            this.trainRadioButton.Location = new System.Drawing.Point(6, 20);
            this.trainRadioButton.Name = "trainRadioButton";
            this.trainRadioButton.Size = new System.Drawing.Size(69, 16);
            this.trainRadioButton.TabIndex = 0;
            this.trainRadioButton.TabStop = true;
            this.trainRadioButton.Text = "Training";
            this.trainRadioButton.UseVisualStyleBackColor = true;
            this.trainRadioButton.CheckedChanged += new System.EventHandler(this.trainRadioButton_CheckedChanged);
            // 
            // analysisRadioButton
            // 
            this.analysisRadioButton.AutoSize = true;
            this.analysisRadioButton.Location = new System.Drawing.Point(6, 42);
            this.analysisRadioButton.Name = "analysisRadioButton";
            this.analysisRadioButton.Size = new System.Drawing.Size(72, 16);
            this.analysisRadioButton.TabIndex = 1;
            this.analysisRadioButton.TabStop = true;
            this.analysisRadioButton.Text = "Analysis";
            this.analysisRadioButton.UseVisualStyleBackColor = true;
            this.analysisRadioButton.CheckedChanged += new System.EventHandler(this.analysisRadioButton_CheckedChanged);
            // 
            // modeGroupBox
            // 
            this.modeGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.modeGroupBox.Controls.Add(this.analysisRadioButton);
            this.modeGroupBox.Controls.Add(this.trainRadioButton);
            this.modeGroupBox.Location = new System.Drawing.Point(508, 114);
            this.modeGroupBox.Name = "modeGroupBox";
            this.modeGroupBox.Size = new System.Drawing.Size(96, 73);
            this.modeGroupBox.TabIndex = 3;
            this.modeGroupBox.TabStop = false;
            this.modeGroupBox.Text = "Mode";
            // 
            // infoTextBox
            // 
            this.infoTextBox.Enabled = false;
            this.infoTextBox.Location = new System.Drawing.Point(617, 129);
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.Size = new System.Drawing.Size(73, 21);
            this.infoTextBox.TabIndex = 4;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(653, 469);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(65, 12);
            this.label.TabIndex = 5;
            this.label.Text = "충남대학교";
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.Window;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Location = new System.Drawing.Point(35, 50);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(453, 407);
            this.panel.TabIndex = 6;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(617, 193);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(79, 23);
            this.clearButton.TabIndex = 7;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // IP_TextBox
            // 
            this.IP_TextBox.Location = new System.Drawing.Point(508, 69);
            this.IP_TextBox.Name = "IP_TextBox";
            this.IP_TextBox.Size = new System.Drawing.Size(141, 21);
            this.IP_TextBox.TabIndex = 8;
            this.IP_TextBox.Text = "168.188.111.43";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(655, 69);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(41, 21);
            this.portTextBox.TabIndex = 9;
            this.portTextBox.Text = "22";
            // 
            // IP_Label
            // 
            this.IP_Label.AutoSize = true;
            this.IP_Label.Location = new System.Drawing.Point(512, 50);
            this.IP_Label.Name = "IP_Label";
            this.IP_Label.Size = new System.Drawing.Size(16, 12);
            this.IP_Label.TabIndex = 10;
            this.IP_Label.Text = "IP";
            // 
            // port_Label
            // 
            this.port_Label.AutoSize = true;
            this.port_Label.Location = new System.Drawing.Point(653, 50);
            this.port_Label.Name = "port_Label";
            this.port_Label.Size = new System.Drawing.Size(29, 12);
            this.port_Label.TabIndex = 11;
            this.port_Label.Text = "포트";
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(615, 114);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(75, 12);
            this.infoLabel.TabIndex = 12;
            this.infoLabel.Text = "Training Info";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(518, 252);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(182, 159);
            this.textBox.TabIndex = 13;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(524, 417);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(80, 29);
            this.saveButton.TabIndex = 14;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(617, 417);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(83, 29);
            this.deleteButton.TabIndex = 16;
            this.deleteButton.Text = "Delete All";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(730, 496);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.port_Label);
            this.Controls.Add(this.IP_Label);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.IP_TextBox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.label);
            this.Controls.Add(this.infoTextBox);
            this.Controls.Add(this.modeGroupBox);
            this.Controls.Add(this.sendButton);
            this.MaximumSize = new System.Drawing.Size(746, 534);
            this.MinimumSize = new System.Drawing.Size(746, 534);
            this.Name = "MainForm";
            this.Text = "텀프로젝트 클라이언트";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.modeGroupBox.ResumeLayout(false);
            this.modeGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.RadioButton trainRadioButton;
        private System.Windows.Forms.RadioButton analysisRadioButton;
        private System.Windows.Forms.GroupBox modeGroupBox;
        private System.Windows.Forms.TextBox infoTextBox;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.TextBox IP_TextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label IP_Label;
        private System.Windows.Forms.Label port_Label;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button deleteButton;
    }
}

