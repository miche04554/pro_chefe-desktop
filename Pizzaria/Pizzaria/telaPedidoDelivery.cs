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
    public partial class telaPedidoDelivery : Form
    {
        pro_chefeEntities9 bd = new pro_chefeEntities9();
        public telaPedidoDelivery()
        {
            InitializeComponent();
            lblTelefone.Text = Convert.ToString(telaDelivery.selecionado.Telefone);
            lblNome.Text = telaDelivery.selecionado.Nome;
            lblLogradouro.Text = telaDelivery.selecionado.Logradouro;
            lblBairro.Text = telaDelivery.selecionado.Bairro;
            lblNumero.Text = telaDelivery.selecionado.Número;
            lblEntregador.Text = telaDelivery.selecionado.entregador;
            lblComplemento.Text = telaDelivery.selecionado.Complemento;
            button1.Click += Voltar;
            btnIncluir.Click += Incluir;
            carregaProdutos();
            carregarValorTotal();
            carregarItensPedidos();
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//codigo para a seleção no datagrid selecionar a linha toda
            this.dataGridView1.MultiSelect = false;
        }

        private void TxtCategoria_Click(object sender, EventArgs e)
        {
            carregaProdutos();
        }

        private void carregarItensPedidos()
        {
            try
            {
                tblItensPedidos.Rows.Clear();
                bd.delivery_produto.Where(i => i.id_delivery == telaDelivery.selecionado.id).ToList().ForEach(i =>
                {
                    bd.produto.ToList().ForEach(j =>
                    {
                        if (j.id == i.id_produto)
                        {
                            tblItensPedidos.Rows.Add(j.nome, i.quantidade, j.preco * i.quantidade); ;
                        }
                    });
                });
                carregarValorTotal();
            }
            catch { };
        }

        private void Incluir(object sender, EventArgs e)
        {
            int idProduto = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            delivery_produto abc = new delivery_produto();
            abc.id_delivery = telaDelivery.selecionado.id;
            abc.id_produto = idProduto;
            abc.quantidade = Convert.ToDecimal(txtQuantidade.Value);
            bd.delivery_produto.Add(abc);
            bd.produto_ingrediente.ToList().ForEach(p =>
            {
                if (p.id_produto == idProduto)
                {
                    bd.ingrediente.ToList().ForEach(i =>
                    {
                        if (i.id == p.id_ingrediente)
                        {

                            i.estoque -= (abc.quantidade * p.quantidade);
                        }
                    });
                }
            });
            bd.SaveChanges();
            MessageBox.Show("Item inserido com sucesso");
            carregarItensPedidos();
            carregarValorTotal();
        }

        private void carregarValorTotal()
        {
            double valor = 0;
            for (int i = 0; i < tblItensPedidos.Rows.Count; i++)
            {
                valor += Convert.ToDouble(tblItensPedidos.Rows[i].Cells[2].Value);
            }
            lblValorTotal.Text = Convert.ToString(valor);
        }
        private void carregaProdutos()
        {
            try
            {
                dataGridView1.Rows.Clear();
                bd.produto.ToList().ForEach(p =>
                {
                    dataGridView1.Rows.Add(p.id, p.nome, p.preco);
                });
            }
            catch { }
        }
        private void Voltar(object sender, EventArgs e)
        {
            this.Hide();
            new telaDelivery().Show();
        }
    }
}
