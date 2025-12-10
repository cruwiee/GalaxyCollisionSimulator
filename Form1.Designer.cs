using OpenTK;

namespace CursWork
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            button1 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            bulgeTextBox = new TextBox();
            darkMatterTextBox = new TextBox();
            densityTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button2 = new Button();
            label4 = new Label();
            pictureBox2 = new PictureBox();
            button3 = new Button();
            menuStrip1 = new MenuStrip();
            справкаToolStripMenuItem = new ToolStripMenuItem();
            помощьF1ToolStripMenuItem = new ToolStripMenuItem();
            helpProvider1 = new HelpProvider();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ButtonHighlight;
            pictureBox1.Location = new Point(37, 135);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(334, 11);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaptionText;
            button1.Font = new Font("Ronduit Capitals Light", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(67, 721);
            button1.Name = "button1";
            button1.RightToLeft = RightToLeft.No;
            button1.Size = new Size(259, 57);
            button1.TabIndex = 1;
            button1.Text = "Запустить";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.BackColor = Color.Black;
            radioButton1.Font = new Font("Ronduit Capitals Light", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radioButton1.ForeColor = SystemColors.ButtonHighlight;
            radioButton1.Location = new Point(70, 241);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(141, 21);
            radioButton1.TabIndex = 2;
            radioButton1.Text = "2 спирали";
            radioButton1.UseVisualStyleBackColor = false;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            radioButton1.Click += radioButton1_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.BackColor = SystemColors.ActiveCaptionText;
            radioButton2.Checked = true;
            radioButton2.Font = new Font("Ronduit Capitals Light", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 204);
            radioButton2.ForeColor = SystemColors.ButtonHighlight;
            radioButton2.Location = new Point(67, 268);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(143, 21);
            radioButton2.TabIndex = 3;
            radioButton2.TabStop = true;
            radioButton2.Text = "4 спирали";
            radioButton2.UseVisualStyleBackColor = false;
            // 
            // bulgeTextBox
            // 
            bulgeTextBox.Location = new Point(69, 365);
            bulgeTextBox.Name = "bulgeTextBox";
            bulgeTextBox.Size = new Size(231, 27);
            bulgeTextBox.TabIndex = 4;
            bulgeTextBox.Text = "1,223235E+41";
            // 
            // darkMatterTextBox
            // 
            darkMatterTextBox.Location = new Point(69, 557);
            darkMatterTextBox.Name = "darkMatterTextBox";
            darkMatterTextBox.Size = new Size(230, 27);
            darkMatterTextBox.TabIndex = 5;
            darkMatterTextBox.Text = "4,9E-16";
            // 
            // densityTextBox
            // 
            densityTextBox.Location = new Point(67, 472);
            densityTextBox.Name = "densityTextBox";
            densityTextBox.Size = new Size(233, 27);
            densityTextBox.TabIndex = 6;
            densityTextBox.Text = "4,62858225218484E+19";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ControlText;
            label1.Font = new Font("Ronduit Capitals Light", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(69, 343);
            label1.Name = "label1";
            label1.Size = new Size(178, 17);
            label1.TabIndex = 7;
            label1.Text = "Масса балджа:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Black;
            label2.Font = new Font("Ronduit Capitals Light", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(67, 428);
            label2.Name = "label2";
            label2.Size = new Size(232, 34);
            label2.TabIndex = 8;
            label2.Text = "Плотность звезд и \r\nразмер галактики:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ControlText;
            label3.Font = new Font("Ronduit Capitals Light", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(67, 529);
            label3.Name = "label3";
            label3.Size = new Size(192, 17);
            label3.TabIndex = 9;
            label3.Text = "k: коэффициент:";
            // 
            // button2
            // 
            button2.Font = new Font("Ronduit Capitals Light", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button2.Location = new Point(67, 652);
            button2.Name = "button2";
            button2.Size = new Size(112, 45);
            button2.TabIndex = 10;
            button2.Text = "Отчёт";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ActiveCaptionText;
            label4.Font = new Font("Ronduit Capitals Light", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(69, 211);
            label4.Name = "label4";
            label4.Size = new Size(257, 17);
            label4.TabIndex = 11;
            label4.Text = "Количество спиралей:";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = SystemColors.ControlText;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.ImageLocation = "";
            pictureBox2.Location = new Point(33, 169);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(337, 637);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Ronduit Capitals Light", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button3.Location = new Point(197, 652);
            button3.Name = "button3";
            button3.Size = new Size(129, 45);
            button3.TabIndex = 14;
            button3.Text = "Сбросить";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { справкаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(6, 3, 0, 3);
            menuStrip1.Size = new Size(410, 30);
            menuStrip1.TabIndex = 15;
            menuStrip1.Text = "menuStrip1";
            // 
            // справкаToolStripMenuItem
            // 
            справкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { помощьF1ToolStripMenuItem });
            справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            справкаToolStripMenuItem.Size = new Size(81, 24);
            справкаToolStripMenuItem.Text = "Справка";
            // 
            // помощьF1ToolStripMenuItem
            // 
            помощьF1ToolStripMenuItem.Name = "помощьF1ToolStripMenuItem";
            помощьF1ToolStripMenuItem.Size = new Size(171, 26);
            помощьF1ToolStripMenuItem.Text = "Помощь F1";
            помощьF1ToolStripMenuItem.Click += помощьF1ToolStripMenuItem_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Ronduit Capitals Light", 10.1999989F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.ControlLightLight;
            label5.Location = new Point(24, 68);
            label5.Name = "label5";
            label5.Size = new Size(361, 40);
            label5.TabIndex = 16;
            label5.Text = "G a l a x y  C o l l i s i o n  \r\nS i m u l a t o r";
            label5.TextAlign = ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(410, 855);
            Controls.Add(label5);
            Controls.Add(button3);
            Controls.Add(label4);
            Controls.Add(button2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(densityTextBox);
            Controls.Add(darkMatterTextBox);
            Controls.Add(bulgeTextBox);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button button1;
        private System.Windows.Forms.Timer timer1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private TextBox bulgeTextBox;
        private TextBox darkMatterTextBox;
        private TextBox densityTextBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button2;
        private Label label4;
        private PictureBox pictureBox2;
        private Button button3;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem справкаToolStripMenuItem;
        private ToolStripMenuItem помощьF1ToolStripMenuItem;
        private HelpProvider helpProvider1;
        private Label label5;
    }
}
