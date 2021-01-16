using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Actions
{
    public interface ICreate<T> where T : class
    {
        T Create(T Obj);
    }
}
