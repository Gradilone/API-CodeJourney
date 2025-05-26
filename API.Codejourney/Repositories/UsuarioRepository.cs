namespace API.Codejourney.Repositories;

using System.Data.SqlClient;
using API.Codejourney.Data;
using API.Codejourney.Models;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Microsoft.AspNetCore.JsonPatch;
using API.Codejourney.DTO;

public class UsuarioRepository : IUsuariosRepository
{
    private readonly ApplicationDbContext _connection;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _connection = context;
    }

    public List<Usuario> GetUsuarios()
    {
        return _connection.Usuarios.ToList();
    }

    public Usuario Get(int id)
    {
        return _connection.Usuarios.FirstOrDefault(x => x.Id == id);
    }

    public void Insert(Usuario usuario)
    {
        _connection.Usuarios.Add(usuario);
        _connection.SaveChanges();
    }

    public void Update(Usuario usuario)
    {
        var usuarioExistente = _connection.Usuarios.FirstOrDefault(x => x.Id == usuario.Id);

        if (usuarioExistente != null)
        {
            if (usuario.Nome != null)
                usuarioExistente.Nome = usuario.Nome;

            if (usuario.UserName != null)
                usuarioExistente.UserName = usuario.UserName;

            if (usuario.Email != null)
                usuarioExistente.Email = usuario.Email;

            if (usuario.DataNascimento != null && usuario.DataNascimento != DateTime.MinValue)
                usuarioExistente.DataNascimento = usuario.DataNascimento;

            if (usuario.Senha != null)
                usuarioExistente.Senha = usuario.Senha;

            _connection.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var usuario = _connection.Usuarios.FirstOrDefault(x => x.Id == id);
        if (usuario != null)
        {
            _connection.Usuarios.Remove(usuario);
            _connection.SaveChanges();
        }
    }

    public void UpdateNivelEProgresso(int id, int nivel, float progresso)
    {
        var usuarioExistente = _connection.Usuarios.FirstOrDefault(x => x.Id == id);

        if (usuarioExistente != null)
        {
            usuarioExistente.Nivel = nivel;
            usuarioExistente.Progresso = progresso;

            _connection.SaveChanges();
        }
    }

    public ProgressoDTO GetNivelEProgresso(int id)
    {
        return _connection.Usuarios
            .Where(u => u.Id == id)
            .Select(u => new ProgressoDTO
            {
                Nivel = u.Nivel,
                Progresso = u.Progresso
            })
            .FirstOrDefault();
    }

}
