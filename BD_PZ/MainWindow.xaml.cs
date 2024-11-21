using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BD_PZ
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OOO_CitadelEntities1 db;
        List<string> list = new List<string> { "maker", "product", "pasport", "Post", "Employer", "Storage", "Shop" };
        List<pasport> pasList = new List<pasport>();
        List<string> help = new List<string>();
        string pop;
        string funcString;
        bool isDark;



        List<product> Prod { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new OOO_CitadelEntities1();
            pasport pasport = new pasport();
            Func("maker");
            var zz = db.GetType().GetProperty(funcString);
            
            

            var item = pasport.GetType().GetProperties().ToList();
            for (int i = 0; i < item.Count; i++)
            {
                string br = item[i].ToString();
                br = br.Substring(br.IndexOf(' ') + 1);
                help.Add(br);
            }

            LizaKingOfMine(funcString);
            comSel.ItemsSource = help;
            Menu.ItemsSource = list;

        }


        private void LizaKingOfMine(string str)
        {

            switch (str)
            {

                case "maker":
                    help = new List<string>();
                    maker maker = new maker();

                    var select = db.maker.ToList();
                    var item = maker.GetType().GetProperties().ToList();
                    for (int i = 0; i < item.Count - 1; i++)
                    {
                        string br = item[i].ToString();
                        br = br.Substring(br.IndexOf(' ') + 1);
                        help.Add(br);

                    }
                    break;
                case "product":
                    product product = new product();
                    help = new List<string>();

                    item = product.GetType().GetProperties().ToList();
                    for (int i = 0; i < item.Count - 2; i++)
                    {
                        string br = item[i].ToString();
                        br = br.Substring(br.IndexOf(' ') + 1);
                        help.Add(br);
                    }
                    break;
                case "pasport":
                    help = new List<string>();
                    pasport pasport = new pasport();
                    item = pasport.GetType().GetProperties().ToList();
                    for (int i = 0; i < item.Count - 1; i++)
                    {
                        string br = item[i].ToString();
                        br = br.Substring(br.IndexOf(' ') + 1);
                        help.Add(br);
                    }
                    break;
                case "Post":

                    Post post = new Post();
                    help = new List<string>();
                    item = post.GetType().GetProperties().ToList();
                    for (int i = 0; i < item.Count - 1; i++)
                    {
                        string br = item[i].ToString();
                        br = br.Substring(br.IndexOf(' ') + 1);
                        help.Add(br);
                    }
                    break;
                case "Employer":
                    Employer employer = new Employer();
                    help = new List<string>();
                    item = employer.GetType().GetProperties().ToList();
                    for (int i = 0; i < item.Count - 4; i++)
                    {
                        string br = item[i].ToString();
                        br = br.Substring(br.IndexOf(' ') + 1);
                        help.Add(br);
                    }
                    break;
                case "Storage":

                    Storage storage = new Storage();
                    help = new List<string>();
                    item = storage.GetType().GetProperties().ToList();
                    for (int i = 0; i < item.Count - 3; i++)
                    {
                        string br = item[i].ToString();
                        br = br.Substring(br.IndexOf(' ') + 1);
                        help.Add(br);
                    }
                    break;
                case "Shop":
                    Shop shop = new Shop();
                    help = new List<string>();
                    item = shop.GetType().GetProperties().ToList();
                    for (int i = 0; i < item.Count - 2; i++)
                    {
                        string br = item[i].ToString();
                        br = br.Substring(br.IndexOf(' ') + 1);
                        help.Add(br);
                    }
                    break;
                default:
                    break;
            }
        }
        private void Func(string str)
        {

            using (SqlConnection conect = new SqlConnection(@"Server=DESKTOP-U6SHDSV;Integrated Security=SSPI;Database=OOO_Citadel"))
            {
                string zapros = $"SELECT * FROM {str}";
                using (SqlCommand cmd = new SqlCommand(zapros, conect))
                {
                    DataTable tab = new DataTable($"{str}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    conect.Open();
                    adapter.Fill(tab);
                    dgPass.ItemsSource = tab.DefaultView;
                    conect.Close();
                }
            }
            funcString = str;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (funcString == "maker")
            {
                maker maker = new maker();
                maker.maker_name = tbId.Text;
                maker.country = tbN.Text;
                db.maker.Add(maker);
                db.SaveChanges();
                dgPass.ItemsSource = db.maker.ToList();
            }
            else if (funcString == "product")
            {
                product product = new product();
                product.prod_name = tbId.Text;
                product.prod_price = Convert.ToInt32(tbN.Text);
                product.prod_productionDate = Convert.ToDateTime(tbD.Text);
                product.marker = tbR.Text;
                db.product.Add(product);
                db.SaveChanges();
                dgPass.ItemsSource = db.product.ToList();
            }
            else if (funcString == "pasport")
            {
                pasport pas = new pasport();
                pas.Pas_info = tbId.Text;
                pas.Full_name = tbN.Text;
                pas.Date_Of_Birth = Convert.ToDateTime(tbD.Text);
                pas.regis = tbR.Text;
                db.pasport.Add(pas);
                db.SaveChanges();
                dgPass.ItemsSource = db.pasport.ToList();
            }
            else if (funcString == "Post")
            {
                Post post = new Post();
                post.post_name = tbId.Text;
                post.salary = Convert.ToInt32(tbN.Text);
                db.Post.Add(post);
                db.SaveChanges();
                dgPass.ItemsSource = db.Post.ToList();
            }
            else if (funcString == "Employer")
            {
                Employer employer = new Employer();
                employer.pas_info = tbId.Text;
                employer.emp_post = tbN.Text;
                db.Employer.Add(employer);
                db.SaveChanges();
                dgPass.ItemsSource = db.Employer.ToList();
            }
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            string pIn = tbId.Text;
            if (funcString == "maker")
            {
                var selectIn = db.maker.Where(w => w.maker_name == pIn).FirstOrDefault();
                db.maker.Remove(selectIn);
                db.SaveChanges();
                dgPass.ItemsSource = db.maker.ToList();
            }
            else if (funcString == "product")
            {
                int par = int.Parse(pIn);
                var selectIn = db.product.Where(w => w.prod_id == par).FirstOrDefault();
                db.product.Remove(selectIn);
                db.SaveChanges();
                dgPass.ItemsSource = db.product.ToList();
            }
            else if (funcString == "pasport")
            {
                var selectIn = db.pasport.Where(w => w.Pas_info == pIn).FirstOrDefault();
                db.pasport.Remove(selectIn);
                db.SaveChanges();
                dgPass.ItemsSource = db.pasport.ToList();
            }
            else if (funcString == "Post")
            {
                var selectIn = db.Post.Where(w => w.post_name == pIn).FirstOrDefault();
                db.Post.Remove(selectIn);
                db.SaveChanges();
                dgPass.ItemsSource = db.Post.ToList();
            }
            else if (funcString == "Employer")
            {
                int par = int.Parse(pIn);
                var selectIn = db.Employer.Where(w => w.emp_id == par).FirstOrDefault();
                db.Employer.Remove(selectIn);
                db.SaveChanges();
                dgPass.ItemsSource = db.Employer.ToList();
            }           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string pIn = tbId.Text;
            var selectIn = db.pasport.Where(w => w.Pas_info == pIn).FirstOrDefault();
            selectIn.Pas_info = tbId.Text;
            selectIn.Full_name = tbN.Text;
            selectIn.Date_Of_Birth = Convert.ToDateTime(tbD);
            selectIn.regis = tbR.Text;
            db.SaveChanges();
            dgPass.ItemsSource = db.pasport.ToList();
        }



        private void Menu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string str = Menu.SelectedItem as string;

            Func(str);
            funcString = str;
            LizaKingOfMine(str);
            comSel.ItemsSource = help;
            Menu.ItemsSource = list;

        }

        private void Report_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog pD = new PrintDialog();
                if (pD.ShowDialog() == true)
                {
                    pD.PrintVisual(dgPass, "Проехт");
                }

            }
            finally
            {
                this.IsEnabled = true;

            }
        }

        private void textN_TextChanged(object sender, TextChangedEventArgs e)
        {


            if (funcString == "maker")
            {
                var sel = db.maker.ToList();
                dgPass.ItemsSource = sel.Where(x =>
                x.GetType().GetProperty(pop).GetValue(x).ToString().Contains(textN.Text))
                .ToList();
                sel.Clear();
            }
            else if (funcString == "product")
            {
                var sel = db.product.ToList();
                dgPass.ItemsSource = sel.Where(x =>
                x.GetType().GetProperty(pop).GetValue(x).ToString().Contains(textN.Text))
                .ToList();
                sel.Clear();
            }
            else if (funcString == "pasport")
            {
                var sel = db.pasport.ToList();
                dgPass.ItemsSource = sel.Where(x =>
                x.GetType().GetProperty(pop).GetValue(x).ToString().Contains(textN.Text))
                .ToList();
                sel = null;
            }
            else if (funcString == "Post")
            {
                var sel = db.Post.ToList();
                dgPass.ItemsSource = sel.Where(x =>
                x.GetType().GetProperty(pop).GetValue(x).ToString().Contains(textN.Text))
                .ToList();
                sel.Clear();
            }
            else if (funcString == "Employer")
            {
                var sel = db.Employer.ToList();
                dgPass.ItemsSource = sel.Where(x =>
                x.GetType().GetProperty(pop).GetValue(x).ToString().Contains(textN.Text))
                .ToList();
                sel.Clear();
            }
            else if (funcString == "Storage")
            {
                var sel = db.Storage.ToList();
                dgPass.ItemsSource = sel.Where(x =>
                x.GetType().GetProperty(pop).GetValue(x).ToString().Contains(textN.Text))
                .ToList();
                sel.Clear();
            }
            else
            {
                var sel = db.Shop.ToList();
                dgPass.ItemsSource = sel.Where(x =>
                x.GetType().GetProperty(pop).GetValue(x).ToString().Contains(textN.Text))
                .ToList();
                sel.Clear();
            }

        }



        private void Dark_Click(object sender, RoutedEventArgs e)
        {
            isDark = !isDark;
            if (isDark)
            {
                Color color = Color.FromRgb(10, 236, 252);
                this.Background = new SolidColorBrush(color);
                dgPass.Background = this.Background;
                SolidColorBrush colorBut = new SolidColorBrush(Color.FromRgb(46, 191, 201));
                foreach (var child in gridNme.Children)
                {
                    if (child is Button button)
                        button.Background = colorBut;
                }
                DarkBut.Content = "Dark";
            }
            else
            {
                Color color = Color.FromRgb(7, 14, 77);
                this.Background = new SolidColorBrush(color);
                dgPass.Background = this.Background;
                SolidColorBrush colorBut = new SolidColorBrush(Color.FromRgb(5, 10, 48));
                foreach (var child in gridNme.Children)
                {
                    if (child is Button button)
                        button.Background = colorBut;
                }
                DarkBut.Content = "Light";
            }
        }





        private void comSel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comSel.SelectedItem != null) pop = comSel.SelectedItem.ToString();
        }

        private void Export_Button(object sender, RoutedEventArgs e)
        {
            dgPass.SelectAllCells();
            dgPass.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dgPass);
            dgPass.UnselectAllCells();

            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            try
            {
                using (StreamWriter writer = new StreamWriter("wpfdata.csv"))
                {
                    writer.WriteLine(result);
                }
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "wpfdata.csv",
                    UseShellExecute = true // Открытие файла с ассоциированным приложением
                };
                Process.Start(psi);


                Process.Start("wpfdata.csv");
            }
            catch (Exception ex) { }
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {

            int pIn = Convert.ToInt32(tbId.Text);
            string fileName;
            var selectIn = db.product.Where(w => w.prod_id == pIn).FirstOrDefault();
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
                byte[] imageBytes = File.ReadAllBytes(destinationFilePath);

                selectIn.ima = imageBytes;
                
                db.SaveChanges();
                dgPass.ItemsSource = db.product.ToList();
               
            }
        }
    }
}
