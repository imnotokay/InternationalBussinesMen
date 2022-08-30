using System.Threading.Tasks;
using System.Collections.Generic;
using InternationalBusinessMen.Domain.Domains;

namespace InternationalBusinessMen.ApplicationCore.Interfaces
{
    public interface IDownloadJsonService<T>
    {
        Task<ICollection<T>> getJsonData();
    }
}
