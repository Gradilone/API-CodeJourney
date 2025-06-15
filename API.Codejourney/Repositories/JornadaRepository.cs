using API.Codejourney.Data;
using API.Codejourney.Models;

namespace API.Codejourney.Repositories
{
    public class JornadaRepository : IJornadaRepository
    {

        private readonly ApplicationDbContext _connection;

        public JornadaRepository(ApplicationDbContext context)
        {
            _connection = context;
        }

        public void Insert(Jornadas jornada)
        { 
            _connection.Jornadas.Add(jornada);
            _connection.SaveChanges();
        }

        public List<Jornadas> GetJornadas(string jornNome, int usuarioId)
        {
           return _connection.Jornadas
                      .Where(j => j.UsuarioId == usuarioId && j.JornNome == jornNome)
                      .ToList();
        }

        public void UpdateJornada(int id, Jornadas jornadaAtualizada)
        {
            var jornadaExistente = _connection.Jornadas.Find(id);

            if (jornadaExistente != null)
            {
                if (!string.IsNullOrWhiteSpace(jornadaAtualizada.JornNome))
                {
                    jornadaExistente.JornNome = jornadaAtualizada.JornNome;
                }


                if (!string.IsNullOrWhiteSpace(jornadaAtualizada.JornFase))
                {
                    jornadaExistente.JornFase = jornadaAtualizada.JornFase;
                }

                if (jornadaAtualizada.JornEstrelas > 0)
                {
                    jornadaExistente.JornEstrelas = jornadaAtualizada.JornEstrelas;
                }

                if (jornadaAtualizada.JornUltimaFase > 0)
                {
                    jornadaExistente.JornUltimaFase = jornadaAtualizada.JornUltimaFase;
                }

                _connection.SaveChanges();
            }
        }
    }
}
