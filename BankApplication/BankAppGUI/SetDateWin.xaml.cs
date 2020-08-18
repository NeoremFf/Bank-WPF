using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BankAppGUI
{
    /// <summary>
    /// Interaction logic for SetDateWin.xaml
    /// Добавление N дней к текущей дате
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
