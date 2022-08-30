using System.Collections.Generic;
using InternationalBusinessMen.Domain.Domains;

namespace InternationalBusinessMen.ApplicationCore.Interfaces
{
    public interface IStorageJsonLocal<T>
    {
        void saveLocalInformation(ICollection<T> data);
        ICollection<T> readLocalInformation();
    }
}
