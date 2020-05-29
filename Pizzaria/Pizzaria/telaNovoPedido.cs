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
    public partial class telaNovoPedido : Form
    {
        mesa selecionada;
        pro_chefeEntities9 bd = new pro_chefeEntities9();
        public telaNovoPedido()
        {
            InitializeComponent();
        }
        public telaNovoPedido(mesa mesaSelecionada)
        {
            InitializeComponent();
            txtNotas.Text = Convert.ToString(mesaSelecionada.observacao);
            button1.Click += salvarNota;
            selecionada = mesaSelecionada;
            label1.Text = $"Mesa {selecionada.id}";
            btnOcupar.Click += trocarStatus;
            if (selecionada.disponivel == true)
            {
                btnOcupar.Text = "Ocupar";
            }
            else
            {
                btnOcupar.Text = "Desocupar";
            }
            btnVoltar.Click += Voltar;
            carregaProdutos();
            btnIncluir.Click += selecionarItem;
            carregarItensPedidos();
            carregarValorTotal();
            btnPedirConta.Click += pedirConta;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//codigo para a seleção no datagrid selecionar a linha toda
            this.dataGridView1.MultiSelect = false;
        }
        private void salvarNota(object sender, EventArgs e)
        {
            mesa m = bd.mesa.Single(c => c.id.Equals(selecionada.id));
            m.observacao = txtNotas.Text;
            bd.SaveChanges();
            MessageBox.Show("Nota salva");
        }

        private void pedirConta(object sender, EventArgs e)
        {
            double valor = 0;
            for (int i = 0; i < tblItensPedidos.Rows.Count; i++)
            {
                valor += Convert.ToDouble(tblItensPedidos.Rows[i].Cells[2].Value);
            }
            MessageBox.Show($"O valor total da conta é {valor}");

            produto_mesa ExclirPg = new produto_mesa();
            bd.produto_mesa.ToList().ForEach(z =>
            {
                if (z.id_mesa.Equals(selecionada.id))
                {
                    ExclirPg = z;
                    bd.produto_mesa.Remove(ExclirPg);
                }
            });
            bd.SaveChanges();
            carregarItensPedidos();
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
        private void carregarItensPedidos()
        {
            try
            {
                tblItensPedidos.Rows.Clear();
                bd.produto_mesa.Where(i => i.id_mesa == selecionada.id).ToList().ForEach(i =>
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

        private void trocarStatus(object sender, EventArgs e)
        {
            if (selecionada.disponivel == true)
            {
                selecionada.disponivel = false;
                btnOcupar.Text = "Desocupar";
                mesa m = bd.mesa.Single(c => c.id.Equals(selecionada.id));
                m.disponivel = false;
                bd.SaveChanges();
            }
            else
            {
                selecionada.disponivel = true;
                btnOcupar.Text = "Ocupar";
                mesa m = bd.mesa.Single(c => c.id.Equals(selecionada.id));
                m.disponivel = true;
                bd.SaveChanges();
            }
        }
        private void selecionarItem(object sender, EventArgs e)
        {
            try
            {
                int idProduto = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                produto_mesa abc = new produto_mesa();
                abc.id_mesa = selecionada.id;
                abc.id_produto = idProduto;
                abc.quantidade = Convert.ToDecimal(txtQuantidade.Value);
                bd.produto_mesa.Add(abc);
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
            catch
            {
                MessageBox.Show("Erro");
            }
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
            new telaMesas().Show();
            this.Hide();
        }
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
