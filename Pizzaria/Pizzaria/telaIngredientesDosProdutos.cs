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
    public partial class telaIngredientesDosProdutos : Form
    {
        pro_chefeEntities9 bd = new pro_chefeEntities9();
        public telaIngredientesDosProdutos()
        {
            InitializeComponent();
            carregaProdutos();
            carregaIngredientes();
            btnRelacionar.Click += relacionar;
            btnIngredientes.Click += Ingredientes;
            btnProdutos.Click += Produtos;
            btnEstoque.Click += Estoquqe;
            btnVoltar.Click += Voltar;
        }
        private void Voltar(object sender, EventArgs e)
        {
            this.Close();
            new telaPosLogin().Show();
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
        private void Ingredientes(object sender, EventArgs e)
        {
            this.Close();
            new telaIngredientes().Show();
        }
        private void relacionar(object sender, EventArgs e)
        {
            try
            {
                bool existe = false;
                produto_ingrediente novo = new produto_ingrediente();
                bd.ingrediente.ToList().ForEach(b =>
                {
                    if (b.nome.Equals(txtIngrediente.Text))
                    {
                        novo.id_ingrediente = b.id;
                    }
                });
                bd.produto.ToList().ForEach(c =>
                   {
                       if (c.nome.Equals(txtProduto.Text))
                       {
                           novo.id_produto = c.id;
                       }
                   });
                novo.quantidade = Convert.ToDecimal(txtQuantidade.Value);
                bd.produto_ingrediente.ToList().ForEach(jb =>
                {
                    if (jb.id_produto == novo.id_produto && jb.id_ingrediente == novo.id_ingrediente)
                    {
                        existe = true;
                    }
                });
                if (existe == false)
                {
                    bd.produto_ingrediente.Add(novo);
                    bd.SaveChanges();
                    txtIngrediente.Text = "";
                    txtProduto.Text = "";
                    MessageBox.Show("A relação entre o produto e o ingrediente foi concluída");

                }
                else
                {
                    MessageBox.Show("Essa relação já existe");
                }
            }
            catch
            {
                MessageBox.Show("Erro ao relacionar");
            }
        }
        private void carregaIngredientes()
        {
            bd.ingrediente.ToList().ForEach(b =>
            {
                txtIngrediente.Items.Add(b.nome);
            });
        }
        private void carregaProdutos()
        {
            bd.produto.ToList().ForEach(a =>
            {
                txtProduto.Items.Add(a.nome);
            });
        }
    }
}
