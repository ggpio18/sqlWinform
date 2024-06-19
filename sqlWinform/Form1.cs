using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sqlWinform
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-Q7UOJO5;Initial Catalog=ClubDb;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
            
        }

        
        private void button1_Click(object sender, EventArgs e)
        {

            //insert ot register user
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-Q7UOJO5;Initial Catalog=ClubDb;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into SBTable (StudentId,FirstName,MiddleName,LastName,Age,Gender,Program) values (@StudentId, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)", con);
            cmd.Parameters.AddWithValue("@StudentId", Convert.ToInt64(txtStudentID.Text));
            cmd.Parameters.AddWithValue("@FirstName", (txtFirstName.Text));
            cmd.Parameters.AddWithValue("@MiddleName", (txtMiddleName.Text));
            cmd.Parameters.AddWithValue("@LastName", (txtLastName.Text));
            cmd.Parameters.AddWithValue("@Age", int.Parse(txtAge.Text));
            cmd.Parameters.AddWithValue("@Gender", (cbGender.Text));
            cmd.Parameters.AddWithValue("@Program", (cbProgram.Text));
            cmd.ExecuteNonQuery();
            con.Close();
             MessageBox.Show("Success");

            //show data on gridvieqw
            SqlCommand cmd2 = new SqlCommand("Select * from SBTable", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //go to next form
            FrmUpdateMembers f2 = new FrmUpdateMembers();
            f2.Show();
            Visible = false;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            //declare array list of program to select
            string[] ListOfProgram = new string[]
            {
                "BS Information Technology",
                "BS Computer Science",
                "BS InformationSystem",
                "BS IN Accountancy",
                "BS IN Hospitality Mangement",
                "BS IN Tourism Mangement"
                };
            //use loop to add the items
            for (int i = 0; i < 6; i++)
            {
                cbProgram.Items.Add(ListOfProgram[i].ToString());
            }

            //show datagridview on load
            SqlCommand cmd2 = new SqlCommand("Select * from SBTable", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            //end
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //update /refresh datagridview
            SqlCommand cmd2 = new SqlCommand("Select * from SBTable", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            //end
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-Q7UOJO5;Initial Catalog=ClubDb;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete SBTable where StudentId=@StudentId",con);
            cmd.Parameters.AddWithValue("@StudentId", Convert.ToInt64(txtStudentID.Text));

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Deleted");
        }
    }
}
