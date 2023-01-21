using ServerSide.Models;
using Broker.Requests;

namespace ServerSide.Data.Repositories
{
    public interface ILibraryRepository
    {
        public bool DoesSameAddressExist(string address);
        public List<DbLibraries> Read();
        public Task<Guid> Create(DbLibraries library);
        public Task<Guid> Update(UpdateLibraryRequest request);
        public Task<Guid> Delete(DeleteLibraryRequest request);
    }

    public class LibraryRepository : ILibraryRepository
    {
        private LibDbContext _context;

        public LibraryRepository(LibDbContext context)
        {
            _context = context;
        }

        public bool DoesSameAddressExist(string address)
        {
            return _context.Libraries.Any( lib => lib.Address == address);
        }

        public async Task<Guid> Create(DbLibraries library)
        {
            _context.Libraries.Add(library);
            _context.SaveChangesAsync();
            return library.Id;
        }

        public List<DbLibraries> Read()
        {
            return _context.Libraries.ToList();
        }

        public async Task<Guid> Update(UpdateLibraryRequest request)
        {
            var lib = _context.Libraries.FirstOrDefault(x => x.Address == request.OldAddress);

            lib.Address = request.NewAddress;
            _context.SaveChangesAsync();

            return lib.Id;
        }

        public async Task<Guid> Delete(DeleteLibraryRequest request)
        {
            var lib = _context.Libraries.FirstOrDefault(x => x.Address == request.Address);

            Guid id = lib.Id;
            _context.Libraries.Remove(lib);
            _context.SaveChangesAsync();

            return id;
        }
    }
}
