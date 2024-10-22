using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstract
{
    public interface IRepositoryManager
    {
        IBookRepository BookRepo { get; }
        ICategoryRepository CategoryRepo { get; }
        Task SaveAsync();
    }
}
