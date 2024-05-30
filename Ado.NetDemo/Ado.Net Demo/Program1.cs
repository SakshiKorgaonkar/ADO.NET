using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace Ado.Net_Demo
{
    internal class Program1
    {
        static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=Contact;Integrated Security=SSPI";
        static DataSet dataSet;
        static SqlDataAdapter dataAdapter;
        static Contact contact;

        static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }

        static void Main(string[] args)
        {
            dataSet = new DataSet();
            dataAdapter = new SqlDataAdapter("SELECT * FROM ContactTable", GetConnection());
            dataAdapter.Fill(dataSet, "ContactTable");
            bool condition = true;
            while (condition)
            {
                Console.WriteLine($"Choose operation : \n1.Add records\n2.Update record\n3.Delete record\n4.View all records\n5.Exit");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        contact= new Contact();
                        contact.AcceptRecord();
                        AddRecord(contact);
                        break;
                    case 2:
                        Console.WriteLine("Enter name of contact to update : ");
                        string name = Console.ReadLine();
                        UpdateRecord(name);
                        break;
                    case 3:
                        Console.WriteLine("Enter name of contact to delete : ");
                        string name1 = Console.ReadLine();
                        DeleteRecord(name1);
                        break;
                    case 4:
                        Console.WriteLine("All Records : ");
                        ViewAllRecords();
                        break;
                    case 5:
                        condition = false;
                        break;
                }
            }
        }
        static void AddRecord(Contact contact)
        {
            DataRow drCurrent = dataSet.Tables["ContactTable"].NewRow();
            drCurrent["FirstName"] = contact.firstName;
            drCurrent["LastName"] = contact.lastName;
            drCurrent["Email"] = contact.email;
            drCurrent["Phone"] = contact.phone;
            drCurrent["Address"] = contact.address;
            drCurrent["City"] = contact.city;
            drCurrent["State"] = contact.state;
            drCurrent["Zip"] = contact.zip;

            dataSet.Tables["ContactTable"].Rows.Add(drCurrent);
            try
            {
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.Update(dataSet, "ContactTable");
                Console.WriteLine("Record added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding record: " + ex.Message);
            }
        }

        static void UpdateRecord(string name)
        {
            DataRow[] dd = dataSet.Tables["ContactTable"].Select($"FirstName = '{name}'");
           
            contact = new Contact();
            contact.AcceptRecord();
            dd[0]["FirstName"] = contact.firstName;
            dd[0]["LastName"] = contact.lastName;
            dd[0]["Email"] = contact.email;
            dd[0]["Phone"] = contact.phone;
            dd[0]["Address"] = contact.address;
            dd[0]["City"] = contact.city;
            dd[0]["State"] = contact.state;
            dd[0]["Zip"] = contact.zip;
            try
            {
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.Update(dataSet, "ContactTable");
                Console.WriteLine("Record updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating record: " + ex.Message);
            }
        }
        static void DeleteRecord(string name)
        {
            DataRow[] dd = dataSet.Tables["ContactTable"].Select($"FirstName = '{name}'");
            try
            {
                foreach (DataRow row in dd)
                {
                        row.Delete();
                }
                Console.WriteLine("Record deleted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting record: " + ex.Message);
            }
            SqlCommand deleteCommand = new SqlCommand("DELETE FROM ContactTable WHERE FirstName = @FirstName", GetConnection());
            deleteCommand.Parameters.AddWithValue("@FirstName", name);
            dataAdapter.DeleteCommand = deleteCommand;
            dataAdapter.Update(dataSet, "ContactTable");
        }

        static void ViewAllRecords()
        {
            DataTable dt = dataSet.Tables["ContactTable"];
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                    Console.Write(row[col] + " ");
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
            }
        }
    }
}