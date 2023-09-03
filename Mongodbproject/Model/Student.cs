using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mongodbproject.Model;

public class Student
{



    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    [BsonIgnore]
    public IFormFile? Photo { get; set; }
    public string? Pics { get; set; }


}
