
namespace InstrumentaionTools
{
    partial class InstrumentationToolsApplication
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
            this.unitConverter1 = new InstrumentaionTools.UnitConverter.UnitConverter();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.analogScaling1 = new InstrumentaionTools.AnalogScaling.AnalogScaling();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // unitConverter1
            // 
            this.unitConverter1.Location = new System.Drawing.Point(6, 6);
            this.unitConverter1.Name = "unitConverter1";
            this.unitConverter1.Size = new System.Drawing.Size(485, 516);
            this.unitConverter1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(505, 557);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.unitConverter1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(497, 528);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Unit Converter";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.analogScaling1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(497, 528);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Analog Scaling";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // analogScaling1
            // 
            this.analogScaling1.Location = new System.Drawing.Point(4, 6);
            this.analogScaling1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.analogScaling1.Name = "analogScaling1";
            this.analogScaling1.Size = new System.Drawing.Size(486, 519);
            this.analogScaling1.TabIndex = 0;
            // 
            // InstrumentationToolsApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 565);
            this.Controls.Add(this.tabControl1);
            this.Name = "InstrumentationToolsApplication";
            this.Text = "Instrumentation Tools";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UnitConverter.UnitConverter unitConverter1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private AnalogScaling.AnalogScaling analogScaling1;
    }
}

