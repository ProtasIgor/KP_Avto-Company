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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace AvtoSityApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowBrigadier.xaml
    /// </summary>
    public partial class WindowBrigadier : Window
    {
        private KursTestContext db;
        public WindowBrigadier()
        {
            InitializeComponent();
        }
        public WindowBrigadier(KursTestContext db)
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

            combobox1master.Items.Clear();
            combobox1brigade.Items.Clear();
            text1phone.Text = "+37599";

            var masters = from master in db.Masters
                          select new
                          {
                              Id = master.Id,
                              Name = $"{master.Name} {master.Surname} {master.Patronymic} | Phone: {master.Phone}",
                          };

            foreach (var item in masters)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                combobox1master.Items.Add(comboBoxItem);
            }

            var bridages = from brigade in db.Brigades
                           .Where (t => (!db.Brigadiers.Any(f => f.BrigadeId == t.Id)))
                           select new
                           {
                               Id = brigade.Id,
                               Name = $"{brigade.Name} | Phone: {brigade.Phone}"
                           };

            foreach (var item in bridages)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                combobox1brigade.Items.Add(comboBoxItem);
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HideGrid();
            grid2.Visibility = Visibility.Visible;

            comboboxEdit.Items.Clear();
            combobox2master.Items.Clear();
            combobox2brigade.Items.Clear();
            text2phone.Text = "+37599";

            var result = from brigadier in db.Brigadiers
                         select new
                         {
                             Id = brigadier.Id,
                             Name = $"{brigadier.Name} {brigadier.Surname} {brigadier.Patronymic} | Phone: {brigadier.Phone}",
                         };

            foreach (var item in result)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                comboboxEdit.Items.Add(comboBoxItem);
            }

            var masters = from master in db.Masters
                          select new
                          {
                              Id = master.Id,
                              Name = $"{master.Name} {master.Surname} {master.Patronymic} | Phone: {master.Phone}",
                          };

            foreach (var item in masters)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                combobox2master.Items.Add(comboBoxItem);
            }

            var bridages = from brigade in db.Brigades
                           .Where(t => (!db.Brigadiers.Any(f => f.BrigadeId == t.Id)))
                           select new
                           {
                               Id = brigade.Id,
                               Name = $"{brigade.Name} | Phone: {brigade.Phone}"
                           };

            foreach (var item in bridages)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                combobox2brigade.Items.Add(comboBoxItem);
            }

        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            HideGrid();
            grid3.Visibility = Visibility.Visible;

            comboboxDelete.Items.Clear();

            var result = from brigadier in db.Brigadiers
                //.Where(t => (!db.Brigadiers.Any(f => f.MasterId == t.Id) &&
                  //           !db.Brigadiers.Any(f => f.BrigadeId == t.Id)))
                         select new
                         {
                             Id = brigadier.Id,
                             Name = $"{brigadier.Name} {brigadier.Surname} {brigadier.Patronymic} | Phone: {brigadier.Phone}",
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
            if (isAge == false || age < 18 )
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


            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)combobox1brigade.SelectedItem;

            var brigade = -1;
            if (selectedComboBoxItem != null)
            {
                brigade = (int)selectedComboBoxItem.Tag;

            }

            ComboBoxItem selectedComboBoxItem2 = (ComboBoxItem)combobox1master.SelectedItem;

            var master = -1;
            if (selectedComboBoxItem2 != null)
            {
                master = (int)selectedComboBoxItem2.Tag;

            }

            Brigadier item = null;
            // создание
            if (brigade == -1 && master == -1)
            {
                item = new Brigadier()
                {
                    Name = text1name.Text,
                    Surname = text1sername.Text,
                    Patronymic = text1personame.Text,
                    Age = age,
                    Phone = text1phone.Text,
                    BrigadeId = null,
                    MasterId = null,
                };
            }
            else if (brigade == -1 && master != -1)
            {
                item = new Brigadier()
                {
                    Name = text1name.Text,
                    Surname = text1sername.Text,
                    Patronymic = text1personame.Text,
                    Age = age,
                    Phone = text1phone.Text,
                    BrigadeId = null,
                    MasterId = master,
                };
            }
            else if (brigade != -1 && master == -1)
            {
                item = new Brigadier()
                {
                    Name = text1name.Text,
                    Surname = text1sername.Text,
                    Patronymic = text1personame.Text,
                    Age = age,
                    Phone = text1phone.Text,
                    BrigadeId = brigade,
                    MasterId = null,
                };

            }
            else if (brigade == 1 && master == 1)
            {
                item = new Brigadier()
                {
                    Name = text1name.Text,
                    Surname = text1sername.Text,
                    Patronymic = text1personame.Text,
                    Age = age,
                    Phone = text1phone.Text,
                    BrigadeId = brigade,
                    MasterId = master,
                };
            }

            if (item != null)
            {
                db.Brigadiers.Add(item);

            }

            MessageBox.Show("Добавление прошло успешно!", "Уведомление", MessageBoxButton.OK);

            text1name.Text = string.Empty;
            text1sername.Text = string.Empty;
            text1personame.Text = string.Empty;
            text1age.Text = string.Empty;
            text1phone.Text = string.Empty;
            combobox1brigade.Items.Clear();
            combobox1master.Items.Clear();
            HideGrid();
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (comboboxEdit.SelectedItem == null)
            {
                MessageBox.Show("Выберите бригадира для редактирования!", "Уведомление", MessageBoxButton.OK);
                return;
            }
            else
            {
                ComboBoxItem selectedComboBoxItem = (ComboBoxItem)comboboxEdit.SelectedItem;

                if (selectedComboBoxItem == null)
                    return;

                var id = (int)selectedComboBoxItem.Tag;

                var item = db.Brigadiers.FirstOrDefault(t => t.Id == id);

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

                    ComboBoxItem selectedComboBoxItem2 = (ComboBoxItem)combobox2brigade.SelectedItem;

                    var brigade = -1;
                    if (selectedComboBoxItem2 != null)
                    {
                        brigade = (int)selectedComboBoxItem2.Tag;
                    }
                    if (brigade != -1)
                    {
                        item.BrigadeId = brigade;
                    }
                    ComboBoxItem selectedComboBoxItem3 = (ComboBoxItem)combobox2master.SelectedItem;

                    var master = -1;
                    if (selectedComboBoxItem3 != null)
                    {
                        master = (int)selectedComboBoxItem3.Tag;
                    }
                    if (master != -1)
                    {
                        item.MasterId = master;
                    }
                }

                text2name.Text = string.Empty;
                text2sername.Text = string.Empty;
                text2personame.Text = string.Empty;
                text2age.Text = string.Empty;
                text2phone.Text = string.Empty;
                comboboxEdit.Items.Clear();
                combobox2brigade.Items.Clear();
                combobox2master.Items.Clear();
                HideGrid();
            }

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (comboboxDelete.SelectedItem == null)
            {
                MessageBox.Show("Выберите бригадира для удаления!", "Уведомление", MessageBoxButton.OK);
                return;
            }
            else
            {
                ComboBoxItem selectedComboBoxItem = (ComboBoxItem)comboboxDelete.SelectedItem;

                if (selectedComboBoxItem == null)
                    return;

                var id = (int)selectedComboBoxItem.Tag;

                var item = db.Brigadiers.FirstOrDefault(t => t.Id == id);

                if (item != null)
                {
                    db.Brigadiers.Remove(item);
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
