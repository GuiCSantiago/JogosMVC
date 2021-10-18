using MVCJogos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCJogos.DAO
{
    public class JogoDAO 
    {
        private SqlParameter[] CriaParametros(JogoViewModel jogo)
        {
            SqlParameter[] p = new SqlParameter[5];

            p[0] = new SqlParameter("id", jogo.Id);
            p[1] = new SqlParameter("descricao", jogo.Descricao);
            p[2] = new SqlParameter("categoriaID", jogo.CategoriaID);
            p[3] = new SqlParameter("data_aquisicao", jogo.DataAquisicao);
            if (jogo.Valor == null)
                p[4] = new SqlParameter("valor_locacao", DBNull.Value);
            else
                p[4] = new SqlParameter("valor_locacao", jogo.Valor);

            return p;
        }

        public void Inserir(JogoViewModel jogo)
        {
            string sql = "insert into jogos (id, descricao, valor_locacao, data_aquisicao, categoriaID) " +
                "values (@id, @descricao, @valor_locacao, @data_aquisicao, @categoriaID)";

            HelperDAO.ExecutaSQL(sql, CriaParametros(jogo));
        }


        public void Alterar(JogoViewModel jogo)
        {
            string sql = "update jogos set descricao = @descricao, " +
                "valor_locacao = @valor_locacao, data_aquisicao = @data_aquisicao, " +
                "categoriaID = @categoriaID  where id  = @id";
            HelperDAO.ExecutaSQL(sql, CriaParametros(jogo));
        }

        public void Excluir(int id)
        {
            string sql = "delete jogos where id = " + id;
            HelperDAO.ExecutaSQL(sql, null);
        }
    
        public JogoViewModel Consulta(int id)
        {
            string sql = "select * from jogos where id = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        public List<JogoViewModel> Lista()
        {
            string sql = "select * from jogos";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            List<JogoViewModel> retorno = new List<JogoViewModel>();

            foreach (DataRow registro in tabela.Rows)
            {
                retorno.Add(MontaModel(registro));
            }

            return retorno;
        }

        public static JogoViewModel MontaModel(DataRow registro)
        {
            JogoViewModel Jogo = new JogoViewModel();
            Jogo.Id = Convert.ToInt32(registro["id"]);
            Jogo.Descricao = registro["descricao"].ToString();
            Jogo.CategoriaID = Convert.ToInt32(registro["categoriaID"]);
            Jogo.Valor = Convert.ToDouble(registro["valor_locacao"]);
            Jogo.DataAquisicao = Convert.ToDateTime(registro["data_aquisicao"]);
            return Jogo;
        }

        public int ProximoId()
        {
            string sql = "select isnull(max(id) +1, 1) as 'MAIOR' from jogos";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
        }
    }
}
