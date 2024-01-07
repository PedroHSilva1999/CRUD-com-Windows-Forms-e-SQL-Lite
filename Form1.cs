using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using static System.Net.Mime.MediaTypeNames;

namespace Clientes
{
    public partial class Form1 : Form
    {
        public int id;
        public Form1()
        {
            InitializeComponent();
            int idreturn = GetMaxClientId();
           
               
            id = idreturn;

            if (!TableExists("cliente"))
            {
                CreateClienteTable();
            }
        }
        
        
        public SQLiteConnection sql()
        {
            //Digite o caminho do seu banco de dados
            string sqllink = "Data Source="; 
            SQLiteConnection conexao = new SQLiteConnection(sqllink);
            conexao.Open();

            return conexao;
        }

        private void btncadastro_Click(object sender, EventArgs e)
        {
            try
            {
               
                id++;
                var conexao = sql().CreateCommand();
                conexao.CommandText = "INSERT INTO cliente (id,nome,idade,cpf) VALUES (@id,@nome,@cpf,@idade)";
                conexao.Parameters.AddWithValue("@id", id);
                conexao.Parameters.AddWithValue("@nome", txtNome.Text);
                conexao.Parameters.AddWithValue("@cpf", txtIdade.Text);
                conexao.Parameters.AddWithValue("@idade", txtCpf.Text);
                conexao.ExecuteNonQuery();
                sql().Close();
                MessageBox.Show("Dados salvos com sucesso");
                txtCpf.Clear();
                txtIdade.Clear();
                txtNome.Clear();
            }
            catch  (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int GetMaxClientId()
        {
           
            try
            {
                var conexao = sql().CreateCommand();
                conexao.CommandText = "SELECT MAX(id) FROM cliente";
                var resultado = conexao.ExecuteScalar();
                conexao.ExecuteNonQuery();
                sql().Close();
                return Convert.ToInt32(resultado);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Nenhum cliente cadastrado, cadastre um cliente!");
                return 0;
            }
          
            
        }

        private bool TableExists(string tableName)
        {

            var conexao = sql().CreateCommand();
            conexao.CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';";
            var result = conexao.ExecuteScalar();
            conexao.ExecuteNonQuery();
            sql().Close();
            return result != null && result.ToString() == tableName;

        }

        private void CreateClienteTable()
        {
            var conexao = sql().CreateCommand();
            conexao.CommandText = "CREATE TABLE cliente (id INTEGER PRIMARY KEY, nome TEXT, idade INTEGER, cpf INTEGER);";
            conexao.ExecuteNonQuery();
            sql().Close();

        }

    }
}
