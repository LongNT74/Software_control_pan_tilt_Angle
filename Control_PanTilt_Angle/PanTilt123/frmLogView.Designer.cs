namespace PanTilt123
{
    partial class frmLogView
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
            this.components = new System.ComponentModel.Container();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvLogView = new System.Windows.Forms.DataGridView();
            this.logEntryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bdLogView = new System.Windows.Forms.BindingSource(this.components);
            this.colDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logEntryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdLogView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(939, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(178, 93);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(939, 111);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(178, 93);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(939, 210);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(178, 93);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvLogView
            // 
            this.dgvLogView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDateTime,
            this.colIndex,
            this.colMessage});
            this.dgvLogView.Location = new System.Drawing.Point(13, 13);
            this.dgvLogView.Name = "dgvLogView";
            this.dgvLogView.RowHeadersWidth = 51;
            this.dgvLogView.RowTemplate.Height = 24;
            this.dgvLogView.Size = new System.Drawing.Size(920, 413);
            this.dgvLogView.TabIndex = 4;
            // 
            // logEntryBindingSource
            // 
            this.logEntryBindingSource.DataSource = typeof(PanTilt123.Class.LogEntry);
            // 
            // bdLogView
            // 
            this.bdLogView.DataSource = typeof(PanTilt123.Class.LogEntry);
            // 
            // colDateTime
            // 
            this.colDateTime.HeaderText = "Date Time";
            this.colDateTime.MinimumWidth = 6;
            this.colDateTime.Name = "colDateTime";
            this.colDateTime.Width = 125;
            // 
            // colIndex
            // 
            this.colIndex.HeaderText = "Index";
            this.colIndex.MinimumWidth = 6;
            this.colIndex.Name = "colIndex";
            this.colIndex.Width = 125;
            // 
            // colMessage
            // 
            this.colMessage.HeaderText = "Message";
            this.colMessage.MinimumWidth = 6;
            this.colMessage.Name = "colMessage";
            this.colMessage.Width = 125;
            // 
            // frmLogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 688);
            this.Controls.Add(this.dgvLogView);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Name = "frmLogView";
            this.Text = "LogView";
            this.Load += new System.EventHandler(this.frmLogView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logEntryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdLogView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.BindingSource bdLogView;
        private System.Windows.Forms.BindingSource logEntryBindingSource;
        private System.Windows.Forms.DataGridView dgvLogView;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessage;
    }
}