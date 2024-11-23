namespace ex_nhibernate.Domain;

public class User
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual DateTime CreatedAt { get; set; }
}
