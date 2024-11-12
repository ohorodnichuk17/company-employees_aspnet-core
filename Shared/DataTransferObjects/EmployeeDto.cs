using System.Runtime.Serialization;

namespace Shared.DataTransferObjects;

[DataContract]
public record EmployeeDto
{
    [DataMember]
    public Guid Id { get; init; }
    [DataMember]
    public string Name { get; init; }
    [DataMember]
    public int Age { get; init; }
    [DataMember]
    public string Position { get; init; }

    public EmployeeDto(Guid id, string name, int age, string position)
    {
        Id = id;
        Name = name;
        Age = age;
        Position = position;
    }
}