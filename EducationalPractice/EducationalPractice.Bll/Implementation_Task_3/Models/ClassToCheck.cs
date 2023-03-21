namespace EducationalPractice.Bll.Implementation_Task_3.Models;
//id, brand (honda, bmw etc), model, registration_number (format: BC1234IC), last_repaired_at (date), bought_at (date), car_mileage.

using System;

public class Car
{
    public Guid CarId { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string RegistrationNumber { get; set; }
    public DateTime LastRepairedAt { get; set; }
    public DateTime BoughtAt { get; set; }
    public int CarMileage { get; set; }

    public Car(string brand, string model, string registrationNumber, DateTime lastRepairedAt,
        DateTime boughtAt, int carMileage)
    {
        CarId = Guid.NewGuid();
        Brand = brand;
        Model = model;
        RegistrationNumber = registrationNumber;
        LastRepairedAt = lastRepairedAt;
        BoughtAt = boughtAt;
        CarMileage = carMileage;

    }
    public Car(){}
}