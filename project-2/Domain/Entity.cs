namespace Domain;

public abstract class Entity
{
    public long Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
}