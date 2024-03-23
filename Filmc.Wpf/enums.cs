using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf
{
    public enum CreatedEntityByUrlStatus
    {
        Created,
        BookUrlToFilm,
        FilmUrlToBook,
        LinkNotSupported,
        ErrorReadingLink
    }

    public enum FilmsMenuMode
    {
        Categories,
        Films,
        Series,
        Priorities
    }

    public enum BooksMenuMode
    {
        Categories,
        Books,
        Priorities
    }

    //Нельзя менять имена перечисления
    public enum ScaleEnum
    {
        Small = 0,
        Medium = 1
    }

    public enum StatusEnum
    {
        Normal,
        Saved,
        UnSaved
    }
}
