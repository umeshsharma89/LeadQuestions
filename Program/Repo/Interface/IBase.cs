using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Repo.Interface
{
  public  interface IBase
    {
        void Save<T>(T entity);
        void Delete<T>(T entity);
        T Find<T>(string id);
    }
}
