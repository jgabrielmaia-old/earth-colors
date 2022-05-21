namespace EarthColors.Domain.Entities;

using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Entity {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid Id { get; set; }
}