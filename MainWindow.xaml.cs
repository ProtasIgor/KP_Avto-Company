using AvtoSityApp.Mode;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AvtoSityApp.Windows;

namespace AvtoSityApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KursTestContext databaseContext;
        // строка подключения к БД
        private string connectionString;
        // Текушая тема
        private bool _isDarkTheme = true;
        // ресурсы темной темы
        private ResourceDictionary _darkThemeDictionary;
        // ресурсы светлой темы
        private ResourceDictionary _lightThemeDictionary;

        public MainWindow()
        {
            // dotnet ef dbcontext scaffold "Data Source=DESKTOP-4NVMSI5\SERVER;Database=kursTest;Integrated Security=True;Trust Server Certificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Model
            InitializeComponent();

            try
            {
                _darkThemeDictionary = new ResourceDictionary()
                {
                    Source = new Uri("./Resources/Dark.xaml", UriKind.Relative)
                };

                _lightThemeDictionary = new ResourceDictionary()
                {
                    Source = new Uri("./Resources/Light.xaml", UriKind.Relative)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                throw;
            }

            // Установка темной етмы при запуске программы
            Application.Current.Resources.MergedDictionaries.Add(_darkThemeDictionary);

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            databaseContext = new KursTestContext(connectionString);
            // Выход из программы
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        // Информация 
        private void ButtonInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Информационная система \"Автопредприятие города\"." +
                "",
                "Информация", MessageBoxButton.OK);
        }
        // Смена темы приложения
        private void ButtonChangeTheme_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            if (_isDarkTheme)
            {
                buttonDarkTheme.IsEnabled = true;
                buttonLightTheme.IsEnabled = false;
                Application.Current.Resources.MergedDictionaries.Add(_lightThemeDictionary);
            }
            else
            {
                buttonLightTheme.IsEnabled = true;
                buttonDarkTheme.IsEnabled = false;
                Application.Current.Resources.MergedDictionaries.Add(_darkThemeDictionary);
            }
            _isDarkTheme = !_isDarkTheme;
        }
        // Скрытие контейнеров
        private void HiddenContainer()
        {
            ex1.Visibility = Visibility.Collapsed;
            ex2.Visibility = Visibility.Collapsed;
            ex3.Visibility = Visibility.Collapsed;
            ex4.Visibility = Visibility.Collapsed;
            ex4.Visibility = Visibility.Collapsed;
            ex5.Visibility = Visibility.Collapsed;
            ex6.Visibility = Visibility.Collapsed;
            ex7.Visibility = Visibility.Collapsed;
            ex8.Visibility = Visibility.Collapsed;
            ex9.Visibility = Visibility.Collapsed;
            ex10.Visibility = Visibility.Collapsed;
            ex11.Visibility = Visibility.Collapsed;
            ex12.Visibility = Visibility.Collapsed;
            ex13.Visibility = Visibility.Collapsed;
            ex14.Visibility = Visibility.Collapsed;
        }
        // Выход из программы
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                var result = MessageBox.Show("Вы действительно хотите выйти из программы?", "Уведомление", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                    Close();
            }
        }


        #region Задание 1
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            HiddenContainer();
            ex1.Visibility = Visibility.Visible;

            listCompany1.Items.Clear();

            foreach (var company in databaseContext.Companies.ToList())
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = company.Name;
                comboBoxItem.Tag = company.Id;

                listCompany1.Items.Add(comboBoxItem);
            }

        }

        private void listCompany1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listCompany1.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var facilityIds = databaseContext.Facilities
                .Where(f => f.CompanyId == selectedCompanyId)
                .Select(f => f.Id)
                .ToList();

            var selectedCars = from avto in databaseContext.Avtos
                               join status in databaseContext.StatusAvtos on avto.StatusId equals status.Id
                               join type in databaseContext.TypeAvtos on avto.TypeAvtoId equals type.Id
                               join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                               join facility in databaseContext.Facilities on avto.FacilityId equals facility.Id
                               where facilityIds.Contains(facility.Id)
                               select new
                               {
                                   Статус__авто = status.Name,
                                   Тип__авто = type.Name,
                                   Марка__авто = marka.Marka,
                                   Модель__авто = marka.Model,
                                   Объект__гаражного__хозяйства = facility.Name,
                                   Цвет = avto.Color,
                                   Вес__кг = avto.Weight,
                                   Вместительность__человек = avto.PeopleCapacity,
                                   Грузоподъемность__кг = avto.LiftingCapacity,
                                   Мощность__л__с = avto.Horsepower,
                                   Дата__покупки = avto.ReceptionDateTime,
                                   Дата__списания = avto.DisposalDateTime
                               };

            datagrid1.ItemsSource = selectedCars.ToList();
        }
        #endregion

        #region Задание 2
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            HiddenContainer();
            ex2.Visibility = Visibility.Visible;

            textBlockCountPeople2.Text = string.Empty;

            // listCompany2
            listCompany2.Items.Clear();
            foreach (var company in databaseContext.Companies.ToList())
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = company.Name;
                comboBoxItem.Tag = company.Id;

                listCompany2.Items.Add(comboBoxItem);
            }

            // listAvto2
            listAvto2.Items.Clear();

            var selectedCars = from avto in databaseContext.Avtos
                               join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                               join facility in databaseContext.Facilities on avto.FacilityId equals facility.Id
                               select new
                               {
                                   Id = avto.Id,
                                   Marka = marka.Marka,
                                   Facility = facility.Name,
                                   ReceptionDateTime = avto.ReceptionDateTime
                               };

            foreach (var avto in selectedCars)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = $"{avto.Marka} - {avto.Facility} - Дата приёмка: {avto.ReceptionDateTime}";
                comboBoxItem.Tag = avto.Id;

                listAvto2.Items.Add(comboBoxItem);
            }
        }

        private void listCompany2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listAvto2.SelectedIndex = -1;

            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listCompany2.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var BrigadeIds = databaseContext.Brigades
                .Where(f => f.CompanyId == selectedCompanyId)
                .Select(f => f.Id)
                .ToList();

            var selectedEmployes = from employee in databaseContext.Employees
                                   join typeEmployee in databaseContext.TypeEmployees on employee.TypeEmployeeId equals typeEmployee.Id
                                   join brigade in databaseContext.Brigades on employee.BrigadeId equals brigade.Id
                                   where (BrigadeIds.Contains(brigade.Id) && typeEmployee.Name == "Водитель")
                                   select new
                                   {
                                       Бригада = brigade.Name,
                                       Тип__сотрудника = typeEmployee.Name,
                                       Имя = employee.Name,
                                       Фамилия = employee.Surname,
                                       Отчество = employee.Patronymic,
                                       Возраст__лет = employee.Age,
                                       Телефон = employee.Phone,
                                       Опыт__лет = employee.Experience,
                                   };

            datagrid2.ItemsSource = selectedEmployes.ToList();

            textBlockCountPeople2.Text = datagrid2.Items.Count.ToString();
        }

        private void listAvto2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listCompany2.SelectedIndex = -1;

            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listAvto2.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedAvtoId = (int)selectedComboBoxItem.Tag;

            var selectedEmployes = from employee in databaseContext.Employees
                                   join typeEmployee in databaseContext.TypeEmployees on employee.TypeEmployeeId equals typeEmployee.Id
                                   join brigade in databaseContext.Brigades on employee.BrigadeId equals brigade.Id
                                   where (selectedAvtoId == employee.AvtoId && typeEmployee.Name == "Водитель")
                                   select new
                                   {
                                       Бригада = brigade.Name,
                                       Тип__сотрудника = typeEmployee.Name,
                                       Имя = employee.Name,
                                       Фамилия = employee.Surname,
                                       Отчество = employee.Patronymic,
                                       Возраст__лет = employee.Age,
                                       Телефон = employee.Phone,
                                       Опыт__лет = employee.Experience,
                                   };

            datagrid2.ItemsSource = selectedEmployes.ToList();

            textBlockCountPeople2.Text = datagrid2.Items.Count.ToString();
        }

        #endregion

        #region Задание 3
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            HiddenContainer();
            ex3.Visibility = Visibility.Visible;
            datagrid3.ItemsSource = null;

            var driverDistribution = from avto in databaseContext.Avtos
                                     join type in databaseContext.TypeAvtos on avto.TypeAvtoId equals type.Id
                                     join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                                     join driver in databaseContext.Employees on avto.Id equals driver.AvtoId into driversGroup
                                     select new
                                     {
                                         Автомобиль = $"{marka.Marka} {marka.Model}",
                                         Дата__прихода = avto.ReceptionDateTime,
                                         Водители = string.Join("\n", driversGroup.Select(d => $"{d.Name} {d.Surname} (Возраст в годах: {d.Age}) | " +
                                         $"Опыт в годах: ({d.Experience})"))
                                         //Водители = driversGroup.ToList()
                                     };

            // Привязка данных к DataGrid
            datagrid3.ItemsSource = driverDistribution.ToList();

        }
        #endregion

        #region Задание 4
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            HiddenContainer();
            ex4.Visibility = Visibility.Visible;
            datagrid4.ItemsSource = null;

            var result = from drive in databaseContext.Drives
                         join avto in databaseContext.Avtos on drive.AvtoId equals avto.Id
                         join typeAvto in databaseContext.TypeAvtos on avto.TypeAvtoId equals typeAvto.Id
                         join statusAvto in databaseContext.StatusAvtos on avto.StatusId equals statusAvto.Id
                         join markaAvto in databaseContext.MarkaAvtos on avto.MarkaId equals markaAvto.Id
                         join routes in databaseContext.Routes on drive.RouteId equals routes.Id
                         where drive.TypeDriveId == 1
                         group new { drive, avto, routes, typeAvto, statusAvto, markaAvto } by routes.Name into g
                         select new
                         {
                             Маршрут = g.Key,
                             Общее__количество__поездок = g.Count(),
                             Общая__дистанция = g.Sum(x => x.drive.Distance),
                             Средний__расход__топлива = g.Average(x => x.drive.FuelConsumption),
                             Время__завершения__последней__поездка = g.Max(x => x.drive.FinishDrive),
                             Автомобили = string.Join("\n\n", g.Select(x => $"{x.markaAvto.Marka} {x.markaAvto.Model} " +
                             $"({x.statusAvto.Name})\n" +
                             $"Дата принятия: {x.avto.ReceptionDateTime}.\n" +
                             $"Количество пассажиров: {x.drive.Сapacity}.\n"))
                         };


            datagrid4.ItemsSource = result.ToList();
        }
        #endregion

        #region Задание 5
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            HiddenContainer();
            ex5.Visibility = Visibility.Visible;

            listAvto5.Items.Clear();
            listCategoryAvto5.Items.Clear();
            datagrid5.ItemsSource = null;


            var result = from drive in databaseContext.Drives
                         join avto in databaseContext.Avtos on drive.AvtoId equals avto.Id
                         join typeAvto in databaseContext.TypeAvtos on avto.TypeAvtoId equals typeAvto.Id
                         group typeAvto by typeAvto.Id into g
                         select new
                         {
                             Id = g.Key,
                             Name = $"{g.Select(x => x.Name).Distinct().FirstOrDefault()}"
                         };

            foreach (var avto in result)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = avto.Name;
                comboBoxItem.Tag = avto.Id;

                listCategoryAvto5.Items.Add(comboBoxItem);
            }

            result = from drive in databaseContext.Drives
                     join avto in databaseContext.Avtos on drive.AvtoId equals avto.Id
                     join typeAvto in databaseContext.TypeAvtos on avto.TypeAvtoId equals typeAvto.Id
                     join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                     group new { avto, marka } by drive.AvtoId into g
                     select new
                     {
                         Id = g.Key.Value,
                         Name = $"{g.Select(x => $"{x.marka.Marka} {x.marka.Model}").FirstOrDefault()} | {g.Select(x => x.avto.ReceptionDateTime).FirstOrDefault()}"
                     };

            foreach (var avto in result)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = avto.Name;
                comboBoxItem.Tag = avto.Id;

                listAvto5.Items.Add(comboBoxItem);
            }
        }

        private void listAvto5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateTimebefore5.SelectedDate.HasValue == false ||
                dateTimeafter5.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Пожалуйста укажите диапазон времени для запроса.", "Уведомление", MessageBoxButton.OK);
                return;
            }

            DateTime? receptionDateTime = dateTimebefore5.SelectedDate; // входные данные даты и времени приема
            DateTime? disposalDateTime = dateTimeafter5.SelectedDate;

            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listAvto5.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var result = from drive in databaseContext.Drives
                         join avto in databaseContext.Avtos on drive.AvtoId equals avto.Id
                         join typeAvto in databaseContext.TypeAvtos on avto.TypeAvtoId equals typeAvto.Id
                         join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                         join status in databaseContext.StatusAvtos on avto.StatusId equals status.Id
                         where (drive.AvtoId == selectedCompanyId &&
                         receptionDateTime <= drive.StartDrive &&
                         disposalDateTime >= drive.StartDrive)
                         group new { drive, marka, status, typeAvto } by drive.AvtoId into g
                         select new
                         {
                             Общая__дистанция = g.Sum(x => x.drive.Distance),
                         };

            datagrid5.ItemsSource = result.ToList();
        }

        private void listCategoryAvto5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateTimebefore5.SelectedDate.HasValue == false ||
                dateTimeafter5.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Пожалуйста укажите диапазон времени для запроса.", "Уведомление", MessageBoxButton.OK);
                return;
            }

            DateTime? receptionDateTime = dateTimebefore5.SelectedDate; // входные данные даты и времени приема
            DateTime? disposalDateTime = dateTimeafter5.SelectedDate;

            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listCategoryAvto5.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var result = from drive in databaseContext.Drives
                         join avto in databaseContext.Avtos on drive.AvtoId equals avto.Id
                         join typeAvto in databaseContext.TypeAvtos on avto.TypeAvtoId equals typeAvto.Id
                         join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                         join status in databaseContext.StatusAvtos on avto.StatusId equals status.Id
                         where (typeAvto.Id == selectedCompanyId &&
                         receptionDateTime <= drive.StartDrive &&
                         disposalDateTime >= drive.StartDrive)
                         group new { drive, marka, status, typeAvto } by typeAvto.Id into g
                         select new
                         {
                             Общая__дистанция = g.Sum(x => x.drive.Distance),
                         };

            datagrid5.ItemsSource = result.ToList();
        }

        #endregion



        #region Задание 6
        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            HiddenContainer();
            ex6.Visibility = Visibility.Visible;

            listAvto6.Items.Clear();
            listCategoryAvto6.Items.Clear();
            listMarkaAvto6.Items.Clear();

            datagrid6.ItemsSource = null;


            var result = from repair in databaseContext.Repairs
                         join avto in databaseContext.Avtos on repair.AvtoId equals avto.Id
                         join typeAvto in databaseContext.TypeAvtos on avto.TypeAvtoId equals typeAvto.Id
                         group typeAvto by typeAvto.Id into g
                         select new
                         {
                             Id = g.Key,
                             Name = $"{g.Select(x => x.Name).Distinct().FirstOrDefault()}"
                         };

            foreach (var avto in result)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = avto.Name;
                comboBoxItem.Tag = avto.Id;

                listCategoryAvto6.Items.Add(comboBoxItem);
            }

            result = from repair in databaseContext.Repairs
                     join avto in databaseContext.Avtos on repair.AvtoId equals avto.Id
                     join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                     group marka by marka.Id into g
                     select new
                     {
                         Id = g.Key,
                         Name = $"{g.Select(x => x.Marka).Distinct().FirstOrDefault()} {g.Select(x => x.Model).Distinct().FirstOrDefault()}"
                         //Name = $"{marka.Marka} {marka.Model}"
                     };

            foreach (var avto in result)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = avto.Name;
                comboBoxItem.Tag = avto.Id;

                listMarkaAvto6.Items.Add(comboBoxItem);
            }

            result = from repair in databaseContext.Repairs
                     join avto in databaseContext.Avtos on repair.AvtoId equals avto.Id
                     join typeAvto in databaseContext.TypeAvtos on avto.TypeAvtoId equals typeAvto.Id
                     join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                     group new { avto, marka } by repair.AvtoId into g
                     select new
                     {
                         Id = g.Key.Value,
                         Name = $"{g.Select(x => $"{x.marka.Marka} {x.marka.Model}").FirstOrDefault()} | {g.Select(x => x.avto.ReceptionDateTime).FirstOrDefault()}"
                     };

            foreach (var avto in result)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = avto.Name;
                comboBoxItem.Tag = avto.Id;

                listAvto6.Items.Add(comboBoxItem);
            }

        }
        private void listCategoryAvto6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dateTimebefore6.SelectedDate.HasValue == false ||
                dateTimeafter6.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Пожалуйста укажите диапазон времени для запроса.", "Уведомление", MessageBoxButton.OK);
                return;
            }

            DateTime? receptionDateTime = dateTimebefore6.SelectedDate; // входные данные даты и времени приема
            DateTime? disposalDateTime = dateTimeafter6.SelectedDate;

            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listCategoryAvto6.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var result = from repair in databaseContext.Repairs
                         join avto in databaseContext.Avtos on repair.AvtoId equals avto.Id
                         join typeAvto in databaseContext.TypeAvtos on avto.TypeAvtoId equals typeAvto.Id
                         join nodeToRepairs in databaseContext.NodeToRepairs on repair.NodeId equals nodeToRepairs.Id
                         where (typeAvto.Id == selectedCompanyId &&
                         receptionDateTime <= repair.StartDateTime &&
                         disposalDateTime >= repair.StartDateTime)
                         group new { repair, typeAvto, nodeToRepairs } by typeAvto.Id into g
                         select new
                         {
                             Количество__ремонтов = g.Count(),
                             Общая__сумма__ремонта = g.Sum(x => x.nodeToRepairs.Cost),
                         };

            datagrid6.ItemsSource = result.ToList();
        }
        private void listMarkaAvto6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dateTimebefore6.SelectedDate.HasValue == false ||
                dateTimeafter6.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Пожалуйста укажите диапазон времени для запроса.", "Уведомление", MessageBoxButton.OK);
                return;
            }

            DateTime? receptionDateTime = dateTimebefore6.SelectedDate; // входные данные даты и времени приема
            DateTime? disposalDateTime = dateTimeafter6.SelectedDate;

            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listMarkaAvto6.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var result = from repair in databaseContext.Repairs
                         join avto in databaseContext.Avtos on repair.AvtoId equals avto.Id
                         join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                         join nodeToRepairs in databaseContext.NodeToRepairs on repair.NodeId equals nodeToRepairs.Id
                         where (marka.Id == selectedCompanyId &&
                         receptionDateTime <= repair.StartDateTime &&
                         disposalDateTime >= repair.StartDateTime)
                         group new { repair, marka, nodeToRepairs } by marka.Id into g
                         select new
                         {
                             Количество__ремонтов = g.Count(),
                             Общая__сумма__ремонта = g.Sum(x => x.nodeToRepairs.Cost),
                         };

            datagrid6.ItemsSource = result.ToList();
        }
        private void listAvto6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dateTimebefore6.SelectedDate.HasValue == false ||
                dateTimeafter6.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Пожалуйста укажите диапазон времени для запроса.", "Уведомление", MessageBoxButton.OK);
                return;
            }

            DateTime? receptionDateTime = dateTimebefore6.SelectedDate; // входные данные даты и времени приема
            DateTime? disposalDateTime = dateTimeafter6.SelectedDate;

            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listAvto6.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var result = from repair in databaseContext.Repairs
                         join avto in databaseContext.Avtos on repair.AvtoId equals avto.Id
                         join marka in databaseContext.MarkaAvtos on avto.TypeAvtoId equals marka.Id
                         join nodeToRepairs in databaseContext.NodeToRepairs on repair.NodeId equals nodeToRepairs.Id
                         where (avto.Id == selectedCompanyId &&
                         receptionDateTime <= repair.StartDateTime &&
                         disposalDateTime >= repair.StartDateTime)
                         group new { repair, marka, nodeToRepairs } by avto.Id into g
                         select new
                         {
                             Количество__ремонтов = g.Count(),
                             Общая__сумма__ремонта = g.Sum(x => x.nodeToRepairs.Cost),
                         };

            datagrid6.ItemsSource = result.ToList();
        }

        #endregion

        #region Задание 7
        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            HiddenContainer();
            ex7.Visibility = Visibility.Visible;

            listCompany7.Items.Clear();

            foreach (var company in databaseContext.Companies.ToList())
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = company.Name;
                comboBoxItem.Tag = company.Id;

                listCompany7.Items.Add(comboBoxItem);
            }


        }

        private void listCompany7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listCompany7.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var personnelData = from brigadier in databaseContext.Brigadiers
                                join brigade in databaseContext.Brigades on brigadier.BrigadeId equals brigade.Id
                                join master in databaseContext.Masters on brigadier.MasterId equals master.Id
                                join sectionChief in databaseContext.NachalnikUchastkas on master.NachalnikUchastkaId equals sectionChief.Id
                                join workshopChief in databaseContext.NachalnikTsekhas on sectionChief.NachalnikTsekhaId equals workshopChief.Id
                                join employee in databaseContext.Employees on brigade.Id equals employee.BrigadeId
                                join typeEmployee in databaseContext.TypeEmployees on employee.TypeEmployeeId equals typeEmployee.Id
                                where workshopChief.CompanyId == selectedCompanyId
                                select new
                                {
                                    Рабочий = $"{employee.Name} {employee.Surname} {employee.Patronymic} ({typeEmployee.Name})",
                                    Бригадир = $"{brigadier.Name} {brigadier.Surname} {brigadier.Patronymic} ({brigadier.Phone})",
                                    Мастер = $"{master.Name} {master.Surname} {master.Patronymic} ({master.Phone})",
                                    Начальник__Участка = $"{sectionChief.Name} {sectionChief.Surname} {sectionChief.Patronymic} ({sectionChief.Phone})",
                                    Начальник__Цеха = $"{workshopChief.Name} {workshopChief.Surname} {workshopChief.Patronymic} ({workshopChief.Phone})"
                                };

            datagrid7.ItemsSource = personnelData.ToList();
        }
        #endregion

        
        // 8
        #region Задание 8
        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            HiddenContainer();
            ex8.Visibility = Visibility.Visible;

            listCompany8.Items.Clear();

            foreach (var company in databaseContext.Companies.ToList())
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = company.Name;
                comboBoxItem.Tag = company.Id;

                listCompany8.Items.Add(comboBoxItem);
            }
        }

        private void listCompany8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listCompany8.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var result = (from facility in databaseContext.Facilities
                          join avto in databaseContext.Avtos on facility.Id equals avto.FacilityId
                          join typeAvto in databaseContext.TypeAvtos on avto.TypeAvtoId equals typeAvto.Id
                          where facility.CompanyId == selectedCompanyId
                          group facility by typeAvto.Name into g
                          select new
                          {
                                Категория__транспорта = g.Key,    
                                Количество__объектов = g.Count(),                             
                                Объекты__гаражного__хозяйства = string.Join("\n\n", g.Select(x => $"Название {x.Name}\n" +
                                $"Адрес: {x.Address}\n" +
                                $"Площадь: {x.Square}\n" +
                                $"Телефон: {x.Phone}\n"))
                          }
              ).ToList();

            datagrid8.ItemsSource = result;
        }

        #endregion

        // 9
        #region Задание 9
        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            HiddenContainer();
            ex9.Visibility = Visibility.Visible;

            listCompany9.Items.Clear();

            foreach (var company in databaseContext.Companies.ToList())
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = company.Name;
                comboBoxItem.Tag = company.Id;

                listCompany9.Items.Add(comboBoxItem);
            }
        }
        private void listCompany9_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listCompany9.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var facilityIds = databaseContext.Facilities
                .Where(f => f.CompanyId == selectedCompanyId)
                .Select(f => f.Id)
                .ToList();

            var selectedCars = from facility in databaseContext.Facilities
                               join avtos in databaseContext.Avtos on facility.Id equals avtos.FacilityId into AvtosGroup
                               where facilityIds.Contains(facility.Id)
                               select new
                               {
                                   Обьъект__гаражного__хозяйства = facility.Name,
                                   Авто = from avto in AvtosGroup
                                          join status in databaseContext.StatusAvtos on avto.StatusId equals status.Id
                                          join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                                          select new
                                          {
                                              Статус = status.Name,
                                              Марка = marka.Marka,
                                              Модель = marka.Model
                                          }
                               };


            var dataGridItems = new ObservableCollection<dynamic>(selectedCars);

            datagrid9.ItemsSource = dataGridItems;

            //datagrid9.ItemsSource = selectedCars.ToList();
        }
        #endregion

        #region Задание 10
        private void Button10_Click(object sender, RoutedEventArgs e)
        {
            HiddenContainer();
            ex10.Visibility = Visibility.Visible;

            listAvto10.Items.Clear();

            var result = from avto in databaseContext.Avtos
                         join markaAvto in databaseContext.MarkaAvtos on avto.MarkaId equals markaAvto.Id
                         join statusAvto in databaseContext.StatusAvtos on avto.StatusId equals statusAvto.Id
                         join typeAvto in databaseContext.TypeEmployees on avto.TypeAvtoId equals typeAvto.Id
                         //where typeAvto.Name == "Грузовой транспорт" // id == 5
                         where typeAvto.Id == 5
                         select new
                         {
                             Id = avto.Id,
                             Name = $"{markaAvto.Marka} {markaAvto.Model} ({statusAvto.Name}) " +
                             $"| Дата принятия: {avto.ReceptionDateTime}."
                         };

            foreach (var avto in result)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = avto.Name;
                comboBoxItem.Tag = avto.Id;

                listAvto10.Items.Add(comboBoxItem);
            }
        }

        private void listCompany10_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateTimebefore10.SelectedDate.HasValue == false ||
                dateTimeafter10.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Пожалуйста укажите диапазон времени для запроса.", "Уведомление", MessageBoxButton.OK);
                return;
            }

            DateTime? receptionDateTime = dateTimebefore10.SelectedDate; // входные данные даты и времени приема
            DateTime? disposalDateTime = dateTimeafter10.SelectedDate;

            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listAvto10.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var personnelData = from drive in databaseContext.Drives
                                join route in databaseContext.Routes on drive.RouteId equals route.Id
                                join avto in databaseContext.Avtos on drive.AvtoId equals avto.Id 
                                where (drive.AvtoId == selectedCompanyId &&
                                    drive.StartDrive >= receptionDateTime &&
                                    drive.StartDrive <= disposalDateTime)
                                select new
                                {
                                    Маршрут = route.Name,
                                    Начальная__точка = route.StartPoint,
                                    Конечная__точка = route.EndPoint,
                                    Протяженность__км = drive.Distance,
                                    Потрачено__топлива__литр = drive.FuelConsumption, 
                                    Вес__груза = drive.CargoWeight
                                };

            datagrid10.ItemsSource = personnelData.ToList();
        }
        #endregion      
        
        // 12
        #region Задание 12

        private void Button12_Click(object sender, RoutedEventArgs e)
        {
            HiddenContainer();
            ex12.Visibility = Visibility.Visible;

            datagrid12.ItemsSource = null;
        }

        private void radioButton121_Checked(object sender, RoutedEventArgs e)
        {
            if (dateTimebefore12.SelectedDate.HasValue == false ||
                dateTimeafter12.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Пожалуйста укажите диапазон времени для запроса.", "Уведомление", MessageBoxButton.OK);
                return;
            }

            DateTime? receptionDateTime = dateTimebefore12.SelectedDate; // входные данные даты и времени приема
            DateTime? disposalDateTime = dateTimeafter12.SelectedDate; // входные данные даты и времени списания

            var scrappedCars = from avto in databaseContext.Avtos
                               join status in databaseContext.StatusAvtos on avto.StatusId equals status.Id
                               join type in databaseContext.TypeAvtos on avto.TypeAvtoId equals type.Id
                               join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                               where (avto.StatusId != 5 && avto.ReceptionDateTime >= receptionDateTime
                                    && avto.ReceptionDateTime <= disposalDateTime)
                               select new
                               {
                                   Тип__авто = type.Name,
                                   Марка__авто = marka.Marka,
                                   Модель__авто = marka.Model,
                                   Цвет = avto.Color,
                                   Вес__кг = avto.Weight,
                                   Вместительность__человек = avto.PeopleCapacity,
                                   Грузоподъемность__кг = avto.LiftingCapacity,
                                   Мощность__л__с = avto.Horsepower
                               };

            datagrid12.ItemsSource = scrappedCars.ToList();
        }

        private void radioButton122_Checked(object sender, RoutedEventArgs e)
        {
            if (dateTimebefore12.SelectedDate.HasValue == false ||
                dateTimeafter12.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Пожалуйста укажите диапазон времени для запроса.", "Уведомление", MessageBoxButton.OK);
                return;
            }

            DateTime? receptionDateTime = dateTimebefore12.SelectedDate; // входные данные даты и времени приема
            DateTime? disposalDateTime = dateTimeafter12.SelectedDate; // входные данные даты и времени списания

            var scrappedCars = from avto in databaseContext.Avtos
                               join status in databaseContext.StatusAvtos on avto.StatusId equals status.Id
                               join type in databaseContext.TypeAvtos on avto.TypeAvtoId equals type.Id
                               join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                               where (avto.StatusId == 5 && avto.DisposalDateTime >= receptionDateTime
                                    && avto.DisposalDateTime <= disposalDateTime)
                               select new
                               {
                                   Тип__авто = type.Name,
                                   Марка__авто = marka.Marka,
                                   Модель__авто = marka.Model,
                                   Цвет = avto.Color,
                                   Вес__кг = avto.Weight,
                                   Вместительность__человек = avto.PeopleCapacity,
                                   Грузоподъемность__кг = avto.LiftingCapacity,
                                   Мощность__л__с = avto.Horsepower
                               };

            datagrid12.ItemsSource = scrappedCars.ToList();

        }
        #endregion

        // 13
        #region Задание 13

        private int option13 = 0;
        private void Button13_Click(object sender, RoutedEventArgs e)
        {
            HiddenContainer();
            ex13.Visibility = Visibility.Visible;

            listPeopole13.Items.Clear();
            option13 = 0;
        }

        private void RadioButton13_Checked(object sender, RoutedEventArgs e)
        {
            listPeopole13.Items.Clear();

            RadioButton radioButton = (RadioButton)sender;


            if (radioButton.IsChecked == true)
            {

                string option = radioButton.Content.ToString();

                if (option == "Бригадир")
                {
                    option13 = 1;

                    var workshopChiefEmployees = from brigadiers in databaseContext.Brigadiers
                                                     //join brig in databaseContext.Brigades on brigadiers.BrigadeId equals brig.Id
                                                     //join employee in databaseContext.Employees on brig.Id equals employee.Id
                                                 select new
                                                 {
                                                     Id = brigadiers.Id,
                                                     Имя = brigadiers.Name,
                                                     Фамилия = brigadiers.Surname,
                                                     Отчество = brigadiers.Patronymic
                                                 };

                    foreach (var item in workshopChiefEmployees)
                    {
                        ComboBoxItem comboBoxItem = new ComboBoxItem();
                        comboBoxItem.Content = $"{item.Имя} {item.Фамилия} {item.Отчество}";
                        comboBoxItem.Tag = item.Id;

                        listPeopole13.Items.Add(comboBoxItem);
                    }
                }
                else if (option == "Мастер")
                {
                    option13 = 2;

                    var workshopChiefEmployees = from master in databaseContext.Masters
                                                 select new
                                                 {
                                                     Id = master.Id,
                                                     Имя = master.Name,
                                                     Фамилия = master.Surname,
                                                     Отчество = master.Patronymic
                                                 };

                    foreach (var item in workshopChiefEmployees)
                    {
                        ComboBoxItem comboBoxItem = new ComboBoxItem();
                        comboBoxItem.Content = $"{item.Имя} {item.Фамилия} {item.Отчество}";
                        comboBoxItem.Tag = item.Id;

                        listPeopole13.Items.Add(comboBoxItem);
                    }
                }
                else if (option == "Начальник участка")
                {
                    option13 = 3;

                    var workshopChiefEmployees = from nachalnikUchastka in databaseContext.NachalnikUchastkas
                                                 select new
                                                 {
                                                     Id = nachalnikUchastka.Id,
                                                     Имя = nachalnikUchastka.Name,
                                                     Фамилия = nachalnikUchastka.Surname,
                                                     Отчество = nachalnikUchastka.Patronymic
                                                 };


                    foreach (var item in workshopChiefEmployees)
                    {
                        ComboBoxItem comboBoxItem = new ComboBoxItem();
                        comboBoxItem.Content = $"{item.Имя} {item.Фамилия} {item.Отчество}";
                        comboBoxItem.Tag = item.Id;

                        listPeopole13.Items.Add(comboBoxItem);
                    }
                }
                else if (option == "Начальник цеха")
                {
                    option13 = 4;

                    var workshopChiefEmployees = from nachalnik_Tsekha in databaseContext.NachalnikTsekhas
                                                 select new
                                                 {
                                                     Id = nachalnik_Tsekha.Id,
                                                     Имя = nachalnik_Tsekha.Name,
                                                     Фамилия = nachalnik_Tsekha.Surname,
                                                     Отчество = nachalnik_Tsekha.Patronymic
                                                 };

                    foreach (var item in workshopChiefEmployees)
                    {
                        ComboBoxItem comboBoxItem = new ComboBoxItem();
                        comboBoxItem.Content = $"{item.Имя} {item.Фамилия} {item.Отчество}";
                        comboBoxItem.Tag = item.Id;

                        listPeopole13.Items.Add(comboBoxItem);
                    }
                }

            }
        }

        private void listCompany13_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listPeopole13.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            if (option13 == 1)
            {
                var BrigadeId = from brigadiers in databaseContext.Brigadiers
                                where brigadiers.Id == selectedCompanyId
                                select new
                                {
                                    BrigadeId = brigadiers.BrigadeId
                                };
                var brigadeId = BrigadeId.FirstOrDefault()?.BrigadeId ?? 0;

                var workshopChiefEmployees = from employee in databaseContext.Employees
                                             join typeemployee in databaseContext.TypeEmployees on employee.TypeEmployeeId equals typeemployee.Id
                                             where employee.BrigadeId == brigadeId
                                             select new
                                             {
                                                 Должность = typeemployee.Name,
                                                 Имя = employee.Name,
                                                 Фамилия = employee.Surname,
                                                 Отчество = employee.Patronymic,
                                                 Возраст = employee.Age,
                                                 Телефон = employee.Phone,
                                                 Стаж__лет = employee.Experience,
                                             };

                datagrid13.ItemsSource = workshopChiefEmployees.ToList();
            }
            else if (option13 == 2)
            {
                var workshopChiefEmployees = from brigadiers in databaseContext.Brigadiers
                                             where brigadiers.MasterId == selectedCompanyId
                                             select new
                                             {
                                                 Имя = brigadiers.Name,
                                                 Фамилия = brigadiers.Surname,
                                                 Отчество = brigadiers.Patronymic,
                                                 Возраст = brigadiers.Age,
                                                 Телефон = brigadiers.Phone
                                             };

                datagrid13.ItemsSource = workshopChiefEmployees.ToList();
            }
            else if (option13 == 3)
            {
                var workshopChiefEmployees = from master in databaseContext.Masters
                                             where master.NachalnikUchastkaId == selectedCompanyId
                                             select new
                                             {
                                                 Имя = master.Name,
                                                 Фамилия = master.Surname,
                                                 Отчество = master.Patronymic,
                                                 Возраст = master.Age,
                                                 Телефон = master.Phone
                                             };

                datagrid13.ItemsSource = workshopChiefEmployees.ToList();
            }
            else if (option13 == 4)
            {
                var workshopChiefEmployees = from nachalnikUchastkas in databaseContext.NachalnikUchastkas
                                             where nachalnikUchastkas.NachalnikTsekhaId == selectedCompanyId
                                             select new
                                             {
                                                 Имя = nachalnikUchastkas.Name,
                                                 Фамилия = nachalnikUchastkas.Surname,
                                                 Отчество = nachalnikUchastkas.Patronymic,
                                                 Возраст = nachalnikUchastkas.Age,
                                                 Телефон = nachalnikUchastkas.Phone
                                             };

                datagrid13.ItemsSource = workshopChiefEmployees.ToList();
            }
        }

        #endregion


        #region Задание 11

        private void Button11_Click(object sender, RoutedEventArgs e)
        {
            HiddenContainer();
            ex11.Visibility = Visibility.Visible;

            listAvto11.Items.Clear();
            listCategoryAvto11.Items.Clear();
            listMarkaAvto11.Items.Clear();

            datagrid11.ItemsSource = null;


            var result = from repair in databaseContext.Repairs
                         join avto in databaseContext.Avtos on repair.AvtoId equals avto.Id
                         join typeAvto in databaseContext.TypeAvtos on avto.TypeAvtoId equals typeAvto.Id
                         group typeAvto by typeAvto.Id into g
                         select new
                         {
                             Id = g.Key,
                             Name = $"{g.Select(x => x.Name).Distinct().FirstOrDefault()}"
                         };

            foreach (var avto in result)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = avto.Name;
                comboBoxItem.Tag = avto.Id;

                listCategoryAvto11.Items.Add(comboBoxItem);
            }

            result = from repair in databaseContext.Repairs
                     join avto in databaseContext.Avtos on repair.AvtoId equals avto.Id
                     join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                     group marka by marka.Id into g
                     select new
                     {
                         Id = g.Key,
                         Name = $"{g.Select(x => x.Marka).Distinct().FirstOrDefault()} {g.Select(x => x.Model).Distinct().FirstOrDefault()}"
                         //Name = $"{marka.Marka} {marka.Model}"
                     };

            foreach (var avto in result)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = avto.Name;
                comboBoxItem.Tag = avto.Id;

                listMarkaAvto11.Items.Add(comboBoxItem);
            }

            result = from repair in databaseContext.Repairs
                     join avto in databaseContext.Avtos on repair.AvtoId equals avto.Id
                     join typeAvto in databaseContext.TypeAvtos on avto.TypeAvtoId equals typeAvto.Id
                     join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                     group new { avto, marka } by repair.AvtoId into g
                     select new
                     {
                         Id = g.Key.Value,
                         Name = $"{g.Select(x => $"{x.marka.Marka} {x.marka.Model}").FirstOrDefault()} | {g.Select(x => x.avto.ReceptionDateTime).FirstOrDefault()}"
                     };

            foreach (var avto in result)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = avto.Name;
                comboBoxItem.Tag = avto.Id;

                listAvto11.Items.Add(comboBoxItem);
            }

        }
        private void listCategoryAvto11_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dateTimebefore11.SelectedDate.HasValue == false ||
                dateTimeafter11.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Пожалуйста укажите диапазон времени для запроса.", "Уведомление", MessageBoxButton.OK);
                return;
            }

            DateTime? receptionDateTime = dateTimebefore11.SelectedDate; // входные данные даты и времени приема
            DateTime? disposalDateTime = dateTimeafter11.SelectedDate;

            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listCategoryAvto11.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var result = from repair in databaseContext.Repairs
                         join avto in databaseContext.Avtos on repair.AvtoId equals avto.Id
                         join typeAvto in databaseContext.TypeAvtos on avto.TypeAvtoId equals typeAvto.Id
                         join nodeToRepairs in databaseContext.NodeToRepairs on repair.NodeId equals nodeToRepairs.Id
                         where (typeAvto.Id == selectedCompanyId &&
                         receptionDateTime <= repair.StartDateTime &&
                         disposalDateTime >= repair.StartDateTime)
                         group new { repair, typeAvto, nodeToRepairs } by nodeToRepairs.Name into g
                         select new
                         {
                             Название__узл = g.Key,
                             Число__использованных__для__ремонта__указанных__узлов = g.Sum(x => x.repair.NumOfRepairedNodes),
                         };

            datagrid11.ItemsSource = result.ToList();
        }
        private void listMarkaAvto11_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dateTimebefore11.SelectedDate.HasValue == false ||
                dateTimeafter11.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Пожалуйста укажите диапазон времени для запроса.", "Уведомление", MessageBoxButton.OK);
                return;
            }

            DateTime? receptionDateTime = dateTimebefore11.SelectedDate; // входные данные даты и времени приема
            DateTime? disposalDateTime = dateTimeafter11.SelectedDate;

            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listMarkaAvto11.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var result = from repair in databaseContext.Repairs
                         join avto in databaseContext.Avtos on repair.AvtoId equals avto.Id
                         join marka in databaseContext.MarkaAvtos on avto.MarkaId equals marka.Id
                         join nodeToRepairs in databaseContext.NodeToRepairs on repair.NodeId equals nodeToRepairs.Id
                         where (marka.Id == selectedCompanyId &&
                         receptionDateTime <= repair.StartDateTime &&
                         disposalDateTime >= repair.StartDateTime)
                         group new { repair, marka, nodeToRepairs } by nodeToRepairs.Name into g
                         select new
                         {
                             Название__узл = g.Key,
                             Число__использованных__для__ремонта__указанных__узлов = g.Sum(x => x.repair.NumOfRepairedNodes),
                         };

            datagrid11.ItemsSource = result.ToList();
        }
        private void listAvto11_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dateTimebefore11.SelectedDate.HasValue == false ||
                dateTimeafter11.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Пожалуйста укажите диапазон времени для запроса.", "Уведомление", MessageBoxButton.OK);
                return;
            }

            DateTime? receptionDateTime = dateTimebefore11.SelectedDate; // входные данные даты и времени приема
            DateTime? disposalDateTime = dateTimeafter11.SelectedDate;

            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listAvto11.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var result = from repair in databaseContext.Repairs
                         join avto in databaseContext.Avtos on repair.AvtoId equals avto.Id
                         join marka in databaseContext.MarkaAvtos on avto.TypeAvtoId equals marka.Id
                         join nodeToRepairs in databaseContext.NodeToRepairs on repair.NodeId equals nodeToRepairs.Id
                         where (avto.Id == selectedCompanyId &&
                         receptionDateTime <= repair.StartDateTime &&
                         disposalDateTime >= repair.StartDateTime)
                         group new { repair, marka, nodeToRepairs } by nodeToRepairs.Name into g
                         select new
                         {
                             Название__узл = g.Key,
                             Число__использованных__для__ремонта__указанных__узлов = g.Sum(x => x.repair.NumOfRepairedNodes),
                         };

            datagrid11.ItemsSource = result.ToList();
        }


        #endregion

        #region Задание 14
        private void Button14_Click(object sender, RoutedEventArgs e)
        {
            HiddenContainer();
            ex14.Visibility = Visibility.Visible;

            listAvto14.Items.Clear();

            var selectedCars = from repair in databaseContext.Repairs
                               join avtos in databaseContext.Avtos on repair.AvtoId equals avtos.Id
                               join employee in databaseContext.Employees on repair.EmployeeId equals employee.Id
                               join typeEmployee in databaseContext.TypeEmployees on employee.TypeEmployeeId equals typeEmployee.Id
                               select new
                               {
                                   Id = employee.Id,
                                   Name = $"{employee.Name} {employee.Surname} {employee.Patronymic} ({typeEmployee.Name}) |" +
                                   $" {employee.Phone}"
                               };

            foreach (var company in selectedCars)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = company.Name;
                comboBoxItem.Tag = company.Id;

                listAvto14.Items.Add(comboBoxItem);
            }
        }

        private void listCompany14_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateTimebefore14.SelectedDate.HasValue == false ||
                dateTimeafter14.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Пожалуйста укажите диапазон времени для запроса.", "Уведомление", MessageBoxButton.OK);
                return;
            }

            DateTime? receptionDateTime = dateTimebefore14.SelectedDate;
            DateTime? disposalDateTime = dateTimeafter14.SelectedDate;

            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)listAvto14.SelectedItem;

            if (selectedComboBoxItem == null)
                return;

            var selectedCompanyId = (int)selectedComboBoxItem.Tag;

            var selectedCars = from repair in databaseContext.Repairs
                               join avtos in databaseContext.Avtos on repair.AvtoId equals avtos.Id
                               join marka in databaseContext.MarkaAvtos on avtos.MarkaId equals marka.Id
                               join nodeToRepair in databaseContext.NodeToRepairs on repair.NodeId equals nodeToRepair.Id
                               join employee in databaseContext.Employees on repair.EmployeeId equals employee.Id
                               where (repair.EmployeeId == selectedCompanyId &&
                                    repair.StartDateTime >= receptionDateTime
                                    && repair.StartDateTime <= disposalDateTime)
                               select new
                               {
                                   Ремонтируемый__узел = nodeToRepair.Name,
                                   Стоимость__узла = nodeToRepair.Cost,
                                   Количество__ремонтируемых__узлов = repair.NumOfRepairedNodes,
                                   Общая__сумма = repair.NumOfRepairedNodes * nodeToRepair.Cost,
                                   Время__начала__ремонта = repair.StartDateTime,
                                   Время__конца__ремонта = repair.EndDateTime,
                                   Марка__авто = $"{marka.Marka} {marka.Model}",
                                   Цвет__авто = avtos.Color
                               };

            datagrid14.ItemsSource = selectedCars.ToList();
        }
        #endregion



        private void Button15_Click(object sender, RoutedEventArgs e)
        {
            WindowTypeFacility window = new(databaseContext)
            {
                Owner = this
            };

            window.ShowDialog();
        }
        private void Button16_Click(object sender, RoutedEventArgs e)
        {
            WindowTypeEmployee window = new(databaseContext)
            {
                Owner = this
            };

            window.ShowDialog();
        }
        private void Button17_Click(object sender, RoutedEventArgs e)
        {
            WindowTypeDrive window = new(databaseContext)
            {
                Owner = this
            };

            window.ShowDialog();
        }
        private void Button18_Click(object sender, RoutedEventArgs e)
        {
            WindowTypeAvto window = new(databaseContext)
            {
                Owner = this
            };

            window.ShowDialog();
        }
        private void Button19_Click(object sender, RoutedEventArgs e)
        {
            WindowStatusAvto window = new(databaseContext)
            {
                Owner = this
            };

            window.ShowDialog();
        }
        private void Button20_Click(object sender, RoutedEventArgs e)
        {
            WindowMarkaAvto window = new(databaseContext)
            {
                Owner = this
            };

            window.ShowDialog();
        }
        private void Button21_Click(object sender, RoutedEventArgs e)
        {
            WindowBrigadier window = new(databaseContext)
            {
                Owner = this
            };

            window.ShowDialog();
        }
        private void Button22_Click(object sender, RoutedEventArgs e)
        {
            WindowMaster window = new(databaseContext)
            {
                Owner = this
            };

            window.ShowDialog();
        }
        private void Button23_Click(object sender, RoutedEventArgs e)
        {
            WindowNachalnikUchastka window = new(databaseContext)
            {
                Owner = this
            };

            window.ShowDialog();
        }
        private void Button24_Click(object sender, RoutedEventArgs e)
        {
            WindowNachalnikTsekha window = new(databaseContext)
            {
                Owner = this
            };

            window.ShowDialog();
        }
        private void Button25_Click(object sender, RoutedEventArgs e)
        {
            WindowAvto window = new(databaseContext)
            {
                Owner = this
            };

            window.ShowDialog();
        }
    }
}