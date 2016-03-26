using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfacesTravelMN
{
    public interface IMaintenance<T>
    {
        void Save(T t);
        T Get(int id);
        List<T> GetAll();
    }
}
