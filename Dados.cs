using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace Clientes
{
    public partial class Dados : Form
    {
        public Dados()
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
            InitializeComponent();
        }
        public SQLiteConnection sql()
        {
            //Digite o caminho do seu banco de dados
            string sqllink = "Data Source=";
            SQLiteConnection conexao = new SQLiteConnection(sqllink);
            conexao.Open();

            return conexao;
        }

        public DataTable DadosDaTabela()
        {
            SQLiteDataAdapter da;
            DataTable dt = new DataTable();

            var sqldata = sql().CreateCommand().CommandText = "Select * FROM cliente";


            da = new SQLiteDataAdapter(sqldata, sql());

            da.Fill(dt);
            return dt;

        }
      
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                var dados = sql().CreateCommand();
                dados.CommandText = "DELETE FROM cliente WHERE id=" + Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                dados.ExecuteNonQuery();
                sql().Close();
                MessageBox.Show("Deletado com sucesso! Atualize a pagina");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dados = DadosDaTabela();
            dataGridView1.DataSource = dados;
            dataGridView1.Refresh();
        }

        private void Dados_Load(object sender, EventArgs e)
        {
            var dados = DadosDaTabela();
            dataGridView1.DataSource = dados;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var dados = sql().CreateCommand();
                dados.CommandText = "UPDATE cliente SET nome = @nome, idade = @idade, cpf= @cpf";
      
                dados.Parameters.AddWithValue("@nome", dataGridView1.SelectedRows[0].Cells["nome"].Value);
                dados.Parameters.AddWithValue("@idade", dataGridView1.SelectedRows[0].Cells["idade"].Value);
                dados.Parameters.AddWithValue("@cpf", dataGridView1.SelectedRows[0].Cells["cpf"].Value);
                dados.ExecuteNonQuery();
                sql().Close();
                MessageBox.Show("Atualizado com sucesso! Atualize a pagina");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
    }
}
