using System;

namespace ConsoleCalculator
{
    public class Calculator
    {   /* Initial value declaration */
        string alphaValue1 = "", alphaValue2 = "", _finalAnswer = "";
        char sign;
        int place = 1, holdDot = 0;
        double numValue1 = 0, numValue2 = 0;

        public string SendKeyPress(char key)
        {  
            /* decimal is acceptable only one time */
            if (key == '.') holdDot = 1;    

            /* Handling decimal values */
            if (holdDot == 1 && !isSign(key))
            {

                if (place == 1)
                {
                    if (key == '.' && !alphaValue1.Contains("."))
                    { alphaValue1 += key; }
                    else if (key != '.') alphaValue1 += key;
                    return (alphaValue1);

                }
                else if (place == 2 && !alphaValue1.Contains("."))
                {
                    if (key == '.' && !alphaValue1.Contains("."))
                        alphaValue2 += key;
                    else if (key != '.') alphaValue2 += key;
                    return (alphaValue2);
                }

            }
            else if (key == '0' && holdDot == 0 && alphaValue1 == "0")
            {
                if (place == 1)
                {
                    if (alphaValue1 == "" || alphaValue1 == "0") alphaValue1 = "0";
                    return (alphaValue1);
                }
                else if (place == 2)
                {
                    if (alphaValue1 == "" || alphaValue1 == "0") alphaValue1 = "0"; return (alphaValue1);
                }
            }
            else if (Char.IsNumber(key) && place == 1)  // Concatenating 1st operand
            {
                alphaValue1 += key;
                return (alphaValue1);
            }
            else if (Char.IsNumber(key) && place == 2)  // Concatenating 1st operand
            {
                alphaValue2 += key;
                return (alphaValue2);
            }
            else if (key == '+' || key == '/' || key == '*' || key == '-')  // Handling Operator
            {
                holdDot = 0;
                if (place == 1)
                {
                    numValue1 = Convert.ToDouble(alphaValue1);
                    place = 2;
                    sign = key;
                    return alphaValue1;
                }
                else if (place == 2)
                {
                    if (alphaValue2 != "") numValue2 = Convert.ToDouble(alphaValue2);
                    else numValue2 = 0;
                    alphaValue2 = "";
                    numValue1 = Convert.ToDouble(getResult(numValue1, numValue2, sign));
                    sign = key;
                    return (alphaValue1);
                }

            }
            else if (key == '=')   //Computing Result
            {
                if (alphaValue2 == "") _finalAnswer = alphaValue1;
                else
                {
                    numValue2 = Convert.ToDouble(alphaValue2);
                    _finalAnswer = (getResult(numValue1, numValue2, sign));

                }
                return _finalAnswer;
            }
            else if (key == 'c')    // Clearing Console
            {
                alphaValue1 = "0"; alphaValue2 = "0";
                numValue1 = 0; numValue2 = 0;
                place = 1;
                return ("0");
            }
            else if (key == 's')   // Toggling Value
            {
                if (place == 1)
                {
                    alphaValue1 = "-" + alphaValue1;
                    numValue1 = (Convert.ToDouble(alphaValue1));
                    return (numValue1.ToString());
                }
                else
                {
                    alphaValue2 = "-" + alphaValue2;
                    numValue2 = (Convert.ToDouble(alphaValue2));
                    return (numValue2.ToString());
                }

            }
            else
            {
                return alphaValue1;
            }
            return _finalAnswer;
        }

        public bool isSign(char key)   // Checking for sign
        {
            return (key == '+' || key == '/' || key == '*' || key == '-' || key == '=' || key == 'c' || key == 's');
        }
        /* Computing values after performing operation */
        public string getResult(double alphaValue1, double alphaValue2, char sign)
        {
            string result = "";
            switch (sign)
            {
                case '+': result = (alphaValue1 + alphaValue2).ToString(); break;
                case '-': result = (alphaValue1 - alphaValue2).ToString(); break;
                case '*': result = (alphaValue1 * alphaValue2).ToString(); break;
                case '/':
                    {
                        if (alphaValue2 != 0)
                            result = (alphaValue1 / alphaValue2).ToString();
                        else result = "-E-";
                        break;
                    }
                default: result = (alphaValue1).ToString(); break;
            }
            return result;
        }

        public string getAnswer()   // Return final answer
        {
            return _finalAnswer;
        }


    }

    class tester
    {
        static void Main(string[] args)
        {
            Calculator p = new Calculator();
            string s = "10/2+3s=";
            foreach (char x in s)
            {
                Console.Write(p.SendKeyPress(x) + " ");
            }
            Console.WriteLine();
            Console.Write("Result: " + p.getAnswer());
        }
    }
}
