namespace EarthColors.Domain.Entities;

using System;

public class Vote : Entity {
    public Guid CountryId { get; set; }
    public string Color { get; set; }
};