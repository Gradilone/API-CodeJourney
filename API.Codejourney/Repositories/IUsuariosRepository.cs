using API.Codejourney.Models;

namespace API.Codejourney.Repositories
{
    public interface IUsuariosRepository
    {
        public List<Usuario> GetUsuarios();

        public Usuario Get(int id);

        public void Insert(Usuario usuario);

        public void Update(Usuario usuario);

        public void Delete(int id);  
    }
}
