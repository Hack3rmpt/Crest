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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace krestiki_noloky
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int fighter;
        private static List<int> NumList = new List<int>();
        private int Resmove;
        List<Button> buttons;
        public MainWindow()
        {
            InitializeComponent();
            buttons = new List<Button>() { but1, but2, but3, but4, but5, but6, but7, but8, but9 };
            disableField();
        }

        private void but1_Click(object sender, RoutedEventArgs e)
        {
            RestartBT.IsEnabled = true;
            NgameBT.IsEnabled = true;

            switch (fighter)
            {
                case 0:
                    sender.GetType().GetProperty("Content").SetValue(sender, "O");
                    fighter = 1;
                    NumList.Add(1);
                    if (NumList.Count != 9)
                    {
                        Robot();
                        fighter = 0;
                    }
                    break;
                case 1:
                    sender.GetType().GetProperty("Content").SetValue(sender, "X");
                    fighter = 0;
                    NumList.Add(1);
                    if (NumList.Count != 9)
                    {
                        Robot();
                        fighter = 1;
                    }
                    break;
            }
            sender.GetType().GetProperty("IsEnabled").SetValue(sender, false);
            vincheker();
        }
        private void vincheker()
        {
            Button[] buttons = { but1, but2, but3, but4, but5, but6, but7, but8, but9 };
            int[,] winCombos = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };

            for (int i = 0; i < winCombos.GetLength(0); i++)
            {
                for (int j = 0; j < winCombos.GetLength(1); j++)
                {
                    if (buttons[winCombos[i,0]].Content == buttons[winCombos[i,1]].Content && buttons[winCombos[i,1]].Content == buttons[winCombos[i,2]].Content)
                    {
                        vinchekPro(buttons[winCombos[i,0]]);
                        return;
                    }
                }
            }
            if (NumList.Count == 9)
            {
                WinnerTB.Text = "Ничья";
            }
        }

        private void vinchekPro(Button but)
        {
            if (but.Content.ToString() != "")
            {
                if (but.Content.ToString() == "O")
                {
                    WinnerTB.Text = "Выйграл: O";
                    disableField();
                    RestartBT.IsEnabled = true;
                    NgameBT.IsEnabled = true;
                }
                else
                {
                    WinnerTB.Text = "Выйграл: x";
                    disableField();
                    RestartBT.IsEnabled = true;
                    NgameBT.IsEnabled = true;
                }
            }
        }

        private void XButton_Click(object sender, RoutedEventArgs e)
        {
            fighter = 1;
            Resmove = fighter;
            XButtom.IsEnabled = false;
            Obutton.IsEnabled = false;
            enableField();
        }

        private void Obutton_Click(object sender, RoutedEventArgs e)
        {
            fighter = 0;
            Resmove = fighter;
            Obutton.IsEnabled = false;
            XButtom.IsEnabled = false;
            enableField();
        }

        private void RestartBT_Click(object sender, RoutedEventArgs e)
        {
            fighter = Resmove;
            WinnerTB.Text = "";
            enableField();
            clearField();
            NumList.Clear();
            Obutton.IsEnabled = false;
            XButtom.IsEnabled = false;
        }
        private void NgameBT_Click(object sender, RoutedEventArgs e)
        {
            WinnerTB.Text = "";
            disableField();
            clearField();
            NumList.Clear();
            Obutton.IsEnabled = true;
            XButtom.IsEnabled = true;
        }
        private void Robot()
        {
            Random point = new Random();
            bool stop = true;
            while (stop)
            {
                int step = point.Next(0, 9);
                if (buttons[step].Content.ToString() == "")
                {
                    buttons[step].Content = fighter == 0 ? "O" : "X";
                    NumList.Add(1);
                    buttons[step].IsEnabled = false;
                    stop = false;
                }
            }
            vincheker();
        }
        private void disableField()
        {
            foreach (var item in buttons) item.IsEnabled = false;
            RestartBT.IsEnabled = false;
            NgameBT.IsEnabled = false;

        }
        private void clearField()
        {
            foreach (var item in buttons) item.Content = "";
        }

        private void enableField()
        {

            foreach (var item in buttons) item.IsEnabled = true;
        }
    }
}