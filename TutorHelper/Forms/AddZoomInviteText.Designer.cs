namespace TutorHelper.Forms
{
    partial class AddZoomInviteText
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
            this.textBoxInviteMessage = new System.Windows.Forms.TextBox();
            this.buttonAddInviteText = new System.Windows.Forms.Button();
            this.buttonCancelInviteText = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxInviteMessage
            // 
            this.textBoxInviteMessage.AcceptsReturn = true;
            this.textBoxInviteMessage.Location = new System.Drawing.Point(23, 12);
            this.textBoxInviteMessage.Multiline = true;
            this.textBoxInviteMessage.Name = "textBoxInviteMessage";
            this.textBoxInviteMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInviteMessage.Size = new System.Drawing.Size(758, 362);
            this.textBoxInviteMessage.TabIndex = 0;
            this.textBoxInviteMessage.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // buttonAddInviteText
            // 
            this.buttonAddInviteText.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonAddInviteText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddInviteText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAddInviteText.Location = new System.Drawing.Point(341, 392);
            this.buttonAddInviteText.Name = "buttonAddInviteText";
            this.buttonAddInviteText.Size = new System.Drawing.Size(212, 47);
            this.buttonAddInviteText.TabIndex = 1;
            this.buttonAddInviteText.Text = "Add Invite Text";
            this.buttonAddInviteText.UseVisualStyleBackColor = false;
            this.buttonAddInviteText.Click += new System.EventHandler(this.buttonAddInviteText_Click);
            // 
            // buttonCancelInviteText
            // 
            this.buttonCancelInviteText.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonCancelInviteText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancelInviteText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCancelInviteText.Location = new System.Drawing.Point(569, 392);
            this.buttonCancelInviteText.Name = "buttonCancelInviteText";
            this.buttonCancelInviteText.Size = new System.Drawing.Size(212, 46);
            this.buttonCancelInviteText.TabIndex = 2;
            this.buttonCancelInviteText.Text = "Cancel";
            this.buttonCancelInviteText.UseVisualStyleBackColor = false;
            this.buttonCancelInviteText.Click += new System.EventHandler(this.buttonCancelInviteText_Click);
            // 
            // AddZoomInviteText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonCancelInviteText);
            this.Controls.Add(this.buttonAddInviteText);
            this.Controls.Add(this.textBoxInviteMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddZoomInviteText";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Zoom Invite Text";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxInviteMessage;
        private Button buttonAddInviteText;
        private Button buttonCancelInviteText;
    }
}