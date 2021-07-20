using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace InstrumentaionTools.AnalogScaling
{
    public partial class AnalogScaling : UserControl
    {

        public string doublePrecision = "0.############";
        public Dictionary<string, List<string>> Measurements = new Dictionary<string, List<string>>()
    {
      {
        "Voltage ",
        new List<string>()
        {
          "±50 mV",
          "±80 mV",
          "±250 mV",
          "±500 mV",
          "±1 V",
          "±2.5 V",
          "1 V to 5 V",
          "±5 V",
          "±10 V"
        }
      },
      {
        "Current",
        new List<string>()
        {
          "0 mA to 20 mA",
          "4 mA to 20 mA",
          "±20 mA"
        }
      },
      {
        "Resistor",
        new List<string>() { "150 Ω", "300 Ω", "600 Ω", "6000 Ω" }
      }
    };
        public Dictionary<string, List<double>> SignalLimits = new Dictionary<string, List<double>>()
    {
      {
        "±50 mV",
        new List<double>() { -50.0, 50.0 }
      },
      {
        "±80 mV",
        new List<double>() { -80.0, 80.0 }
      },
      {
        "±250 mV",
        new List<double>() { -250.0, 250.0 }
      },
      {
        "±500 mV",
        new List<double>() { -500.0, 500.0 }
      },
      {
        "±1 V",
        new List<double>() { -1.0, 1.0 }
      },
      {
        "±2.5 V",
        new List<double>() { -2.5, 2.5 }
      },
      {
        "1 V to 5 V",
        new List<double>() { 1.0, 5.0 }
      },
      {
        "±5 V",
        new List<double>() { -5.0, 5.0 }
      },
      {
        "±10 V",
        new List<double>() { -10.0, 10.0 }
      },
      {
        "0 mA to 20 mA",
        new List<double>() { 0.0, 20.0 }
      },
      {
        "4 mA to 20 mA",
        new List<double>() { 4.0, 20.0 }
      },
      {
        "±20 mA",
        new List<double>() { -20.0, 20.0 }
      },
      {
        "150 Ω",
        new List<double>() { 0.0, 150.0 }
      },
      {
        "300 Ω",
        new List<double>() { 0.0, 300.0 }
      },
      {
        "600 Ω",
        new List<double>() { 0.0, 600.0 }
      },
      {
        "6000 Ω",
        new List<double>() { 0.0, 6000.0 }
      }
    };

        public AnalogScaling()
        {
            InitializeComponent();
            InitializationMeasurementTab1();
        }

        #region Tab-1
        public void InitializationMeasurementTab1()
        {
            this.comboBox_tab1Measurement.Items.AddRange((object[])new List<string>((IEnumerable<string>)this.Measurements.Keys).ToArray());
            this.comboBox_tab1Measurement.SelectedIndex = 1;
        }

        public void InitializationSignalTypeTab1()
        {
            List<string> measurement = this.Measurements[(string)this.comboBox_tab1Measurement.SelectedItem];
            this.comboBox_tab1SignalType.Items.Clear();
            this.comboBox_tab1SignalType.Items.AddRange((object[])measurement.ToArray());
            this.comboBox_tab1SignalType.SelectedIndex = 1;
        }

        private void InputValueTypeChangedTab1()
        {
            if (this.radioButton_tab1InputPlcCount.Checked)
            {
                this.label_tab1OutputValue.Text = "Value";
                this.label_tab1InputValue.Text = "Count";
            }
            else if (this.radioButton_tab1InputSensorValue.Checked)
            {
                this.label_tab1OutputValue.Text = "Count";
                this.label_tab1InputValue.Text = "Value";
            }

            this.button_tab1Perc50.PerformClick();
            if (!this.checkBox_tab1AutoConvert.Checked)
                return;
            this.ExcuteConvertTab1();
        }

        private void ExcuteConvertTab1()
        {
            int plcCountLow;
            int plcCountHigh;
            double sensorLimitLow;
            double sensorLimitHigh;
            double inputValue;

            if (!int.TryParse(this.textBox_tab1DigitalCountLow.Text, out plcCountLow)
               || !int.TryParse(this.textBox_tab1DigitalCountHigh.Text, out plcCountHigh)
               || !double.TryParse(this.textBox_tab1SensingLimitLow.Text, out sensorLimitLow)
               || !double.TryParse(this.textBox_tab1SensingLimitHigh.Text, out sensorLimitHigh)
               || !double.TryParse(this.textBox_tab1InputValue.Text, out inputValue))
                return;

            if (this.radioButton_tab1InputSensorValue.Checked)
                this.textBox_tab1OutputValue.Text = Convert.ToInt32((inputValue - sensorLimitLow) / (sensorLimitHigh - sensorLimitLow) * (double)(plcCountHigh - plcCountLow) + (double)plcCountLow).ToString();
            else if (this.radioButton_tab1InputPlcCount.Checked)
                this.textBox_tab1OutputValue.Text = ((inputValue - plcCountLow) / (double)(plcCountHigh - plcCountLow) * (sensorLimitHigh - sensorLimitLow) + (double)sensorLimitLow).ToString(this.doublePrecision);

            double sensingResolution;
            if (!double.TryParse(this.textBox_tab1SensingResolution.Text, out sensingResolution))
                return;

            if (this.checkBox_tab1SignBit.Checked)
                --sensingResolution;

            double num = Math.Pow(2.0, sensingResolution);
            this.textBox_tab1SensingGranularity.Text = ((sensorLimitHigh - sensorLimitLow) / num).ToString(this.doublePrecision);

            List<double> signalLimit = this.SignalLimits[(string)this.comboBox_tab1SignalType.SelectedItem];

            if (signalLimit[0] < 0.0)
                signalLimit[0] = 0.0;


            this.textBox_tab1SignalGranularity.Text = ((signalLimit[1] - signalLimit[0]) / num).ToString(this.doublePrecision);
        }

        private void button_Percentage_Click(object sender, EventArgs e)
        {
            string percentageString = ((Control)sender).Text;
            double percentage;
            if (!double.TryParse(percentageString.Remove(percentageString.Length - 1), out percentage))
                return;
            if (this.radioButton_tab1InputPlcCount.Checked)
            {
                int plcCountLow;
                int plcCountHigh;
                if (!int.TryParse(this.textBox_tab1DigitalCountLow.Text, out plcCountLow) || !int.TryParse(this.textBox_tab1DigitalCountHigh.Text, out plcCountHigh))
                    return;
                this.textBox_tab1InputValue.Text = Convert.ToInt32((double)(plcCountHigh - plcCountLow) * (percentage / 100.0) + (double)plcCountLow).ToString();
            }
            else
            {
                double sensorLimitLow;
                double sensorLimitHigh;
                if (!this.radioButton_tab1InputSensorValue.Checked || !double.TryParse(this.textBox_tab1SensingLimitLow.Text, out sensorLimitLow) || !double.TryParse(this.textBox_tab1SensingLimitHigh.Text, out sensorLimitHigh))
                    return;
                this.textBox_tab1InputValue.Text = ((sensorLimitHigh - sensorLimitLow) * (percentage / 100.0) + sensorLimitLow).ToString(this.doublePrecision);
            }
        }

        private void comboBox_tab1SignalType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = (string)this.comboBox_tab1SignalType.SelectedItem;
            this.label_tab1SignalGranularity.Text = selectedItem.Substring(selectedItem.Length - 2);
            if (!this.checkBox_tab1AutoConvert.Checked)
                return;
            this.ExcuteConvertTab1();
        }

        private void button_tab1Convert_Click(object sender, EventArgs e) => this.ExcuteConvertTab1();
        private void radioButton_tab1InputPlcCount_CheckedChanged(object sender, EventArgs e) => this.InputValueTypeChangedTab1();
        private void radioButton_tab1InputSensorValue_CheckedChanged(object sender, EventArgs e) => this.InputValueTypeChangedTab1();
        private void comboBox_tab1Measurement_SelectedIndexChanged(object sender, EventArgs e) => this.InitializationSignalTypeTab1();


        #region tab-1 Auto convert


        private void checkBox_tab1AutoConvert_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_tab1AutoConvert.Checked)
            {
                this.checkBox_tab1SignBit.CheckedChanged += new EventHandler(this.checkBox_tab1SignBit_CheckedChanged);
                this.textBox_tab1SensingLimitHigh.TextChanged += new EventHandler(this.textBox_tab1SensingLimitHigh_TextChanged);
                this.textBox_tab1SensingLimitLow.TextChanged += new EventHandler(this.textBox_tab1SensingLimitLow_TextChanged);
                this.textBox_tab1DigitalCountHigh.TextChanged += new EventHandler(this.textBox_tab1DigitalCountHigh_TextChanged);
                this.textBox_tab1DigitalCountLow.TextChanged += new EventHandler(this.textBox_tab1DigitalCountLow_TextChanged);
                this.textBox_tab1SensingResolution.TextChanged += new EventHandler(this.textBox_tab1SensingResolution_TextChanged);
                this.textBox_tab1InputValue.TextChanged += new EventHandler(this.textBox_tab1InputValue_TextChanged);
                this.ExcuteConvertTab1();
            }
            else
            {
                this.checkBox_tab1SignBit.CheckedChanged -= new EventHandler(this.checkBox_tab1SignBit_CheckedChanged);
                this.textBox_tab1SensingLimitHigh.TextChanged -= new EventHandler(this.textBox_tab1SensingLimitHigh_TextChanged);
                this.textBox_tab1SensingLimitLow.TextChanged -= new EventHandler(this.textBox_tab1SensingLimitLow_TextChanged);
                this.textBox_tab1DigitalCountHigh.TextChanged -= new EventHandler(this.textBox_tab1DigitalCountHigh_TextChanged);
                this.textBox_tab1DigitalCountLow.TextChanged -= new EventHandler(this.textBox_tab1DigitalCountLow_TextChanged);
                this.textBox_tab1SensingResolution.TextChanged -= new EventHandler(this.textBox_tab1SensingResolution_TextChanged);
                this.textBox_tab1InputValue.TextChanged -= new EventHandler(this.textBox_tab1InputValue_TextChanged);
            }
        }

        private void textBox_tab1InputValue_TextChanged(object sender, EventArgs e) => this.ExcuteConvertTab1();


        private void textBox_tab1SensingLimitLow_TextChanged(object sender, EventArgs e) => this.ExcuteConvertTab1();

        private void textBox_tab1SensingLimitHigh_TextChanged(object sender, EventArgs e) => this.ExcuteConvertTab1();

        private void textBox_tab1DigitalCountLow_TextChanged(object sender, EventArgs e) => this.ExcuteConvertTab1();

        private void textBox_tab1DigitalCountHigh_TextChanged(object sender, EventArgs e) => this.ExcuteConvertTab1();

        private void textBox_tab1SensingResolution_TextChanged(object sender, EventArgs e) => this.ExcuteConvertTab1();

        private void checkBox_tab1SignBit_CheckedChanged(object sender, EventArgs e) => this.ExcuteConvertTab1();
        #endregion

        #endregion

    }
}
