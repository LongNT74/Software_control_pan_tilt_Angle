using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using PanTilt123.Class;

namespace PanTilt123
{
    

    public partial class frmLogView : Form
    {
        
        string filename;
        private bool AutoScroll = true;
        public ObservableCollection<LogEntry> LogEntries { get; set; }
        
        private static frmLogView instance = null;

        public static frmLogView GetInstance()
        {
            if (instance == null)
            {
                instance = new frmLogView();
            }
            return instance;
        }

        public frmLogView()
        {
            InitializeComponent();
            LogEntries = new ObservableCollection<LogEntry>();
            instance = this;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void frmLogView_Load(object sender, EventArgs e)
        {
            foreach (LogEntry entry in LogEntries)
            {
                this.dgvLogView.Rows.Add(entry.DateTime, entry.Index, entry.Message);
            }
        }
    }



    public enum LogType
    {
        Error,
        Info,
        Warning,
        Critical,
        Verbose,
        Default
    }
}
