using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Gra_w_kosci_2026_desktop
{
    public partial class MainWindow : Window
    {
        private readonly int[] dice = new int[5];
        private readonly bool[] locked = new bool[5];
        private readonly Image[] images;
        private readonly Random rng = new Random();

        public MainWindow()
        {
            InitializeComponent();

            images = new Image[]
            {
                Dice0,
                Dice1,
                Dice2,
                Dice3,
                Dice4
            };

            NewGame();
        }

        private void NewGame()
        {
            for (int i = 0; i < dice.Length; i++)
            {
                dice[i] = 0;
                locked[i] = false;

                SetDiceImage(i, "kosc0.png");
                images[i].Opacity = 1;
            }

            ResultTextBlock.Text = "0";
        }

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            int sum = 0;

            for (int i = 0; i < dice.Length; i++)
            {
                if (!locked[i])
                {
                    dice[i] = rng.Next(1, 7);
                    SetDiceImage(i, $"kosc{dice[i]}.png");
                }

                sum += dice[i];
            }

            ResultTextBlock.Text = sum.ToString();
        }

        private void Dice_Click(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is Image selectedDice))
                return;

            int diceNumber = int.Parse(selectedDice.Tag.ToString());

            if (dice[diceNumber] == 0)
                return;

            locked[diceNumber] = !locked[diceNumber];

            if (locked[diceNumber])
                selectedDice.Opacity = 0.45;
            else
                selectedDice.Opacity = 1.0;
        }

        private void SetDiceImage(int number, string fileName)
        {
            try
            {
                string path = $"pack://application:,,,/obrazy/{fileName}";

                images[number].Source = new BitmapImage(
                    new Uri(path, UriKind.Absolute));
            }
            catch
            {
                try
                {
                    string path = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "obrazy",
                        fileName);

                    images[number].Source = new BitmapImage(
                        new Uri(path, UriKind.Absolute));
                }
                catch
                {
                    images[number].Source = null;
                }
            }
        }
    }
}