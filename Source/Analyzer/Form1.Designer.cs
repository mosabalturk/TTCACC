namespace Analyzer {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.funcCompBtn = new System.Windows.Forms.Button();
            this.allFuncsbtn = new System.Windows.Forms.Button();
            this.mainbtn = new System.Windows.Forms.Button();
            this.definesbtn = new System.Windows.Forms.Button();
            this.librariesbtn = new System.Windows.Forms.Button();
            this.structsbtn = new System.Windows.Forms.Button();
            this.classesbtn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.showCodebtn = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 28);
            this.button2.TabIndex = 7;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(96, 39);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "show all tokens";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button6);
            this.splitContainer1.Panel1.Controls.Add(this.showCodebtn);
            this.splitContainer1.Panel1.Controls.Add(this.button5);
            this.splitContainer1.Panel1.Controls.Add(this.button4);
            this.splitContainer1.Panel1.Controls.Add(this.funcCompBtn);
            this.splitContainer1.Panel1.Controls.Add(this.allFuncsbtn);
            this.splitContainer1.Panel1.Controls.Add(this.mainbtn);
            this.splitContainer1.Panel1.Controls.Add(this.definesbtn);
            this.splitContainer1.Panel1.Controls.Add(this.librariesbtn);
            this.splitContainer1.Panel1.Controls.Add(this.structsbtn);
            this.splitContainer1.Panel1.Controls.Add(this.classesbtn);
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1214, 683);
            this.splitContainer1.SplitterDistance = 77;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 9;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(619, 37);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(110, 28);
            this.button5.TabIndex = 17;
            this.button5.Text = "all prototypes";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1010, 40);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(201, 28);
            this.button4.TabIndex = 16;
            this.button4.Text = "prototypes count comparision";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // funcCompBtn
            // 
            this.funcCompBtn.Location = new System.Drawing.Point(1010, 4);
            this.funcCompBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.funcCompBtn.Name = "funcCompBtn";
            this.funcCompBtn.Size = new System.Drawing.Size(201, 28);
            this.funcCompBtn.TabIndex = 15;
            this.funcCompBtn.Text = "function count comparision";
            this.funcCompBtn.UseVisualStyleBackColor = true;
            this.funcCompBtn.Click += new System.EventHandler(this.funcCompBtn_Click);
            // 
            // allFuncsbtn
            // 
            this.allFuncsbtn.Location = new System.Drawing.Point(503, 37);
            this.allFuncsbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.allFuncsbtn.Name = "allFuncsbtn";
            this.allFuncsbtn.Size = new System.Drawing.Size(110, 28);
            this.allFuncsbtn.TabIndex = 14;
            this.allFuncsbtn.Text = "all functions";
            this.allFuncsbtn.UseVisualStyleBackColor = true;
            this.allFuncsbtn.Click += new System.EventHandler(this.allFuncsbtn_Click);
            // 
            // mainbtn
            // 
            this.mainbtn.Location = new System.Drawing.Point(503, 4);
            this.mainbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainbtn.Name = "mainbtn";
            this.mainbtn.Size = new System.Drawing.Size(110, 28);
            this.mainbtn.TabIndex = 13;
            this.mainbtn.Text = "main function";
            this.mainbtn.UseVisualStyleBackColor = true;
            this.mainbtn.Click += new System.EventHandler(this.mainbtn_Click);
            // 
            // definesbtn
            // 
            this.definesbtn.Location = new System.Drawing.Point(401, 37);
            this.definesbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.definesbtn.Name = "definesbtn";
            this.definesbtn.Size = new System.Drawing.Size(96, 28);
            this.definesbtn.TabIndex = 12;
            this.definesbtn.Text = "show defines";
            this.definesbtn.UseVisualStyleBackColor = true;
            this.definesbtn.Click += new System.EventHandler(this.definesbtn_Click);
            // 
            // librariesbtn
            // 
            this.librariesbtn.Location = new System.Drawing.Point(401, 4);
            this.librariesbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.librariesbtn.Name = "librariesbtn";
            this.librariesbtn.Size = new System.Drawing.Size(96, 28);
            this.librariesbtn.TabIndex = 11;
            this.librariesbtn.Text = "show libraries";
            this.librariesbtn.UseVisualStyleBackColor = true;
            this.librariesbtn.Click += new System.EventHandler(this.librariesbtn_Click);
            // 
            // structsbtn
            // 
            this.structsbtn.Location = new System.Drawing.Point(303, 36);
            this.structsbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.structsbtn.Name = "structsbtn";
            this.structsbtn.Size = new System.Drawing.Size(96, 28);
            this.structsbtn.TabIndex = 10;
            this.structsbtn.Text = "show structs";
            this.structsbtn.UseVisualStyleBackColor = true;
            this.structsbtn.Click += new System.EventHandler(this.structsbtn_Click);
            // 
            // classesbtn
            // 
            this.classesbtn.Location = new System.Drawing.Point(303, 3);
            this.classesbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.classesbtn.Name = "classesbtn";
            this.classesbtn.Size = new System.Drawing.Size(96, 28);
            this.classesbtn.TabIndex = 9;
            this.classesbtn.Text = "show classes";
            this.classesbtn.UseVisualStyleBackColor = true;
            this.classesbtn.Click += new System.EventHandler(this.classesbtn_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 40);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 28);
            this.button3.TabIndex = 8;
            this.button3.Text = "input all files";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.richTextBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.richTextBox2);
            this.splitContainer2.Size = new System.Drawing.Size(1214, 601);
            this.splitContainer2.SplitterDistance = 559;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 5;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.ForeColor = System.Drawing.Color.Black;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(559, 601);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.ForeColor = System.Drawing.Color.Black;
            this.richTextBox2.Location = new System.Drawing.Point(0, 0);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox2.Size = new System.Drawing.Size(650, 601);
            this.richTextBox2.TabIndex = 5;
            this.richTextBox2.Text = "";
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // showCodebtn
            // 
            this.showCodebtn.Location = new System.Drawing.Point(216, 3);
            this.showCodebtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.showCodebtn.Name = "showCodebtn";
            this.showCodebtn.Size = new System.Drawing.Size(85, 28);
            this.showCodebtn.TabIndex = 18;
            this.showCodebtn.Text = "show code";
            this.showCodebtn.UseVisualStyleBackColor = true;
            this.showCodebtn.Click += new System.EventHandler(this.showCodebtn_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(96, 3);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(117, 29);
            this.button6.TabIndex = 19;
            this.button6.Text = "show GS tokens";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 683);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Analyzer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button classesbtn;
        private System.Windows.Forms.Button librariesbtn;
        private System.Windows.Forms.Button structsbtn;
        private System.Windows.Forms.Button definesbtn;
        private System.Windows.Forms.Button mainbtn;
        private System.Windows.Forms.Button allFuncsbtn;
        private System.Windows.Forms.Button funcCompBtn;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button showCodebtn;
        private System.Windows.Forms.Button button6;
    }
}

