using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace BD_PZ
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Window
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            Auto auto = new Auto();
            auto.Show();
            Close();
        }

        private void Button_Regis(object sender, RoutedEventArgs e)
        {
            OOO_CitadelEntities1 bd = new OOO_CitadelEntities1();
            string log = RegLogText.Text;
            string pas = RegPasText.Password;

            users users = new users();
            byte[] salt = RetSalt();
            byte[] hash = GenerateHash(pas, salt);

            byte[] hashBytes = new byte[36];       // Создается массив hashBytes размером 36 байтов. Это массив, в который будут записаны соль и хэш вместе.
            Array.Copy(salt, 0, hashBytes, 0, 16); // Эта строка копирует первые 16 байтов соли (сначала массива salt) в массив hashBytes,
                                                   // начиная с нулевого индекса. Так, первые 16 байтов hashBytes будут содержать соль.

            Array.Copy(hash,0, hashBytes, 16, 20); //Эта строка копирует 20 байтов хэша (начиная с нулевого индекса массива hash) в массив hashBytes,
                                                   //начиная с индекса 16. Таким образом, оставшиеся 20 байтов в hashBytes содержат хэш пароля.

            string savePasHash = Convert.ToBase64String(hashBytes);
            users.us_log = log;
            users.us_pas = savePasHash;

            bd.users.Add(users);
            bd.SaveChanges();

            RegLogText.Text = "";
            RegPasText.Password = "";

            MessageBox.Show("Вы зарегестрированы");
            Auto auto = new Auto();
            auto.Show();
            Close();
            

        }
        /// <summary>
        /// Метод RetSalt который возвращает массив байтов, представляющий "соль"
        /// </summary>
        /// <returns></returns>
        private byte[] RetSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            return salt;
        }
        /// <summary>
        /// Метод GenerateHash принимает два аргумента: пароль (pas) и соль (salt). Он создает хэш для пароля с учетом соли, 
        /// что делает хэш уникальным даже при одинаковом пароле, но разной соли. 
        /// </summary>
        /// <param name="pas"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private byte[] GenerateHash(string pas, byte[] salt)
        {
            var pfc = new Rfc2898DeriveBytes(pas, salt, 10000);
            byte[] hash = pfc.GetBytes(20);
            return hash;
        }

        
    }
}
