using System.Text.RegularExpressions;

namespace EducationalPractice.Bll.Implementation_Task_4.Models;


public class Patient
{

    //Клас ПАЦІЄНТ: id, name, date, time, duration (in minutes), doctor_name, department

        public Guid PatientId { get; set; }
        private string _name;
        public string Name 
        { 
            get { return _name; }
            set 
            {
                if (Regex.IsMatch(value, "^[a-zA-Z]+$"))
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentException("Name can only contain letters.");
                }
            }
        }
        public DateOnly Date { get; set; } 
        public TimeOnly Time { get; set; }

        private uint _duration;
        public uint Duration 
        { 
            get { return _duration; }
            set 
            {
                if (value > 0)
                {
                    _duration = value;
                }
                else
                {
                    throw new ArgumentException("Duration should be a positive number.");
                }
            }
        }
        private string _doctorName;
        public string DoctorName 
        { 
            get { return _doctorName; }
            set 
            {
                if (Regex.IsMatch(value, "^[a-zA-Z]+$"))
                {
                    _doctorName = value;
                }
                else
                {
                    throw new ArgumentException("Doctor name can only contain letters.");
                }
            }
        }
        private string _department;
        public string Department 
        { 
            get { return _department; }
            set 
            {
                if (Regex.IsMatch(value, "^[a-zA-Z]+$"))
                {
                    _department = value;
                }
                else
                {
                    throw new ArgumentException("Department name can only contain letters.");
                }
            }
        }
    public Patient(string name, DateOnly date, TimeOnly time, uint duration, string doctorName, string department)
    {
        PatientId = Guid.NewGuid();
        Name = name;
        Date = date;
        Time = time;
        Duration = duration;
        DoctorName = doctorName;
        Department = department;
    }

    public Patient()
    {
    }
}