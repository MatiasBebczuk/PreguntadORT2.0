using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace PreguntadORT.Models;

static class BD{
private static string _connectionString = "Server=MATIAS_ILAN\\SQLEXPRESS; DataBase=PreguntadOrt; Trusted_Connection=True;";
    static List<Categorias> ListaCategorias = new List<Categorias>();
    static List<Dificultades> ListaDificultades = new List<Dificultades>();
    static List<Preguntas> ListaPreguntas = new List<Preguntas>();
    static List<Respuestas> ListaRespuestas= new List<Respuestas>();
    public static List<Categorias> ObtenerCategorias()
{
    List<Categorias> listaCategorias;
    using (SqlConnection db = new SqlConnection(_connectionString))
    {
        string sql = "SELECT * FROM Categorias";
        listaCategorias = db.Query<Categorias>(sql).ToList();
    }
    return listaCategorias ?? new List<Categorias>();
}

public static List<Dificultades> ObtenerDificultades()
{
    List<Dificultades> listaDificultades = new List<Dificultades>();
    using (SqlConnection db = new SqlConnection(_connectionString))
    {
        string sql = "SELECT * FROM Dificultades";
        listaDificultades = db.Query<Dificultades>(sql).ToList();
    }
    return listaDificultades; 
}

    public static List<Preguntas> ObtenerPreguntas(int idDificultad, int idCategoria){
        if(idDificultad!=-1 && idCategoria!=-1){
            using(SqlConnection db = new SqlConnection(_connectionString)){
            string sql= "SELECT * FROM Preguntas where IdCategoria=@pIdCategoria and IdDificultad=@pIdDificultad";
            ListaPreguntas = db.Query<Preguntas>(sql, new{pIdCategoria=idCategoria, pIdDificultad=idDificultad}).ToList();
            }
        }
        else if(idDificultad==-1 && idCategoria!=-1){
            using(SqlConnection db = new SqlConnection(_connectionString)){
            string sql= "SELECT * FROM Preguntas where IdCategoria=@pIdCategoria";
            ListaPreguntas = db.Query<Preguntas>(sql, new{pIdCategoria=idCategoria, pIdDificultad=idDificultad}).ToList();
            }
        }
        else if(idCategoria==-1 && idDificultad!=-1){
            using(SqlConnection db = new SqlConnection(_connectionString)){
            string sql= "SELECT * FROM Preguntas where IdDificultad=@pIdDificultad";
            ListaPreguntas = db.Query<Preguntas>(sql, new{pIdCategoria=idCategoria, pIdDificultad=idDificultad}).ToList();
            }
        }
        else{
            using(SqlConnection db = new SqlConnection(_connectionString)){
            string sql= "SELECT * FROM Preguntas";
            ListaPreguntas = db.Query<Preguntas>(sql).ToList();
            }
        }
        return ListaPreguntas;
    }
    public static List<Respuestas> ObtenerRespuestas(int idPregunta){
        using(SqlConnection db = new SqlConnection(_connectionString)){
            string sql= "Select * from Respuestas where IdPregunta=@pIdPregunta";
            ListaRespuestas = db.Query<Respuestas>(sql, new{pIdPregunta=idPregunta}).ToList();
        }
        return ListaRespuestas;
    }
    public static bool EsCorrecta(int idRespuesta){
        bool Correcta=false;
        using(SqlConnection db = new SqlConnection(_connectionString)){
            string sql= "Select Correcta from Respuestas where IdRespuesta=@pIdRespuesta";
            Correcta = db.QueryFirstOrDefault<bool>(sql, new{pIdRespuesta=idRespuesta});
        }
        return Correcta;
    }
}
