namespace BestCS.HtmlEditor
{
    partial class EditHtmlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        	this.components = new System.ComponentModel.Container();
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditHtmlForm));
        	this.htmlText = new BestCS.FastColoredTextBox();
        	this.bOK = new System.Windows.Forms.Button();
        	this.bCancel = new System.Windows.Forms.Button();
        	((System.ComponentModel.ISupportInitialize)(this.htmlText)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// htmlText
        	// 
        	this.htmlText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.htmlText.AutoScrollMinSize = new System.Drawing.Size(25, 13);
        	this.htmlText.BackBrush = null;
        	this.htmlText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        	this.htmlText.CharHeight = 13;
        	this.htmlText.CharWidth = 7;
        	this.htmlText.CommentPrefix = null;
        	this.htmlText.Cursor = System.Windows.Forms.Cursors.IBeam;
        	this.htmlText.DelayedEventsInterval = 1000;
        	this.htmlText.DelayedTextChangedInterval = 1000;
        	this.htmlText.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
        	this.htmlText.Font = new System.Drawing.Font("Courier New", 9F);
        	this.htmlText.HighlightingRangeType = BestCS.HighlightingRangeType.VisibleRange;
        	this.htmlText.IsReplaceMode = false;
        	this.htmlText.Language = BestCS.Language.HTML;
        	this.htmlText.LeftBracket = '<';
        	this.htmlText.LeftBracket2 = '(';
        	this.htmlText.Location = new System.Drawing.Point(8, 8);
        	this.htmlText.Name = "htmlText";
        	this.htmlText.Paddings = new System.Windows.Forms.Padding(0);
        	this.htmlText.RightBracket = '>';
        	this.htmlText.RightBracket2 = ')';
        	this.htmlText.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
        	this.htmlText.Size = new System.Drawing.Size(576, 320);
        	this.htmlText.TabIndex = 0;
        	this.htmlText.Zoom = 100;
        	
        	// 
        	// bOK
        	// 
        	this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
        	this.bOK.Location = new System.Drawing.Point(416, 336);
        	this.bOK.Name = "bOK";
        	this.bOK.Size = new System.Drawing.Size(75, 23);
        	this.bOK.TabIndex = 1;
        	this.bOK.Text = "OK";
        	// 
        	// bCancel
        	// 
        	this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        	this.bCancel.Location = new System.Drawing.Point(504, 336);
        	this.bCancel.Name = "bCancel";
        	this.bCancel.Size = new System.Drawing.Size(75, 23);
        	this.bCancel.TabIndex = 2;
        	this.bCancel.Text = "Отмена";
        	// 
        	// EditHtmlForm
        	// 
        	this.AcceptButton = this.bOK;
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.CancelButton = this.bCancel;
        	this.ClientSize = new System.Drawing.Size(592, 366);
        	this.Controls.Add(this.bCancel);
        	this.Controls.Add(this.bOK);
        	this.Controls.Add(this.htmlText);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.MaximizeBox = false;
        	this.MinimizeBox = false;
        	this.Name = "EditHtmlForm";
        	this.ShowInTaskbar = false;
        	this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
        	this.Text = "Html";
        	((System.ComponentModel.ISupportInitialize)(this.htmlText)).EndInit();
        	this.ResumeLayout(false);

        }
        #endregion

        private BestCS.FastColoredTextBox htmlText;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
    }
}

