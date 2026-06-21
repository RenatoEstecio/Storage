using Library.BLL;
using Library.DTO;

public class UserRepository : IUserRepository
{
    private readonly DataBaseBLL _db;

    public UserRepository(DataBaseBLL db)
    {
        _db = db;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _db.GetUserByEmail(email);
    }
}
