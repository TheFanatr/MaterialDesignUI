using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace List
{
    /// <summary>
    /// Interaction logic for Input.xaml
    /// </summary>
    public partial class Input : Window
    {
        public Input()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                Content.Focus();
                Content.SelectAll();
                Content.KeyDown += (sI, eI) =>
                {
                    if (eI.Key == Key.Enter) AddButton_Click(sI, eI);
                };
                KeyDown += (sI, eI) =>
                {
                    if (eI.Key == Key.Escape) Close();
                };
            };
        }

        public bool OK { get; set; }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            OK = true;
            Close();
        }
    }
}
