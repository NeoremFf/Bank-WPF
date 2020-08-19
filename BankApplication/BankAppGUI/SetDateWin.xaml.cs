using System;
using System.Windows;

namespace BankAppGUI
{
    /// <summary>
    /// Interaction logic for SetDateWin.xaml
    /// Add N days to current date
    /// </summary>
    public partial class SetDateWin : Window
    {
        MainWindow mainWin;
        private DateTime date;

        public SetDateWin(MainWindow _main, DateTime _date)
        {
            InitializeComponent();

            this.Loaded += SetWinStatusActive;
            this.Closing += SetWinStatusInactive;

            mainWin = _main;
            date = _date;
        }

        // Get count of days from window text box
        private void SetDaysAdd(object sender, RoutedEventArgs e)
        {
            int days = 0;
            try
            {
                days = Convert.ToInt32(textBox_DaysAdd.Text);
            }
            catch (Exception)
            {
                this.Close();
                MessageBox.Show("Введен некорректный формат.");
                return;
            }

            this.Close();
            date = date.AddDays(days);
            mainWin.date = date;
            mainWin.UpdateDate();
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
