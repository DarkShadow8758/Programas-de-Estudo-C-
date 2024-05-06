using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WfaVendas
{
    public partial class splash_screen : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
            (
            int nLeftRect,
            int nTopRect,
            int RightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse

            );
        public splash_screen()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(10, 10, Width, Height, 25, 25));
            progressBar1.Value = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 1;
            progressBar1.Text = progressBar1.Value.ToString() + "%";
         
            if (progressBar1.Value == 100)
            {
                timer1.Enabled = false;
                FrmCadClientes form = new FrmCadClientes(); //se_form
                form.Show();
                this.Hide();
            }
        }
    }
}
