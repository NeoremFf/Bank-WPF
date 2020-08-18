using System;
using System.Windows;

namespace BankAppGUI
{
    /// <summary>
    /// Interaction logic for FindAccountAtId.xaml
    /// Получение Id для поиска счета 
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
