using ServerSide.Models;
using Broker.Requests;

namespace ServerSide.Mappers
{
    public interface IDbLibraryMapper
    {
        public DbLibraries Map(CreateLibraryRequest request);
    }

    public class DbLibraryMapper : IDbLibraryMapper
    {
        public DbLibraries Map(CreateLibraryRequest request)
        {
            return new DbLibraries()
            {
                Id = Guid.NewGuid(),
                Address = request.Address
            };
        }
    }
}
