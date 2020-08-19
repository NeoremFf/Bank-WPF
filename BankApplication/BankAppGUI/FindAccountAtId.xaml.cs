using System;
using System.Windows;

namespace BankAppGUI
{
    /// <summary>
    /// Interaction logic for FindAccountAtId.xaml
    /// Get id for find account with this
    /// </summary>
    public partial class FindAccountAtId : Window
    {
        MainWindow mainWin;

        public FindAccountAtId(MainWindow _mainWin)
        {
            InitializeComponent();

            this.Loaded += SetWinStatusActive;
            this.Closing += SetWinStatusInactive;

            mainWin = _mainWin;
        }

        // Get id from window text box
        private void GetId(object sender, RoutedEventArgs e)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(textBoxIdAccount.Text);
            }
            catch (Exception)
            {
                this.Close();
                MessageBox.Show("Введен некорректный формат.");
                return;
            }
            this.Close();
            mainWin.GetAccountInfo(id);
        }

        // -----------------------------------------------------------------------
        // Update state of current active window
        // -----------------------------------------------------------------------
        private void SetWinStatusActive(object sender, RoutedEventArgs e)
        {
            mainWin.OtherActiveWin = this;
        }
        private void SetWinStatusInactive(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWin.OtherActiveWin = null;
        }
    }
}
