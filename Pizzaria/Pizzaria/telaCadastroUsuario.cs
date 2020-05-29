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
    public partial class telaCadastroUsuario : Form
    {
        pro_chefeEntities9 bd = new pro_chefeEntities9();
        public telaCadastroUsuario()
        {
            InitializeComponent();
            btnSalvar.Click += salvarUsuario;
            btnVoltar.Click += BtnVoltar_Click;
            montarTabela();
            btnExcluir.Click += Excluir;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//codigo para a seleção no datagrid selecionar a linha toda
            this.dataGridView1.MultiSelect = false;
        }
        private void Excluir(object sender, EventArgs e)
        {
            try
            {
                var db = bd.usuario.Where(u => u.login.Equals(txtLoginCadastro.Text)).FirstOrDefault();
                if (db != null)
                {
                    bd.usuario.Remove(db);
                    bd.SaveChanges();
                    MessageBox.Show("Usuario excluído!");
                    db = null;
                    montarTabela();
                }
            }
            catch
            {
                MessageBox.Show("Não existe nenhum usuario com esse nome");
            }
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            new telaPosLogin().Show();
            this.Close();
        }

        private void montarTabela()
        {
            dataGridView1.Rows.Clear();
            bd.usuario.ToList().ForEach(p =>
           {
               dataGridView1.Rows.Add(p.id, p.nome, p.login, p.tipo_usuario);
           });
        }
        private void salvarUsuario(object sender, EventArgs e)
        {
            try
            {
                if (txtConfirmarSenha.Text == txtSenhaCadastro.Text)
                {
                    usuario novo = new usuario()
                    {
                        nome = txtNomeCadastro.Text,
                        login = txtLoginCadastro.Text,
                        senha = txtSenhaCadastro.Text,
                        tipo_usuario = Convert.ToInt32(numericUpDown1.Value)
                    };
                    bd.usuario.Add(novo);
                    bd.SaveChanges();
                    MessageBox.Show("Usuário salvo com sucesso");
                    montarTabela();
                }
                else
                {
                    MessageBox.Show("As senhas não são iguais");
                }
            }
            catch 
            {
                MessageBox.Show("Preencha todos os campos");
            }
        }
    }

}
