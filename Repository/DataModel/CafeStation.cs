﻿using System.ComponentModel.DataAnnotations;


namespace Repository.DataModel;

public class CafeStation
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Description { get; set; }
    public Guid StationId { get; set; }

    public SubwayStation Station { get; set; }
    public CafeStationConfig? Config { get; set; }

    public List<Order> Orders { get; set; } = [];
    public List<CoffeeCup> Coffee { get; set; } = [];
    public List<CafeStationAspNetUser> CafeUsers { get; set; } = [];
    public List<ExclusiveCafeCustomer> ExclusiveUsers { get; set; } = [];

    public List<TimeSheetShiftCafe> TimeSheetsShift { get; set; } = [];
}
