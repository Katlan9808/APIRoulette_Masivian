using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Actions
{
    public interface IRead<T> where T : class
    {
        IEnumerable<T> Get();
        T GetById(int Id);
    }
}
