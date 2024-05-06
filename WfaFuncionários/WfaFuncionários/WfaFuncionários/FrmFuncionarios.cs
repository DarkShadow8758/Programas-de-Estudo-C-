using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WfaFuncionários
{
    public partial class FrmFuncionarios : Form
    {
        List<Funcionario> funcionarios;
        bool incluir = false;
        public FrmFuncionarios()
        {
            InitializeComponent();
            funcionarios = new List<Funcionario>();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            var resposta = MessageBox.Show("Deseja mesmo sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (resposta == DialogResult.Yes)
                Close();
        }

        private void FrmFuncionarios_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo sair?","Atenção", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void HabilitaCampos(Control container, bool hab)
        {
            foreach (Control campo in container.Controls)
            {
                if (!(campo is Label))
                {
                    campo.Enabled = hab;
                }
            }
        }

        private void HabilitaBotoes(Control container, bool hab)
        {
            foreach (Control componente in container.Controls)
            {
                if (componente is Button)
                {
                    componente.Enabled = hab;
                    if (componente.Name == "btnGravar" || componente.Name == "btnCancelar")
                    {
                        componente.Enabled = !hab;
                    }
                }
            }
        }

        private void LimpaCampos(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if(control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
                else if (control is MaskedTextBox)
                {
                    ((MaskedTextBox)control).Clear();
                }
                else if (control is DateTimePicker)
                {
                    ((DateTimePicker)control).Value = DateTime.Now;
                }
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            HabilitaCampos(grpCampos, true);
            HabilitaBotoes(this, false);
            incluir = true;
            txtId.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            HabilitaCampos(grpCampos, false);
            HabilitaBotoes(this, true);
            LimpaCampos(grpCampos);
            btnEditar.Text = "&Editar";
            btnApagar.Text = "&Apagar";
            incluir = false;
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            txtLista.Clear();
            foreach (Funcionario item in funcionarios)
            {
                txtLista.Text += item.ToString();
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            bool encontrou = false;
            Funcionario funcionario = new Funcionario();
            try
            {
                funcionario.FuncionarioId = Int32.Parse(txtId.Text);
                funcionario.Nome = txtNome.Text;
                funcionario.Endereco = txtEndereco.Text;
                funcionario.Cidade = txtCidade.Text;
                funcionario.Estado = mskEstado.Text;
                funcionario.Cep = mskCep.Text;
                funcionario.Cpf = mskCPF.Text;
                funcionario.Telefone = mskTelefone.Text;
                funcionario.Email = txtEmail.Text;
                funcionario.DataNasc = dtpDataNasc.Value;
               if (incluir)
                {
                    funcionarios.Add(funcionario);
                }
                else
                {
                    foreach (Funcionario func in funcionarios)
                    {
                        if (func.FuncionarioId == funcionario.FuncionarioId)
                        {
                            func.Nome = funcionario.Nome;
                            func.Endereco = funcionario.Endereco;
                            func.Cidade = funcionario.Cidade;
                            func.Estado = funcionario.Estado;
                            func.Cep = funcionario.Cep;
                            func.Cpf = funcionario.Cpf;
                            func.Telefone = funcionario.Telefone;
                            func.Email = funcionario.Email;
                            func.DataNasc = funcionario.DataNasc;
                            encontrou = true;
                        }
                    }
                    if (!encontrou)
                    {
                        MessageBox.Show("Funcionário não encontrado!", "Erro:",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro:\n" + ex.Message, "Erro:",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCancelar_Click(null, null);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (btnEditar.Text == "&Editar")
            {
                HabilitaBotoes(this, false);
                btnEditar.Enabled = true;
                btnGravar.Enabled = false;
                txtId.Enabled = true;
                txtId.Focus();
                btnEditar.Text = "&Procurar";
                MessageBox.Show("Informe o ID do funcionário desejado!", "Aviso:",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                try
                {
                    bool encontrou = false;
                    foreach (Funcionario func in funcionarios)
                    {
                        if(func.FuncionarioId == Int32.Parse(txtId.Text))
                        {
                            txtId.Text = func.FuncionarioId.ToString();
                            txtNome.Text = func.Nome;
                            txtEndereco.Text = func.Endereco;
                            txtCidade.Text = func.Cidade;
                            mskEstado.Text = func.Estado;
                            mskCep.Text = func.Cep;
                            mskCPF.Text = func.Cpf;
                            mskTelefone.Text = func.Telefone;
                            txtEmail.Text = func.Email;
                            dtpDataNasc.Value = func.DataNasc;
                            encontrou = true;
                            HabilitaCampos(grpCampos, true);
                            HabilitaBotoes(this, false);
                            txtId.Enabled = false;
                            txtNome.Focus();
                        }
                    }
                    if (!encontrou)
                    {
                        MessageBox.Show("Funcionário não encontrado!", "Erro:",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }          
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro:\n" + ex.Message, "Erro:",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if(btnApagar.Text == "&Apagar")
            {
                HabilitaBotoes(this, false);
                btnApagar.Enabled = true;
                btnGravar.Enabled = false;
                txtId.Enabled = true;
                txtId.Focus();
                btnApagar.Text = "&Confirmar";
                MessageBox.Show("Informe o ID do funcionário desejado!", "Aviso:",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    bool encontrou = false;
                    foreach (Funcionario func in funcionarios)
                    {
                        if (func.FuncionarioId == Int32.Parse(txtId.Text))
                        {
                            encontrou = true;
                            if (MessageBox.Show("Deseja mesmo apagar esse funcionário?", "Atenção",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                funcionarios.Remove(func);
                                btnCancelar_Click(null, null);
                                break;
                            }

                        }
                    }
                    if (!encontrou)
                    {
                        MessageBox.Show("Funcionário não encontrado!", "Erro:",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro:\n" + ex.Message, "Erro:",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                  
            }
        }

        /// /////////////////////////


        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (btnEditar.Text == "&Editar")
            {
                HabilitaBotoes(this, true);
                btnEditar.Enabled = true;
                btnGravar.Enabled = false;
                txtId.Enabled = true;
                txtId.Focus();
                btnEditar.Text = "&Procurar";
                MessageBox.Show("Informe o ID do funcionário desejado!", "Aviso:",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                try
                {
                    bool encontrou = false;
                    foreach (Funcionario func in funcionarios)
                    {
                        if (func.FuncionarioId == Int32.Parse(txtId.Text))
                        {
                            txtId.Text = func.FuncionarioId.ToString();
                            txtNome.Text = func.Nome;
                            txtEndereco.Text = func.Endereco;
                            txtCidade.Text = func.Cidade;
                            mskEstado.Text = func.Estado;
                            mskCep.Text = func.Cep;
                            mskCPF.Text = func.Cpf;
                            mskTelefone.Text = func.Telefone;
                            txtEmail.Text = func.Email;
                            dtpDataNasc.Value = func.DataNasc;
                            encontrou = true;
                            HabilitaCampos(grpCampos, false);
                            HabilitaBotoes(this, false);
                            txtId.Enabled = false;
                            txtNome.Focus();
                            btnEditar.Enabled = true;
                            btnGravar.Enabled = false;
                        }
                    }
                    if (!encontrou)
                    {
                        MessageBox.Show("Funcionário não encontrado!", "Erro:",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro:\n" + ex.Message, "Erro:",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            //txtLista.Clear();


        }




    }
}
