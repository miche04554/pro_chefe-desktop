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
    public partial class telaDelivery : Form
    {
        pro_chefeEntities9 bd = new pro_chefeEntities9();
        public static cliente buscado = new cliente();
        public static delivery selecionado = new delivery();
        public telaDelivery()
        {
            InitializeComponent();
            button1.Click += Voltar;
            btnAdicionar.Click += Salvar;
            btnBuscar.Click += buscar;
            btnLimpar.Click += Limpar;
            btnInformacoes.Click += CarregaPedidoDelivery;
            btnFinalizarEntrega.Click += finalizarEntrega;
            montarTabela();
            carregaEntregador();
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//codigo para a seleção no datagrid selecionar a linha toda
            this.dataGridView1.MultiSelect = false;
            checkBox1.Click += CheckBox1_Click;
        }
        private void CheckBox1_Click(object sender, EventArgs e)
        {
            montarTabela();
        }
        private void finalizarEntrega(object sender, EventArgs e)
        {
            int idSelecionado = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            selecionado = bd.delivery.Where(u => u.id.Equals(idSelecionado)).FirstOrDefault();
            delivery m = bd.delivery.Single(c => c.id.Equals(idSelecionado));
            m.finalizada = true;
            bd.SaveChanges();
            montarTabela();
        }
        private void CarregaPedidoDelivery(object sender, EventArgs e)
        {
            int idSelecionado = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            selecionado = bd.delivery.Where(u => u.id.Equals(idSelecionado)).FirstOrDefault();
            if (selecionado != null)
            {
                new telaPedidoDelivery().Show();
            }
            this.Close();
        }
        private void carregaEntregador()
        {
            bd.funcionario.ToList().ForEach(f =>
            {
                if (f.cargo == "Entregador")
                {
                    txtEntregador.Items.Add(f.nome);
                }
            });
        }
        private void Limpar(object sender, EventArgs e)
        {
            txtTelefone.Clear();
            txtNome.Clear();
            txtLogradouro.Clear();
            txtBairro.Clear();
            txtNumero.Clear();
            txtComplemento.Clear();
        }
        private void buscar(object sender, EventArgs e)
        {
            buscado = bd.cliente.Where(u => u.telefone.Equals(txtTelefone.Text)).FirstOrDefault();
            if (buscado != null)
            {
                txtTelefone.Text = buscado.telefone;
                txtNome.Text = buscado.nome;
                txtLogradouro.Text = buscado.rua;
                txtBairro.Text = buscado.bairro;
                txtNumero.Text = buscado.numero;
                txtComplemento.Text = buscado.complemento;
                buscado = null;
            }
            else
            {
                MessageBox.Show("Não existe nenhum cliente cadastrado com esse número");
            }
        }
        private void montarTabela()
        {
            dataGridView1.Rows.Clear();
            bd.delivery.ToList().ForEach(a =>
            {
                if (!checkBox1.Checked)
                {

                    if (a.finalizada == false)
                    {
                        dataGridView1.Rows.Add(a.id, a.Nome, a.Telefone, a.entregador, "Realizando");
                    }
                    else
                    {
                        {
                            dataGridView1.Rows.Add(a.id, a.Nome, a.Telefone, a.entregador, "Finalizada");
                        }
                    }
                }
                else
                {
                    if (a.finalizada == false)
                    {
                        dataGridView1.Rows.Add(a.id, a.Nome, a.Telefone, a.entregador, "Realizando");
                    }
                }
            });
        }
        private void Salvar(object sender, EventArgs e)
        {
            try
            {
                delivery novo = new delivery()
                {
                    Telefone = txtTelefone.Text,
                    Nome = txtNome.Text,
                    Logradouro = txtLogradouro.Text,
                    Número = txtNumero.Text,
                    Bairro = txtBairro.Text,
                    Complemento = txtComplemento.Text,
                    entregador = txtEntregador.Text,
                    finalizada = false
                };
                bd.delivery.Add(novo);
                bd.SaveChanges();
                MessageBox.Show("Delivery Cadastrado com sucesso");
                montarTabela();
            }
            catch 
            {
                MessageBox.Show("Preencha todos os campos");
            }
        }
        private void Voltar(object sender, EventArgs e)
        {
            new telaPosLogin().Show();
            this.Close();
        }
    }
}
