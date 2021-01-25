using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Model
{
    public interface IRepository<T, TKey>
    {
        List<T> FindAll();
        T FindBy(TKey key);
        TKey Add(T item);
        bool Delete(T item);
        T Save(T item);
    }
}
