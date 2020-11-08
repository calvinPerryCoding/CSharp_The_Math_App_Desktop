using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathCSharpDesktop
{
    public partial class Form1 : Form
    {
        //Functions for program
        void inputValidationExam()
        {
            double validationExam;
            while (string.IsNullOrEmpty(textBoxNameExam.Text) || !Double.TryParse(textBoxCG.Text, out validationExam) || !Double.TryParse(textBoxFEW.Text, out validationExam))
            {
                string title = "ERROR";
                string message = "Invalid input please follow the example";
                MessageBox.Show(message, title);
                textBoxNameExam.Text = "John Doe";
                textBoxCG.Text = "82";
                textBoxFEW.Text = "30";
            }
        }

        void inputValidationRatio()
        {
            double validationRatio;
            while (string.IsNullOrEmpty(textBoxNameRatio.Text) || !Double.TryParse(textBoxWaterRatio.Text, out validationRatio) || !Double.TryParse(textBoxChemicalRatio.Text, out validationRatio))
            {
                string title = "ERROR";
                string message = "Invalid input please follow the example";
                MessageBox.Show(message, title);
                textBoxNameRatio.Text = "Oil";
                comboBoxUnitsRatio.SelectedItem = "Fluid Ounces";
                textBoxWaterRatio.Text = "50";
                textBoxChemicalRatio.Text = "1";
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        //Calculate Final Exam button
        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            //Call function
            inputValidationExam();

            //Declare variables
            string nameExam;
            double gradeDesiredEquation = 1, gradeDesiredDisplay = 1, currentGrade, finalExamWeight, finalExamWeightDecimal, finalExam, finalExamRounded;

            //Set variables equal to text box
            nameExam = textBoxNameExam.Text;
            currentGrade = double.Parse(textBoxCG.Text);
            finalExamWeight = double.Parse(textBoxFEW.Text);

            finalExamWeightDecimal = finalExamWeight / 100;

            /* In the for loop and display I ran into a problem during testing
             * Example: Current grade = 82% and Exam Weight = 30%
             * The output should have been 
             * "To get a(n) 85% in the class you need to get a 92% on the final exam"
             * Instead it was
             * "To get a(n) 85% in the class you need to get a 88.6666666666667% on the final exam"
             * The final exam grade was one behind
             * In order resolve the issue I had to make a two different variables
             * One for the equation loop and one for the display loop
             * Even though they are assigned the same value 
             * I know it's something to do with the for loop, the issue remains resolved for now
            */
            for (int i = 0; i < 100; i++)
            {
                //Formula for final exam
                finalExam = (gradeDesiredEquation++ - ((1 - finalExamWeightDecimal) * currentGrade)) / finalExamWeightDecimal;

                //Round to 2 decimal places
                finalExamRounded = Math.Round(finalExam, 2);

                richTextBoxFER.Text += "In order to get a(n) " + gradeDesiredDisplay++ + "% in the class " + nameExam.ToString() + " needs to get a(n) "
                + finalExamRounded.ToString() + "% on the final exam" + "\n\n";
            }
        }

        //Clears results For Final Exam
        private void buttonClearExam_Click(object sender, EventArgs e)
        {
            richTextBoxFER.Clear();
        }

        //Calculate Ratio button
        private void buttonCalculateRatio_Click(object sender, EventArgs e)
        {
            inputValidationRatio();

            //Declare Variables
            //Name of chemical
            string nameRatio;

            //There are 128 oz in one gallon
            double fLOZToGallons = 128;
            //There are 1000mL in one Liter
            double litersToMilliliters = 1000;
            //There are 15.7725491 US legal cups in one gallon
            double usLegalCupsToGallons = 15.7725491;
            //There are 16 US cups in one gallon
            double usCupsToGallons = 16;

            double waterRatio, chemicalRatio, sumOfRatio;

            //Set variables equal to text box
            nameRatio = textBoxNameRatio.Text;
            waterRatio = int.Parse(textBoxWaterRatio.Text);
            chemicalRatio = int.Parse(textBoxChemicalRatio.Text);

            //Adds the parts of water to the parts of the chemical
            sumOfRatio = waterRatio + chemicalRatio;

            //If fluid ounces is selected
            if (comboBoxUnitsRatio.SelectedItem == "Fluid Ounces")
            {
                //Calculation for fluid ounce(oz)
                double fLOZRatio = fLOZToGallons / sumOfRatio;

                //If the name of the chemical is oil then gasoline will appear instead of water
                if (textBoxNameRatio.Text == "Oil" || textBoxNameRatio.Text == "oil" || textBoxNameRatio.Text == "OIL")
                {
                    //Round to 2 decimal places
                    double fLOZRatioRounded = Math.Round(fLOZRatio, 2);
                    richTextBoxResultsRatio.Text = "You will need to mix " + fLOZRatioRounded + "oz of " + nameRatio.ToString() + " to one gallon of gasoline.";
                }

                else
                {
                    //Round to 2 decimal places
                    double fLOZRatioRounded = Math.Round(fLOZRatio, 2);
                    richTextBoxResultsRatio.Text = "You will need to mix " + fLOZRatioRounded + "oz of " + nameRatio.ToString() + " to one gallon of water.";
                }

            }

            //If Milliliters is selected
            if (comboBoxUnitsRatio.SelectedItem == "Milliliters")
            {
                double millilitersRatio = litersToMilliliters / sumOfRatio;

                //If the name of the chemical is oil then gasoline will appear instead of water
                if (textBoxNameRatio.Text == "Oil" || textBoxNameRatio.Text == "oil" || textBoxNameRatio.Text == "OIL")
                {
                    //Round to 2 decimal places
                    double millilitersRatioRounded = Math.Round(millilitersRatio, 2);
                    richTextBoxResultsRatio.Text = "You will need to mix " + millilitersRatioRounded + "ml of " + nameRatio.ToString() + " to one Liter of gasoline.";
                }

                else
                {
                    //Round to 2 decimal places
                    double millilitersRatioRounded = Math.Round(millilitersRatio, 2);
                    richTextBoxResultsRatio.Text = "You will need to mix " + millilitersRatioRounded + "ml of " + nameRatio.ToString() + " to one Liter of water.";
                }
            }

            //If US Legal Cups is selected
            if (comboBoxUnitsRatio.SelectedItem == "US Legal Cups")
            {
                //Calculation for fluid ounce(oz)
                double usLegalCupsRatio = usLegalCupsToGallons / sumOfRatio;

                //If the name of the chemical is oil then gasoline will appear instead of water
                if (textBoxNameRatio.Text == "Oil" || textBoxNameRatio.Text == "oil" || textBoxNameRatio.Text == "OIL")
                {
                    //Round to 15 decimal places
                    double usLegalCupsRatioRounded = Math.Round(usLegalCupsRatio, 15);
                    richTextBoxResultsRatio.Text = "You will need to mix " + usLegalCupsRatioRounded + " US Legal Cups of " + nameRatio.ToString() + " to one gallon of gasoline.";
                }

                else
                {
                    //Round to 15 decimal places
                    double usLegalCupsRatioRounded = Math.Round(usLegalCupsRatio, 15);
                    richTextBoxResultsRatio.Text = "You will need to mix " + usLegalCupsRatioRounded + " US Legal Cups of " + nameRatio.ToString() + " to one gallon of water.";
                }

            }

            //If US Cups is selected
            if (comboBoxUnitsRatio.SelectedItem == "US Cups")
            {
                //Calculation for fluid ounce(oz)
                double usCupsRatio = usCupsToGallons / sumOfRatio;

                //If the name of the chemical is oil then gasoline will appear instead of water
                if (textBoxNameRatio.Text == "Oil" || textBoxNameRatio.Text == "oil" || textBoxNameRatio.Text == "OIL")
                {
                    //Round to 2 decimal places
                    double usCupsRatioRounded = Math.Round(usCupsRatio, 2);
                    richTextBoxResultsRatio.Text = "You will need to mix " + usCupsRatioRounded + " US Cups of " + nameRatio.ToString() + " to one gallon of gasoline.";
                }

                else
                {
                    //Round to 2 decimal places
                    double usCupsRatioRounded = Math.Round(usCupsRatio, 2);
                    richTextBoxResultsRatio.Text = "You will need to mix " + usCupsRatioRounded + " US Cups of " + nameRatio.ToString() + " to one gallon of water.";
                }
            }

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = "About";
            string message = "This Program Was Made By Calvin Perry";
            MessageBox.Show(message, title);
        }
    }
}
