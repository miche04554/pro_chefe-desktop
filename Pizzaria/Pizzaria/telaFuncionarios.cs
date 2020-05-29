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
    public partial class telaFuncionarios : Form
    {
        pro_chefeEntities9 bd = new pro_chefeEntities9();
        public static funcionario procurado = new funcionario();
        public telaFuncionarios()
        {
            InitializeComponent();
            btnVoltar.Click += voltar;
            btnPesquisar.Click += pesquisar;
            btnCadastrarCliente.Click += Salvar;
            btnEditar.Click += Editar;
            btnExcluir.Click += Excluir;
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
            txtCargo.Text = "";
        }
        private void Excluir(object sender, EventArgs e)
        {
            var db = bd.funcionario.Where(u => u.telefone.Equals(txtTelefone.Text)).FirstOrDefault();
            if (db != null)
            {
                bd.funcionario.Remove(db);
                bd.SaveChanges();
                MessageBox.Show("Funcionário excluído!");
                db = null;
            }
            else
            {
                MessageBox.Show("Não existe nenhum funcionário com esse número");
            }
        }
        private void Editar(object sender, EventArgs e)
        {
            if (btnEditar.Text == "Editar")
            {
                procurado = bd.funcionario.Where(u => u.telefone.Equals(txtTelefone.Text)).FirstOrDefault();
            }
            if (procurado == null)
            {
                MessageBox.Show("Não existe nenhum funcionário com esse número");
            }
            else if (procurado != null && btnEditar.Text == "Editar")
            {
                btnEditar.Text = "Salvar Edição";
                txtNome.Text = procurado.nome;
                txtTelefone.Text = procurado.telefone;
                txtCpf.Text = procurado.cpf;
                txtCargo.Text = procurado.cargo;
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
                funcionario m = bd.funcionario.Single(c => c.telefone.Equals(procurado.telefone));
                m.nome = txtNome.Text;
                m.telefone = txtTelefone.Text;
                m.cargo = txtCargo.Text;
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
                MessageBox.Show("Funcionário Editado");
            }
        }
        private void pesquisar(object sender, EventArgs e)
        {
            procurado = bd.funcionario.Where(u => u.telefone.Equals(txtTelefone.Text)).FirstOrDefault();
            if (procurado == null)
            {
                MessageBox.Show("Não existe nenhum funcionário com esse número");
            }
            else
            {
                MessageBox.Show($"Nome: {procurado.nome}\nFunção: {procurado.cargo}\nCPF:{procurado.cpf}\nNascimento:{procurado.dataNascimento}\nEndereço:{procurado.rua}, {procurado.numero}\n{procurado.bairro}\n{procurado.complemento}\n{procurado.cidade}-{procurado.uf}");
            }
            procurado = null;
        }
        private void Salvar(object sender, EventArgs e)
        {
            funcionario novo = new funcionario();
            novo.nome = txtNome.Text;
            novo.telefone = txtTelefone.Text;
            novo.cargo = txtCargo.Text;
            novo.cpf = txtCpf.Text;
            novo.dataNascimento = Convert.ToDateTime(txtDataDeNascimento.Text);
            novo.rua = txtRua.Text;
            novo.numero = txtNumero.Text;
            novo.bairro = txtBairro.Text;
            novo.cep = txtCep.Text;
            novo.cidade = txtCidade.Text;
            novo.uf = txtUF.Text;
            novo.complemento = txtComplemento.Text;
            bd.funcionario.Add(novo);
            bd.SaveChanges();
            MessageBox.Show("Funcionário Salvo com Sucesso!");
        }
        private void voltar(object sender, EventArgs e)
        {
            this.Close();
            new telaPosLogin().Show();
        }
    }
}
