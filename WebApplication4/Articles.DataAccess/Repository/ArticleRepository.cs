using Articles.DataAccess.Interfaces.Repository;
using Articles.Domain.Contats;
using Articles.Domain.Entites;
using Npgsql;

namespace Articles.DataAccess.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly NpgsqlConnection _con = new NpgsqlConnection(DbContats.CONNECTION_STRING);
        public async Task<bool> CreateAsync(Article item)
        {
            try
            {
                await _con.OpenAsync();
                string query = "INSERT INTO public.article( user_id, title, content, created, is_updated, updated_at, image_path)" +
                    "VALUES( @user_id, @title, @content, @created, @is_updated, @updated_at, @image_path); ";
                NpgsqlCommand cmd = new NpgsqlCommand(query, _con)
                {
                    Parameters =
                {
                    new("user_id",item.UserId),
                    new("title",item.Title),
                    new("content",item.Content),
                    new("created",item.Created),
                    new("is_updated",item.IsUpdated),
                    new("updated_at",item.UpdatedAt),
                    new("image_path",item.ImagePath)
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
                string query = $"delete from article where id = {id};";
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

        public async Task<List<Article>> GetAllAsync()
        {
            try
            {
                await _con.OpenAsync();
                string query = "SELECT * FROM public.article ORDER BY id ASC";
                NpgsqlCommand command = new NpgsqlCommand(query, _con);
                var reader = await command.ExecuteReaderAsync();
                List<Article> articles = new List<Article>();
                while (await reader.ReadAsync())
                {
                    Article article = new Article()
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        Title=reader.GetString(2),
                        Content = reader.GetString(3),
                        Created = reader.GetDateTime(4),
                        IsUpdated=reader.GetBoolean(5),
                        UpdatedAt=reader.GetString(6),
                        ImagePath=reader.GetString(7),
                    };
                    articles.Add(article);
                }
                return articles;
            }
            catch
            {
                return new List<Article>();
            }
            finally
            {
                _con.Close();
            }
        }

        public async Task<Article?> GetByIdAsync(int id)
        {
            try
            {
                await _con.OpenAsync();
                string query = $"SELECT * FROM public.article where id={id};";
                NpgsqlCommand cmd = new NpgsqlCommand(query, _con);
                var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Article()
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        Title = reader.GetString(2),
                        Content = reader.GetString(3),
                        Created = reader.GetDateTime(4),
                        IsUpdated = reader.GetBoolean(5),
                        UpdatedAt = reader.GetString(6),
                        ImagePath = reader.GetString(7),
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

        public async Task<bool> UpdateAsync(int id, Article item)
        {
            try
            {
                await _con.OpenAsync();
                string query = "update tasks set user_id=@user_id, title=@title, content=@content, created=@created, is_updated=@is_updated, updated_at=@updated_at, image_path=@image_path " +
                               $"where id={id}";
                NpgsqlCommand command = new NpgsqlCommand(query, _con)
                {
                    Parameters =
                    {
                        new("user_id",item.UserId),
                        new("title",item.Title),
                        new("content",item.Content),
                        new("created",item.Created),
                        new("is_updated",item.IsUpdated),
                        new("updated_at",item.UpdatedAt),
                        new("image_path",item.ImagePath)
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
