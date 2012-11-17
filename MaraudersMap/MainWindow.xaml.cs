using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;



using Controls = Elysium.Theme.Controls;
using System.Windows;


namespace MaraudersMap
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Controls.Window
	{
        private System.Windows.Forms.NotifyIcon notifier;
        private System.Windows.Forms.ContextMenuStrip contextMenu1;
        private System.Windows.Forms.ToolStripMenuItem refreshItem, correctItem, currentPositionItem, exitItem;
        private Timer refreshTimer;
        private int currentPosition = 0;

		public MainWindow()
		{
            InitializeComponent();
			// Insert code required on object creation below this point.
            notifier = new System.Windows.Forms.NotifyIcon();
            this.contextMenu1 = new System.Windows.Forms.ContextMenuStrip();
            refreshItem = new System.Windows.Forms.ToolStripMenuItem("Refresh");
            correctItem = new System.Windows.Forms.ToolStripMenuItem("Correct Position");
            currentPositionItem = new System.Windows.Forms.ToolStripMenuItem("Current Position");
            exitItem = new System.Windows.Forms.ToolStripMenuItem("Exit");
            this.contextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] { refreshItem, correctItem, currentPositionItem, exitItem });

            refreshItem.Click += new System.EventHandler(this.refreshItem_Click);
            correctItem.Click += new System.EventHandler(this.correcPositiontItem_Click);
            exitItem.Click += new System.EventHandler(this.exitItem_Click);
            notifier.Icon = MaraudersMap.Properties.Resources.TaskbarIconHigh;
            notifier.Visible = true;
            notifier.Click += new EventHandler(notifyIcon1_Click);
            contextMenu1.LostFocus += (object sender, EventArgs a) => { contextMenu1.Hide(); };
            refreshTimer = new Timer(checkPosition, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

        
        }
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        void notifyIcon1_Click(object sender, EventArgs e)
        {
            contextMenu1.Show(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
        }
        private void refreshItem_Click(object Sender, EventArgs e)
        {
        }
        private void correcPositiontItem_Click(object Sender, EventArgs e)
        {
            currentPositionItem.Text = "Clicked";
        }
        private void exitItem_Click(object Sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            notifier.Dispose();
        }

        private void checkPosition(object state)
        {
            currentPosition += 1;
            currentPositionItem.Text = "" + currentPosition;
        }

        private void timeSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            string message = "Refresh every ";
            if(e.NewValue <= 1){
                message += "minute" + ":";
            }
            else{
                message += e.NewValue + " minutes";            
            }

            if(this.RefreshTimeBlock != null)
                this.RefreshTimeBlock.Text = message;
            refreshTimer.Change(TimeSpan.Zero, TimeSpan.FromMinutes(e.NewValue));
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.UsernameInput.Text.Length > 0 && this.PasswordInput.Password.Length > 0)
            {
                //Let's go ahead and login
            }
            else
            {
                //Let's throw a warning message
            }
        }

	}
}