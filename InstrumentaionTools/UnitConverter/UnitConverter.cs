using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace InstrumentaionTools.UnitConverter
{
    public partial class UnitConverter: UserControl
    {

        public UnitConverter()
        {
            InitializeComponent();
            InitializationMeasurement();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
          

        }

        public string GenerateMeasurementClass(string currentMeasurementSelection)
        {

            string mynamespace = GetThisNamespace();

            string ClassName = string.Concat(mynamespace , ".", currentMeasurementSelection);

            return ClassName;
        }

        public List<string> GetClassNames(string nameSpace)
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            List<string> namespacelist = new List<string>();
            List<string> classlist = new List<string>();

            foreach (Type type in asm.GetTypes())
            {
                if (type.Namespace == nameSpace)
                    namespacelist.Add(type.Name);
            }

            foreach (string classname in namespacelist)
                classlist.Add(classname);

            return classlist;
        }

        public List<object> GetClassVaraibles(string className)
        {

            object MeasurementClass = GetInstance(className);

            var bindingFlags = BindingFlags.Instance |
                               BindingFlags.NonPublic |
                               BindingFlags.Public;
            var fieldValues = MeasurementClass.GetType().GetFields(bindingFlags)
                                             .Select(field => field.GetValue(MeasurementClass))
                                             .ToList();

            return fieldValues;
        }

        public object GetInstance(string strFullyQualifiedName)
        {
            // string format "namespace.class"
            Type type = Type.GetType(strFullyQualifiedName);
            if (type != null)
                return Activator.CreateInstance(type);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(strFullyQualifiedName);
                if (type != null)
                    return Activator.CreateInstance(type);
            }
            return null;
        }
        public string GetThisNamespace()
        {
            return GetType().Namespace;
        }

        #region General

        public void InitializationMeasurement()
        {


            string mynamespace = GetThisNamespace();
            string baseclass = "UnitConversionFactor";

            Type baseclasstype = Type.GetType(string.Concat(mynamespace, ".", baseclass));
            Type subclass = null;

            //Getting all the class in this Namespace
            var classes = GetClassNames(mynamespace);

            //Clearing the Measurement Combox
            comboBox_tab1MeasurementType.Items.Clear();

            foreach (string cls in classes)
            {
                subclass = Type.GetType(string.Concat(mynamespace, ".", cls));

                {
                    if (subclass != null && subclass.IsSubclassOf(baseclasstype))
                    {
                        //adding the class if its a dervied class of baseclass
                        comboBox_tab1MeasurementType.Items.Add(cls);
                        comboBox_tab2MeasurementType.Items.Add(cls);
                        comboBox_tab3MeasurementType.Items.Add(cls);
                    }
                }

            }

            try
            {
                comboBox_tab1MeasurementType.SelectedIndex = 1;
                comboBox_tab2MeasurementType.SelectedIndex = 1;
                comboBox_tab3MeasurementType.SelectedIndex = 1;
            }
            catch
            {

                MessageBox.Show("No Measurement Type Found!");
            }

        }

        #endregion


        #region tab-1

        public List<string> GetUnitsArrayTab1()
        {
            string currentMeasurementSelection = (string)comboBox_tab1MeasurementType.SelectedItem;

            string MeasurementClassString = GenerateMeasurementClass(currentMeasurementSelection);
            var units = GetClassVaraibles(MeasurementClassString);

            var unitdict = (Dictionary<string, double>)units[0];

            //string variableName = "conversionFactors";

            //object MeasurementClass = GetInstance(MeasurementClassString);

            //object unitsDict = MeasurementClass.GetType().GetProperty(variableName).GetValue(MeasurementClass, null);

            List<string> unitsList = new List<string>(unitdict.Keys);

            return unitsList;

        }

        public void InitializationUnitsTab1()
        {

            List<string> units = GetUnitsArrayTab1();
            comboBox_tab1InputUnit.Items.Clear();
            comboBox_tab1ConvertUnit1.Items.Clear();
            comboBox_tab1ConvertUnit2.Items.Clear();
            comboBox_tab1ConvertUnit3.Items.Clear();
            comboBox_tab1ConvertUnit4.Items.Clear();

            comboBox_tab1InputUnit.Items.AddRange(units.ToArray());
            comboBox_tab1ConvertUnit1.Items.AddRange(units.ToArray());
            comboBox_tab1ConvertUnit2.Items.AddRange(units.ToArray());
            comboBox_tab1ConvertUnit3.Items.AddRange(units.ToArray());
            comboBox_tab1ConvertUnit4.Items.AddRange(units.ToArray());



            comboBox_tab1InputUnit.SelectedIndex = 1;
            comboBox_tab1ConvertUnit1.SelectedIndex = 2;
            comboBox_tab1ConvertUnit2.SelectedIndex = 3;
            comboBox_tab1ConvertUnit3.SelectedIndex = 4;
            comboBox_tab1ConvertUnit4.SelectedIndex = 5;


        }

        public void ExcuteConvertTab1()
        {

            string currentMeasurementSelection = (string)comboBox_tab1MeasurementType.SelectedItem;

            string MeasurementClass = GenerateMeasurementClass(currentMeasurementSelection);

            Type MeasurementClassType = Type.GetType(MeasurementClass);
            object currentMeasurement = GetInstance(MeasurementClass);


            MethodInfo convertUnit = MeasurementClassType.GetMethod("ConvertUnit");

            var bFlag = double.TryParse(textBox_tab1InputValue.Text, out Double value);

            if (!bFlag) { return; }

            string unit = (string)comboBox_tab1InputUnit.SelectedItem;
            string converUnit1 = (string)comboBox_tab1ConvertUnit1.SelectedItem;
            string converUnit2 = (string)comboBox_tab1ConvertUnit2.SelectedItem;
            string converUnit3 = (string)comboBox_tab1ConvertUnit3.SelectedItem;
            string converUnit4 = (string)comboBox_tab1ConvertUnit4.SelectedItem;

            double convert1 = (double)convertUnit.Invoke(currentMeasurement, new object[] { value, unit, converUnit1 });
            textBox_tab1ConvertValue1.Text = convert1.ToString();

            double convert2 = (double)convertUnit.Invoke(currentMeasurement, new object[] { value, unit, converUnit2 });
            textBox_tab1ConvertValue2.Text = convert2.ToString();

            double convert3 = (double)convertUnit.Invoke(currentMeasurement, new object[] { value, unit, converUnit3 });
            textBox_tab1ConvertValue3.Text = convert3.ToString();

            double convert4 = (double)convertUnit.Invoke(currentMeasurement, new object[] { value, unit, converUnit4 });
            textBox_tab1ConvertValue4.Text = convert4.ToString();
        }

        private void button_tab1Convert_Click(object sender, EventArgs e)
        {
            ExcuteConvertTab1();
        }

        private void comboBox_tab1MeasurementType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializationUnitsTab1();
        }

        private void checkBox_tab1AutoConvert_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox_tab1AutoConvert.Checked)
            {
                this.textBox_tab1InputValue.TextChanged -= this.textBox_tab1InputValue_TextChanged;
                this.comboBox_tab1InputUnit.SelectedIndexChanged -= (this.comboBox_tab1InputUnit_SelectedIndexChanged);
                this.comboBox_tab1ConvertUnit1.SelectedIndexChanged -= (this.comboBox_tab1ConvertUnit1_SelectedIndexChanged);
                this.comboBox_tab1ConvertUnit3.SelectedIndexChanged -= (this.comboBox_tab1ConvertUnit3_SelectedIndexChanged);
                this.comboBox_tab1ConvertUnit2.SelectedIndexChanged -= (this.comboBox_tab1ConvertUnit2_SelectedIndexChanged);
                this.comboBox_tab1ConvertUnit4.SelectedIndexChanged -= (this.comboBox_tab1ConvertUnit4_SelectedIndexChanged);
            }
            else
            {
                this.textBox_tab1InputValue.TextChanged += new System.EventHandler(this.textBox_tab1InputValue_TextChanged);
                this.comboBox_tab1InputUnit.SelectedIndexChanged += new System.EventHandler(this.comboBox_tab1InputUnit_SelectedIndexChanged);
                this.comboBox_tab1ConvertUnit1.SelectedIndexChanged += new System.EventHandler(this.comboBox_tab1ConvertUnit1_SelectedIndexChanged);
                this.comboBox_tab1ConvertUnit3.SelectedIndexChanged += new System.EventHandler(this.comboBox_tab1ConvertUnit3_SelectedIndexChanged);
                this.comboBox_tab1ConvertUnit2.SelectedIndexChanged += new System.EventHandler(this.comboBox_tab1ConvertUnit2_SelectedIndexChanged);
                this.comboBox_tab1ConvertUnit4.SelectedIndexChanged += new System.EventHandler(this.comboBox_tab1ConvertUnit4_SelectedIndexChanged);
                ExcuteConvertTab1();
            }
        }

        private void textBox_tab1InputValue_TextChanged(object sender, EventArgs e)
        {
            ExcuteConvertTab1();
        }

        private void comboBox_tab1InputUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExcuteConvertTab1();
        }

        private void comboBox_tab1ConvertUnit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExcuteConvertTab1();
        }

        private void comboBox_tab1ConvertUnit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExcuteConvertTab1();
        }

        private void comboBox_tab1ConvertUnit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExcuteConvertTab1();
        }

        private void comboBox_tab1ConvertUnit4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExcuteConvertTab1();
        }




        #endregion


        #region tab-2

        public List<string> GetUnitsArrayTab2()
        {
            string currentMeasurementSelection = (string)comboBox_tab2MeasurementType.SelectedItem;

            string MeasurementClassString = GenerateMeasurementClass(currentMeasurementSelection);
            var units = GetClassVaraibles(MeasurementClassString);

            var unitdict = (Dictionary<string, double>)units[0];
         
            List<string> unitsList = new List<string>(unitdict.Keys);

            return unitsList;

        }
        public void InitializationUnitsTab2()
        {
            List<string> units = GetUnitsArrayTab2();
            comboBox_tab2InputUnit.Items.Clear();
            comboBox_tab2ConvertUnit.Items.Clear();
 
            comboBox_tab2InputUnit.Items.AddRange(units.ToArray());
            comboBox_tab2ConvertUnit.Items.AddRange(units.ToArray());

            comboBox_tab2InputUnit.SelectedIndex = 1;
            comboBox_tab2ConvertUnit.SelectedIndex = 2;

        }
        public void ExcuteConvertTab2()
        {

            string currentMeasurementSelection = (string)comboBox_tab2MeasurementType.SelectedItem;

            string MeasurementClass = GenerateMeasurementClass(currentMeasurementSelection);

            Type MeasurementClassType = Type.GetType(MeasurementClass);
            object currentMeasurement = GetInstance(MeasurementClass);


            MethodInfo convertUnit = MeasurementClassType.GetMethod("ConvertUnit");

            var bFlag = double.TryParse(textBox_tab2StartValue.Text, out Double value);

            if (!bFlag) { return; }

            string unit = (string)comboBox_tab2InputUnit.SelectedItem;
            string converUnit = (string)comboBox_tab2ConvertUnit.SelectedItem;
            bFlag = int.TryParse(textBox_tab2RowsCount.Text, out int rowCount);
            if (!bFlag) { return; }
            bFlag = double.TryParse(textBox_tab2IncrementStep.Text, out double incrementStep);
            if (!bFlag) { return; }

            double convert = 0.0;

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn(unit, typeof(double)));
            dt.Columns.Add(new DataColumn(converUnit, typeof(double)));

            for (int i = 0; i < rowCount; i++)
            {
                
                convert = (double)convertUnit.Invoke(currentMeasurement, new object[] { value, unit, converUnit });
                dt.Rows.Add(value, convert);
                value += incrementStep;
                
            }

            dataGridView_tab2.DataSource = dt;

        }
        private void comboBox_tab2MeasurementType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializationUnitsTab2();
        }
        private void checkBox_tab2AutoConvert_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox_tab2AutoConvert.Checked)
            {
                this.comboBox_tab2InputUnit.SelectedIndexChanged -= (this.comboBox_tab2InputUnit_SelectedIndexChanged);
                this.textBox_tab2StartValue.TextChanged -= (this.textBox_tab2StartValue_TextChanged);
                this.textBox_tab2IncrementStep.TextChanged -= (this.textBox_tab2IncrementStep_TextChanged);
                this.textBox_tab2RowsCount.TextChanged -= (this.textBox_tab2RowsCount_TextChanged);
                this.comboBox_tab2ConvertUnit.SelectedIndexChanged -= (this.comboBox_tab2ConvertUnit_SelectedIndexChanged);
            }
            else
            {
                this.comboBox_tab2InputUnit.SelectedIndexChanged += new System.EventHandler(this.comboBox_tab2InputUnit_SelectedIndexChanged);
                this.textBox_tab2StartValue.TextChanged += new System.EventHandler(this.textBox_tab2StartValue_TextChanged);
                this.textBox_tab2IncrementStep.TextChanged += new System.EventHandler(this.textBox_tab2IncrementStep_TextChanged);
                this.textBox_tab2RowsCount.TextChanged += new System.EventHandler(this.textBox_tab2RowsCount_TextChanged);
                this.comboBox_tab2ConvertUnit.SelectedIndexChanged += new System.EventHandler(this.comboBox_tab2ConvertUnit_SelectedIndexChanged);
                ExcuteConvertTab2();
            }
        }
        private void textBox_tab2StartValue_TextChanged(object sender, EventArgs e)
        {
            ExcuteConvertTab2();
        }
        private void button_tab2Convert_Click(object sender, EventArgs e)
        {
            ExcuteConvertTab2();
        }

        private void comboBox_tab2InputUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExcuteConvertTab2();
        }

        private void textBox_tab2IncrementStep_TextChanged(object sender, EventArgs e)
        {
            ExcuteConvertTab2();
        }

        private void textBox_tab2RowsCount_TextChanged(object sender, EventArgs e)
        {
            ExcuteConvertTab2();
        }

        private void comboBox_tab2ConvertUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExcuteConvertTab2();
        }


        #endregion

        #region tab-3

        public List<string> GetUnitsArrayTab3()
        {
            string currentMeasurementSelection = (string)comboBox_tab3MeasurementType.SelectedItem;

            string MeasurementClassString = GenerateMeasurementClass(currentMeasurementSelection);
            var units = GetClassVaraibles(MeasurementClassString);

            var unitdict = (Dictionary<string, double>)units[0];

            List<string> unitsList = new List<string>(unitdict.Keys);

            return unitsList;

        }
        public void InitializationUnitsTab3()
        {
            List<string> units = GetUnitsArrayTab3();
            comboBox_tab3InputUnit.Items.Clear();


            comboBox_tab3InputUnit.Items.AddRange(units.ToArray());


            comboBox_tab3InputUnit.SelectedIndex = 1;


        }
        public void ExcuteConvertTab3()
        {

            string currentMeasurementSelection = (string)comboBox_tab3MeasurementType.SelectedItem;

            string MeasurementClass = GenerateMeasurementClass(currentMeasurementSelection);

            Type MeasurementClassType = Type.GetType(MeasurementClass);
            object currentMeasurement = GetInstance(MeasurementClass);


            MethodInfo convertUnit = MeasurementClassType.GetMethod("ConvertUnit");

            var bFlag = double.TryParse(textBox_tab3ConvertValue.Text, out Double value);

            if (!bFlag) { return; }

            string unit = (string)comboBox_tab3InputUnit.SelectedItem;


            double convert = 0.0;

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn(currentMeasurementSelection, typeof(string)));
            dt.Columns.Add(new DataColumn("Convert Value", typeof(double)));

            List<string> units = GetUnitsArrayTab3();


            foreach (string converUnit in units)
            {

                convert = (double)convertUnit.Invoke(currentMeasurement, new object[] { value, unit, converUnit });
                dt.Rows.Add(converUnit, convert);

            }

            dataGridView_tab3.DataSource = dt;

        }
        private void comboBox_tab3MeasurementType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializationUnitsTab3();
        }
        private void checkBox_tab3AutoConvert_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox_tab3AutoConvert.Checked)
            {
                this.comboBox_tab3InputUnit.SelectedIndexChanged -= (this.comboBox_tab3InputUnit_SelectedIndexChanged);
                this.textBox_tab3ConvertValue.TextChanged -= (this.textBox_tab3ConvertValue_TextChanged);

            }
            else
            {
                this.comboBox_tab3InputUnit.SelectedIndexChanged += new System.EventHandler(this.comboBox_tab3InputUnit_SelectedIndexChanged);
                this.textBox_tab3ConvertValue.TextChanged += new System.EventHandler(this.textBox_tab3ConvertValue_TextChanged);

                ExcuteConvertTab3();
            }
        }

        private void comboBox_tab3InputUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExcuteConvertTab3();
        }

        private void textBox_tab3ConvertValue_TextChanged(object sender, EventArgs e)
        {
            ExcuteConvertTab3();
        }

        private void button_tab3Convert_Click(object sender, EventArgs e)
        {
            ExcuteConvertTab3();
        }
        #endregion
    }
}
