﻿namespace Configurator
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
			this.Panel = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// Panel
			// 
			this.Panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel.Location = new System.Drawing.Point(0, 0);
			this.Panel.MinimumSize = new System.Drawing.Size(1024, 600);
			this.Panel.Name = "Panel";
			this.Panel.Size = new System.Drawing.Size(1024, 600);
			this.Panel.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1024, 600);
			this.Controls.Add(this.Panel);
			this.MinimumSize = new System.Drawing.Size(1024, 600);
			this.Name = "Form1";
			this.Text = "Конфигуратор iec61850 - iec60870";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel Panel;
	}
}

