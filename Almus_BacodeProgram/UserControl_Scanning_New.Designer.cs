﻿namespace Almus_BacodeProgram
{
    partial class UserControl_Scanning_New
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSerialScanData = new System.Windows.Forms.TextBox();
            this.txtUnitBox = new System.Windows.Forms.TextBox();
            this.txtSkuScanData = new System.Windows.Forms.TextBox();
            this.cbbCarton = new System.Windows.Forms.ComboBox();
            this.cbbMiddle = new System.Windows.Forms.ComboBox();
            this.cbbMaster = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtMiddlePerCarton = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtUnitPerMiddle = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtUnitPerCarton = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnPassFail = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblTestCount = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblMiddleBoxNumber = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.txtSerialScanData);
            this.groupBox1.Controls.Add(this.txtUnitBox);
            this.groupBox1.Controls.Add(this.txtSkuScanData);
            this.groupBox1.Controls.Add(this.cbbCarton);
            this.groupBox1.Controls.Add(this.cbbMiddle);
            this.groupBox1.Controls.Add(this.cbbMaster);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Corbel", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1023, 355);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SCAN INFOMATION";
            // 
            // txtSerialScanData
            // 
            this.txtSerialScanData.BackColor = System.Drawing.Color.Gainsboro;
            this.txtSerialScanData.Location = new System.Drawing.Point(318, 289);
            this.txtSerialScanData.Name = "txtSerialScanData";
            this.txtSerialScanData.Size = new System.Drawing.Size(620, 40);
            this.txtSerialScanData.TabIndex = 3;
            this.txtSerialScanData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSerialScanData_KeyUp);
            // 
            // txtUnitBox
            // 
            this.txtUnitBox.BackColor = System.Drawing.Color.MediumAquamarine;
            this.txtUnitBox.Location = new System.Drawing.Point(267, 84);
            this.txtUnitBox.Name = "txtUnitBox";
            this.txtUnitBox.Size = new System.Drawing.Size(671, 40);
            this.txtUnitBox.TabIndex = 3;
            // 
            // txtSkuScanData
            // 
            this.txtSkuScanData.BackColor = System.Drawing.Color.White;
            this.txtSkuScanData.Location = new System.Drawing.Point(318, 240);
            this.txtSkuScanData.Name = "txtSkuScanData";
            this.txtSkuScanData.Size = new System.Drawing.Size(620, 40);
            this.txtSkuScanData.TabIndex = 3;
            this.txtSkuScanData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSkuScanData_KeyUp);
            // 
            // cbbCarton
            // 
            this.cbbCarton.BackColor = System.Drawing.Color.Lavender;
            this.cbbCarton.Font = new System.Drawing.Font("Corbel", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbCarton.FormattingEnabled = true;
            this.cbbCarton.Location = new System.Drawing.Point(267, 182);
            this.cbbCarton.Name = "cbbCarton";
            this.cbbCarton.Size = new System.Drawing.Size(671, 41);
            this.cbbCarton.TabIndex = 2;
            // 
            // cbbMiddle
            // 
            this.cbbMiddle.BackColor = System.Drawing.Color.MediumTurquoise;
            this.cbbMiddle.Font = new System.Drawing.Font("Corbel", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbMiddle.FormattingEnabled = true;
            this.cbbMiddle.Location = new System.Drawing.Point(267, 135);
            this.cbbMiddle.Name = "cbbMiddle";
            this.cbbMiddle.Size = new System.Drawing.Size(671, 41);
            this.cbbMiddle.TabIndex = 2;
            // 
            // cbbMaster
            // 
            this.cbbMaster.BackColor = System.Drawing.Color.Tomato;
            this.cbbMaster.Font = new System.Drawing.Font("Corbel", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbMaster.FormattingEnabled = true;
            this.cbbMaster.Location = new System.Drawing.Point(267, 37);
            this.cbbMaster.Name = "cbbMaster";
            this.cbbMaster.Size = new System.Drawing.Size(671, 41);
            this.cbbMaster.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Corbel", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(74, 300);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(179, 23);
            this.label9.TabIndex = 0;
            this.label9.Text = "SERIAL SCAN DATA:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Corbel", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(74, 251);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(154, 23);
            this.label7.TabIndex = 0;
            this.label7.Text = "SKU SCAN DATA:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Corbel", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(74, 202);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "CARTON BOX:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Corbel", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(74, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "MIDDLE BOX:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Corbel", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(74, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "UNIT BOX:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Corbel", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(74, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "MASTER:";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Controls.Add(this.txtMiddlePerCarton);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtUnitPerMiddle);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtUnitPerCarton);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Font = new System.Drawing.Font("Corbel", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 375);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1023, 102);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "INSPECTION COUNT SETTING";
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.LimeGreen;
            this.btnStart.Font = new System.Drawing.Font("Corbel", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(837, 16);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(180, 80);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtMiddlePerCarton
            // 
            this.txtMiddlePerCarton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMiddlePerCarton.Location = new System.Drawing.Point(697, 44);
            this.txtMiddlePerCarton.Name = "txtMiddlePerCarton";
            this.txtMiddlePerCarton.Size = new System.Drawing.Size(114, 26);
            this.txtMiddlePerCarton.TabIndex = 1;
            this.txtMiddlePerCarton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(552, 47);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(139, 19);
            this.label15.TabIndex = 0;
            this.label15.Text = "MIDDLE/CARTON:";
            // 
            // txtUnitPerMiddle
            // 
            this.txtUnitPerMiddle.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnitPerMiddle.Location = new System.Drawing.Point(412, 44);
            this.txtUnitPerMiddle.Name = "txtUnitPerMiddle";
            this.txtUnitPerMiddle.Size = new System.Drawing.Size(117, 26);
            this.txtUnitPerMiddle.TabIndex = 1;
            this.txtUnitPerMiddle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(290, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(112, 19);
            this.label14.TabIndex = 0;
            this.label14.Text = "UNIT/MIDDLE:";
            // 
            // txtUnitPerCarton
            // 
            this.txtUnitPerCarton.BackColor = System.Drawing.Color.DarkGray;
            this.txtUnitPerCarton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnitPerCarton.Location = new System.Drawing.Point(148, 44);
            this.txtUnitPerCarton.Name = "txtUnitPerCarton";
            this.txtUnitPerCarton.ReadOnly = true;
            this.txtUnitPerCarton.Size = new System.Drawing.Size(118, 26);
            this.txtUnitPerCarton.TabIndex = 1;
            this.txtUnitPerCarton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(24, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(118, 19);
            this.label13.TabIndex = 0;
            this.label13.Text = "UNIT/CARTON:";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox3.Controls.Add(this.btnPassFail);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Font = new System.Drawing.Font("Corbel", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(13, 483);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1023, 252);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "COUNT INFOMATION";
            // 
            // btnPassFail
            // 
            this.btnPassFail.BackColor = System.Drawing.Color.LavenderBlush;
            this.btnPassFail.Font = new System.Drawing.Font("Corbel", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPassFail.Location = new System.Drawing.Point(733, 20);
            this.btnPassFail.Name = "btnPassFail";
            this.btnPassFail.Size = new System.Drawing.Size(284, 226);
            this.btnPassFail.TabIndex = 1;
            this.btnPassFail.Text = "PASS(0) / FAIL(0)";
            this.btnPassFail.UseVisualStyleBackColor = false;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.lblTestCount);
            this.groupBox5.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(376, 41);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(257, 205);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "TEST COUNT";
            // 
            // lblTestCount
            // 
            this.lblTestCount.Font = new System.Drawing.Font("Corbel", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestCount.Location = new System.Drawing.Point(6, 23);
            this.lblTestCount.Name = "lblTestCount";
            this.lblTestCount.Size = new System.Drawing.Size(245, 167);
            this.lblTestCount.TabIndex = 0;
            this.lblTestCount.Text = "0";
            this.lblTestCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.lblMiddleBoxNumber);
            this.groupBox4.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(23, 41);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(257, 205);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "MIDDLE BOX NUMBER";
            // 
            // lblMiddleBoxNumber
            // 
            this.lblMiddleBoxNumber.Font = new System.Drawing.Font("Corbel", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMiddleBoxNumber.Location = new System.Drawing.Point(6, 23);
            this.lblMiddleBoxNumber.Name = "lblMiddleBoxNumber";
            this.lblMiddleBoxNumber.Size = new System.Drawing.Size(245, 167);
            this.lblMiddleBoxNumber.TabIndex = 0;
            this.lblMiddleBoxNumber.Text = "0";
            this.lblMiddleBoxNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserControl_Scanning_New
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserControl_Scanning_New";
            this.Size = new System.Drawing.Size(1050, 738);
            this.Load += new System.EventHandler(this.UserControl_Scanning_New_Load);
            this.VisibleChanged += new System.EventHandler(this.UserControl_Scanning_New_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMiddlePerCarton;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtUnitPerMiddle;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtUnitPerCarton;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblMiddleBoxNumber;
        private System.Windows.Forms.ComboBox cbbCarton;
        private System.Windows.Forms.ComboBox cbbMiddle;
        private System.Windows.Forms.ComboBox cbbMaster;
        private System.Windows.Forms.Label lblTestCount;
        private System.Windows.Forms.TextBox txtSerialScanData;
        private System.Windows.Forms.TextBox txtSkuScanData;
        private System.Windows.Forms.TextBox txtUnitBox;
        private System.Windows.Forms.Button btnPassFail;
    }
}
