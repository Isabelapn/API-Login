using Dapper;
using Login.Data.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Login.Data.Repositories
{
    public class LoginRepository : RepositoryBase
    {
        private object unidade; //saber o que é - pesquisar
        public LoginRepository(IConfiguration configuration)
        {
            base.configuration = configuration;
        }

        public int Insert(LoginEntity login)
        {
            using var db = Connection;

            var query = @"INSERT INTO login 
                                (nome,
                                EMAIL,
                                senha)
                                VALUES (@Nome,
                                        @Email,
                                        @Senha)
                                    RETURNING id_users;";

            return db.ExecuteScalar<int>(query, new
            {
                login.Nome,
                login.Email,
                login.Senha

            });
        }

        public int GetIdByEmail(string email) //busque o Id cujo e-mail é igual a: 
        {
            using var db = Connection;

            var query = @"SELECT id_users
                            FROM login
                                 WHERE email = @email";
            return db.ExecuteScalar<int>(query, new { email });
        }

        public int UpdateDados(LoginEntity login)
        {
            using var db = Connection;

            var query = @"UPDATE login
                            SET nome = @Nome,
                                email = @Email,
                                senha = @Senha
                            WHERE id_users = @IdUsers;";
            return db.Execute(query, new
            {
                login.Nome,
                login.Email,
                login.Senha,
                login.IdUsers //depois apagar
            });
        }

        public LoginEntity GetDadosById(int id)
        {
            using var db = Connection;
            var query = @"SELECT id_users,
                                nome,
                                email,
                                senha,
                                status
                        FROM Login
                        WHERE id_users = @id;";

            return db.QueryFirstOrDefault<LoginEntity>(query, new { id });
        }

        public IEnumerable<LoginEntity> GetAllDados() // traz todos os dados de alguns parametros que vc quiser (todos os emails da tabela)
        {
            using var db = Connection;
            var query = @"SELECT * FROM Login WHERE status = 1;"; //Select *From vai trazer absolutamente todos os dados - inviável

            // var query = @"SELECT id_users,
            //                      nome,
            //                      email,
            //                      senha,
            //                      status
            //                  FROM Login
            //                  WHERE status = 1;";

            return db.Query<LoginEntity>(query);

        }

        public int Delete(int id)
        {
            using var db = Connection;
            var query = @"UPDATE Login " +
                            "SET status = 2" +
                                "WHERE id_users = @id;";
            return db.Execute(query, new { id });
        }
    }
}
