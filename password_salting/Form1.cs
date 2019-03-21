using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EncryptPasswordHelper.Crypto;
using EncryptPasswordHelper.Entities;

namespace password_salting
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            EncryptDecrypt.Encrypter(textBox1.Text);
            MessageBox.Show("Password Encrypted and Saved to Disk.", "Password Encryption", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String s = EncryptDecrypt.Decrypter();
            var msg = "Decrypted Password: " + s;
            MessageBox.Show(msg, "Password Decryption", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }
    }
}
