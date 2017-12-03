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
            this.clearbtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.pointersCntrbtn = new System.Windows.Forms.Button();
            this.getArraysCntrbtn = new System.Windows.Forms.Button();
            this.varCounterbtn = new System.Windows.Forms.Button();
            this.valuesCntrbtn = new System.Windows.Forms.Button();
            this.kewWordsCntrbtn = new System.Windows.Forms.Button();
            this.datatypeCntrbtn = new System.Windows.Forms.Button();
            this.opCntrbtn = new System.Windows.Forms.Button();
            this.shoPointersbtn = new System.Windows.Forms.Button();
            this.showArraysbtn = new System.Windows.Forms.Button();
            this.structObjectsGS = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.allFuncsbtn = new System.Windows.Forms.Button();
            this.mainbtn = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.definesbtn = new System.Windows.Forms.Button();
            this.librariesbtn = new System.Windows.Forms.Button();
            this.structsbtn = new System.Windows.Forms.Button();
            this.classesbtn = new System.Windows.Forms.Button();
            this.filesInputbtn = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // clearbtn
            // 
            this.clearbtn.Location = new System.Drawing.Point(3, 4);
            this.clearbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clearbtn.Name = "clearbtn";
            this.clearbtn.Size = new System.Drawing.Size(87, 28);
            this.clearbtn.TabIndex = 7;
            this.clearbtn.Text = "Clear";
            this.clearbtn.UseVisualStyleBackColor = true;
            this.clearbtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(96, 35);
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
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.button6);
            this.splitContainer1.Panel1.Controls.Add(this.definesbtn);
            this.splitContainer1.Panel1.Controls.Add(this.librariesbtn);
            this.splitContainer1.Panel1.Controls.Add(this.structsbtn);
            this.splitContainer1.Panel1.Controls.Add(this.classesbtn);
            this.splitContainer1.Panel1.Controls.Add(this.filesInputbtn);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.clearbtn);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1477, 683);
            this.splitContainer1.SplitterDistance = 77;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.pointersCntrbtn);
            this.groupBox1.Controls.Add(this.getArraysCntrbtn);
            this.groupBox1.Controls.Add(this.varCounterbtn);
            this.groupBox1.Controls.Add(this.valuesCntrbtn);
            this.groupBox1.Controls.Add(this.kewWordsCntrbtn);
            this.groupBox1.Controls.Add(this.datatypeCntrbtn);
            this.groupBox1.Controls.Add(this.opCntrbtn);
            this.groupBox1.Controls.Add(this.shoPointersbtn);
            this.groupBox1.Controls.Add(this.showArraysbtn);
            this.groupBox1.Controls.Add(this.structObjectsGS);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.allFuncsbtn);
            this.groupBox1.Controls.Add(this.mainbtn);
            this.groupBox1.Location = new System.Drawing.Point(416, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(906, 73);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "global scobe";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(784, 12);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 28);
            this.button2.TabIndex = 44;
            this.button2.Text = "all counters gs";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // pointersCntrbtn
            // 
            this.pointersCntrbtn.Location = new System.Drawing.Point(784, 43);
            this.pointersCntrbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pointersCntrbtn.Name = "pointersCntrbtn";
            this.pointersCntrbtn.Size = new System.Drawing.Size(120, 28);
            this.pointersCntrbtn.TabIndex = 43;
            this.pointersCntrbtn.Text = "pointers counter";
            this.pointersCntrbtn.UseVisualStyleBackColor = true;
            this.pointersCntrbtn.Click += new System.EventHandler(this.pointersCntrbtn_Click);
            // 
            // getArraysCntrbtn
            // 
            this.getArraysCntrbtn.Location = new System.Drawing.Point(658, 44);
            this.getArraysCntrbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.getArraysCntrbtn.Name = "getArraysCntrbtn";
            this.getArraysCntrbtn.Size = new System.Drawing.Size(120, 28);
            this.getArraysCntrbtn.TabIndex = 42;
            this.getArraysCntrbtn.Text = "arrays counter";
            this.getArraysCntrbtn.UseVisualStyleBackColor = true;
            this.getArraysCntrbtn.Click += new System.EventHandler(this.getArraysCntrbtn_Click);
            // 
            // varCounterbtn
            // 
            this.varCounterbtn.Location = new System.Drawing.Point(658, 11);
            this.varCounterbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.varCounterbtn.Name = "varCounterbtn";
            this.varCounterbtn.Size = new System.Drawing.Size(120, 28);
            this.varCounterbtn.TabIndex = 41;
            this.varCounterbtn.Text = "variables counter";
            this.varCounterbtn.UseVisualStyleBackColor = true;
            this.varCounterbtn.Click += new System.EventHandler(this.varCounterbtn_Click);
            // 
            // valuesCntrbtn
            // 
            this.valuesCntrbtn.Location = new System.Drawing.Point(516, 43);
            this.valuesCntrbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.valuesCntrbtn.Name = "valuesCntrbtn";
            this.valuesCntrbtn.Size = new System.Drawing.Size(136, 28);
            this.valuesCntrbtn.TabIndex = 40;
            this.valuesCntrbtn.Text = "values counter";
            this.valuesCntrbtn.UseVisualStyleBackColor = true;
            this.valuesCntrbtn.Click += new System.EventHandler(this.valuesCntrbtn_Click);
            // 
            // kewWordsCntrbtn
            // 
            this.kewWordsCntrbtn.Location = new System.Drawing.Point(516, 13);
            this.kewWordsCntrbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.kewWordsCntrbtn.Name = "kewWordsCntrbtn";
            this.kewWordsCntrbtn.Size = new System.Drawing.Size(136, 28);
            this.kewWordsCntrbtn.TabIndex = 39;
            this.kewWordsCntrbtn.Text = "key words counter";
            this.kewWordsCntrbtn.UseVisualStyleBackColor = true;
            this.kewWordsCntrbtn.Click += new System.EventHandler(this.kewWordsCntrbtn_Click);
            // 
            // datatypeCntrbtn
            // 
            this.datatypeCntrbtn.Location = new System.Drawing.Point(374, 43);
            this.datatypeCntrbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.datatypeCntrbtn.Name = "datatypeCntrbtn";
            this.datatypeCntrbtn.Size = new System.Drawing.Size(136, 28);
            this.datatypeCntrbtn.TabIndex = 38;
            this.datatypeCntrbtn.Text = "datatype counter";
            this.datatypeCntrbtn.UseVisualStyleBackColor = true;
            this.datatypeCntrbtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // opCntrbtn
            // 
            this.opCntrbtn.Location = new System.Drawing.Point(374, 14);
            this.opCntrbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.opCntrbtn.Name = "opCntrbtn";
            this.opCntrbtn.Size = new System.Drawing.Size(136, 28);
            this.opCntrbtn.TabIndex = 37;
            this.opCntrbtn.Text = "operations counter";
            this.opCntrbtn.UseVisualStyleBackColor = true;
            this.opCntrbtn.Click += new System.EventHandler(this.opCntrbtn_Click);
            // 
            // shoPointersbtn
            // 
            this.shoPointersbtn.Location = new System.Drawing.Point(264, 43);
            this.shoPointersbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.shoPointersbtn.Name = "shoPointersbtn";
            this.shoPointersbtn.Size = new System.Drawing.Size(104, 28);
            this.shoPointersbtn.TabIndex = 36;
            this.shoPointersbtn.Text = "show pointers";
            this.shoPointersbtn.UseVisualStyleBackColor = true;
            // 
            // showArraysbtn
            // 
            this.showArraysbtn.Location = new System.Drawing.Point(264, 14);
            this.showArraysbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.showArraysbtn.Name = "showArraysbtn";
            this.showArraysbtn.Size = new System.Drawing.Size(104, 28);
            this.showArraysbtn.TabIndex = 35;
            this.showArraysbtn.Text = "show arrays";
            this.showArraysbtn.UseVisualStyleBackColor = true;
            // 
            // structObjectsGS
            // 
            this.structObjectsGS.Location = new System.Drawing.Point(132, 14);
            this.structObjectsGS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.structObjectsGS.Name = "structObjectsGS";
            this.structObjectsGS.Size = new System.Drawing.Size(126, 28);
            this.structObjectsGS.TabIndex = 34;
            this.structObjectsGS.Text = "struct objects GS";
            this.structObjectsGS.UseVisualStyleBackColor = true;
            this.structObjectsGS.Click += new System.EventHandler(this.structObjects_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(132, 45);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(126, 28);
            this.button5.TabIndex = 33;
            this.button5.Text = "all prototypes";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // allFuncsbtn
            // 
            this.allFuncsbtn.Location = new System.Drawing.Point(16, 45);
            this.allFuncsbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.allFuncsbtn.Name = "allFuncsbtn";
            this.allFuncsbtn.Size = new System.Drawing.Size(110, 28);
            this.allFuncsbtn.TabIndex = 32;
            this.allFuncsbtn.Text = "all functions";
            this.allFuncsbtn.UseVisualStyleBackColor = true;
            this.allFuncsbtn.Click += new System.EventHandler(this.allFuncsbtn_Click);
            // 
            // mainbtn
            // 
            this.mainbtn.Location = new System.Drawing.Point(16, 14);
            this.mainbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainbtn.Name = "mainbtn";
            this.mainbtn.Size = new System.Drawing.Size(110, 28);
            this.mainbtn.TabIndex = 31;
            this.mainbtn.Text = "main function";
            this.mainbtn.UseVisualStyleBackColor = true;
            this.mainbtn.Click += new System.EventHandler(this.mainbtn_Click);
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
            // definesbtn
            // 
            this.definesbtn.Location = new System.Drawing.Point(314, 37);
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
            this.librariesbtn.Location = new System.Drawing.Point(314, 4);
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
            this.structsbtn.Location = new System.Drawing.Point(216, 36);
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
            this.classesbtn.Location = new System.Drawing.Point(216, 4);
            this.classesbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.classesbtn.Name = "classesbtn";
            this.classesbtn.Size = new System.Drawing.Size(96, 28);
            this.classesbtn.TabIndex = 9;
            this.classesbtn.Text = "show classes";
            this.classesbtn.UseVisualStyleBackColor = true;
            this.classesbtn.Click += new System.EventHandler(this.classesbtn_Click);
            // 
            // filesInputbtn
            // 
            this.filesInputbtn.Location = new System.Drawing.Point(3, 36);
            this.filesInputbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.filesInputbtn.Name = "filesInputbtn";
            this.filesInputbtn.Size = new System.Drawing.Size(87, 28);
            this.filesInputbtn.TabIndex = 8;
            this.filesInputbtn.Text = "input all files";
            this.filesInputbtn.UseVisualStyleBackColor = true;
            this.filesInputbtn.Click += new System.EventHandler(this.filesInputbtn_Click);
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
            this.splitContainer2.Panel1.Controls.Add(this.webBrowser1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.richTextBox2);
            this.splitContainer2.Size = new System.Drawing.Size(1477, 601);
            this.splitContainer2.SplitterDistance = 679;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 5;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(679, 601);
            this.webBrowser1.TabIndex = 0;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.ForeColor = System.Drawing.Color.Black;
            this.richTextBox2.Location = new System.Drawing.Point(0, 0);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox2.Size = new System.Drawing.Size(793, 601);
            this.richTextBox2.TabIndex = 5;
            this.richTextBox2.Text = "";
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1328, 44);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 28);
            this.button3.TabIndex = 45;
            this.button3.Text = "spit";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1477, 683);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Analyzer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button clearbtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button filesInputbtn;
        private System.Windows.Forms.Button classesbtn;
        private System.Windows.Forms.Button librariesbtn;
        private System.Windows.Forms.Button structsbtn;
        private System.Windows.Forms.Button definesbtn;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button pointersCntrbtn;
        private System.Windows.Forms.Button getArraysCntrbtn;
        private System.Windows.Forms.Button varCounterbtn;
        private System.Windows.Forms.Button valuesCntrbtn;
        private System.Windows.Forms.Button kewWordsCntrbtn;
        private System.Windows.Forms.Button datatypeCntrbtn;
        private System.Windows.Forms.Button opCntrbtn;
        private System.Windows.Forms.Button shoPointersbtn;
        private System.Windows.Forms.Button showArraysbtn;
        private System.Windows.Forms.Button structObjectsGS;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button allFuncsbtn;
        private System.Windows.Forms.Button mainbtn;
        private System.Windows.Forms.Button button3;
    }
}

