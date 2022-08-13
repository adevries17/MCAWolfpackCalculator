using System;
using System.Windows.Forms;

namespace TorpedoAssist
{
    enum Units
    {
        kilometers,
        hectometers
    };

    enum CalculationType
    {
        speedCalculation = 2,
        rangeCalculation = 3
    }

    enum Lang
    {
        rus = 4,
        eng = 5,
        ger = 6
    }

    public partial class Form1 : Form
    {

        int historyLine = 0;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        string strNameless;
        string strTarget;
        string strRange;
        string strSpeed;
        string strHm;
        string strKm;

        private void ChangeLang(Lang language) //Text constants for language
        {
            switch (language)
            {
                case Lang.eng:
                    timeTextBox.Left = 140;
                    calculationHistoryHead.Left = 465;
                    strNameless = "Unnamed";
                    strTarget = "Target: \"";
                    strRange = "\" Range = ";
                    strSpeed = "\" Speed (knots) = ";
                    strHm = " hm, ";
                    strKm = " m";
                    targetNameHead.Text = "Target ID:";
                    notNecessaryLabel.Text = "(optionally)";
                    headRangeHead.Text = "Range to target";
                    mastHeightLabel.Text = "Mast height:";
                    linesNumberLabel.Text = "Lines amount:";
                    isZoomedBox.Text = "Is zoomed";
                    calculateRangeButton.Text = "Calculate the distance";
                    calcRangeLabel.Text = "Range to target =";
                    headTargetSpeedHead.Text = "Speed of target";
                    lengthLabel.Text = "Target length (m):";
                    timeLabel.Text = "Elapsed time:";
                    calculateSpeedButton.Text = "Calculate speed";
                    calcSpeedLabel.Text = "Speed in knots = ";
                    langLabel.Text = "Language:";
                    calculationHistoryHead.Text = "Calculation log";
                    ResetCLButton.Text = "Reset log";
                    clearFieldsButton.Text = "Reset fields";
                    break;
                case Lang.rus:
                    calculationHistoryHead.Left = 453;
                    timeTextBox.Left = 190;
                    strNameless = "Без имени";
                    strTarget = "Цель: \"";
                    strRange = "\" Расстояние = ";
                    strSpeed = "\" Скорость = ";
                    strHm = " гм, ";
                    strKm = " м";
                    targetNameHead.Text = "Имя цели:";
                    notNecessaryLabel.Text = "(можно не указывать)";
                    headRangeHead.Text = "Дистанция до цели";
                    mastHeightLabel.Text = "Высота мачты:";
                    linesNumberLabel.Text = "Кол-во рисок:";
                    isZoomedBox.Text = "Есть увеличение";
                    calculateRangeButton.Text = "Рассчитать дистанцию";
                    calcRangeLabel.Text = "ДИАПАЗОН ДО ЦЕЛИ = ";
                    headTargetSpeedHead.Text = "Скорость цели";
                    lengthLabel.Text = "Длина цели (м):";
                    timeLabel.Text = "Время на прохождение корпуса:";
                    calculateSpeedButton.Text = "Рассчитать скорость";
                    calcSpeedLabel.Text = "Скорость в узлах =";
                    langLabel.Text = "Язык:";
                    calculationHistoryHead.Text = "История расчётов";
                    ResetCLButton.Text = "Очистить историю";
                    clearFieldsButton.Text = "Очистить поля";
                    break;
                case Lang.ger:
                    timeTextBox.Left = 165;
                    calculationHistoryHead.Left = 465;
                    strNameless = "Unnamed";
                    strTarget = "Ziel: \"";
                    strRange = "\" Bereich = ";
                    strSpeed = "\" Geschwindigkeit(Knoten) = ";
                    strHm = " hm, ";
                    strKm = " m";
                    targetNameHead.Text = "Ziel:";
                    notNecessaryLabel.Text = "(optional)";
                    headRangeHead.Text = "Reichweite zum Ziel";
                    mastHeightLabel.Text = "Masthöhe:";
                    linesNumberLabel.Text = "Zeilenbetrag:";
                    isZoomedBox.Text = "GEZOOMT";
                    calculateRangeButton.Text = "Berechnen Sie die Entfernung";
                    calcRangeLabel.Text = "REICHWEITE ZUM ZIEL =";
                    headTargetSpeedHead.Text = "Geschwindigkeit des Ziels";
                    lengthLabel.Text = "Ziellänge (m):";
                    timeLabel.Text = "Zeit, den Rumpf fertigzustellen:";
                    calculateSpeedButton.Text = "Geschwindigkeit berechnen";
                    calcSpeedLabel.Text = "Geschwindigkeit in Knoten = ";
                    langLabel.Text = "Sprache:";
                    calculationHistoryHead.Text = "Berechnungsprotokoll";
                    ResetCLButton.Text = "Protokoll zurücksetzen";
                    clearFieldsButton.Text = "Felder zurücksetzen";
                    break;
            }
        }

