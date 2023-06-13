namespace Clicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Income = new System.Windows.Forms.TextBox();
            this.Baker = new System.Windows.Forms.Button();
            this.BakerCount = new System.Windows.Forms.TextBox();
            this.BakeryCount = new System.Windows.Forms.TextBox();
            this.Bakery = new System.Windows.Forms.Button();
            this.FactoryCount = new System.Windows.Forms.TextBox();
            this.Factory = new System.Windows.Forms.Button();
            this.CountryCount = new System.Windows.Forms.TextBox();
            this.Country = new System.Windows.Forms.Button();
            this.PlanetCount = new System.Windows.Forms.TextBox();
            this.Planet = new System.Windows.Forms.Button();
            this.UpdateClick = new System.Windows.Forms.Button();
            this.UpdateCookie = new System.Windows.Forms.Button();
            this.Cookie = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Cookies = new System.Windows.Forms.TextBox();
            this.Money = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(205, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(411, 81);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cookie Clicker";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "Доход";
            // 
            // Income
            // 
            this.Income.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Income.Location = new System.Drawing.Point(98, 9);
            this.Income.Name = "Income";
            this.Income.ReadOnly = true;
            this.Income.Size = new System.Drawing.Size(101, 38);
            this.Income.TabIndex = 3;
            this.Income.Text = "0";
            // 
            // Baker
            // 
            this.Baker.Location = new System.Drawing.Point(587, 105);
            this.Baker.Name = "Baker";
            this.Baker.Size = new System.Drawing.Size(153, 38);
            this.Baker.TabIndex = 4;
            this.Baker.Text = "Пекарь [10], +1";
            this.Baker.UseVisualStyleBackColor = true;
            this.Baker.Click += new System.EventHandler(this.Baker_Click);
            // 
            // BakerCount
            // 
            this.BakerCount.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BakerCount.Location = new System.Drawing.Point(746, 105);
            this.BakerCount.Name = "BakerCount";
            this.BakerCount.ReadOnly = true;
            this.BakerCount.Size = new System.Drawing.Size(54, 38);
            this.BakerCount.TabIndex = 5;
            this.BakerCount.Text = "0";
            // 
            // BakeryCount
            // 
            this.BakeryCount.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BakeryCount.Location = new System.Drawing.Point(746, 149);
            this.BakeryCount.Name = "BakeryCount";
            this.BakeryCount.ReadOnly = true;
            this.BakeryCount.Size = new System.Drawing.Size(54, 38);
            this.BakeryCount.TabIndex = 7;
            this.BakeryCount.Text = "0";
            // 
            // Bakery
            // 
            this.Bakery.Location = new System.Drawing.Point(587, 149);
            this.Bakery.Name = "Bakery";
            this.Bakery.Size = new System.Drawing.Size(153, 38);
            this.Bakery.TabIndex = 6;
            this.Bakery.Text = "Пекарня [100] + 5";
            this.Bakery.UseVisualStyleBackColor = true;
            this.Bakery.Click += new System.EventHandler(this.Bakery_Click);
            // 
            // FactoryCount
            // 
            this.FactoryCount.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FactoryCount.Location = new System.Drawing.Point(746, 195);
            this.FactoryCount.Name = "FactoryCount";
            this.FactoryCount.ReadOnly = true;
            this.FactoryCount.Size = new System.Drawing.Size(54, 38);
            this.FactoryCount.TabIndex = 9;
            this.FactoryCount.Text = "0";
            // 
            // Factory
            // 
            this.Factory.Location = new System.Drawing.Point(587, 195);
            this.Factory.Name = "Factory";
            this.Factory.Size = new System.Drawing.Size(153, 38);
            this.Factory.TabIndex = 8;
            this.Factory.Text = "Завод [1000] + 30";
            this.Factory.UseVisualStyleBackColor = true;
            this.Factory.Click += new System.EventHandler(this.Factory_Click);
            // 
            // CountryCount
            // 
            this.CountryCount.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CountryCount.Location = new System.Drawing.Point(746, 241);
            this.CountryCount.Name = "CountryCount";
            this.CountryCount.ReadOnly = true;
            this.CountryCount.Size = new System.Drawing.Size(54, 38);
            this.CountryCount.TabIndex = 11;
            this.CountryCount.Text = "0";
            // 
            // Country
            // 
            this.Country.Location = new System.Drawing.Point(587, 239);
            this.Country.Name = "Country";
            this.Country.Size = new System.Drawing.Size(153, 48);
            this.Country.TabIndex = 10;
            this.Country.Text = "Страна [100000] + 1000";
            this.Country.UseVisualStyleBackColor = true;
            this.Country.Click += new System.EventHandler(this.Country_Click);
            // 
            // PlanetCount
            // 
            this.PlanetCount.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PlanetCount.Location = new System.Drawing.Point(746, 297);
            this.PlanetCount.Name = "PlanetCount";
            this.PlanetCount.ReadOnly = true;
            this.PlanetCount.Size = new System.Drawing.Size(54, 38);
            this.PlanetCount.TabIndex = 13;
            this.PlanetCount.Text = "0";
            // 
            // Planet
            // 
            this.Planet.Location = new System.Drawing.Point(587, 293);
            this.Planet.Name = "Planet";
            this.Planet.Size = new System.Drawing.Size(153, 84);
            this.Planet.TabIndex = 12;
            this.Planet.Text = "Планета [99999999] + 10000000";
            this.Planet.UseVisualStyleBackColor = true;
            this.Planet.Click += new System.EventHandler(this.Planet_Click);
            // 
            // UpdateClick
            // 
            this.UpdateClick.Location = new System.Drawing.Point(12, 108);
            this.UpdateClick.Name = "UpdateClick";
            this.UpdateClick.Size = new System.Drawing.Size(233, 38);
            this.UpdateClick.TabIndex = 14;
            this.UpdateClick.Text = "Улучшить клик [100]";
            this.UpdateClick.UseVisualStyleBackColor = true;
            this.UpdateClick.Click += new System.EventHandler(this.UpdateClick_Click);
            // 
            // UpdateCookie
            // 
            this.UpdateCookie.Location = new System.Drawing.Point(12, 152);
            this.UpdateCookie.Name = "UpdateCookie";
            this.UpdateCookie.Size = new System.Drawing.Size(233, 40);
            this.UpdateCookie.TabIndex = 15;
            this.UpdateCookie.Text = "Новое печенье [1000]";
            this.UpdateCookie.UseVisualStyleBackColor = true;
            this.UpdateCookie.Click += new System.EventHandler(this.UpdateCookie_Click);
            // 
            // Cookie
            // 
            this.Cookie.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cookie.Image = ((System.Drawing.Image)(resources.GetObject("Cookie.Image")));
            this.Cookie.Location = new System.Drawing.Point(267, 119);
            this.Cookie.Name = "Cookie";
            this.Cookie.Size = new System.Drawing.Size(296, 231);
            this.Cookie.TabIndex = 16;
            this.Cookie.UseVisualStyleBackColor = true;
            this.Cookie.Click += new System.EventHandler(this.Cookie_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(25, 316);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 31);
            this.label3.TabIndex = 17;
            this.label3.Text = "Деньги";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(25, 215);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 31);
            this.label4.TabIndex = 18;
            this.label4.Text = "Печенья испечено";
            // 
            // Cookies
            // 
            this.Cookies.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Cookies.Location = new System.Drawing.Point(25, 264);
            this.Cookies.Name = "Cookies";
            this.Cookies.ReadOnly = true;
            this.Cookies.Size = new System.Drawing.Size(210, 38);
            this.Cookies.TabIndex = 19;
            this.Cookies.Text = "0";
            // 
            // Money
            // 
            this.Money.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Money.Location = new System.Drawing.Point(25, 350);
            this.Money.Name = "Money";
            this.Money.ReadOnly = true;
            this.Money.Size = new System.Drawing.Size(210, 38);
            this.Money.TabIndex = 20;
            this.Money.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Money);
            this.Controls.Add(this.Cookies);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Cookie);
            this.Controls.Add(this.UpdateCookie);
            this.Controls.Add(this.UpdateClick);
            this.Controls.Add(this.PlanetCount);
            this.Controls.Add(this.Planet);
            this.Controls.Add(this.CountryCount);
            this.Controls.Add(this.Country);
            this.Controls.Add(this.FactoryCount);
            this.Controls.Add(this.Factory);
            this.Controls.Add(this.BakeryCount);
            this.Controls.Add(this.Bakery);
            this.Controls.Add(this.BakerCount);
            this.Controls.Add(this.Baker);
            this.Controls.Add(this.Income);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label1;
        private Label label2;
        private TextBox Income;
        private Button Baker;
        private TextBox BakerCount;
        private TextBox BakeryCount;
        private Button Bakery;
        private TextBox FactoryCount;
        private Button Factory;
        private TextBox CountryCount;
        private Button Country;
        private TextBox PlanetCount;
        private Button Planet;
        private Button UpdateClick;
        private Button UpdateCookie;
        private Button Cookie;
        private Label label3;
        private Label label4;
        private TextBox Cookies;
        private TextBox Money;
    }
}