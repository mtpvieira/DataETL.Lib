using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataETL.Interfaces
{
    public interface ITransformer<T>
    {
        Task<IEnumerable<T>> TransformAsync(IEnumerable<T> input);
    }
}
