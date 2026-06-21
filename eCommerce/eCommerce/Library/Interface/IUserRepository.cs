using Library.DTO;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
}
