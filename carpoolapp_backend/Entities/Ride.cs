using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using carpoolapp_backend.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace carpoolapp_backend.Entities;

public class Ride {
    public int Id { get; set; }
    public string DriverName { get; set; }
    public string StartLocation { get; set; }
    public string EndLocation { get; set; }
    public int AvailableSeats { get; set; }
    public int VehicleSeats { get; set; }
    public decimal Price { get; set; }
    public DateTime DepartureDate { get; set; }
    public RideStatus Status { get; set; }
    public DateTime CreationDate { get; set; }

}