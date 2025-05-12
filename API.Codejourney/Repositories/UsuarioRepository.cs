namespace API.Codejourney.Repositories;

using System.Data.SqlClient;
using API.Codejourney.Data;
using API.Codejourney.Models;
using Microsoft.EntityFrameworkCore;
using Dapper;

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
            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.UserName = usuario.UserName;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.DataNascimento = usuario.DataNascimento;
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

}
