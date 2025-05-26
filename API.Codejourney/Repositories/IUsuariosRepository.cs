using API.Codejourney.DTO;
using API.Codejourney.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace API.Codejourney.Repositories
{
    public interface IUsuariosRepository
    {
        public List<Usuario> GetUsuarios();

        public Usuario Get(int id);

        public void Insert(Usuario usuario);

        public void Update(Usuario usuario);

        public void Delete(int id);

        public void UpdateNivelEProgresso(int id, int nivel, float progresso);
        public ProgressoDTO GetNivelEProgresso(int id);
    }
}
