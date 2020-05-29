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
    public partial class Produtos : Form
    {
        pro_chefeEntities9 bd = new pro_chefeEntities9();
        public Produtos()
        {
            InitializeComponent();
            btnCriarProduto.Click += criarProduto;
            carregarTabelaProdutos();
            btnVoltar.Click += voltar;
            btnIngredientes.Click += Ingredientes;
            btnEstoque.Click += Estoque;
            btnIngredientesDosProdutos.Click += IngredientesDosProdutos;
            btnExcluir.Click += excluir;
        }
        private void excluir(object sender, EventArgs e)
        {
            bd.produto.ToList().ForEach(z =>
            {
                if (z.nome.Equals(txtDescrição.Text))
                {
                    produto db = z;
                    bd.produto.Remove(db);
                    bd.produto_ingrediente.ToList().ForEach(a =>
                    {

                        if (a.id_produto == z.id)
                        {
                            produto_ingrediente aka = a;
                            bd.produto_ingrediente.Remove(aka);
                        }
                    });
                }
            });
            bd.SaveChanges();
            MessageBox.Show("Produto excluído!");
            carregarTabelaProdutos();
        }
        private void IngredientesDosProdutos(object sender, EventArgs e)
        {
            this.Close();
            new telaIngredientesDosProdutos().Show();
        }
        private void Estoque(object sender, EventArgs e)
        {
            this.Close();
            new telaEstoque().Show();
        }
        private void Ingredientes(object sender, EventArgs e)
        {
            this.Close();
            new telaIngredientes().Show();
        }
        private void voltar(object sender, EventArgs e)
        {
            this.Close();
            new telaPosLogin().Show();
        }
        private void carregarTabelaProdutos()
        {
            dataGridView1.Rows.Clear();
            bd.produto.ToList().ForEach(p =>
            {
                dataGridView1.Rows.Add(p.id, p.nome, p.preco);
            });
        }
        private void criarProduto(object sender, EventArgs e)
        {
            produto novoP = new produto();
            novoP.nome = txtDescrição.Text;
            novoP.preco = Convert.ToDecimal(txtPreco.Text);
            carregarTabelaProdutos();
            bd.produto.Add(novoP);
            bd.SaveChanges();
            carregarTabelaProdutos();
        }
    }
}
