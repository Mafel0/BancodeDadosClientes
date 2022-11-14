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
    public partial class frmCadastro : Form
    {
        MySqlConnection Conexao = new MySqlConnection(
        "Persist Security Info = False;" + // não pedir usuário a senha para conectar BD 
        "server=localhost;" + // local onde está o banco de dados
        "database=meuteste;" + //nome do banco de dados
        "uid=root;" + // usuário do banco de dados
        "password=suasenha;" + // senha do banco de dados
        "pwd="

        );

        public frmCadastro()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            try
            {
                if(Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }

            this.Close();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Conexao.Open();
                if (Conexao.State == ConnectionState.Open)
                {
                    MySqlCommand comandoSQL = Conexao.CreateCommand();

                    comandoSQL.CommandText = "INSERT INTO regist " +
                        "(NOME, ENDERECO, CIDADE, ESTADO, CEP, TELEFONE, EMAIL)" +
                        " VALUES " +
                        "('"+txtNome.Text+"','"+txtEndereco.Text+"','"+txtCidade.Text+"','"+txtEstado.Text+
                        "','"+txtCep.Text+"','"+txtFone.Text+"','"+txtEmail.Text+"')";

                    comandoSQL.Connection = Conexao;
                    comandoSQL.ExecuteNonQuery();

                    Conexao.Close();
                    this.Close();
                }
            }
            catch (Exception Erro)
            {
                MessageBox.Show
                    ("Houve um erro na inserção do novo registro no banco de dados:\n\n" + Erro.ToString(),
                    "Informação de Erro!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }
    }
}
