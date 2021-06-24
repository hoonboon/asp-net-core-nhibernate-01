using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebMVC_NHibernate_01.Models.Books
{
    public class Book
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }

    }
}
