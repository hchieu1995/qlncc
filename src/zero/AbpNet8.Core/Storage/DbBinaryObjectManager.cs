using Abp.Dependency;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpNet8.Storage
{
    public class DbBinaryObjectManager : IBinaryObjectManager, ITransientDependency
    {
        private readonly IRepository<BinaryObject, Guid> _binaryObjectRepository;

        public DbBinaryObjectManager(IRepository<BinaryObject, Guid> binaryObjectRepository)
        {
            _binaryObjectRepository = binaryObjectRepository;
        }

        public Task<BinaryObject> GetOrNullAsync(Guid id)
        {
            return _binaryObjectRepository.FirstOrDefaultAsync(id);
        }

        public Task SaveAsync(BinaryObject file)
        {
            return _binaryObjectRepository.InsertAsync(file);
        }

        public Task DeleteAsync(Guid id)
        {
            return _binaryObjectRepository.DeleteAsync(id);
        }
    }
}
