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
            this.button_doit = new System.Windows.Forms.Button();
            this.textBox_height = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_x = new System.Windows.Forms.TextBox();
            this.textBox_y = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 21;
            this.listBox1.Items.AddRange(new object[] {
            "Drop your YouTube VTT file here!"});
            this.listBox1.Location = new System.Drawing.Point(40, 29);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1565, 277);
            this.listBox1.TabIndex = 1;
            // 
            // button_doit
            // 
            this.button_doit.Location = new System.Drawing.Point(619, 353);
            this.button_doit.Name = "button_doit";
            this.button_doit.Size = new System.Drawing.Size(418, 105);
            this.button_doit.TabIndex = 3;
            this.button_doit.Text = "Do it!";
            this.button_doit.UseVisualStyleBackColor = true;
            this.button_doit.Click += new System.EventHandler(this.button_doit_Click);
            // 
            // textBox_height
            // 
            this.textBox_height.Location = new System.Drawing.Point(145, 353);
            this.textBox_height.Name = "textBox_height";
            this.textBox_height.Size = new System.Drawing.Size(100, 31);
            this.textBox_height.TabIndex = 6;
            this.textBox_height.Text = "50";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 356);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "行高";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 395);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "坐标 X:";
            // 
            // textBox_x
            // 
            this.textBox_x.Location = new System.Drawing.Point(145, 392);
            this.textBox_x.Name = "textBox_x";
            this.textBox_x.Size = new System.Drawing.Size(100, 31);
            this.textBox_x.TabIndex = 8;
            this.textBox_x.Text = "196";
            // 
            // textBox_y
            // 
            this.textBox_y.Location = new System.Drawing.Point(288, 392);
            this.textBox_y.Name = "textBox_y";
            this.textBox_y.Size = new System.Drawing.Size(100, 31);
            this.textBox_y.TabIndex = 10;
            this.textBox_y.Text = "1024";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 395);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 21);
            this.label3.TabIndex = 11;
            this.label3.Text = "Y:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(40, 597);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1565, 455);
            this.richTextBox1.TabIndex = 12;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(145, 511);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1439, 31);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "Style: Default,Arial,20,&H00FFFFFF,&H000000FF,&HC0000000,&H00000000,0,0,0,0,100,1" +
    "00,0,0,3,2,0,1,10,10,10,1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 514);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 21);
            this.label4.TabIndex = 14;
            this.label4.Text = "样式";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1639, 1095);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_y);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_x);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_height);
            this.Controls.Add(this.button_doit);
            this.Controls.Add(this.listBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "YouTube2ASS v0.01 by Sofronio.cn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button_doit;
        private System.Windows.Forms.TextBox textBox_height;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_x;
        private System.Windows.Forms.TextBox textBox_y;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
    }
}

