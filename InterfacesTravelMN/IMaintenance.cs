using System.Collections.Generic;

namespace InterfacesTravelMN
{
    public interface IMaintenance<T>
    {
        void Save(T t);
        T Get(int id);
        List<T> GetAll();
        List<T> GetAllId(int Id);   
        void Update(T t);
        void Delete(T t);
        List<T> Search(string[] args);
    }
}
