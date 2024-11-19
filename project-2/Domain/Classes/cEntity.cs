namespace Domain;

public abstract class cEntity
{
    public long Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
}