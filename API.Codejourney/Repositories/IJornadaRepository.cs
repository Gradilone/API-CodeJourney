using API.Codejourney.Models;

namespace API.Codejourney.Repositories
{
    public interface IJornadaRepository
    {
        public void Insert(Jornadas jornada, int usuarioId);
        public List<Jornadas> GetJornadas(string jornNome, int usuarioId);
        public void UpdateJornada(int id, Jornadas jornadaAtualizada);
    }
}
