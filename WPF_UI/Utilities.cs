using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BE;
using BL;

namespace WPF_UI
{
    public static class Utilities
    {
        /// <summary>
        /// Function that checks if the character is a letter
        /// </summary>
        /// <param name="a"></param>
        /// <returns>True if a is a letter</returns>
        public static bool IsLetter(char a)
        {
            if (((int)a > 64 && (int)a < 91) || ((int)a > 96 && (int)a < 123) || (int)a == 32)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Function that checks if str is a string of words
        /// </summary>
        /// <param name="str"></param>
        /// <returns>True if str is a string of words</returns>
        public static bool IsWords(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!IsLetter(str[i]))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Function to check if character is a number
        /// </summary>
        /// <param name="c"></param>
        /// <returns>True if c is a number</returns>
        public static bool IsNumber(char c)
        {
            if ((int)c > 47 && (int)c < 58)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Function that checks if str is a string of numbers
        /// </summary>
        /// <param name="str"></param>
        /// <returns>True if str is a string of numbers</returns>
        public static bool IsStringNumbers(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!IsNumber(str[i]))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Function that turns the first letter of a word to upper case, and the other letters to lower case.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToProper(string str)
        {
            string s = str[0].ToString().ToUpper();
            for (int i = 1; i < str.Length; i++)
            {
                s += str[i].ToString().ToLower();
            }
            return s;
        }
        /// <summary>
        /// Function to print errors through a message box
        /// </summary>
        /// <param name="str"></param>
        public static void ErrorBox(string str)
        {
            MessageBox.Show(str, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        /// <summary>
        /// Function to print information through a message box
        /// </summary>
        /// <param name="str"></param>
        public static void InformationBox(string str)
        {
            MessageBox.Show(str, "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        /// <summary>
        /// Function to ensure the user wants to do a given action
        /// </summary>
        /// <param name="str">Description of the action the user would like to do</param>
        /// <returns>True if the "yes" button is clicked, and false if "no" button is clicked</returns>
        public static bool AreYouSureBox(string str)
        {
            if (MessageBox.Show("Are you sure you would like to " + str + "?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                return true;
            return false;
        }
        /// <summary>
        /// Function that tries to return the list of testers
        /// </summary>
        /// <returns>The list if it's not empty. Otherwise, null.</returns>
        public static List<Tester> ReturnTesters()
        {
            try
            {
                return FactoryBL.Instance.ReturnTesters();
            }
            catch (Exception ex)
            {
                ErrorBox(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Function that tries to return the list of trainees
        /// </summary>
        /// <returns>The list if it's not empty. Otherwise, null.</returns>
        public static List<Trainee> ReturnTrainees()
        {
            try
            {
                return FactoryBL.Instance.ReturnTrainees();
            }
            catch (Exception ex)
            {
                ErrorBox(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Function that tries to return the list of tests
        /// </summary>
        /// <returns>The list if it's not empty. Otherwise, null.</returns>
        public static List<Test> ReturnTests()
        {
            try
            {
                return FactoryBL.Instance.ReturnTests();
            }
            catch(Exception ex)
            {
                ErrorBox(ex.Message);
                return null;
            }
        }
    }
}
