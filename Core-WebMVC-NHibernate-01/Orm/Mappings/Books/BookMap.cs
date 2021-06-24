using Core_WebMVC_NHibernate_01.Models.Books;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebMVC_NHibernate_01.Orm.Mappings.Books
{
    public class BookMap : ClassMapping<Book>
    {
        public BookMap()
        {
            this.Schema("Core");
            this.Table("Books");

            this.Id(x => x.Id, x =>
            {
                x.Column("BookId");
                x.Type(NHibernateUtil.Guid);
                x.Generator(Generators.Guid);
                x.UnsavedValue(Guid.Empty);
            });

            this.Property(x => x.Title, x => {
                x.Type(NHibernateUtil.StringClob);
                x.Length(50);
                x.NotNullable(true);
            });
        }

    }
}
