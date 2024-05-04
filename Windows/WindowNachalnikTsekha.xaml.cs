using AvtoSityApp.Mode;
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

namespace AvtoSityApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowNachalnikTsekha.xaml
    /// </summary>
    public partial class WindowNachalnikTsekha : Window
    {
        private KursTestContext db;
        public WindowNachalnikTsekha()
        {
            InitializeComponent();
        }
        public WindowNachalnikTsekha(KursTestContext db)
        {
            InitializeComponent();

            this.db = db;

        }

        // сохранение в БД
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
            MessageBox.Show("Изменения приняты!", "Уведомление", MessageBoxButton.OK);
            HideGrid();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HideGrid();
            grid1.Visibility = Visibility.Visible;

            combobox1nach.Items.Clear();
            text1phone.Text = "+37599";

            var companys = from company in db.Companies
                        select new
                        {
                            Id = company.Id,
                            Name = $"{company.Name} | Адрес: {company.Аddress}",
                        };

            foreach (var item in companys)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                combobox1nach.Items.Add(comboBoxItem);
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HideGrid();
            grid2.Visibility = Visibility.Visible;

            comboboxEdit.Items.Clear();
            combobox2nach.Items.Clear();
            text2phone.Text = "+37599";

            var result = from nachalnikTsekhas in db.NachalnikTsekhas
                         select new
                         {
                             Id = nachalnikTsekhas.Id,
                             Name = $"{nachalnikTsekhas.Name} {nachalnikTsekhas.Surname} {nachalnikTsekhas.Patronymic} | Phone: {nachalnikTsekhas.Phone}",
                         };

            foreach (var item in result)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                comboboxEdit.Items.Add(comboBoxItem);
            }

            var companys = from company in db.Companies
                           select new
                           {
                               Id = company.Id,
                               Name = $"{company.Name} | Адрес: {company.Аddress}",
                           };

            foreach (var item in companys)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                combobox2nach.Items.Add(comboBoxItem);
            }

        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            HideGrid();
            grid3.Visibility = Visibility.Visible;

            comboboxDelete.Items.Clear();

            var result = from nachalnikTsekha in db.NachalnikTsekhas
                         .Where(t => (!db.NachalnikUchastkas.Any(f => f.NachalnikTsekhaId == t.Id)))
                         select new
                         {
                             Id = nachalnikTsekha.Id,
                             Name = $"{nachalnikTsekha.Name} {nachalnikTsekha.Surname} {nachalnikTsekha.Patronymic} | Phone: {nachalnikTsekha.Phone}",
                         };

            foreach (var item in result)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                comboboxDelete.Items.Add(comboBoxItem);
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            // проверка фио
            if (text1name.Text == string.Empty || text1name.Text == null ||
                text1sername.Text == string.Empty || text1sername.Text == null ||
                text1personame.Text == string.Empty || text1personame.Text == null)
            {
                MessageBox.Show("Введите ФИО!", "Уведомление", MessageBoxButton.OK);
                return;
            }
            // проверка возраста
            var age = 18;
            var isAge = int.TryParse(text1age.Text, out age);
            if (isAge == false || age < 18)
            {
                MessageBox.Show("Введите Возраст (от 18 лет)!", "Уведомление", MessageBoxButton.OK);
                return;
            }
            // проверка телефона
            if (text1phone.Text.StartsWith("+37599") == false || text1phone.Text.Length != 13 || int.TryParse(text1phone.Text.Substring(1), out int p) == false)
            {
                MessageBox.Show("Введите Номер телефона (+375 99 ХХХ ХХ ХХ) без пробелов!", "Уведомление", MessageBoxButton.OK);
                return;
            }


            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)combobox1nach.SelectedItem;

            var brigade = -1;
            if (selectedComboBoxItem != null)
            {
                brigade = (int)selectedComboBoxItem.Tag;

            }

            NachalnikTsekha item = null;
            // создание
            if (brigade == -1)
            {
                item = new NachalnikTsekha()
                {
                    Name = text1name.Text,
                    Surname = text1sername.Text,
                    Patronymic = text1personame.Text,
                    Age = age,
                    Phone = text1phone.Text,
                    CompanyId = null,
                };
            }
            else
            {
                item = new NachalnikTsekha()
                {
                    Name = text1name.Text,
                    Surname = text1sername.Text,
                    Patronymic = text1personame.Text,
                    Age = age,
                    Phone = text1phone.Text,
                    CompanyId = brigade,
                };
            }

            if (item != null)
            {
                db.NachalnikTsekhas.Add(item);

            }

            MessageBox.Show("Добавление прошло успешно!", "Уведомление", MessageBoxButton.OK);

            text1name.Text = string.Empty;
            text1sername.Text = string.Empty;
            text1personame.Text = string.Empty;
            text1age.Text = string.Empty;
            text1phone.Text = string.Empty;
            combobox1nach.Items.Clear();
            HideGrid();
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (comboboxEdit.SelectedItem == null)
            {
                MessageBox.Show("Выберите начальника цеха для редактирования!", "Уведомление", MessageBoxButton.OK);
                return;
            }
            else
            {
                ComboBoxItem selectedComboBoxItem = (ComboBoxItem)comboboxEdit.SelectedItem;

                if (selectedComboBoxItem == null)
                    return;

                var id = (int)selectedComboBoxItem.Tag;

                var item = db.NachalnikTsekhas.FirstOrDefault(t => t.Id == id);

                if (item != null)
                {
                    if (text2name.Text != string.Empty && text2name.Text != null)
                    {
                        item.Name = text2sername.Text;
                    }
                    if (text1sername.Text != string.Empty && text2sername.Text != null)
                    {
                        item.Surname = text2sername.Text;
                    }
                    if (text2personame.Text != string.Empty && text2personame.Text != null)
                    {
                        item.Patronymic = text2personame.Text;
                    }
                    // проверка возраста
                    var age = 18;
                    var isAge = int.TryParse(text2age.Text, out age);
                    if (isAge == true || age >= 18)
                    {
                        item.Age = age;
                    }
                    // проверка телефона
                    if (text2phone.Text.StartsWith("+37599") == true && text2phone.Text.Length == 13 || int.TryParse(text1phone.Text.Substring(1), out int p) == true)
                    {
                        item.Phone = text2phone.Text;
                    }

                    ComboBoxItem selectedComboBoxItem2 = (ComboBoxItem)combobox2nach.SelectedItem;

                    var brigade = -1;
                    if (selectedComboBoxItem2 != null)
                    {
                        brigade = (int)selectedComboBoxItem2.Tag;
                    }
                    if (brigade != -1)
                    {
                        item.CompanyId = brigade;
                    }
                }

                text2name.Text = string.Empty;
                text2sername.Text = string.Empty;
                text2personame.Text = string.Empty;
                text2age.Text = string.Empty;
                text2phone.Text = string.Empty;
                comboboxEdit.Items.Clear();
                combobox2nach.Items.Clear();
                HideGrid();
            }

        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (comboboxDelete.SelectedItem == null)
            {
                MessageBox.Show("Выберите начальника цеха для удаления!", "Уведомление", MessageBoxButton.OK);
                return;
            }
            else
            {
                ComboBoxItem selectedComboBoxItem = (ComboBoxItem)comboboxDelete.SelectedItem;

                if (selectedComboBoxItem == null)
                    return;

                var id = (int)selectedComboBoxItem.Tag;

                var item = db.NachalnikTsekhas.FirstOrDefault(t => t.Id == id);

                if (item != null)
                {
                    db.NachalnikTsekhas.Remove(item);
                    MessageBox.Show("Удаление прошло успешно!", "Уведомление", MessageBoxButton.OK);
                }

                comboboxDelete.Items.Clear();
                HideGrid();

            }

        }

        private void HideGrid()
        {
            grid1.Visibility = Visibility.Collapsed;
            grid2.Visibility = Visibility.Collapsed;
            grid3.Visibility = Visibility.Collapsed;
        }
    }
}
