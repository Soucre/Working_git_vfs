namespace SubmitToApi
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
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.URL = new System.Windows.Forms.TextBox();
            this.DataPOST = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.METHOL = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonChangePrice = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // URL
            // 
            this.URL.Location = new System.Drawing.Point(78, 58);
            this.URL.Name = "URL";
            this.URL.Size = new System.Drawing.Size(838, 20);
            this.URL.TabIndex = 0;
            this.URL.Text = resources.GetString("URL.Text");
            // 
            // DataPOST
            // 
            this.DataPOST.Location = new System.Drawing.Point(78, 107);
            this.DataPOST.Name = "DataPOST";
            this.DataPOST.Size = new System.Drawing.Size(838, 20);
            this.DataPOST.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(231, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // METHOL
            // 
            this.METHOL.Location = new System.Drawing.Point(411, 12);
            this.METHOL.Name = "METHOL";
            this.METHOL.Size = new System.Drawing.Size(100, 20);
            this.METHOL.TabIndex = 3;
            this.METHOL.Text = "GET";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(77, 144);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Login";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(397, 144);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Get All Orders";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonChangePrice
            // 
            this.buttonChangePrice.Location = new System.Drawing.Point(529, 144);
            this.buttonChangePrice.Name = "buttonChangePrice";
            this.buttonChangePrice.Size = new System.Drawing.Size(75, 23);
            this.buttonChangePrice.TabIndex = 7;
            this.buttonChangePrice.Text = "Changeprice";
            this.buttonChangePrice.UseVisualStyleBackColor = true;
            this.buttonChangePrice.Click += new System.EventHandler(this.button4_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(704, 143);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(157, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Mapping to Entity Lazada";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(831, 228);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "TEst Regex";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(831, 257);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 10;
            this.button6.Text = "AutoLogin";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(77, 188);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(748, 360);
            this.webBrowser1.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 639);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.buttonChangePrice);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.METHOL);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DataPOST);
            this.Controls.Add(this.URL);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox URL;
        private System.Windows.Forms.TextBox DataPOST;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox METHOL;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonChangePrice;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

