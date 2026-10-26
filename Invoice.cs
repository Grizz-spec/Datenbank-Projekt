using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Datenbank_Projekt
{
    public partial class Invoice : Form
    {
        private SqlConnection databaseConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\Flori\OneDrive\Dokumente\Datenbank Projekt.mdf;Integrated Security = True; Connect Timeout = 30");
        private int lastSelectedProductKey;
        public Invoice()
        {

            InitializeComponent();
            ShowInvoice();
            
        }

        private void buttonInvoiceReturn_Click(object sender, EventArgs e)
        {
            MainMenuScreen mainMenuScreen = new MainMenuScreen();
            mainMenuScreen.Show();
            this.Close();
        }

        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            if (textBoxID.Text == null || textBoxCustomer.Text == null || textBoxTotalPrice.Text == null)
            {
                MessageBox.Show("Bitte fülle alle Felder aus!");
                return;
            }

            string InvoiceID = textBoxID.Text;
            string Customer = textBoxCustomer.Text;
            string TotalPrice = textBoxTotalPrice.Text;

            string query = string.Format("insert into Invoice values('{0}','{1}','{2}')", InvoiceID, Customer, TotalPrice);
            ExecuteQuery(query);

            ClearAllFields();
            ShowInvoice();

        }

        private void btnEditInvoice_Click(object sender, EventArgs e)
        {
            if (lastSelectedProductKey == 0)
            {
                MessageBox.Show("Bitte wähle zuerst ein Produkt aus.");
                return;
            }

            string InvoiceID = textBoxID.Text;
            string Customer = textBoxCustomer.Text;
            string TotalPrice = textBoxTotalPrice.Text;

            string query = string.Format("update Invoice set CustomerID='{0}', Customer='{1}', TotalPrice='{2}' where ID={3}", InvoiceID, Customer, TotalPrice, lastSelectedProductKey);
            ExecuteQuery(query);

            ShowInvoice();
        }

        private void btnClearFieldInvoice_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        private void btnDeleteInvoice_Click(object sender, EventArgs e)
        {
            if (lastSelectedProductKey == 0)
            {
                MessageBox.Show("Bitte wähle zuerst ein Produkt aus.");
                return;
            }
            string query = string.Format("delete from Invoice where ID = {0};", lastSelectedProductKey);
            ExecuteQuery(query);

            ClearAllFields();
            ShowInvoice();
        }

        private void ClearAllFields()
        {
            textBoxID.Text = "";
            textBoxCustomer.Text = "";
            textBoxTotalPrice.Text = "";
        }


        private void InvoiceDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = InvoiceDGV.Rows[e.RowIndex];

            textBoxID.Text = InvoiceDGV.SelectedRows[0].Cells[1].Value.ToString();
            textBoxCustomer.Text = InvoiceDGV.SelectedRows[0].Cells[2].Value.ToString();
            textBoxTotalPrice.Text = InvoiceDGV.SelectedRows[0].Cells[3].Value.ToString();

            lastSelectedProductKey = (int)InvoiceDGV.SelectedRows[0].Cells[0].Value;
        }

        private void ExecuteQuery(string query)
        {
            databaseConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, databaseConnection);
            sqlCommand.ExecuteNonQuery();
            databaseConnection.Close();
        }
        private void ShowInvoice()
        {
            databaseConnection.Open();

            string query = "select * from Invoice";
            SqlDataAdapter sql = new SqlDataAdapter(query, databaseConnection);

            var dataSet = new DataSet();
            sql.Fill(dataSet);

            InvoiceDGV.DataSource = dataSet.Tables[0];

            InvoiceDGV.Columns[0].Visible = false;
            InvoiceDGV.RowHeadersVisible = false;

            databaseConnection.Close();
        }
    }
}
