namespace FactorioSolver
{
    partial class Ui
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
            this.ButtonCalculate = new System.Windows.Forms.Button();
            this.boxDebug = new System.Windows.Forms.TextBox();
            this.boxIngredient = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.boxTotalPerSecond = new System.Windows.Forms.TextBox();
            this.ButtonOptimizeBeltLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonCalculate
            // 
            this.ButtonCalculate.Location = new System.Drawing.Point(124, 117);
            this.ButtonCalculate.Name = "ButtonCalculate";
            this.ButtonCalculate.Size = new System.Drawing.Size(75, 23);
            this.ButtonCalculate.TabIndex = 1;
            this.ButtonCalculate.Text = "Calculate";
            this.ButtonCalculate.UseVisualStyleBackColor = true;
            this.ButtonCalculate.Click += new System.EventHandler(this.ButtonCalculate_Click);
            // 
            // boxDebug
            // 
            this.boxDebug.Location = new System.Drawing.Point(245, 12);
            this.boxDebug.Multiline = true;
            this.boxDebug.Name = "boxDebug";
            this.boxDebug.Size = new System.Drawing.Size(1493, 1135);
            this.boxDebug.TabIndex = 2;
            // 
            // boxIngredient
            // 
            this.boxIngredient.Location = new System.Drawing.Point(22, 34);
            this.boxIngredient.Name = "boxIngredient";
            this.boxIngredient.Size = new System.Drawing.Size(177, 20);
            this.boxIngredient.TabIndex = 3;
            this.boxIngredient.Text = "High Tech Science Pack";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter the name of a crafting product";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Total per second";
            // 
            // boxTotalPerSecond
            // 
            this.boxTotalPerSecond.Location = new System.Drawing.Point(25, 91);
            this.boxTotalPerSecond.Name = "boxTotalPerSecond";
            this.boxTotalPerSecond.Size = new System.Drawing.Size(174, 20);
            this.boxTotalPerSecond.TabIndex = 6;
            this.boxTotalPerSecond.Text = "1";
            // 
            // ButtonOptimizeBeltLoad
            // 
            this.ButtonOptimizeBeltLoad.Location = new System.Drawing.Point(90, 163);
            this.ButtonOptimizeBeltLoad.Name = "ButtonOptimizeBeltLoad";
            this.ButtonOptimizeBeltLoad.Size = new System.Drawing.Size(109, 23);
            this.ButtonOptimizeBeltLoad.TabIndex = 7;
            this.ButtonOptimizeBeltLoad.Text = "Optimize Belt Load";
            this.ButtonOptimizeBeltLoad.UseVisualStyleBackColor = true;
            this.ButtonOptimizeBeltLoad.Click += new System.EventHandler(this.ButtonOptimizeBeltLoad_Click);
            // 
            // Ui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1776, 1187);
            this.Controls.Add(this.ButtonOptimizeBeltLoad);
            this.Controls.Add(this.boxTotalPerSecond);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.boxIngredient);
            this.Controls.Add(this.boxDebug);
            this.Controls.Add(this.ButtonCalculate);
            this.Name = "Ui";
            this.Text = "Factorio Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ButtonCalculate;
        private System.Windows.Forms.TextBox boxDebug;
        private System.Windows.Forms.TextBox boxIngredient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox boxTotalPerSecond;
        private System.Windows.Forms.Button ButtonOptimizeBeltLoad;
    }
}

