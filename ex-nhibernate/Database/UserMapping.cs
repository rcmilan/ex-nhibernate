using ex_nhibernate.Domain;
using FluentNHibernate.Mapping;

namespace ex_nhibernate.Database;

public class UserMapping : ClassMap<User>
{
    public UserMapping()
    {
        Table($"[{nameof(User)}]");

        Id(u => u.Id).GeneratedBy.Identity();
        Map(u => u.Name).Not.Nullable();
        Map(u => u.CreatedAt).Not.Nullable();
    }
}
