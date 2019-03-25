using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Ex_Crud
{
    public partial class formCadastro : Form
    {
        MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=bd_crud;UID=root;PASSWORD=;");


        public formCadastro()
        {
            InitializeComponent();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
                MySqlCommand comandos = new MySqlCommand("insert into tb_dados(codigo, nome, email, cidade, estado) values (null, ?, ?, ?, ?)", conexao);
                comandos.Parameters.Add("@nome", MySqlDbType.VarChar, 30).Value = txtNome.Text;
                comandos.Parameters.Add("@email", MySqlDbType.VarChar, 30).Value = txtEmail.Text;
                comandos.Parameters.Add("@cidade", MySqlDbType.VarChar, 30).Value = cmbCidade.SelectedItem.ToString();
                comandos.Parameters.Add("@estado", MySqlDbType.VarChar, 2).Value = cmbEstado.SelectedItem.ToString();

                txtNome.Text = "";
                txtEmail.Text = "";
                cmbCidade.Text = "";
                cmbEstado.Text = "";

                comandos.ExecuteNonQuery();

                MessageBox.Show("Cadastrado com sucesso!");
                conexao.Close();
            }

            catch
            {
                MessageBox.Show("Não conectado");
            }
        }

        private void formCadastro_Load(object sender, EventArgs e)
        {
            cmbEstado.Items.Add("CE"); cmbEstado.Items.Add("BA"); cmbEstado.Items.Add("PE");
            cmbCidade.Items.Add("Fortaleza"); cmbCidade.Items.Add("Salvador"); cmbCidade.Items.Add("Recife");
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                //abre o banco
                conexao.Open();
                //comandos do mysql com seus devidos parametros
                MySqlCommand comandos = new MySqlCommand("delete from tb_dados where codigo = ?", conexao);
                comandos.Parameters.Clear();
                comandos.Parameters.Add("@codigo", MySqlDbType.Int32).Value = txtCod.Text;

                txtCod.Text = "";
                txtNome.Text = "";
                txtEmail.Text = "";
                cmbCidade.Text = "";
                cmbEstado.Text = "";

                //executa o comando
                comandos.CommandType = CommandType.Text;
                comandos.ExecuteNonQuery();
                //fecha a conexão
                conexao.Close();
                MessageBox.Show("Registro deletado com sucesso!");

            }

            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível deletar" + erro);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //abre o banco
                conexao.Open();
                //comandos do mysql com seus devidos parametros
                MySqlCommand comandos = new MySqlCommand("select nome, email, estado, cidade from tb_dados where codigo = ?", conexao);
                comandos.Parameters.Clear();
                comandos.Parameters.Add("@codigo", MySqlDbType.Int32).Value = txtCod.Text;

                //executa o comando
                comandos.CommandType = CommandType.Text;
                //recebe o conteúdo que vem do banco
                MySqlDataReader dr;
                dr = comandos.ExecuteReader();
                //insere as informações recebidas do banco para os componentes do form
                dr.Read();
                txtNome.Text = dr.GetString(0);
                txtEmail.Text = dr.GetString(1);
                cmbEstado.Text = dr.GetString(2);
                cmbCidade.Text = dr.GetString(3);


                //fecha a conexão
                conexao.Close();
            }

            catch (Exception erro)
            {
                MessageBox.Show("Código não encontrado" + erro);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                //abre o banco
                conexao.Open();
                //comandos do mysql com seus devidos parametros
                MySqlCommand comandos = new MySqlCommand("update tb_dados set nome = ?, email = ?, cidade = ?, estado = ? where codigo = ?", conexao);
                comandos.Parameters.Clear();
                comandos.Parameters.Add("@nome", MySqlDbType.VarChar, 30).Value = txtNome.Text;
                comandos.Parameters.Add("@email", MySqlDbType.VarChar, 30).Value = txtEmail.Text;
                comandos.Parameters.Add("@cidade", MySqlDbType.VarChar, 30).Value = cmbCidade.SelectedItem.ToString();
                comandos.Parameters.Add("@estado", MySqlDbType.VarChar, 2).Value = cmbEstado.SelectedItem.ToString();
                comandos.Parameters.Add("@codigo", MySqlDbType.Int32).Value = txtCod.Text;

                //limpa os campos
                txtCod.Text = "";
                txtNome.Text = "";
                txtEmail.Text = "";
                cmbCidade.Text = "";
                cmbEstado.Text = "";

                //executando comando
                comandos.CommandType = CommandType.Text;
                comandos.ExecuteNonQuery();

                //fecha a conexão
                conexao.Close();

                MessageBox.Show("Editado com sucesso!");
            }

            catch (Exception erro)
            {
                MessageBox.Show("Falha ao editar" + erro);
            }
        }
    }
}