        private double CalculateRange(Units unit, double mastHeight, double linesNumber, bool isZoomed)
        {
            if (mastHeight == 0 || linesNumber == 0) return 0;
            else if (mastHeight > 200 || linesNumber > 30) return 0;
            switch (unit)
            {
                case Units.kilometers:
                    if (isZoomed) return ((mastHeight / linesNumber) * 4) * 100;
                    else return (mastHeight / linesNumber) * 100;
                case Units.hectometers:
                    if (isZoomed) return (mastHeight / linesNumber) * 4;
                    else return (mastHeight / linesNumber);
            }
            return 0;
        }

        private double CalculateSpeed(double length, double time)
        {
            if (length == 0 || time == 0) return 0;
            else if (length > 500 || time > 600) return 0;
            return length / time * 2;
        }

        private void ToHistory(string targetName, CalculationType calcType) //Add last data to calculation log
        {
            if (historyLine == 21)
            {
                historyTextBox.Clear();
                historyLine = 0;
            }
            if (targetName == "") targetName = strNameless;
            switch (calcType)
            {
                case CalculationType.rangeCalculation:
                    historyTextBox.Text += strTarget + targetName + strRange + rangeValueLabel.Text + Environment.NewLine;
                    break;
                case CalculationType.speedCalculation:
                    historyTextBox.Text += strTarget + targetName + strSpeed + speedValueLabel.Text + Environment.NewLine;
                    break;
            }
            historyLine++;
        }

        
        private bool ValidateAllBoxes()
        {
            bool wasValidate = false;
            wasValidate = validateTextbox(mastHeightTextBox) || validateTextbox(linesNumberTextBox)
                          || validateTextbox(lengthTextBox)  || validateTextbox(timeTextBox);

            if (wasValidate) clearTextBoxes();

            return wasValidate;
        }

        private bool validateTextbox(TextBox txtbox)
        {
            bool result = !(IsNum(txtbox.Text));

            return result;
        }

        private void clearTextBoxes()
        {
            mastHeightTextBox.Text = "0";
            linesNumberTextBox.Text = "0";
            lengthTextBox.Text = "0";
            timeTextBox.Text = "0";
        }


       private bool IsNum(string s)
        {
            int m = 0;
            for(int i = 0; i < s.Length;i++)
            {
                if (!Char.IsDigit(s[i]) && s[i]!=',' ) return false;
                if (s[i] == ',') m++;
                if (m > 1) return false;
            }
            
            return true;
        }
        
        private void CalculateRangeButton_Click(object sender, EventArgs e)
        {
            if(ValidateAllBoxes()) return;
            double rangeInHecto;
            double rangeInKilo;
            rangeInHecto = CalculateRange(Units.hectometers, double.Parse(mastHeightTextBox.Text), double.Parse(linesNumberTextBox.Text), isZoomedBox.Checked);
            rangeInKilo = CalculateRange(Units.kilometers, double.Parse(mastHeightTextBox.Text), double.Parse(linesNumberTextBox.Text), isZoomedBox.Checked);
            rangeValueLabel.Text = rangeInHecto.ToString("F2") + strHm + rangeInKilo.ToString("F2") + strKm;
            ToHistory(targetNameTextBox.Text,CalculationType.rangeCalculation);
        }

        private void CalculateSpeedButton_Click(object sender, EventArgs e)
        {
            if (ValidateAllBoxes()) return;
            double speed;
            speed = CalculateSpeed(double.Parse(lengthTextBox.Text), double.Parse(timeTextBox.Text));
            speedValueLabel.Text = speed.ToString("F2");
            ToHistory(targetNameTextBox.Text, CalculationType.speedCalculation);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                ChangeLang(Lang.eng);
            }
            if (comboBox1.SelectedIndex == 1)
            {
                ChangeLang(Lang.rus);
            }
            if (comboBox1.SelectedIndex == 2)
            {
                ChangeLang(Lang.ger);
            }
        }

        private void ClearHistorybutton_Click(object sender, EventArgs e)
        {
            historyTextBox.Clear();
            historyLine = 0;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
                targetNameTextBox.Text = "";
                mastHeightTextBox.Text = "0";
                linesNumberTextBox.Text = "0";
                lengthTextBox.Text = "0";
                timeTextBox.Text = "0";
        }

    }


}






 
