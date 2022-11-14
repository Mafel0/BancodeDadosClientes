using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TarefaBancodeDados
{
    public partial class frmExcluir : Form
    {
        MySqlConnection Conexao = new MySqlConnection(
           "Persist Security Info = False;" + // não pedir usuário a senha para conectar BD 
           "server=localhost;" + // local onde está o banco de dados
           "database=meuteste;" + //nome do banco de dados
           "uid=root;" + // usuário do banco de dados
           "password=suasenha;" + // senha do banco de dados
           "pwd="
           );

        string ID = null;

        public frmExcluir(string ID)
        {
            InitializeComponent();
            try
            {
                Conexao.Open();
                if (Conexao.State == ConnectionState.Open)
                {
                    MySqlCommand comandoSQL = Conexao.CreateCommand();
                    comandoSQL.CommandText = "SELECT * FROM REGIST WHERE idCliente =" + ID;

                    comandoSQL.Connection = Conexao;

                    MySqlDataReader dadosCliente = comandoSQL.ExecuteReader();

                    dadosCliente.Read();

                    txtNome.Text = dadosCliente["NOME"].ToString();
                    txtEndereco.Text = dadosCliente["ENDERECO"].ToString();
                    txtCidade.Text = dadosCliente["CIDADE"].ToString();
                    txtEstado.Text = dadosCliente["ESTADO"].ToString();
                    txtFone.Text = dadosCliente["TELEFONE"].ToString();
                    txtCep.Text = dadosCliente["CEP"].ToString();
                    txtEmail.Text = dadosCliente["EMAIL"].ToString();

                    Conexao.Close();
                    Conexao.Dispose();
                }

            }
            catch (Exception Erro)
            {

                MessageBox.Show
                    ("Houve um erro na comunicação com o banco de dados:\n\n" + Erro.ToString(),
                    "Informação de Erro!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show(
                "Deseja realmente EXCLUIR esse registro?",
                "Informação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Error)== DialogResult.Yes)
            {
                Conexao.Open();
                if(Conexao.State==ConnectionState.Open)
                {
                    MySqlCommand comandoSQL = Conexao.CreateCommand();

                    comandoSQL.Connection = Conexao;
                    comandoSQL.CommandText = "DELETE FROM REGIST WHERE idCliente=?codigo";
                    comandoSQL.Parameters.Add("?codigo", MySqlDbType.VarChar).Value = ID;
                    comandoSQL.ExecuteNonQuery();

                    Conexao.Close();
                    Conexao.Dispose();

                    MessageBox.Show(
                        "Registro excluido com sucesso!",
                        "Informação",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.Close();
                }
            }                
        }
    }
}
