using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Actions
{
    public interface IUpdate<T> where T : class
    {
        T Update(T obj);
    }
}
