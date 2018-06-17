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
            this.drawingMainTopLeft = new System.Windows.Forms.TextBox();
            this.textSizeCheck = new System.Windows.Forms.Label();
            this.drawingRefineryTopLeft = new System.Windows.Forms.TextBox();
            this.drawingMiningNeeds = new System.Windows.Forms.TextBox();
            this.boxMiningProductivity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxBeltSplit = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ButtonCalculate
            // 
            this.ButtonCalculate.Location = new System.Drawing.Point(118, 184);
            this.ButtonCalculate.Name = "ButtonCalculate";
            this.ButtonCalculate.Size = new System.Drawing.Size(75, 23);
            this.ButtonCalculate.TabIndex = 1;
            this.ButtonCalculate.Text = "Calculate";
            this.ButtonCalculate.UseVisualStyleBackColor = true;
            this.ButtonCalculate.Click += new System.EventHandler(this.ButtonCalculate_Click);
            // 
            // boxDebug
            // 
            this.boxDebug.Location = new System.Drawing.Point(12, 1774);
            this.boxDebug.Multiline = true;
            this.boxDebug.Name = "boxDebug";
            this.boxDebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.boxDebug.Size = new System.Drawing.Size(1685, 348);
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
            this.label2.Location = new System.Drawing.Point(19, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Total per second";
            // 
            // boxTotalPerSecond
            // 
            this.boxTotalPerSecond.Location = new System.Drawing.Point(22, 85);
            this.boxTotalPerSecond.Name = "boxTotalPerSecond";
            this.boxTotalPerSecond.Size = new System.Drawing.Size(174, 20);
            this.boxTotalPerSecond.TabIndex = 6;
            this.boxTotalPerSecond.Text = "1";
            // 
            // ButtonOptimizeBeltLoad
            // 
            this.ButtonOptimizeBeltLoad.Location = new System.Drawing.Point(3, 184);
            this.ButtonOptimizeBeltLoad.Name = "ButtonOptimizeBeltLoad";
            this.ButtonOptimizeBeltLoad.Size = new System.Drawing.Size(109, 23);
            this.ButtonOptimizeBeltLoad.TabIndex = 7;
            this.ButtonOptimizeBeltLoad.Text = "Optimize Belt Load";
            this.ButtonOptimizeBeltLoad.UseVisualStyleBackColor = true;
            this.ButtonOptimizeBeltLoad.Click += new System.EventHandler(this.ButtonOptimizeBeltLoad_Click);
            // 
            // drawingMainTopLeft
            // 
            this.drawingMainTopLeft.Location = new System.Drawing.Point(222, 15);
            this.drawingMainTopLeft.Name = "drawingMainTopLeft";
            this.drawingMainTopLeft.Size = new System.Drawing.Size(354, 20);
            this.drawingMainTopLeft.TabIndex = 8;
            this.drawingMainTopLeft.Text = "For positioning the main drawing area";
            this.drawingMainTopLeft.Visible = false;
            // 
            // textSizeCheck
            // 
            this.textSizeCheck.AutoSize = true;
            this.textSizeCheck.Location = new System.Drawing.Point(1530, 18);
            this.textSizeCheck.Name = "textSizeCheck";
            this.textSizeCheck.Size = new System.Drawing.Size(167, 13);
            this.textSizeCheck.TabIndex = 9;
            this.textSizeCheck.Text = "for getting the dimension of strings";
            this.textSizeCheck.Visible = false;
            // 
            // drawingRefineryTopLeft
            // 
            this.drawingRefineryTopLeft.Location = new System.Drawing.Point(12, 213);
            this.drawingRefineryTopLeft.Name = "drawingRefineryTopLeft";
            this.drawingRefineryTopLeft.Size = new System.Drawing.Size(195, 20);
            this.drawingRefineryTopLeft.TabIndex = 10;
            this.drawingRefineryTopLeft.Text = "For positioning the refinery drawing area";
            this.drawingRefineryTopLeft.Visible = false;
            // 
            // drawingMiningNeeds
            // 
            this.drawingMiningNeeds.Location = new System.Drawing.Point(1732, 1794);
            this.drawingMiningNeeds.Name = "drawingMiningNeeds";
            this.drawingMiningNeeds.Size = new System.Drawing.Size(266, 20);
            this.drawingMiningNeeds.TabIndex = 11;
            this.drawingMiningNeeds.Text = "For positioning the mining needs(not being used)";
            this.drawingMiningNeeds.Visible = false;
            // 
            // boxMiningProductivity
            // 
            this.boxMiningProductivity.Location = new System.Drawing.Point(19, 135);
            this.boxMiningProductivity.Name = "boxMiningProductivity";
            this.boxMiningProductivity.Size = new System.Drawing.Size(174, 20);
            this.boxMiningProductivity.TabIndex = 13;
            this.boxMiningProductivity.Text = "39";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Mining Productity";
            // 
            // checkBoxBeltSplit
            // 
            this.checkBoxBeltSplit.AutoSize = true;
            this.checkBoxBeltSplit.Checked = true;
            this.checkBoxBeltSplit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBeltSplit.Location = new System.Drawing.Point(19, 161);
            this.checkBoxBeltSplit.Name = "checkBoxBeltSplit";
            this.checkBoxBeltSplit.Size = new System.Drawing.Size(125, 17);
            this.checkBoxBeltSplit.TabIndex = 14;
            this.checkBoxBeltSplit.Text = "Split for express belts";
            this.checkBoxBeltSplit.UseVisualStyleBackColor = true;
            // 
            // Ui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(3036, 2134);
            this.Controls.Add(this.checkBoxBeltSplit);
            this.Controls.Add(this.boxMiningProductivity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.drawingMiningNeeds);
            this.Controls.Add(this.drawingRefineryTopLeft);
            this.Controls.Add(this.textSizeCheck);
            this.Controls.Add(this.drawingMainTopLeft);
            this.Controls.Add(this.ButtonOptimizeBeltLoad);
            this.Controls.Add(this.boxTotalPerSecond);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.boxIngredient);
            this.Controls.Add(this.boxDebug);
            this.Controls.Add(this.ButtonCalculate);
            this.Name = "Ui";
            this.Text = "Factorio Calculator";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ui_KeyPress);
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
        private System.Windows.Forms.TextBox drawingMainTopLeft;
        private System.Windows.Forms.Label textSizeCheck;
        private System.Windows.Forms.TextBox drawingRefineryTopLeft;
        private System.Windows.Forms.TextBox drawingMiningNeeds;
        private System.Windows.Forms.TextBox boxMiningProductivity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxBeltSplit;
    }
}

