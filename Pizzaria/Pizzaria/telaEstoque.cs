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
    public partial class telaEstoque : Form
    {
        pro_chefeEntities9 bd = new pro_chefeEntities9();
        public telaEstoque()
        {
            InitializeComponent();
            btnVoltar.Click += Voltar;
            btnRegistrarEntrada.Click += RegistrarEntrada;
            btnRegistrarSaida.Click += RegistrarSaida;
            btnIngredientes.Click += Ingredientes;
            btnProdutos.Click += Produtos;
            btnIngredientesDosProdutos.Click += IngredientesDosProdutos;
            carregaTabela();
            carregaIngredientes();
        }
        private void carregaIngredientes()
        {
            bd.ingrediente.ToList().ForEach(f =>
            {
                txtIngredientes.Items.Add(f.nome);
            });
        }
        private void carregaTabela()
        {
            dataGridView1.Rows.Clear();
            bd.estoque.ToList().ForEach(p =>
            {
                dataGridView1.Rows.Add(p.item, p.quantidade, p.movimentacao);
            });
        }
        private void Produtos(object sender, EventArgs e)
        {
            this.Close();
            new Produtos().Show();
        }

        private void IngredientesDosProdutos(object sender, EventArgs e)
        {
            this.Close();
            new telaIngredientesDosProdutos().Show();
        }
        private void Ingredientes(object sender, EventArgs e)
        {
            this.Close();
            new telaIngredientes().Show();
        }
        private void RegistrarSaida(object sender, EventArgs e)
        {
            estoque novaSaida = new estoque();
            novaSaida.item = txtIngredientes.Text;
            novaSaida.quantidade = Convert.ToDecimal(txtQuantidade.Text);
            novaSaida.movimentacao = "Saída";
            bd.estoque.Add(novaSaida);
            ingrediente procurado = bd.ingrediente.Single(u => u.nome.Equals(txtIngredientes.Text));
            procurado.estoque -= Convert.ToDecimal(txtQuantidade.Text);
            bd.SaveChanges();
            carregaTabela();
            MessageBox.Show("Saída registrada");
        }
        private void RegistrarEntrada(object sender, EventArgs e)
        {
            estoque novaEntrada = new estoque();
            novaEntrada.item = txtIngredientes.Text;
            novaEntrada.quantidade = Convert.ToDecimal(txtQuantidade.Text);
            novaEntrada.movimentacao = "Entrada";
            bd.estoque.Add(novaEntrada);
            ingrediente procurado = bd.ingrediente.Single(u => u.nome.Equals(txtIngredientes.Text));
            procurado.estoque += Convert.ToDecimal(txtQuantidade.Text);
            bd.SaveChanges();
            carregaTabela();
            MessageBox.Show("Entrada registrada");
        }
        private void Voltar(object sender, EventArgs e)
        {
            this.Close();
            new telaPosLogin().Show();
        }
    }
}
