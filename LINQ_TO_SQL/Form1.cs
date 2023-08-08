using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LINQ_TO_SQL
{
    public partial class Form1 : Form
    {
        StudentDBDataContext db;

        public Form1()
        {
            InitializeComponent();
        }
        private void BindGridView()
        {
            db = new StudentDBDataContext();
            dataGridView1.DataSource = db.Students;
        }
        private void Cleartextboxes()
        {
            foreach (Control ctr in this.Controls)
            {
                if (ctr is System.Windows.Forms.TextBox) 
                {
                    System.Windows.Forms.TextBox txt = ctr as System.Windows.Forms.TextBox;
                    
                   
                    txt = ctr as System.Windows.Forms.TextBox;
                    txt.Clear();
                }
                
            }
            nametextBox.Focus();

        }

        private void insertbutton_Click(object sender, EventArgs e)
        {
            if (nametextBox.Text == "" || gendertextBox.Text == "" || agetextBox.Text == "" || classtextBox.Text == "")
            {
                MessageBox.Show("All feilds are required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                db = new StudentDBDataContext();
                Student std = new Student
                {
                    name = nametextBox.Text,
                    gender = gendertextBox.Text,
                    age = int.Parse(agetextBox.Text),
                    standard = int.Parse(classtextBox.Text)
                };
                db.Students.InsertOnSubmit(std);
                db.SubmitChanges();
                MessageBox.Show("Data Has been inserted Sucessfully ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cleartextboxes();
                BindGridView();
            }
        }

        private void clearbutton_Click(object sender, EventArgs e)
        {
            Cleartextboxes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            nametextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            gendertextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            agetextBox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            classtextBox.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                db = new StudentDBDataContext();
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                Student std = db.Students.FirstOrDefault(s => s.Id == id);
                std.name = nametextBox.Text;
                std.gender = gendertextBox.Text;
                std.age = int.Parse(agetextBox.Text);
                std.standard = int.Parse(classtextBox.Text);
                db.SubmitChanges();
                MessageBox.Show("Data Has been updated Sucessfully ", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cleartextboxes();
                BindGridView();
            }
            else
            {
                MessageBox.Show("Please select a row","Warning",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void deletebutton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Are you sure to Delete data ??", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    db = new StudentDBDataContext();
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    Student std = db.Students.FirstOrDefault(s => s.Id == id);
                    db.Students.DeleteOnSubmit(std);
                    db.SubmitChanges();
                    MessageBox.Show("Data Has been deleted Sucessfully ", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cleartextboxes();
                    BindGridView();
                }
            }
            else
            {
                MessageBox.Show("Please select a row", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
              
        }
    }
}
