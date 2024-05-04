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
    /// Логика взаимодействия для WindowAvto.xaml
    /// </summary>
    public partial class WindowAvto : Window
    {
        private KursTestContext db;
        public WindowAvto()
        {
            InitializeComponent();
        }
        public WindowAvto(KursTestContext db)
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

            combobox1facility.Items.Clear();
            combobox1marka.Items.Clear();
            combobox1status.Items.Clear();
            combobox1typeavto.Items.Clear();

            text1cargoweight.Text = string.Empty;
            text1color.Text = string.Empty;
            text1cost.Text = string.Empty;
            text1peoplecap.Text = string.Empty;
            text1power.Text = string.Empty;
            text1weight.Text = string.Empty;

            data1start.SelectedDate = null;
            data1end.SelectedDate = null;

            // объект гаражного хозяйства
            var facilitys = from facs in db.Facilities
                           select new
                           {
                               Id = facs.Id,
                               Name = $"{facs.Name} | Адрес: {facs.Address}",
                           };

            foreach (var item in facilitys)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                combobox1facility.Items.Add(comboBoxItem);
            }
            // тип авто
            var typeAvtos = from facs in db.TypeAvtos
                            select new
                            {
                                Id = facs.Id,
                                Name = $"{facs.Name}",
                            };

            foreach (var item in typeAvtos)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                combobox1typeavto.Items.Add(comboBoxItem);
            }
            // статус авто
            var statusAvtos = from facs in db.StatusAvtos
                            select new
                            {
                                Id = facs.Id,
                                Name = $"{facs.Name}",
                            };

            foreach (var item in statusAvtos)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                combobox1status.Items.Add(comboBoxItem);
            }
            // Марка авто
            var markaAvtos = from facs in db.MarkaAvtos
                            select new
                            {
                                Id = facs.Id,
                                Name = $"{facs.Marka} {facs.Model}",
                            };

            foreach (var item in markaAvtos)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                combobox1marka.Items.Add(comboBoxItem);
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HideGrid();
            grid2.Visibility = Visibility.Visible;

            comboboxEdit.Items.Clear();
            combobox2facility.Items.Clear();
            combobox2marka.Items.Clear();
            combobox2status.Items.Clear();
            combobox2typeavto.Items.Clear();

            text2cargoweight.Text = string.Empty;
            text2color.Text = string.Empty;
            text2cost.Text = string.Empty;
            text2peoplecap.Text = string.Empty;
            text2power.Text = string.Empty;
            text2weight.Text = string.Empty;

            data2start.SelectedDate = null;
            data2end.SelectedDate = null;

            var result = from av in db.Avtos
                         join status in db.StatusAvtos on av.StatusId equals status.Id
                         join type in db.TypeAvtos on av.TypeAvtoId equals type.Id
                         join marka in db.MarkaAvtos on av.MarkaId equals marka.Id
                         select new
                         {
                             Id = av.Id,
                             Name = $"{marka.Marka} {marka.Model} ({status.Name}) | Дата приема: {av.ReceptionDateTime}",
                         };

            foreach (var item in result)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                comboboxEdit.Items.Add(comboBoxItem);
            }

            // объект гаражного хозяйства
            var facilitys = from facs in db.Facilities
                            select new
                            {
                                Id = facs.Id,
                                Name = $"{facs.Name} | Адрес: {facs.Address}",
                            };

            foreach (var item in facilitys)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                combobox2facility.Items.Add(comboBoxItem);
            }
            // тип авто
            var typeAvtos = from facs in db.TypeAvtos
                            select new
                            {
                                Id = facs.Id,
                                Name = $"{facs.Name}",
                            };

            foreach (var item in typeAvtos)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                combobox2typeavto.Items.Add(comboBoxItem);
            }
            // статус авто
            var statusAvtos = from facs in db.StatusAvtos
                              select new
                              {
                                  Id = facs.Id,
                                  Name = $"{facs.Name}",
                              };

            foreach (var item in statusAvtos)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                combobox2status.Items.Add(comboBoxItem);
            }
            // Марка авто
            var markaAvtos = from facs in db.MarkaAvtos
                             select new
                             {
                                 Id = facs.Id,
                                 Name = $"{facs.Marka} {facs.Model}",
                             };

            foreach (var item in markaAvtos)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{item.Name}";
                comboBoxItem.Tag = item.Id;

                combobox2marka.Items.Add(comboBoxItem);
            }

        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            HideGrid();
            grid3.Visibility = Visibility.Visible;

            comboboxDelete.Items.Clear();

            var result = from avtos in db.Avtos
                         .Where(t => ((!db.Drives.Any(f => f.AvtoId == t.Id)) &&
                                        (!db.Employees.Any(f => f.AvtoId == t.Id)) &&
                                        (!db.Repairs.Any(f => f.AvtoId == t.Id))))
                         join status in db.StatusAvtos on avtos.StatusId equals status.Id
                         join type in db.TypeAvtos on avtos.TypeAvtoId equals type.Id
                         join marka in db.MarkaAvtos on avtos.MarkaId equals marka.Id
                         select new
                         {
                             Id = avtos.Id,
                             Name = $"{marka.Marka} {marka.Model} ({status.Name}) | Дата приема: {avtos.ReceptionDateTime}",
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
            // проверка Цвет
            if (text1color.Text == string.Empty || text1color.Text == null)
            {
                MessageBox.Show("Введите Цвет Автомобиля!", "Уведомление", MessageBoxButton.OK);
                return;
            }

            // проверка вес
            var weight = 1000;
            var isWeight = int.TryParse(text1weight.Text, out weight);
            if (isWeight == false || weight < 500 || weight > 100000)
            {
                MessageBox.Show("Введите Вес Автомобиля (от 500 кг) [500 - 100 000]!", "Уведомление", MessageBoxButton.OK);
                return;
            }
            // проверка стоимости
            var cost = 1000;
            var isCost = int.TryParse(text1cost.Text, out cost);
            if (isCost == false || cost < 1 || cost > 1000000)
            {
                MessageBox.Show("Введите Стоимость Автомобиля (в бел.руб.) [1 - 1 000 000]!", "Уведомление", MessageBoxButton.OK);
                return;
            }
            // проверка Количество пассажирских мест
            var peoplecap = 1000;
            if (text1peoplecap.Text != string.Empty)
            {
                var isPeopleCap = int.TryParse(text1peoplecap.Text, out peoplecap);
                if (isPeopleCap == false || peoplecap > 255 || peoplecap < 1)
                {
                    MessageBox.Show("Введите Количество пассажирских мест (целое число [1 - 255] (Включительно).)!", "Уведомление", MessageBoxButton.OK);
                    return;
                }
            }
            // проверка Максимальной грузоподъемности
            var cargoweight = 1000;
            if (text1cargoweight.Text != string.Empty)
            {
                var isPeopleCap = int.TryParse(text1cargoweight.Text, out cargoweight);
                if (isPeopleCap == false || cargoweight < 100)
                {
                    MessageBox.Show("Введите Максимальную грузоподъемность (не менее 100кг)!", "Уведомление", MessageBoxButton.OK);
                    return;
                }
            }
            // проверка 
            var power = 1000;
            if (text1power.Text != string.Empty)
            {
                var isPeopleCap = int.TryParse(text1power.Text, out power);
                if (isPeopleCap == false || power < 10 || power > 2500)
                {
                    MessageBox.Show("Введите Количество лошадинных сил (целое число) [10 - 2 500]!", "Уведомление", MessageBoxButton.OK);
                    return;
                }
            }

            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)combobox1facility.SelectedItem;

            var facility = -1;
            if (selectedComboBoxItem != null)
            {
                facility = (int)selectedComboBoxItem.Tag;

            }

            ComboBoxItem selectedComboBoxItem2 = (ComboBoxItem)combobox1status.SelectedItem;

            var status = -1;
            if (selectedComboBoxItem2 != null)
            {
                status = (int)selectedComboBoxItem2.Tag;

            }

            ComboBoxItem selectedComboBoxItem3 = (ComboBoxItem)combobox1typeavto.SelectedItem;

            var typeavto = -1;
            if (selectedComboBoxItem3 != null)
            {
                typeavto = (int)selectedComboBoxItem3.Tag;

            }

            ComboBoxItem selectedComboBoxItem4 = (ComboBoxItem)combobox1marka.SelectedItem;

            var marka = -1;
            if (selectedComboBoxItem4 != null)
            {
                marka = (int)selectedComboBoxItem4.Tag;

            }
            // дата приема
            DateTime? receptionDateTime = null;
            if (data1start.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Пожалуйста укажите дату приема авто.", "Уведомление", MessageBoxButton.OK);
                return;
            }
            else
            {
                receptionDateTime = data1start.SelectedDate;

            }
            // дата списания
            DateTime? dispDateTime = null;
            if (data1end.SelectedDate.HasValue != false)
            {
                if (data1end.SelectedDate > receptionDateTime)
                    dispDateTime = data1end.SelectedDate;
            }


            var item = new Avto()
            {
                FacilityId = facility,
                MarkaId = marka,
                TypeAvtoId = typeavto,
                StatusId = status,
                Color = text1color.Text,
                Weight = weight,
                Cost = cost,
                Horsepower = power,
                PeopleCapacity = null,
                LiftingCapacity = null,
                ReceptionDateTime = receptionDateTime,
                DisposalDateTime = dispDateTime,
            };

            // если грузовик
            if (typeavto == 5)
            {
                item.LiftingCapacity = cargoweight;
            }
            else
            {
                item.PeopleCapacity = peoplecap;
            }

            if (item != null)
            {
                db.Avtos.Add(item);
                MessageBox.Show("Добавление прошло успешно!", "Уведомление", MessageBoxButton.OK);
            }


            combobox1facility.Items.Clear();
            combobox1marka.Items.Clear();
            combobox1status.Items.Clear();
            combobox1typeavto.Items.Clear();

            text1cargoweight.Text = string.Empty;
            text1color.Text = string.Empty;
            text1cost.Text = string.Empty;
            text1peoplecap.Text = string.Empty;
            text1power.Text = string.Empty;
            text1weight.Text = string.Empty;

            data1start.SelectedDate = null;
            data1end.SelectedDate = null;
            HideGrid();
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (comboboxEdit.SelectedItem == null)
            {
                MessageBox.Show("Выберите Автомобиль для редактирования!", "Уведомление", MessageBoxButton.OK);
                return;
            }
            else
            {
                ComboBoxItem selectedComboBoxItem = (ComboBoxItem)comboboxEdit.SelectedItem;

                if (selectedComboBoxItem == null)
                    return;

                var id = (int)selectedComboBoxItem.Tag;

                var item = db.Avtos.FirstOrDefault(t => t.Id == id);

                if (item != null)
                {
                    // проверка Цвет
                    if (text2color.Text != string.Empty || text2color.Text != null)
                    {
                        item.Color = text2color.Text;
                    }

                    // проверка вес
                    var weight = 1000;
                    var isWeight = int.TryParse(text2weight.Text, out weight);
                    if (isWeight == true && weight > 500 && weight < 100000)
                    {
                        item.Weight = weight;
                    }
                    // проверка стоимости
                    var cost = 1000;
                    var isCost = int.TryParse(text2cost.Text, out cost);
                    if (isCost == true && cost > 1 && cost < 1000000)
                    {
                        item.Cost = cost;
                    }
                    // проверка Количество пассажирских мест
                    var peoplecap = 1000;
                    if (item.TypeAvtoId != 5 && text1peoplecap.Text != string.Empty)
                    {
                        var isPeopleCap = int.TryParse(text2peoplecap.Text, out peoplecap);
                        if (isPeopleCap == true && peoplecap < 255 && peoplecap > 1)
                        {
                            item.PeopleCapacity = peoplecap;
                        }
                    }
                    // проверка Максимальной грузоподъемности
                    var cargoweight = 1000;
                    if (item.TypeAvtoId == 5 && text2cargoweight.Text != string.Empty)
                    {
                        var isPeopleCap = int.TryParse(text2cargoweight.Text, out cargoweight);
                        if (isPeopleCap == true || cargoweight >= 100)
                        {
                            item.LiftingCapacity = cargoweight;
                        }
                    }
                    // проверка 
                    var power = 1000;
                    if (text2power.Text != string.Empty)
                    {
                        var isPeopleCap = int.TryParse(text2power.Text, out power);
                        if (isPeopleCap == true && power > 10 && power < 2500)
                        {
                            item.Horsepower = power;
                        }
                    }

                    ComboBoxItem selectedComboBoxItem5 = (ComboBoxItem)combobox2facility.SelectedItem;

                    var facility = -1;
                    if (selectedComboBoxItem5 != null)
                    {
                        item.FacilityId = (int)selectedComboBoxItem5.Tag;

                    }

                    ComboBoxItem selectedComboBoxItem2 = (ComboBoxItem)combobox2status.SelectedItem;

                    var status = -1;
                    if (selectedComboBoxItem2 != null)
                    {
                        item.StatusId = (int)selectedComboBoxItem2.Tag;

                    }

                    ComboBoxItem selectedComboBoxItem3 = (ComboBoxItem)combobox2typeavto.SelectedItem;

                    var typeavto = -1;
                    if (selectedComboBoxItem3 != null)
                    {
                        item.PeopleCapacity = null;
                        item.LiftingCapacity = null;
                        item.TypeAvtoId = (int)selectedComboBoxItem3.Tag;

                    }

                    ComboBoxItem selectedComboBoxItem4 = (ComboBoxItem)combobox2marka.SelectedItem;

                    var marka = -1;
                    if (selectedComboBoxItem4 != null)
                    {
                        item.MarkaId = (int)selectedComboBoxItem4.Tag;

                    }
                    // дата приема
                    if (data2start.SelectedDate.HasValue == true)
                    {
                        item.ReceptionDateTime = data2start.SelectedDate;
                    }
                    // дата списания
                    if (data2end.SelectedDate.HasValue == true)
                    {
                        if (data2end.SelectedDate > data2start.SelectedDate)
                        {
                            item.DisposalDateTime = data2end.SelectedDate;
                        }
                    }
                }

                comboboxEdit.Items.Clear();
                combobox2facility.Items.Clear();
                combobox2marka.Items.Clear();
                combobox2status.Items.Clear();
                combobox2typeavto.Items.Clear();

                text2cargoweight.Text = string.Empty;
                text2color.Text = string.Empty;
                text2cost.Text = string.Empty;
                text2peoplecap.Text = string.Empty;
                text2power.Text = string.Empty;
                text2weight.Text = string.Empty;

                data2start.SelectedDate = null;
                data2end.SelectedDate = null;
                HideGrid();
            }

        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (comboboxDelete.SelectedItem == null)
            {
                MessageBox.Show("Выберите Автомобиль для удаления!", "Уведомление", MessageBoxButton.OK);
                return;
            }
            else
            {
                ComboBoxItem selectedComboBoxItem = (ComboBoxItem)comboboxDelete.SelectedItem;

                if (selectedComboBoxItem == null)
                    return;

                var id = (int)selectedComboBoxItem.Tag;

                var item = db.Avtos.FirstOrDefault(t => t.Id == id);

                if (item != null)
                {
                    db.Avtos.Remove(item);
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
