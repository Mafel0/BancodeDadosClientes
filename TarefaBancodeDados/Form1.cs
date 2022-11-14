using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// usado para conectar com o mySQL
using MySql.Data.MySqlClient;

namespace TarefaBancodeDados
{
    public partial class Form1 : Form
    {

        MySqlConnection Conexao = new MySqlConnection(
            "Persist Security Info = False;" + // não pedir usuário a senha para conectar BD 
            "server=localhost;" + // local onde está o banco de dados
            "database=meuteste;" + //nome do banco de dados
            "uid=root;" + // usuário do banco de dados
            "password=suasenha;" + // senha do banco de dados
            "pwd="

            );

        // usado para pegar o Id selecionado no datagridview
        string ID = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                //abrindo a conexão com o banco
                Conexao.Open();

                if(Conexao.State == ConnectionState.Open)
                {
                    // limpa o dgvClientes
                    dgvClientes.Rows.Clear();
                    //nmr de colunas
                    dgvClientes.ColumnCount = 4;
                    dgvClientes.Columns[0].Width = 30; //largura da coluna
                    dgvClientes.Columns[0].Name = "ID"; // nome da coluna
                    dgvClientes.Columns[1].Width = 150;
                    dgvClientes.Columns[1].Name = "Nome";
                    dgvClientes.Columns[2].Width = 200;
                    dgvClientes.Columns[2].Name = "Fone";
                    dgvClientes.Columns[3].Width = 300;
                    dgvClientes.Columns[3].Name = "Email";

                    // fazendo um SELECT no banco de dados 
                    MySqlCommand comandoSQL = Conexao.CreateCommand();
                    comandoSQL.CommandText = "SELECT idCliente, NOME, TELEFONE, EMAIL FROM REGIST";

                    // definir a conexão com o banco de dados
                    comandoSQL.Connection = Conexao;

                    // executar o comando 
                    MySqlDataReader dadosClientes = comandoSQL.ExecuteReader();

                    string[] linha;

                    while(dadosClientes.Read())
                    {
                        linha = new string[]
                        {
                            dadosClientes["idCliente"].ToString(),
                            dadosClientes["NOME"].ToString(),
                            dadosClientes["TELEFONE"].ToString(),
                            dadosClientes["EMAIL"].ToString()

                        };
                        dgvClientes.Rows.Add(linha);
                    }

                }

                //fechado a conexão
                Conexao.Close();
            }
            catch (Exception Erro)
            {
                MessageBox.Show
                    ("Houve um erro na comunicação com o banco de dados:\n\n"+Erro.ToString(),
                    "Informação de Erro!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error              
                    );

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnAtualizar.PerformClick();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            frmCadastro frmCad = new frmCadastro();

            frmCad.ShowDialog();
        }

        private void btnSair_Click(object sender, EventArgs e)
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

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (ID != null)
            {
                frmAtualizar frmAtual = new frmAtualizar(ID);

                frmAtual.ShowDialog();

                btnAtualizar.PerformClick();

                ID = null;
            }
            else
            {
                MessageBox.Show(
                    "Selecione um registro!",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }
        }

        private void dgvClientes_MouseClick(object sender, MouseEventArgs e)
        {
            int linhaSelecionada = dgvClientes.CurrentRow.Index;
            int colunaInteresse = 0;

            ID = dgvClientes.Rows[linhaSelecionada].Cells[colunaInteresse].Value.ToString();
            //MessageBox.Show("Valor..: " + ID);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (ID != null)
            {
                frmExcluir frmExc = new frmExcluir(ID);

                frmExc.ShowDialog();

                btnAtualizar.PerformClick();

                ID = null;
            }
            else
            {
                MessageBox.Show(
                    "Selecione um registro!",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }
        }
    }
}
