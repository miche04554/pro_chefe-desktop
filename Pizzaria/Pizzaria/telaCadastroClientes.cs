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
    public partial class telaCadastroClientes : Form
    {
        pro_chefeEntities9 bd = new pro_chefeEntities9();
        public static cliente procurado = new cliente();
        public static cliente deletar = new cliente();
        public telaCadastroClientes()
        {
            InitializeComponent();
            btnCadastrarCliente.Click += Salvar;
            btnVoltar.Click += Voltar;
            bntPesquisarCliente.Click += Pesquisar;
            btnEditar.Click += Editar;
            btnExcluirCliente.Click += Excluir;
            btnLimpar.Click += Limpar;
        }

        private void Limpar(object sender, EventArgs e)
        {
            txtTelefone.Clear();
            txtNome.Clear();
            txtRua.Clear();
            txtBairro.Clear();
            txtNumero.Clear();
            txtComplemento.Clear();
            txtCpf.Clear();
            txtDataDeNascimento.Clear();
            txtCep.Clear();
            txtCidade.Clear();
            txtUF.Clear();
        }
        private void Excluir(object sender, EventArgs e)
        {
            try
            {
                var db = bd.cliente.Where(u => u.telefone.Equals(txtTelefone.Text)).FirstOrDefault();
                if (db != null)
                {
                    bd.cliente.Remove(db);
                    bd.SaveChanges();
                    MessageBox.Show("Cliente excluído!");
                    db = null;
                }
                else
                {
                    MessageBox.Show("Não existe nenhum Cliente com esse número");
                }
            }
            catch {
                MessageBox.Show("Erro ao tentar excluir");
            }
        }
        private void Editar(object sender, EventArgs e)
        {
            try
            {
                if (btnEditar.Text == "Editar")
                {
                    procurado = bd.cliente.Where(u => u.telefone.Equals(txtTelefone.Text)).FirstOrDefault();
                }
                if (procurado == null)
                {
                    MessageBox.Show("Não existe nenhum cliente com esse número");
                }
                else if (procurado != null && btnEditar.Text == "Editar")
                {
                    btnEditar.Text = "Salvar Edição";
                    txtNome.Text = procurado.nome;
                    txtTelefone.Text = procurado.telefone;
                    txtCpf.Text = procurado.cpf;
                    txtDataDeNascimento.Text = Convert.ToString(procurado.dataNascimento);
                    txtRua.Text = procurado.rua;
                    txtNumero.Text = procurado.numero;
                    txtBairro.Text = procurado.bairro;
                    txtCep.Text = procurado.cep;
                    txtCidade.Text = procurado.cidade;
                    txtUF.Text = procurado.uf;
                    txtComplemento.Text = procurado.complemento;
                }
                else
                {
                    cliente m = bd.cliente.Single(c => c.telefone.Equals(procurado.telefone));
                    m.nome = txtNome.Text;
                    m.telefone = txtTelefone.Text;
                    m.cpf = txtCpf.Text;
                    m.dataNascimento = Convert.ToDateTime(txtDataDeNascimento.Text);
                    m.rua = txtRua.Text;
                    m.numero = txtNumero.Text;
                    m.bairro = txtBairro.Text;
                    m.cep = txtCep.Text;
                    m.cidade = txtCidade.Text;
                    m.uf = txtUF.Text;
                    m.complemento = txtComplemento.Text;
                    bd.SaveChanges();
                    procurado = null;
                    btnEditar.Text = "Editar";
                    MessageBox.Show("Cliente Editado");
                }
            }
            catch {
                MessageBox.Show("Erro");
            }
        }
        private void Pesquisar(object sender, EventArgs e)
        {
            procurado = bd.cliente.Where(u => u.telefone.Equals(txtTelefone.Text)).FirstOrDefault();
            if (procurado == null)
            {
                MessageBox.Show("Não existe nenhum cliente com esse número");
            }
            else
            {
                MessageBox.Show($"Nome: {procurado.nome}\nCPF:{procurado.cpf}\nNascimento:{procurado.dataNascimento}\nEndereço:{procurado.rua}, {procurado.numero}\n{procurado.bairro}\n{procurado.complemento}\n{procurado.cidade}-{procurado.uf}");
            }
            procurado = null;
        }
        private void Voltar(object sender, EventArgs e)
        {
            this.Close();
            new telaPosLogin().Show();
        }
        private void Salvar(object sender, EventArgs e)
        {
            try
            {
                cliente novo = new cliente();
                novo.nome = txtNome.Text;
                novo.telefone = txtTelefone.Text;
                novo.cpf = txtCpf.Text;
                novo.dataNascimento = Convert.ToDateTime(txtDataDeNascimento.Text);
                novo.rua = txtRua.Text;
                novo.numero = txtNumero.Text;
                novo.bairro = txtBairro.Text;
                novo.cep = txtCep.Text;
                novo.cidade = txtCidade.Text;
                novo.uf = txtUF.Text;
                novo.complemento = txtComplemento.Text;
                bd.cliente.Add(novo);
                bd.SaveChanges();
                MessageBox.Show("Cliente Salvo com Sucesso!");
            }
            catch {
                MessageBox.Show("Preencha todos os campos");
            }
        }
    }
}
