using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ListAndEditForm1.CourseNScore
{
    internal class Course
    {
        MY_DB mydb = new MY_DB();
        public bool insertCourse(int id,string courseName, int hourseNumber, string description)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Course (id, label, period, description) VALUES (@id, @name, @hrs, @descr)", mydb.getConnection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            command.Parameters.Add("@name", SqlDbType.VarChar).Value = courseName;
            command.Parameters.Add("@hrs", SqlDbType.Int).Value = hourseNumber;
            command.Parameters.Add("@descr", SqlDbType.Text).Value = description;

            mydb.openConnection();
            if (command.ExecuteNonQuery()==1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool updateCourse ( int courseID, string courseName, int hourseNumber, string description)
        {
            SqlCommand command = new SqlCommand("UPDATE Course SET label=@name, period=@hrs, description=@descr WHERE id=@cid", mydb.getConnection);
            command.Parameters.Add("@cid", SqlDbType.Int).Value = courseID;
            command.Parameters.Add("@name", SqlDbType.VarChar).Value = courseName;
            command.Parameters.Add("@hrs", SqlDbType.Int).Value = hourseNumber;
            command.Parameters.Add("@descr", SqlDbType.VarChar).Value = description;
            mydb.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool deleteCourse(int courseID)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Course WHERE id = @cid ", mydb.getConnection);
            command.Parameters.Add("@cid", SqlDbType.Int).Value = courseID;
            mydb.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkCourseName(string courseName, int courseId = 0)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Course WHERE label=@cName AND id<>@cID", mydb.getConnection);
            command.Parameters.Add("@cID", SqlDbType.Int).Value = courseId;
            command.Parameters.Add("@cName", SqlDbType.VarChar).Value = courseName;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            if ((table.Rows.Count > 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DataTable getAllCourses()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Course ", mydb.getConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public DataTable getCourseById(int courseID)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Course WHERE id = @cid", mydb.getConnection);
            command.Parameters.Add("@cid", SqlDbType.VarChar).Value = courseID;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        string execCount(string query)
        {
            SqlCommand command = new SqlCommand(query, mydb.getConnection);
            mydb.openConnection();
            String count = command.ExecuteScalar().ToString();
            mydb.closeConnection();
            return count;
        }

        public string totalCourse()
        {
            return execCount("SELECT COUNT(*)  FROM Course");
        }
    }
}
