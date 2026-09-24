using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Gra_w_kosci_2026_desktop
{
    public partial class MainWindow : Window
    {
        private int[] diceValues = new int[5];
        private bool[] isDiceLocked = new bool[5];
        private Random random = new Random();
        private Image[] diceImages;

        public MainWindow()
        {
            InitializeComponent();

            diceImages = new Image[] { Dice0, Dice1, Dice2, Dice3, Dice4 };

            ResetGame();
        }
        private void ResetGame()
        {
            for (int i = 0; i < 5; i++)
            {
                diceValues[i] = 0;
                isDiceLocked[i] = false;
                UpdateDiceVisual(i, "kosc0.png", 1.0);
            }
            ResultTextBlock.Text = "0";
        }

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            int totalScore = 0;

            for (int i = 0; i < 5; i++)
            {

                if (!isDiceLocked[i])
                {
                    diceValues[i] = random.Next(1, 7);
                    string imageName = $"kosc{diceValues[i]}.png";
                    UpdateDiceVisual(i, imageName, 1.0);
                }

                totalScore += diceValues[i];
            }

            ResultTextBlock.Text = totalScore.ToString();
        }

        private void Dice_Click(object sender, MouseButtonEventArgs e)
        {
            Image clickedImage = sender as Image;
            if (clickedImage == null) return;

            int index = Convert.ToInt32(clickedImage.Tag);

            if (diceValues[index] == 0) return;

            isDiceLocked[index] = !isDiceLocked[index];

            clickedImage.Opacity = isDiceLocked[index] ? 0.5 : 1.0;
        }

        private void UpdateDiceVisual(int index, string filename, double opacity)
        {
            try
            {
                BitmapImage bitmap = new BitmapImage(new Uri($"pack://application:,,,/obrazy/{filename}", UriKind.RelativeOrAbsolute));
                diceImages[index].Source = bitmap;
            }
            catch
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "obrazy", filename), UriKind.Absolute));
                    diceImages[index].Source = bitmap;
                }
                catch
                {

                }
            }
            diceImages[index].Opacity = opacity;
        }
    }
}