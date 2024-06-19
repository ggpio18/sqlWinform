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

namespace sqlWinform
{
    public partial class FrmUpdateMembers : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-Q7UOJO5;Initial Catalog=ClubDb;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        public FrmUpdateMembers()
        {
            InitializeComponent();
           
        }

       
        private void FrmUpdateMembers_Load(object sender, EventArgs e)
        {
            //declare array list of program
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
                CbProgram2.Items.Add(ListOfProgram[i].ToString());
            }
            //END

            //try show cbstudent id data from sql
            string sql = "select * from SBTable";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cbStudentId.Items.Add(dr["StudentId"]);
            }
            con.Close();
            //end
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           //update data
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-Q7UOJO5;Initial Catalog=ClubDb;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("update SBTable set Firstname=@FirstName, MiddleName=@MiddleName, LastName=@LastName, Age=@Age, Gender=@Gender, Program=@Program where StudentId=@StudentId", con);
            cmd.Parameters.AddWithValue("@StudentId", Convert.ToInt64(cbStudentId.Text));
            cmd.Parameters.AddWithValue("@FirstName", (txtFname2.Text));
            cmd.Parameters.AddWithValue("@MiddleName", (txtMname2.Text));
            cmd.Parameters.AddWithValue("@LastName", (txtLname2.Text));
            cmd.Parameters.AddWithValue("@Age", int.Parse(txtAge2.Text));
            cmd.Parameters.AddWithValue("@Gender", (cbGender2.Text));
            cmd.Parameters.AddWithValue("@Program", (CbProgram2.Text));

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Success updated");

            //go back to form1
            Form1 f1 = new Form1();
            f1.Show();
            Visible = false;
            //end
        }

        private void cbStudentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //show and retrieve data from SQL then show to textbox and combobox
            cmd = new SqlCommand("select * from SBTable WHERE StudentId=@StudentId", con);
            cmd.Parameters.AddWithValue("@StudentId", cbStudentId.Text);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string FNAME = dr["FirstName"].ToString();
                string MNAME = dr["MiddleName"].ToString();
                string LNAME = dr["LastName"].ToString();
                string AGE = dr["Age"].ToString();
                string GENDER = dr["Gender"].ToString();
                string PROG = dr["Program"].ToString();

                txtFname2.Text = FNAME;
                txtMname2.Text = MNAME;
                txtLname2.Text = LNAME;
                txtAge2.Text = AGE;
                cbGender2.Text = GENDER;
                CbProgram2.Text = PROG;
            }
            con.Close();
            //END
        }
    }
}
