using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialize_People
{ 
[Serializable]
  class Person : IDeserializationCallback
    {
      
        public string name;
        public DateTime dateOfBirth;
        [NonSerialized] public int age;

        void IDeserializationCallback.OnDeserialization(Object sender)
        {
            // ����� �������������� ��������� ������� 
            CalculateAge();
        }
        public Person(string _name, DateTime _dateOfBirth)
        {
            name = _name;
            dateOfBirth = _dateOfBirth;
            CalculateAge();
        }

        private static void Serialize(Person sp)
        {
            // ������� ���� ��� ���������� ������
            FileStream fs = new FileStream("Person.Dat", FileMode.Create);
            // ������� ������ BinaryFormatter ��� ���������� ������������ 
        BinaryFormatter bf = new BinaryFormatter();

            // ���������� ������ BinaryFormatter ��� ������������ ������ � ���� 
            bf.Serialize(fs, sp);
            // ��������� ���� 
            fs.Close();
        }

        public Person()
        {
        }

        public override string ToString()
        {
            return name + " was born on " + dateOfBirth.ToShortDateString() + " and is " + age.ToString() + " years old.";
        }

        private void CalculateAge()
        {
            age = DateTime.Now.Year - dateOfBirth.Year;

            // If they haven't had their birthday this year, 
            // subtract a year from their age
            if (dateOfBirth.AddYears(DateTime.Now.Year - dateOfBirth.Year) > DateTime.Now)
            {
                age--;
            }
        }

        
    }
}
