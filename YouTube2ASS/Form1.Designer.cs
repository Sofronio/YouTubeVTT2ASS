namespace YouTube2ASS
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox_height = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_x = new System.Windows.Forms.TextBox();
            this.textBox_y = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_style = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_saveSetting = new System.Windows.Forms.Button();
            this.checkBox_writeScript = new System.Windows.Forms.CheckBox();
            this.button_doit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Items.AddRange(new object[] {
            "Drag and drop your YouTube VTT file here.",
            "",
            "Download VTT Auto-subtitles using youtube-dl. Find more: https://rg3.github.io/yo" +
                "utube-dl/",
            "",
            "Once loaded, click button, it\'ll automatically generate an ASS file with similar " +
                "style of YouTube.",
            "You can tweak the Style at the bottom, if the style is not working, just delete s" +
                "etting.json file.",
            "This program also carries command line nvencc generator, Find more about nvencc: " +
                "https://github.com/rigaya/NVEnc",
            "",
            "If you find this useful, please consider supporting me. Alipay: xbox2xbox@gmail.c" +
                "om",
            "My website is http://sofronio.cn , and the latest version will be there. Have a g" +
                "ood day."});
            this.listBox1.Location = new System.Drawing.Point(22, 17);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(855, 160);
            this.listBox1.TabIndex = 1;
            // 
            // textBox_height
            // 
            this.textBox_height.Location = new System.Drawing.Point(79, 202);
            this.textBox_height.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_height.Name = "textBox_height";
            this.textBox_height.Size = new System.Drawing.Size(56, 21);
            this.textBox_height.TabIndex = 6;
            this.textBox_height.Text = "54";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 203);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "行高";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 226);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "坐标 X:";
            // 
            // textBox_x
            // 
            this.textBox_x.Location = new System.Drawing.Point(79, 224);
            this.textBox_x.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_x.Name = "textBox_x";
            this.textBox_x.Size = new System.Drawing.Size(56, 21);
            this.textBox_x.TabIndex = 8;
            this.textBox_x.Text = "196";
            // 
            // textBox_y
            // 
            this.textBox_y.Location = new System.Drawing.Point(157, 224);
            this.textBox_y.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_y.Name = "textBox_y";
            this.textBox_y.Size = new System.Drawing.Size(56, 21);
            this.textBox_y.TabIndex = 10;
            this.textBox_y.Text = "1024";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(137, 226);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "Y:";
            // 
            // textBox_style
            // 
            this.textBox_style.Location = new System.Drawing.Point(79, 292);
            this.textBox_style.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_style.Name = "textBox_style";
            this.textBox_style.Size = new System.Drawing.Size(787, 21);
            this.textBox_style.TabIndex = 13;
            this.textBox_style.Text = "Style: YouTubeAuto,Arial,20,&H00FFFFFF,&HFF0000FF,&HC0000000,&H00000000,0,0,0,0,1" +
    "00,100,0,0,3,0.07,0,1,10,10,10,1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 294);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "样式";
            // 
            // button_saveSetting
            // 
            this.button_saveSetting.Location = new System.Drawing.Point(601, 202);
            this.button_saveSetting.Margin = new System.Windows.Forms.Padding(2);
            this.button_saveSetting.Name = "button_saveSetting";
            this.button_saveSetting.Size = new System.Drawing.Size(228, 60);
            this.button_saveSetting.TabIndex = 15;
            this.button_saveSetting.Text = "Save Setting";
            this.button_saveSetting.UseVisualStyleBackColor = true;
            this.button_saveSetting.Click += new System.EventHandler(this.button_saveSetting_Click);
            // 
            // checkBox_writeScript
            // 
            this.checkBox_writeScript.AutoSize = true;
            this.checkBox_writeScript.Location = new System.Drawing.Point(338, 268);
            this.checkBox_writeScript.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_writeScript.Name = "checkBox_writeScript";
            this.checkBox_writeScript.Size = new System.Drawing.Size(216, 16);
            this.checkBox_writeScript.TabIndex = 16;
            this.checkBox_writeScript.Text = "write avisynth and nvencc script";
            this.checkBox_writeScript.UseVisualStyleBackColor = true;
            // 
            // button_doit
            // 
            this.button_doit.Location = new System.Drawing.Point(338, 202);
            this.button_doit.Margin = new System.Windows.Forms.Padding(2);
            this.button_doit.Name = "button_doit";
            this.button_doit.Size = new System.Drawing.Size(228, 60);
            this.button_doit.TabIndex = 17;
            this.button_doit.Text = "Do it!";
            this.button_doit.UseVisualStyleBackColor = true;
            this.button_doit.Click += new System.EventHandler(this.button_doit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 334);
            this.Controls.Add(this.button_doit);
            this.Controls.Add(this.checkBox_writeScript);
            this.Controls.Add(this.button_saveSetting);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_style);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_y);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_x);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_height);
            this.Controls.Add(this.listBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "YouTube2ASS v0.0.3 by Sofronio.cn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox_height;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_x;
        private System.Windows.Forms.TextBox textBox_y;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_style;
        private System.Windows.Forms.Button button_saveSetting;
        private System.Windows.Forms.CheckBox checkBox_writeScript;
        private System.Windows.Forms.Button button_doit;
    }
}

