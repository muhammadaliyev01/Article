using Articles.DataAccess.Interfaces.Repository;
using Articles.Domain.Contats;
using Articles.Domain.Entites;
using Npgsql;

namespace Articles.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly NpgsqlConnection _con = new NpgsqlConnection(DbContats.CONNECTION_STRING);
        public async Task<bool> CreateAsync(User item)
        {
            try
            {
                await _con.OpenAsync();
                string query = "INSERT INTO public.Users(full_name, email, password_hash, salt, image_path, created_at)" +
                    "VALUES (@full_name, @email, @password_hash, @salt, @image_path, @created_at);";
                NpgsqlCommand cmd = new NpgsqlCommand(query, _con)
                {
                    Parameters =
                {
                    new("full_name",item.FullName),
                    new("email",item.Email),
                    new("password_hash",item.PasswordHash),
                    new("salt",item.Salt),
                    new("image_path",item.ImagePath),
                    new("created_at",item.Created)
                }
                };
                var resalt = await cmd.ExecuteNonQueryAsync();
                return resalt > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                _con.Close();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _con.OpenAsync();
                string query = $"delete from Users where id = {id};";
                NpgsqlCommand cmd = new NpgsqlCommand(query, _con);
                var resalt = await cmd.ExecuteNonQueryAsync();
                return resalt > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                _con.Close();
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                await _con.OpenAsync();
                string query = "SELECT * FROM public.Users ORDER BY id ASC";
                NpgsqlCommand command = new NpgsqlCommand(query, _con);
                var reader = await command.ExecuteReaderAsync();
                List<User> users = new List<User>();
                while (await reader.ReadAsync())
                {
                    User user = new User()
                    {
                        Id=reader.GetInt32(0),
                        FullName=reader.GetString(1),
                        Email=reader.GetString(2),
                        PasswordHash=reader.GetString(3),
                        Salt=reader.GetString(4),
                        ImagePath=reader.GetString(5),
                        Created=reader.GetDateTime(6),
                    };
                    users.Add(user);
                }
                return users;
            }
            catch
            {
                return new List<User>();
            }
            finally
            {
                _con.Close();
            }
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            try
            {
                await _con.OpenAsync();
                string query = $"SELECT * FROM public.Users where id={id};";
                NpgsqlCommand cmd = new NpgsqlCommand(query, _con);
                var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new User()
                    {
                        Id = reader.GetInt32(0),
                        FullName = reader.GetString(1),
                        Email = reader.GetString(2),
                        PasswordHash = reader.GetString(3),
                        Salt = reader.GetString(4),
                        ImagePath = reader.GetString(5),
                        Created = reader.GetDateTime(6),
                    };
                }
                else return null;

            }
            catch
            {
                return null;
            }
            finally
            {
                _con.Close();
            }
        }

        public async Task<bool> UpdateAsync(int id, User item)
        {
            try
            {
                await _con.OpenAsync();
                string query = "update tasks set full_name=@full_name, email=@email, password_hash=@password_hash, salt=@salt, image_path=@image_path, created_at=@created_at " +
                               $"where id={id}";
                NpgsqlCommand command = new NpgsqlCommand(query, _con)
                {
                    Parameters =
                    {
                        new("full_name",item.FullName),
                        new("email",item.Email),
                        new("password_hash",item.PasswordHash),
                        new("salt",item.Salt),
                        new("image_path",item.ImagePath),
                        new("created_at",item.Created)
                    }
                };
                int result = await command.ExecuteNonQueryAsync();

                if (result == 0) return false;
                else return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                await _con.CloseAsync();
            }
        }
    }
}
