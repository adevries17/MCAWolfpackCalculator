using System.Windows;

namespace MCAWolfpackCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int attacklogline = 0;
        readonly string targetundefined = "UNDEFINED";


        private void WriteToAttackLog(string targetname)
        {
            if (attacklogline >= 21)
            {
                AttackLogTextBox.Clear();
                attacklogline = 0;
            }
            if (targetname == string.Empty)
            {
                targetname = targetundefined;
            }

            AttackLogTextBox.Text += string.Format($"{targetname} {CalculatedDistanceTextBox.Text}hm {CalculatedSpeedTextBox.Text}kn");
            attacklogline++;
        }

        private void ResetFieldsButton_Click(object sender, RoutedEventArgs e)
        {
            TargetInputBox.Text = string.Empty;
            MastHeightTextBox.Text = string.Empty;
            MilsTextBox.Text = string.Empty;
            TargetLengthTextBox.Text = string.Empty;
            SecondsTextbox.Text = string.Empty;
            CalculatedDistanceTextBox.Text = string.Empty;
            CalculatedSpeedTextBox.Text = string.Empty;
        }

        private void ResetLogButton_Click(object sender, RoutedEventArgs e)
        {
            AttackLogTextBox.Clear();
            attacklogline = 0;
        }

        private void CalcRangeButton_Click(object sender, RoutedEventArgs e)
        {
            double mastheight = double.Parse(MastHeightTextBox.Text);
            double mils = double.Parse(MilsTextBox.Text);
            bool? ischecked = ZoomedCheckBox.IsChecked;

            double calculatedrange = Calculations.CalculateTargetRange(mastheight, mils, ischecked);
            CalculatedDistanceTextBox.Text = calculatedrange.ToString();
        }

        private void CalcSpeedButton_Click(object sender, RoutedEventArgs e)
        {
            double shiplength = double.Parse(TargetLengthTextBox.Text);
            double seconds = double.Parse(SecondsTextbox.Text);

            double calculatedspeed = Calculations.CalculateTargetSpeed(shiplength, seconds);
            CalculatedSpeedTextBox.Text = calculatedspeed.ToString();
        }

        private void AddToLogButton_Click(object sender, RoutedEventArgs e)
        {
            WriteToAttackLog(TargetInputBox.Text);
        }

        private void English_Click(object sender, RoutedEventArgs e)
        {
            App.SetCulture("en-US");
        }

        private void German_Click(object sender, RoutedEventArgs e)
        {
            App.SetCulture("de-DE");
        }

        private void Russia_Click(object sender, RoutedEventArgs e)
        {
            App.SetCulture("ru-RU");
        }
    }
}