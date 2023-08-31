using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace First
{


    public partial class SecondWindow : Window
    {
        Module module = new Module();
        private List<Module> modules = new List<Module>();
        private int numberOfWeeks = 0;
        private DateTime startDate = DateTime.MinValue;

        public SecondWindow()
        {
            InitializeComponent();

        }

        //button to store the module details 
        private void AddModule_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                module.Code = moduleCodeTextBox.Text;
                module.Name = moduleNameTextBox.Text;
                module.Credits = int.Parse(creditsTextBox.Text);
                module.ClassHoursPerWeek = int.Parse(classHoursTextBox.Text);


                modules.Add(module);
                moduleComboBox.Items.Add(module.Code);
                MessageBox.Show("Module added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                moduleCodeTextBox.Text = "";
                moduleNameTextBox.Text = "";
                creditsTextBox.Text = "";
                classHoursTextBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! Please fill in the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //button to store the details of the of the semester 
        private void EnterDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                numberOfWeeks = int.Parse(numberOfWeeksTextBox.Text);
                startDate = startDatePicker.SelectedDate ?? DateTime.MinValue;

                MessageBox.Show("Semester details entered successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! Please fill in the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Button allows you to select a specific module be calculated
        private void RecordHours_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Module selectedModule = modules.FirstOrDefault(module => module.Code == moduleComboBox.SelectedItem?.ToString());

                if (selectedModule != null)
                {
                    DateTime date = recordDatePicker.SelectedDate ?? DateTime.MinValue;
                    double hours = double.Parse(hoursTextBox.Text);

                    selectedModule.StudyHoursRecord[date] = hours;
                    MessageBox.Show("Study hours recorded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! Please fill in the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // the button calculates the self study hours 
        private void CalculateHours_Click(object sender, RoutedEventArgs e)
        {
            //Method that alllows the calculation to happen
            calculateRemaingHours();
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }


        public void calculateRemaingHours()
        {
            try
            {
                DateTime currentDate = DateTime.Now;

                if (startDate > currentDate)
                {
                    MessageBox.Show("Semester has not started yet.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                resultListBox.Items.Clear();

                foreach (Module module in modules)
                {
                    double totalStudyHours = module.Credits * 10.0 / numberOfWeeks - module.ClassHoursPerWeek;

                    if (module.StudyHoursRecord.ContainsKey(currentDate))
                    {
                        double recordedHours = module.StudyHoursRecord
                            .Where(pair => pair.Key >= currentDate.AddDays(-7))
                            .Sum(pair => pair.Value);

                        double remainingHours = totalStudyHours - recordedHours;
                        module.SelfStudyHoursPerWeek = Math.Max(remainingHours, 0);

                        resultListBox.Items.Add($"Module: {module.Code} - Remaining self-study hours: {module.SelfStudyHoursPerWeek}hr(s) for the current week.");
                    }
                    else
                    {
                        module.SelfStudyHoursPerWeek = totalStudyHours;
                        resultListBox.Items.Add($"Module: {module.Code} - Remaining self-study hours: {module.SelfStudyHoursPerWeek}hr(s) for the current week.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! Please fill in the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}



