﻿using System;
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
using WpfApp1.DataModel;
using WpfApp1.Windows;

namespace WpfApp1.CustomControls
{
    public partial class PrognosisWeather : UserControl
    {
        private Prognosis _thisprognosis;
        public Prognosis prognosis
        {
            get { return _thisprognosis; }
            set
            {
                _thisprognosis = value
                ?? throw new ArgumentNullException(nameof(_thisprognosis));
            }
        }
        public PrognosisWeather(Prognosis progn)
        {
            InitializeComponent();
            prognosis = progn;
            Fill();
        }
        private void Fill() 
        {
        label_ID.Content = prognosis.ID;
        label_Date.Content = prognosis.Date.Value.ToShortDateString();
        }

        private void buttonShowWeather_Click(object sender, RoutedEventArgs e)
        {
            ShowWeatherAtTimeWindow window = new ShowWeatherAtTimeWindow(prognosis);
            window.ShowDialog();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = MessageBox.Show(
            $"Вы действительно хотите удалить запись в ID - {prognosis.ID}",
            "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (MessageBoxResult.Yes == messageBox)
            {
                ModelEF model = new ModelEF();
                model.Prognosis.Remove(model.Prognosis.First(x =>
                _thisprognosis.ID == x.ID));
            try
                {
                    model.SaveChanges();
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                this.Visibility = Visibility.Hidden;
            }
        }
        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button_Delete.Visibility = Visibility.Visible;
        }
        private void PrognosisWeatherControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Button_Delete.Visibility = Visibility.Hidden;
        }
    }
}
