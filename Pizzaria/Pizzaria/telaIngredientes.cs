using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pizzaria
{
    public partial class telaIngredientes : Form
    {
        pro_chefeEntities9 bd = new pro_chefeEntities9();
        public telaIngredientes()
        {
            InitializeComponent();
            btnVoltar.Click += Voltar;
            btnProdutos.Click += Produtos;
            btnEstoque.Click += Estoquqe;
            btnIngredientesDosProdutos.Click += IngredientesDosprod;
            btnCriarIngrediente.Click += criarIngrediente;
            carregaTabela();
        }
        private void carregaTabela()
        {
            dataGridView1.Rows.Clear();
            bd.ingrediente.ToList().ForEach(p =>
            {
                dataGridView1.Rows.Add(p.id, p.nome, p.estoque);
            });
        }

        private void criarIngrediente(object sender, EventArgs e)
        {
            bool existe = false;
            bd.ingrediente.ToList().ForEach(a =>
            {
                if (a.nome == txtNome.Text)
                {
                    existe = true;
                    MessageBox.Show("Esse ingrediente já existe");
                }
            });
            if (!existe)
            {
                ingrediente novo = new ingrediente();
                novo.nome = txtNome.Text;
                novo.estoque = 0;
                bd.ingrediente.Add(novo);
                bd.SaveChanges();
                MessageBox.Show("Ingrediente Salvo");
                dataGridView1.Rows.Clear();
                carregaTabela();
            }
        }
        private void IngredientesDosprod(object sender, EventArgs e)
        {
            this.Close();
            new telaIngredientesDosProdutos().Show();
        }
        private void Estoquqe(object sender, EventArgs e)
        {
            this.Close();
            new telaEstoque().Show();
        }

        private void Produtos(object sender, EventArgs e)
        {
            this.Close();
            new Produtos().Show();
        }


        private void Voltar(object sender, EventArgs e)
        {
            this.Close();
            new telaPosLogin().Show();
        }

        private void Ingredientes_Load(object sender, EventArgs e)
        {

        }
    }
}
