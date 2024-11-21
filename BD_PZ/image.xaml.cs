using Microsoft.Win32;
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
using System.IO;

namespace BD_PZ
{
    /// <summary>
    /// Логика взаимодействия для image.xaml
    /// </summary>
    public partial class image : Window
    {
        string fileName;
        public image()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string sourceFilePath = openFileDialog.FileName;

                string path = AppDomain.CurrentDomain.BaseDirectory;
                string imagesFolder = Path.Combine(path, "Images");
               
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(sourceFilePath);
                string destinationFilePath = Path.Combine(imagesFolder, fileName);

                File.Copy(sourceFilePath, destinationFilePath, false);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string imagesFolder = Path.Combine(appFolder, "Images");

           
            string filePath = Path.Combine(imagesFolder, fileName);

         
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не найден.");
                return;
            }

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

         
            ima.Source = bitmap;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
