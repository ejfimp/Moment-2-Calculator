using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;


namespace Moment_2
{
    public partial class Form1 : Form
    {
        CultureInfo ci = CultureInfo.InstalledUICulture;

        decimal current_number = 0;
        decimal previous_number = 0;
        char chosen_operator = '0';        

        public Form1()
        {
            InitializeComponent();
        }

        private void num_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (current_number_display.TextLength >= 16)
            {
                //don't add a number, when the length of the textbox is reached
                //this also removes the possible OverFlowException for decimal type
            }
            else if (current_number_display.Text == "0")//replaces the default 0
            {
                current_number_display.Text = "";
                current_number_display.Text = b.Text;
            }
            else//adds a number to the display
                current_number_display.Text += b.Text;
        }
        private void operator_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            try
            {
                if (previous_number_display.Text == "")
                {
                    previous_number = decimal.Parse(current_number_display.Text);
                    chosen_operator = char.Parse(b.Text);
                    previous_number_display.Text = $"{previous_number} {chosen_operator}";
                    current_number_display.Text = "0";
                }//if nothing but the current_number_display has visible value
                else if (chosen_operator != '0')
                {
                    chosen_operator = char.Parse(b.Text);
                    previous_number_display.Text = $"{previous_number} {chosen_operator}";
                    current_number_display.Text = "0";
                }//if an operator has been previously chosen,
                 //it gets replaced with the new one and allows for continous calculations
            }
            catch (FormatException ex)//expected if system language is set to a language that
            {                         //uses "." as a decimal but is not en-US or en-GB
            
                MessageBox.Show(ex.Message);
            }
        }
        private void equals_click(object sender, EventArgs e)
        {
            if (previous_number_display.Text != "") //nothing happens if there is no calculation to be done
            {
                current_number = decimal.Parse(current_number_display.Text);
                previous_number_display.Text = $"{previous_number} {chosen_operator} {current_number} = ";
                try
                {
                    switch (chosen_operator)
                    {
                        case '+':
                            current_number += previous_number;
                            break;
                        case '-':
                            current_number = previous_number - current_number;
                            break;
                        case '*':
                            current_number *= previous_number;
                            break;
                        case '/':
                            current_number = previous_number / current_number;
                            break;
                    }
                    current_number = Math.Round(current_number, 10);
                    //shortens the value to not clutter the textbox
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Number is too large, try again");
                }
                catch (DivideByZeroException)
                {
                    MessageBox.Show("Dividing by zero is not possible, try again");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }//end try-catch

                current_number_display.Text = current_number.ToString();
                previous_number = current_number;
            }//end if
        }
        private void decimal_click(object sender, EventArgs e)
        {
            //checks system language to determine which decimal to use            
            if (ci.Name == "en-US" || ci.Name == "en-GB")
            {
                if (!current_number_display.Text.Contains("."))
                    current_number_display.Text += ".";
            }
            else
            {
                if (!current_number_display.Text.Contains(','))
                    current_number_display.Text += ",";
            }
        }
        private void plus_minus_switch_click(object sender, EventArgs e)
        {
            
            current_number = decimal.Parse(current_number_display.Text);
            current_number *= -1; //switches input between negative and positive values
            current_number_display.Text = current_number.ToString();
            current_number = 0;
        }
        private void clear_all_click(object sender, EventArgs e)
        {
            //resets everything
            current_number = 0;
            current_number_display.Text = "0";
            previous_number = 0;
            previous_number_display.Text = "";
            chosen_operator = '0';
        }
        private void clear_entry_click(object sender, EventArgs e)
        {
            //resets current number
            current_number = 0;
            current_number_display.Text = "0";
        }
    }
}