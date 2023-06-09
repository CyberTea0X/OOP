namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ImperialValue1 = new System.Windows.Forms.TextBox();
            this.metricValue1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.ImperialValue2 = new System.Windows.Forms.TextBox();
            this.metricValue2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(788, 435);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ImperialValue1);
            this.tabPage1.Controls.Add(this.metricValue1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(780, 402);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Измерение длины";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ImperialValue1
            // 
            this.ImperialValue1.Location = new System.Drawing.Point(706, 226);
            this.ImperialValue1.Name = "ImperialValue1";
            this.ImperialValue1.ReadOnly = true;
            this.ImperialValue1.Size = new System.Drawing.Size(66, 27);
            this.ImperialValue1.TabIndex = 15;
            // 
            // metricValue1
            // 
            this.metricValue1.Location = new System.Drawing.Point(706, 193);
            this.metricValue1.Name = "metricValue1";
            this.metricValue1.ReadOnly = true;
            this.metricValue1.Size = new System.Drawing.Size(66, 27);
            this.metricValue1.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(504, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Эмпирическое значение";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(504, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Метрическое значение";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(489, 135);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(284, 52);
            this.button4.TabIndex = 11;
            this.button4.Text = "Закончить сбор данных";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.StopCollecting1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(489, 77);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(284, 52);
            this.button3.TabIndex = 10;
            this.button3.Text = "Начать сбор данных";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.startCollecting1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(489, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(284, 52);
            this.button1.TabIndex = 8;
            this.button1.Text = "Создать устройство";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.createDevice1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(8, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(475, 364);
            this.listBox1.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ImperialValue2);
            this.tabPage2.Controls.Add(this.metricValue2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.button9);
            this.tabPage2.Controls.Add(this.button10);
            this.tabPage2.Controls.Add(this.button12);
            this.tabPage2.Controls.Add(this.listBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(780, 402);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Измерение массы";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(489, 135);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(284, 52);
            this.button9.TabIndex = 18;
            this.button9.Text = "Закончить сбор данных";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.StopCollecting2_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(489, 77);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(284, 52);
            this.button10.TabIndex = 17;
            this.button10.Text = "Начать сбор данных";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.startCollecting2_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(489, 19);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(284, 52);
            this.button12.TabIndex = 15;
            this.button12.Text = "Создать устройство";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.createDevice2_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(8, 19);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(475, 364);
            this.listBox2.TabIndex = 14;
            // 
            // ImperialValue2
            // 
            this.ImperialValue2.Location = new System.Drawing.Point(706, 226);
            this.ImperialValue2.Name = "ImperialValue2";
            this.ImperialValue2.ReadOnly = true;
            this.ImperialValue2.Size = new System.Drawing.Size(66, 27);
            this.ImperialValue2.TabIndex = 22;
            // 
            // metricValue2
            // 
            this.metricValue2.Location = new System.Drawing.Point(706, 193);
            this.metricValue2.Name = "metricValue2";
            this.metricValue2.ReadOnly = true;
            this.metricValue2.Size = new System.Drawing.Size(66, 27);
            this.metricValue2.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(504, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "Эмпирическое значение";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(504, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "Метрическое значение";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button button4;
        private Button button3;
        private Button button1;
        private ListBox listBox1;
        private TabPage tabPage2;
        private Button button9;
        private Button button10;
        private Button button12;
        private ListBox listBox2;
        private Label label1;
        private TextBox ImperialValue1;
        private TextBox metricValue1;
        private Label label2;
        private TextBox ImperialValue2;
        private TextBox metricValue2;
        private Label label3;
        private Label label4;
    }
}