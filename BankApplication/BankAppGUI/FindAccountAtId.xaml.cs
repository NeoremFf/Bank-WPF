using System;
using System.Windows;

namespace BankAppGUI
{
    /// <summary>
    /// Interaction logic for FindAccountAtId.xaml
    /// </summary>
    public partial class FindAccountAtId : Window
    {
        MainWindow mainWin;

        public FindAccountAtId(MainWindow _mainWin)
        {
            InitializeComponent();

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
    }
}
