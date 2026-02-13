using System;

public partial class Airline
{
    private string destination = "";
    private int flightNumber;
    private string airplaneType = "";
    private TimeSpan departureTime;
    private string[] daysOfWeek = Array.Empty<string>();

    private readonly int id;

    public const string CompanyName = "Международные авиакомпании";

    private static int objectCount;
    static Airline()
    {
        objectCount = 0;
    }

    private Airline()
    {
        id = new Random().Next();
        objectCount++;
    }

    public Airline(string destination, int flightNumber, string airplaneType, TimeSpan departureTime, string[] daysOfWeek)
        : this()
    {
        Destination = destination;
        FlightNumber = flightNumber;
        AirplaneType = airplaneType;
        DepartureTime = departureTime;
        DaysOfWeek = daysOfWeek;
    }

    public Airline(string destination, int flightNumber)
            : this(destination, flightNumber, "Самолет по умолчанию", new TimeSpan(12, 0, 0), new string[] { "Понедельник" })
    {
    }
}