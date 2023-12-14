﻿using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;
using WpfApp1.Classes;
using WpfApp1.CustomControls;
using WpfApp1.DataModel;

namespace WpfApp1.Windows
{
    public partial class ShowWeatherAtTimeWindow : Window
    {
        private Prognosis prognosis;
        public ShowWeatherAtTimeWindow(Prognosis prog)
        {
            InitializeComponent();
            prognosis = prog;
            labelContent.Content += " " + prognosis.Date.Value.ToShortTimeString();
            LoadDataWrapPanel(prog.Time);
        }
        private void LoadDataWrapPanel(string info)
        {
            List<ComplexWeatherInfo> infos;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ComplexWeatherInfo>));
            using (StringReader reader = new StringReader(info)) 
            { 
            infos = xmlSerializer.Deserialize(reader) as List<ComplexWeatherInfo>;
            }
            infos.ForEach(item => wrapPanelWeather.Children.Add(new WeatherCustomControl(item)));
        }
    }
}
