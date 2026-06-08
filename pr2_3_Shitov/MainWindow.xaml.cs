using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using pr2_3_Shitov;
using System;
using System.Linq;

namespace pr2_3_Shitov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Shop shop;

        public MainWindow()
        {
            InitializeComponent();
            InitializeShop();

            playlist = new Playlist();
            UpdateSongList();
        }

        private void InitializeShop()
        {
            shop = new Shop();

            // Добавляем тестовые товары
            shop.CreateProduct("Кола", 85, 200);
            shop.CreateProduct("Сок \"Добрый\"", 100, 50);
            shop.CreateProduct("Чипсы Lays", 85, 150);
            shop.CreateProduct("Шоколад Alpen Gold", 75, 100);
            shop.CreateProduct("Печенье Oreo", 120, 80);

            RefreshProductList();
            UpdateProfitDisplay();
        }

        private void RefreshProductList()
        {
            var displayList = shop.GetAllProducts().Select(kvp => new
            {
                ProductName = kvp.Key.Name,
                ProductPrice = kvp.Key.Price,
                Count = kvp.Value
            }).ToList();

            dgProducts.ItemsSource = displayList;
        }

        private void UpdateProfitDisplay()
        {
            txtProfit.Text = $"Прибыль: {shop.Profit:F2} руб.";
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка названия
                if (string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    MessageBox.Show("Введите название товара!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка цены
                string priceText = txtPrice.Text.Replace('.', ',');
                if (!decimal.TryParse(priceText, out decimal price) || price <= 0)
                {
                    MessageBox.Show("Введите корректную цену (больше 0)!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка количества
                if (!int.TryParse(txtCount.Text, out int count) || count <= 0)
                {
                    MessageBox.Show("Введите корректное количество (больше 0)!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка на дубликат
                if (shop.FindByName(txtProductName.Text) != null)
                {
                    MessageBox.Show($"Товар '{txtProductName.Text}' уже существует!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string productName = txtProductName.Text;
                shop.CreateProduct(productName, price, count);
                RefreshProductList();

                // Очистка полей
                txtProductName.Clear();
                txtPrice.Text = "00,00";
                txtCount.Text = "1";

                MessageBox.Show($"Товар '{productName}' добавлен!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSell_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка названия
                if (string.IsNullOrWhiteSpace(txtSellName.Text))
                {
                    MessageBox.Show("Введите название товара!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка кол-ва
                int count = 1;
                if (!string.IsNullOrWhiteSpace(txtSellCount.Text))
                {
                    if (!int.TryParse(txtSellCount.Text, out count) || count <= 0)
                    {
                        MessageBox.Show("Введите корректное количество!", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                string productName = txtSellName.Text;

                for (int i = 0; i < count; i++)
                {
                    shop.Sell(productName);
                }

                RefreshProductList();
                UpdateProfitDisplay();

                txtSellName.Clear();
                txtSellCount.Text = "1";

                MessageBox.Show($"Продано {count} шт. '{productName}'!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Кнопка покупки товара
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                dynamic item = button?.Tag;

                if (item != null)
                {
                    shop.Sell(item.ProductName);
                    RefreshProductList();
                    UpdateProfitDisplay();

                    MessageBox.Show($"Куплен товар '{item.ProductName}'!", "Покупка",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Автоматическая замена точки на запятую
            if (txtPrice.Text.Contains('.'))
            {
                txtPrice.Text = txtPrice.Text.Replace('.', ',');
                txtPrice.CaretIndex = txtPrice.Text.Length;
            }
        }
        //ПЕРЕКЛЮЧЕНИЯ МЕЖДУ ЗАДАНИЯМИ
        private void ShowTask1(object sender, RoutedEventArgs e)
        {
            GridTask1.Visibility = Visibility.Visible;
            GridTask2.Visibility = Visibility.Collapsed;
        }

        private void ShowTask2(object sender, RoutedEventArgs e)
        {
            GridTask1.Visibility = Visibility.Collapsed;
            GridTask2.Visibility = Visibility.Visible;
        }
        private Playlist playlist;

        private void InitPlaylist()
        {
            playlist = new Playlist();
            UpdateSongList();
        }

        //Обновление списка песен
        private void UpdateSongList()
        {
            if (playlist != null)
            {
                var allSongs = playlist.GetAllSongs();
                lstSongs.ItemsSource = null;
                lstSongs.ItemsSource = allSongs;

                if (!playlist.IsEmpty())
                {
                    Song currentSong = playlist.CurrentSong();
                    lstSongs.SelectedItem = currentSong;

                    lstSongs.ScrollIntoView(currentSong);
                }
            }
        }

        //Обзор файла
        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Аудио файлы|*.mp3;*.wav;*.wma|Все файлы|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                txtFilename.Text = openFileDialog.FileName;
            }
        }

        //Добавление песни
        private void BtnAddSong_Click(object sender, RoutedEventArgs e)
        {
            if (playlist == null)
            {
                playlist = new Playlist();
            }
            if (string.IsNullOrWhiteSpace(txtAuthor.Text) ||
                string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtFilename.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!File.Exists(txtFilename.Text))
            {
                MessageBox.Show("Файл не существует!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }           
            playlist.AddSong(txtAuthor.Text, txtTitle.Text, txtFilename.Text);
            UpdateSongList();

            txtAuthor.Clear();
            txtTitle.Clear();
            txtFilename.Clear();
        }
        

        //Предыдущая песня
        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (playlist.IsEmpty())
            {
                MessageBox.Show("Плейлист пуст!", "Ошибка");
                return;
            }

            playlist.PreviousSong();
            UpdateSongList();
        }

        //Следующая песня
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (playlist.IsEmpty())
            {
                MessageBox.Show("Плейлист пуст!", "Ошибка");
                return;
            }

            playlist.NextSong();
            UpdateSongList();
        }
        //в начало плейлиста
        private void BtnFirst_Click(object sender, RoutedEventArgs e)
        {
            if (playlist.IsEmpty())
            {
                MessageBox.Show("Плейлист пуст!", "Ошибка");
                return;
            }

            playlist.GoToFirst();
            UpdateSongList();

            // Показываем текущую песню
            Song current = playlist.CurrentSong();
            MessageBox.Show($"Переход в начало!\nТекущая песня: {current.Author} - {current.Title}",
                "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Удаление выбранной песни
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstSongs.SelectedItem == null)
            {
                MessageBox.Show("Выберите песню для удаления!", "Ошибка");
                return;
            }

            Song selectedSong = (Song)lstSongs.SelectedItem;
            playlist.RemoveSong(selectedSong);
            UpdateSongList();
        }

        //Очистка всего плейлиста
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите очистить весь плейлист?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                playlist.ClearPlaylist();
                UpdateSongList();
            }
        }

        //Выбор песни из списка
        private void LstSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstSongs.SelectedItem != null && playlist != null && !playlist.IsEmpty())
            {
                Song selectedSong = (Song)lstSongs.SelectedItem;
                var allSongs = playlist.GetAllSongs();
                int index = allSongs.IndexOf(selectedSong);
                if (index != -1)
                {
                    playlist.GoToIndex(index);
                }
            }
        }
       
    }
}
