using apipessoa.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace apipessoa.Repositories
{
    public class PessoaDAO
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PessoaDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public IEnumerable<Pessoa> consuteTodas()
        {
            using(IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT P.*, E.*, T.*, TT.* FROM PESSOA P
                                    INNER JOIN ENDERECO E ON P.endereco = E.id
                                    LEFT JOIN PESSOA_TELEFONE PT ON P.id = PT.id_pessoa
                                    LEFT JOIN TELEFONE T ON PT.id_telefone = T.id
                                    LEFT JOIN TELEFONE_TIPO TT ON t.tipo = TT.id";
                dbConnection.Open();
                var pessoaDict = new Dictionary<int, Pessoa>();
                var pessoas = dbConnection.Query<Pessoa, Endereco, Telefone, TelefoneTipo, Pessoa>(
                    query,
                    (P, E, T, TT) =>
                    {
                        if (!pessoaDict.TryGetValue(P.Id, out var currentPessoa))
                        {
                            currentPessoa = P;
                            currentPessoa.EnderecoObj = E;
                            currentPessoa.Telefones = new List<Telefone>();
                            pessoaDict.Add(currentPessoa.Id, currentPessoa);
                        }

                        if (T != null)
                        {
                            T.TipoObj = TT;
                            currentPessoa.Telefones.Add(T);
                        }
                        return currentPessoa;
                    },
                    splitOn: "id"

                    );
                return pessoas.Distinct();
            }
        }

        public Pessoa consulte(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT P.*, E.*, T.*, TT.* FROM PESSOA P
                                    INNER JOIN ENDERECO E ON P.endereco = E.id
                                    LEFT JOIN PESSOA_TELEFONE PT ON P.id = PT.id_pessoa
                                    LEFT JOIN TELEFONE T ON PT.id_telefone = T.id
                                    LEFT JOIN TELEFONE_TIPO TT ON t.tipo = TT.id
                                    WHERE P.id = @id"; //DIFERENÇA EM RELAÇÃO A CONSULTAR TODAS
                dbConnection.Open();
                var pessoaDict = new Dictionary<int, Pessoa>();
                var pessoa = dbConnection.Query<Pessoa, Endereco, Telefone, TelefoneTipo, Pessoa>(
                    query,
                    (P, E, T, TT) =>
                    {
                        if (!pessoaDict.TryGetValue(P.Id, out var currentPessoa))
                        {
                            currentPessoa = P;
                            currentPessoa.EnderecoObj = E;
                            currentPessoa.Telefones = new List<Telefone>();
                            pessoaDict.Add(currentPessoa.Id, currentPessoa);
                        }

                        if (T != null)
                        {
                            T.TipoObj = TT;
                            currentPessoa.Telefones.Add(T);
                        }
                        return currentPessoa;
                    },
                    new {Id = id },//diferença
                    splitOn: "id"
                    ).FirstOrDefault();//diferença
                return pessoa;//diferença
            }
        }

        public int insira(Pessoa pessoa)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        //Inserir Endereço
                        string enderecoQuery = @"INSERT INTO ENDERECO (logradouro, numero, cep, BAIRRO, cidade, estado)
                                             VALUES (@Logradouro, @Numero, @Cep, @Bairro, @Cidade, @Estado);
                                             SELECT CAST(SCOPE_IDENTITY() as int)";
                        var enderecoId = dbConnection.Query<int>(enderecoQuery, pessoa.EnderecoObj, transaction).Single();
                        pessoa.Endereco = enderecoId;

                        //Inserir Endereço
                        string pessoaQuery = @"INSERT INTO PESSOA (nome, cpf, endereco)
                                             VALUES (@Nome, @Cpf, @Endereco);
                                             SELECT CAST(SCOPE_IDENTITY() as int)";
                        var pessoaId = dbConnection.Query<int>(pessoaQuery, pessoa, transaction).Single();
                        pessoa.Id = pessoaId;

                        //Inserir Telefones
                        if (pessoa.Telefones != null && pessoa.Telefones.Count > 0)
                        {
                            foreach (var telefone in pessoa.Telefones)
                            {
                                //inserir telefone
                                string telefoneQuery = @"INSERT INTO TELEFONE (numero, ddd, tipo)
                                             VALUES (@Numero, @Ddd, @Tipo);
                                             SELECT CAST(SCOPE_IDENTITY() as int)";
                                var telefoneId = dbConnection.Query<int>(telefoneQuery, telefone, transaction).Single();
                                telefone.Id = telefoneId;

                                //inserir pessoa ao telefone
                                string pessoaTelQuery = @"INSERT INTO PESSOA_TELEFONE (id_pessoa, id_telefone)
                                             VALUES (@IdPessoa, @IdTelefone)";
                                dbConnection.Execute(pessoaTelQuery, new { IdPessoa = pessoaId, IdTelefone = telefoneId }, transaction);
                            }
                        }
                        transaction.Commit();
                        return pessoaId;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    
                }
            }
        }

        public void Altere(Pessoa pessoa)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        //Atualiza Endereço
                        pessoa.EnderecoObj.Id = pessoa.Endereco;
                        string enderecoQuery = @"UPDATE ENDERECO SET logradouro = @Logradouro, numero = @Numero, cep = @Cep,
                                                bairro = @Bairro, cidade = @Cidade, estado = @Estado
                                                WHERE id = @Id";
                        dbConnection.Execute(enderecoQuery, pessoa.EnderecoObj, transaction: transaction);
                        pessoa.Endereco = pessoa.EnderecoObj.Id;

                        //Atualiza Pessoa
                        string pessoaQuery = @"UPDATE PESSOA SET nome = @Nome, cpf = @Cpf
                                                WHERE id = @Id";
                        dbConnection.Execute(pessoaQuery, pessoa, transaction: transaction);

                        //Gerenciar Telefones
                        string deletePessoaTelefoneQuery = @"DELETE FROM PESSOA_TELEFONE WHERE id_pessoa = @IdPessoa";
                        dbConnection.Execute(deletePessoaTelefoneQuery, new { IdPessoa = pessoa.Id}, transaction);

                        string deleteOrphanTelefoneQuery = @"DELETE FROM TELEFONE WHERE id NOT IN (SELECT id_telefone FROM PESSOA_TELEFONE)";
                        dbConnection.Execute(deleteOrphanTelefoneQuery, transaction: transaction);

                        //Inserir Telefones
                        if (pessoa.Telefones != null && pessoa.Telefones.Count > 0)
                        {
                            foreach (var telefone in pessoa.Telefones)
                            {
                                //inserir telefone
                                string telefoneQuery = @"INSERT INTO TELEFONE (numero, ddd, tipo)
                                             VALUES (@Numero, @Ddd, @Tipo);
                                             SELECT CAST(SCOPE_IDENTITY() as int)";
                                var telefoneId = dbConnection.Query<int>(telefoneQuery, telefone, transaction).Single();
                                telefone.Id = telefoneId;

                                //inserir pessoa ao telefone
                                string pessoaTelQuery = @"INSERT INTO PESSOA_TELEFONE (id_pessoa, id_telefone)
                                             VALUES (@IdPessoa, @IdTelefone)";
                                dbConnection.Execute(pessoaTelQuery, new { IdPessoa = pessoa.Id, IdTelefone = telefoneId }, transaction);
                            }
                        }
                        transaction.Commit();
                        
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                }
            }
        }

        public void Exclua(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        //excluindo registros de PESSOA_TELEFONE
                        string deletePessoaTelefoneQuery = @"DELETE FROM PESSOA_TELEFONE WHERE id_pessoa = @IdPessoa";
                        dbConnection.Execute(deletePessoaTelefoneQuery, new { IdPessoa = id }, transaction);

                        //excluindo registros orfãos de telefone
                        string deleteOrphanTelefoneQuery = @"DELETE FROM TELEFONE WHERE id NOT IN (SELECT id_telefone FROM PESSOA_TELEFONE)";
                        dbConnection.Execute(deleteOrphanTelefoneQuery, transaction: transaction);

                        //obtendo o id do endereço
                        string endereçoIdQuery = @"SELECT endereco FROM PESSOA WHERE id = @Id";
                        var enderecoId = dbConnection.Query<int>(endereçoIdQuery, new { Id = id }, transaction).SingleOrDefault();

                        //Removendo a pessoa
                        string deletePessoaQuery = @"DELETE FROM PESSOA WHERE id = @Id";
                        dbConnection.Execute(deletePessoaQuery, new { Id = id }, transaction);

                        //DeletaR ENDERECO caso não exista outra pessoa no mesmo
                        string verificarEnderecoQuery = @"SELECT COUNT(*) FROM PESSOA WHERE endereco = @EnderecoId";
                        var count = dbConnection.Query<int>(verificarEnderecoQuery, new { EnderecoId = enderecoId }, transaction).Single();
                        if (count == 0)
                        {
                            string deleteEnderecoQuery = @"DELETE FROM ENDERECO WHERE id = @Id";
                            dbConnection.Execute(deleteEnderecoQuery, new {Id = id}, transaction);
                        }

                        transaction.Commit();

                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                }
            }
        }
    }
}
