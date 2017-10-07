using ProjectEssential.BMIModel;
using ProjectEssential.EnumLib;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace BMICalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using(var _db = new BMIDbContext())
            {
                var data = _db.BMI
                    .Select(m => new
                    {
                        m.Name,
                        m.Height,
                        m.Weight,
                        m.BMIValue,
                        m.Unit
                    })
                    .AsEnumerable()
                    .Select(m => new BMIViewModel
                {
                     BMIValue = Math.Round(m.BMIValue,2),
                      Height = m.Height,
                       Name = m.Name,
                        Unit = GetUnitTypeFriendlyName(m.Unit),
                         Weight = m.Weight
                }).ToList();
                BMITable.ItemsSource = data;
            }



            UnitTypeDropDown.ItemsSource = Enum.GetValues(typeof(UnitTypes)).Cast<Enum>().Select(value => new
            {
                (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                value
            })
        .ToList();
            UnitTypeDropDown.DisplayMemberPath = "Description";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var model = new BMI()
            {
                Weight = Convert.ToInt32(Weight.Text),
                Height = Convert.ToInt32(Height.Text),
                Name = Name.Text,
            };
            var val = UnitTypeDropDown.SelectedIndex;
            if (val == 0)
            {
                var weight = model.Weight * .45;
                var height = model.Height * .0254;
                model.Unit = UnitTypes.InchesAndPound;
                model.BMIValue = weight / (height * height);
            }
            else
            {
                var height = model.Height * .01;
                model.Unit = UnitTypes.CentimeterAndKilogram;
                model.BMIValue = model.Weight / (height * height);
            }

            using(var _db = new BMIDbContext())
            {
                _db.BMI.Add(model);
                _db.SaveChanges();
            }

            BMI.Text = model.BMIValue.ToString();
            Console.Write(val);
        }

        public string GetUnitTypeFriendlyName(UnitTypes type)
        {
            switch (type)
            {
                case UnitTypes.CentimeterAndKilogram:
                    return "Centimeter & KG";

                default:
                case UnitTypes.InchesAndPound:
                    return "Inches & Pound";

            }
        }
        
    }
}
