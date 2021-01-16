using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Actions
{
    public interface IDelete<T> where T : class
    {
        T Delete(int Id);
    }
}
