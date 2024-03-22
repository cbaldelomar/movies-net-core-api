using Microsoft.EntityFrameworkCore;

namespace Movies.Infrastructure.Persistence;

internal sealed class MySqlFunctions(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public byte[] GuidToBin(Guid value)
    {
        return _context.Database
            .SqlQuery<byte[]>($"SELECT UUID_TO_BIN('{value}', true)")
            .FirstOrDefault()!;

    }

    public Guid BinToGuid(byte[] value)
    {
        return _context.Database
            .SqlQuery<Guid>($"SELECT BIN_TO_UUID({value}, true)")
            .FirstOrDefault();
    }
}
