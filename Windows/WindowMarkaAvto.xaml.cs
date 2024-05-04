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
    /// Логика взаимодействия для WindowMarkaAvto.xaml
    /// </summary>
    public partial class WindowMarkaAvto : Window
    {
        private KursTestContext db;
        public WindowMarkaAvto()
        {
            InitializeComponent();
        }
        public WindowMarkaAvto(KursTestContext db)
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
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HideGrid();
            grid2.Visibility = Visibility.Visible;

            comboboxEdit.Items.Clear();

            var result = from markaAvto in db.MarkaAvtos
                         select new
                         {
                             Id = markaAvto.Id,
                             Name = $"{markaAvto.Marka} {markaAvto.Model}",
                         };

            foreach (var item in result)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                comboboxEdit.Items.Add(comboBoxItem);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            HideGrid();
            grid3.Visibility = Visibility.Visible;

            comboboxDelete.Items.Clear();

            var result = from markaAvto in db.MarkaAvtos
                .Where(t => !db.Avtos.Any(f => f.MarkaId == t.Id))
                         select new
                         {
                             Id = markaAvto.Id,
                             Name = $"{markaAvto.Marka} {markaAvto.Model}",
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
            if (text1.Text == string.Empty || text1.Text == null ||
                text1model.Text == string.Empty || text1model.Text == null)
            {
                MessageBox.Show("Введите значение!", "Уведомление", MessageBoxButton.OK);
                return;
            }
            else
            {
                var item = new MarkaAvto()
                {
                    Marka = text1.Text,
                    Model = text1model.Text,
                };

                db.MarkaAvtos.Add(item);

                MessageBox.Show("Добавление прошло успешно!", "Уведомление", MessageBoxButton.OK);

                text1.Text = string.Empty;
                text1model.Text = string.Empty;
                comboboxEdit.Items.Clear();
                HideGrid();

            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (comboboxEdit.SelectedItem == null)
            {
                MessageBox.Show("Введите Марку Автомобиля для редактирования!", "Уведомление", MessageBoxButton.OK);
                return;
            }
            else
            {
                ComboBoxItem selectedComboBoxItem = (ComboBoxItem)comboboxEdit.SelectedItem;

                if (selectedComboBoxItem == null)
                    return;

                var id = (int)selectedComboBoxItem.Tag;

                var item = db.MarkaAvtos.FirstOrDefault(t => t.Id == id);

                if (item != null)
                {
                    if (textEdit.Text != string.Empty && textEdit.Text != null)
                    {
                        item.Marka = textEdit.Text;
                    }
                    if (textEditmodel.Text != string.Empty && textEditmodel.Text != null)
                    {
                        item.Model = textEditmodel.Text;
                    }
                    
                    if ((textEdit.Text != string.Empty && textEdit.Text != null) ||
                        (textEditmodel.Text != string.Empty && textEditmodel.Text != null))
                    {
                        MessageBox.Show("Редактирование прошло успешно!", "Уведомление", MessageBoxButton.OK);

                    }
                }

                textEdit.Text = string.Empty;
                textEditmodel.Text = string.Empty;
                comboboxEdit.Items.Clear();
                HideGrid();

            }

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (comboboxDelete.SelectedItem == null)
            {
                MessageBox.Show("Выберите марку авто для удаления!", "Уведомление", MessageBoxButton.OK);
                return;
            }
            else
            {
                ComboBoxItem selectedComboBoxItem = (ComboBoxItem)comboboxDelete.SelectedItem;

                if (selectedComboBoxItem == null)
                    return;

                var id = (int)selectedComboBoxItem.Tag;

                var item = db.MarkaAvtos.FirstOrDefault(t => t.Id == id);

                if (item != null)
                {
                    db.MarkaAvtos.Remove(item);
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
