using System.Text.RegularExpressions;
using System;
using System.ComponentModel;

namespace ПР4
{
    public class ComplexNumberFormatException : FormatException
    {
        public ComplexNumberFormatException(string message) : base(message) { }
    }

    struct ComplexNumber
    {
        public double real;
        public double imaginary;

        public ComplexNumber(double real, double imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.real + b.real, a.imaginary + b.imaginary);
        }

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.real - b.real, a.imaginary - b.imaginary);
        }

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.real * b.real - a.imaginary * b.imaginary, a.real * b.imaginary + a.imaginary * b.real);
        }

        public static ComplexNumber operator /(ComplexNumber c1, ComplexNumber c2)
        {
            double denominator = c2.real * c2.real + c2.imaginary * c2.imaginary;
            double real = (c1.real * c2.real + c1.imaginary * c2.imaginary) / denominator;
            double imaginary = (c1.imaginary * c2.real - c1.real * c2.imaginary) / denominator;
            return new ComplexNumber(real, imaginary);
        }
        public static bool operator ==(ComplexNumber a, ComplexNumber b)
        {
            return a.real == b.real && a.imaginary == b.imaginary;
        }

        public static bool operator !=(ComplexNumber a, ComplexNumber b)
        {
            return !(a == b);
        }
        public override string ToString()
        {
            return $"{real} + {imaginary}i";
        }
        public static ComplexNumber Parse(string num)
        {
            string pattern = @"(?<left>\d+)\s*\+\s*(?<right>\d+)";
            Match match = Regex.Match(num, pattern);
            int left, right;
            if (match.Success)
            {
                if (!int.TryParse(match.Groups["left"].Value, out left) ||
                    !int.TryParse(match.Groups["right"].Value, out right))
                {
                    throw new ComplexNumberFormatException("Неверный формат комплексного числа");
                }
                return new ComplexNumber(left, right);
            }
            else
            {
                throw new ComplexNumberFormatException("Неверный формат комплексного числа");
            }
        }
        public static bool TryParse(string str_complex, out ComplexNumber complex)
        {
            try
            {
                complex = ComplexNumber.Parse(str_complex);
                return true;
            }
            catch (ComplexNumberFormatException)
            {
                complex = new ComplexNumber(0, 0);
                return false;
            }
        }
    }
}