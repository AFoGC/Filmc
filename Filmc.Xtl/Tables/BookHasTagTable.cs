using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Tables
{
    public class BookHasTagTable : Table<BookHasTag>
    {

        public BookHasTagTable()
        {

        }

        public override bool Remove(BookHasTag item)
        {
            if (this.Contains(item))
            {
                item.TagId = 0;
                item.BookId = 0;
            }

            return base.Remove(item);
        }

        protected override void OnLoaded()
        {
            foreach (var item in this)
                item.BookId = item.BookId;

            base.OnLoaded();
        }
    }
}
