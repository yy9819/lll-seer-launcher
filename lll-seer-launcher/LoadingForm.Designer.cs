﻿namespace lll_seer_launcher
{
    partial class LoadingForm
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
            this.loadingWebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // loadingWebBrowser
            // 
            this.loadingWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadingWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.loadingWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.loadingWebBrowser.Name = "loadingWebBrowser";
            this.loadingWebBrowser.ScrollBarsEnabled = false;
            this.loadingWebBrowser.Size = new System.Drawing.Size(469, 303);
            this.loadingWebBrowser.TabIndex = 0;
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 303);
            this.Controls.Add(this.loadingWebBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoadingForm";
            this.Load += new System.EventHandler(this.LoadingForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser loadingWebBrowser;
    }
}