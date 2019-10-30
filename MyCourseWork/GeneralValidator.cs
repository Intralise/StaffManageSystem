using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCourseWork
{
    public enum ValidateValues
    {
        EngString,
        RusString,
        Digit,
        Char
    }
    class GeneralValidator
    {
        public GeneralValidator()
        {
            Validator Rus = IsRus;
            Validator Eng = IsEng;
            Validator Digit = IsDigit;
            Validator Char = IsMail;

            _actions = new Dictionary<ValidateValues, Delegate>();
            _actions.Add(ValidateValues.RusString, Rus);
            _actions.Add(ValidateValues.EngString, Eng);
            _actions.Add(ValidateValues.Digit, Digit);
            _actions.Add(ValidateValues.Char, Char);
        }

        public bool Validate(char letter, ValidateValues[] values)
        {
            foreach (ValidateValues value in values)
            {
                if (_actions.ContainsKey(value)) 
                {
                    Validator d;
                    d = (Validator)_actions.FirstOrDefault(t => t.Key == value).Value;
                    if (d(letter) != true) { return false; }
                }
            }
            return true;
        }

        public bool Validate(char letter, ValidateValues value)
        { 
            Validator d;
            d = (Validator)_actions.FirstOrDefault(t => t.Key == value).Value;
            if (d(letter) != true) { return false; }

            return true;
        }

        public bool IsRus(char letter)
        {
            if (letter >= 'а' && letter <= 'я' || letter >= 'А' && letter <= 'Я')
            {
                return true;
            }
            else { return false; }
        }

        public bool IsEng(char letter)
        {
            if (letter >= 'a' && letter <= 'z' || letter >= 'A' && letter <= 'Z')
            {
                return true;
            }
            else { return false; }
        }

        public bool IsDigit(char letter)
        {
            if (Char.IsDigit(letter))
            {
                return true;
            }
            else { return false; }
        }

        public bool IsMail(char letter)
        {
            if (letter == '.' || letter == '@')
            {
                return true;
            }
            else { return false; }
        }

        delegate bool Validator(char letter);

        private Dictionary<ValidateValues, Delegate> _actions;
    }
}
