using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace WfaVendas
{//lembra do form
    public partial class FrmCadClientes : MaterialForm
    {
        bool incluir = false;
        bool alterar = false;
        
        public FrmCadClientes()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        MaterialSkinManager ThemeManeger = MaterialSkinManager.Instance;

        private void FrmCadClientes_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'lP2DataSet.pc_clientes'. Você pode movê-la ou removê-la conforme necessário.
            this.pc_clientesTableAdapter.Fill(this.lP2DataSet.pc_clientes);

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            habilitaBotoes(false);
            habilitaCampos(true);
            limpaCampos();
            incluir = true;
            txtCodcli.Focus();
        }
        private void habilitaBotoes(bool hab)
        {
            btnIncluir.Enabled = hab;
            btnAlterar.Enabled = hab;
            btnExcluir.Enabled = hab;
            btnPesquisar.Enabled = hab;
            btnSair.Enabled = hab;
            btnGravar.Enabled = !hab;
            btnCancelar.Enabled = !hab;
        }
        private void habilitaCampos(bool hab)
        {
            txtCodcli.Enabled = hab;
            txtNome.Enabled = hab;
            txtEndereco.Enabled = hab;
            txtCidade.Enabled = hab;
            txtBairro.Enabled = hab;
            mskUf.Enabled = hab;
            mskCep.Enabled = hab;
            mskTelefone.Enabled = hab;
            dtpDataNasc.Enabled = hab;
        }
        private void limpaCampos()
        {
            foreach (Control item in this.Controls)
            {
                if ((item is TextBox))
                {
                    ((TextBox)item).Clear();
                }
                if (item is MaskedTextBox)
                {
                    ((MaskedTextBox)item).Clear();
                }
                if (item is DateTimePicker)
                {
                    ((DateTimePicker)item).Value = DateTime.Now;
                }
            }
        }

        private void themeToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (themeToggle.Checked)
            {
                ThemeManeger.Theme = MaterialSkinManager.Themes.DARK;
            }
            else
            {
                ThemeManeger.Theme = MaterialSkinManager.Themes.LIGHT;
            }
        }
        
        

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                alterar = true;
                habilitaBotoes(false);
                habilitaCampos(true);
                txtCodcli.Enabled = false;
                txtCodcli.Text = dgvClientes[0, dgvClientes.CurrentRow.Index].Value.ToString();
                txtNome.Text = dgvClientes[1, dgvClientes.CurrentRow.Index].Value.ToString();
                txtEndereco.Text = dgvClientes[2, dgvClientes.CurrentRow.Index].Value.ToString();
                txtCidade.Text = dgvClientes[3, dgvClientes.CurrentRow.Index].Value.ToString();
                txtBairro.Text = dgvClientes[4, dgvClientes.CurrentRow.Index].Value.ToString();
                mskCep.Text = dgvClientes[5, dgvClientes.CurrentRow.Index].Value.ToString();
                mskUf.Text = dgvClientes[6, dgvClientes.CurrentRow.Index].Value.ToString();
                mskTelefone.Text = dgvClientes[7, dgvClientes.CurrentRow.Index].Value.ToString();
                dtpDataNasc.Text = dgvClientes[8, dgvClientes.CurrentRow.Index].Value.ToString();
                txtNome.Focus();
            }
            else
            {
                MessageBox.Show(null, "Selecione um cliente primeiro!", "Erro:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                if (incluir)
                {
                   /* pc_clientesTableAdapter.Insert(Convert.ToInt32(txtCodcli.Text),
                        txtNome.Text, txtEndereco.Text, txtCidade.Text, txtBairro.Text,
                        mskUf.Text, mskCep.Text, mskTelefone.Text, dtpDataNasc.Value);
                    MessageBox.Show("Incluido com sucesso!", "Incluir", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);*/
                } 
                if (alterar)
                {
                    pc_clientesTableAdapter.Update(txtNome.Text, txtEndereco.Text,
                        txtCidade.Text, txtBairro.Text, mskUf.Text, mskCep.Text,
                        mskTelefone.Text, dtpDataNasc.Value, Convert.ToInt32(txtCodcli.Text));
                    MessageBox.Show("Alterado com sucesso!", "Alterar", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                FrmCadClientes_Load(null, null);
                btnCancelar_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro:\n" + ex.Message, "Erro:",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            incluir = false;
            alterar = false;
            habilitaBotoes(true);
            habilitaCampos(false);
            limpaCampos();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClientes.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show(null, "Deseja mesmo excluir o cliente selecionado?",
                        "Atenção:", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        pc_clientesTableAdapter.Delete(Convert.ToInt32(
                            dgvClientes[0, dgvClientes.CurrentRow.Index].Value.ToString()));
                        FrmCadClientes_Load(null, null);
                        MessageBox.Show(null, "Apagado com sucesso!", "Exclusão",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(null, "Selecione um cliente primeiro!", "Erro ao excluir:",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, "Ocorreu um erro:\n" + ex.Message, "Erro:",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtNome.Enabled == false)
            {
                txtNome.Enabled = true;
                txtNome.Focus();
                habilitaBotoes(false);
                btnPesquisar.Enabled = true;
                btnGravar.Enabled = false;
                btnCancelar.Enabled = false;
                MessageBox.Show(null, "Digite o nome desejado ou" +
                    "\nparte dele.", "Pesquisa", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
            /*    pc_clientesTableAdapter.FillByNome(this.vendasDataSet.pc_clientes,
                    "%" + txtNome.Text + "%");
                btnCancelar_Click(null, null); */
            }
        }

        private void dgvClientes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvClientes.Columns[e.ColumnIndex].DataPropertyName == "Telefone" || this.dgvClientes.Columns[e.ColumnIndex].DataPropertyName == "Telefone2") 
            { 
                if(e.Value != null) 
                {
                    string stringValue = (string)e.Value;
                    if (stringValue != "")
                    {
                        stringValue = "(" + stringValue.Substring(2, 5) + "-" + stringValue.Substring(7, 4);
                        e.Value = stringValue;
                    }
                }
            }
            else if (this.dgvClientes.Columns[e.ColumnIndex].DataPropertyName == "CEP")
            {
                if(e.Value != null)
                {
                    string stringValue = (string)e.Value;
                    if(stringValue != "")
                    {
                        stringValue = stringValue.Substring(0, 5) + "-" + stringValue.Substring(5, 3);
                        e.Value = stringValue;
                    }
                }
            }
        }
        

    }
}
