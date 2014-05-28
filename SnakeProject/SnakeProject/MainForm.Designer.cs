namespace SnakeProject
{
	partial class MainForm
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
			this.butSimple = new System.Windows.Forms.Button();
			this.butHard = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butSimple
			// 
			this.butSimple.Location = new System.Drawing.Point(12, 12);
			this.butSimple.Name = "butSimple";
			this.butSimple.Size = new System.Drawing.Size(100, 100);
			this.butSimple.TabIndex = 0;
			this.butSimple.Text = "Простой";
			this.butSimple.UseVisualStyleBackColor = true;
			this.butSimple.Click += new System.EventHandler(this.butSimple_Click);
			// 
			// butHard
			// 
			this.butHard.Location = new System.Drawing.Point(130, 12);
			this.butHard.Name = "butHard";
			this.butHard.Size = new System.Drawing.Size(100, 100);
			this.butHard.TabIndex = 1;
			this.butHard.Text = "Сложный";
			this.butHard.UseVisualStyleBackColor = true;
			this.butHard.Click += new System.EventHandler(this.butHard_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(244, 126);
			this.Controls.Add(this.butHard);
			this.Controls.Add(this.butSimple);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(260, 165);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(260, 165);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Уроверь сложности";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button butSimple;
		private System.Windows.Forms.Button butHard;
	}
}