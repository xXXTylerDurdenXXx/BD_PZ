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
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;


namespace BD_PZ
{
    /// <summary>
    /// Логика взаимодействия для Auto.xaml
    /// </summary>
    public partial class Auto : Window
    {
        int countFeils = 0;
        public Auto()
        {
            InitializeComponent();
        }

       
        private void Button_Reg(object sender, RoutedEventArgs e)
        {
            Reg reg = new Reg();
            reg.Show();
            Close();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OOO_CitadelEntities1 bd = new OOO_CitadelEntities1();
            users users = new users();
           
            string log = LoginTextBox.Text;
            string pas = PasswordBox.Password;
            try
            {
                var user = bd.users.Where(d => (d.us_log == log)).FirstOrDefault();
                if (user != null)
                {

                    string savePasHash = user.us_pas;

                    byte[] hashBytes = Convert.FromBase64String(savePasHash);
                    byte[] salt = new byte[16];
                    Array.Copy(hashBytes, 0, salt, 0, 16);

                    var rfc = new Rfc2898DeriveBytes(pas, salt, 10000);
                    byte[] hash = rfc.GetBytes(20);
                    /* Сравнение вводимых данных с сохранными данными в бд с помощью цикла, начинаем с 16 так как первые 16 символов это соль */
                    for (int i = 0; i < 20; i++)
                    {
                        if (hashBytes[i + 16] != hash[i])
                            throw new UnauthorizedAccessException();

                    }


                    MessageBox.Show("С возвращением бро");
                    MainWindow main = new MainWindow();
                    main.Show();
                    Close();


                }
                else 
                {
                    if (countFeils == 3)
                    {
                        Cap cap = new Cap();
                        cap.Show();
                    }
                    else
                    {
                        MessageBox.Show("ЧТО ТО НЕ ТАК проверь или зарегестрируй ЙОУ ");
                        countFeils++;
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());

            }

        }
    }
}
