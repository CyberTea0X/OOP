using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПР1_проект
{
    internal class Person
    {
        public string name;   // имя
        public int age;       // возраст
        public Person()
        {
            Console.WriteLine("Создание объекта Person");
            name = "Tom";
            age = 37;
        }
    }
}
