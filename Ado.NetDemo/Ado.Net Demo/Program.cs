using Ado.Net_Demo;
using System;
using System.Data.SqlClient;

namespace ConsoleApp
{
    class Program
    {
        static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=Contact;Integrated Security=SSPI";
        static Contact contact;
        static void Main1(string[] args)
        { 
            bool condition=true;
            while (condition)
            {
                Console.WriteLine($"Choose operation : \n1.Add records\n2.Update record\n3.Delete record\n4.View all records\n5.Exit");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        contact = new Contact();
                        contact.AcceptRecord();
                        InsertContact(contact);
                        break;
                    case 2:
                        Console.WriteLine("Enter name of contact to update : ");
                        string name=Console.ReadLine();
                        UpdateRecord(name);
                        break;
                    case 3:
                        Console.WriteLine("Enter name of contact to delete : ");
                        string name1=Console.ReadLine();
                        DeleteRecord(name1);
                        break;
                    case 4:
                        Console.WriteLine("All contacts : ");
                        SelectAllContacts();
                        break;
                    case 5:
                        condition = false;
                        break;
                }
            }            
        }
        static void InsertContact(Contact contact)
        {
            string query = "INSERT INTO ContactTable (FirstName,LastName,Email,Phone,Address,City,State,Zip) VALUES (@firstName,@lastName, @email, @phone, @address, @city, @state, @zip);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@firstName", contact.firstName);
                command.Parameters.AddWithValue("@lastName", contact.lastName);
                command.Parameters.AddWithValue("@email", contact.email);
                command.Parameters.AddWithValue("@phone", contact.phone);
                command.Parameters.AddWithValue("@address", contact.address);
                command.Parameters.AddWithValue("@city", contact.city);
                command.Parameters.AddWithValue("@state",contact.state);
                command.Parameters.AddWithValue("@zip", contact.zip);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Record inserted successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        static void UpdateRecord(string name)
        {
            contact = new Contact();
            contact.AcceptRecord();

            string query = "UPDATE ContactTable SET FirstName=@firstName, LastName=@lastName, Email=@email, Phone=@phone, Address=@address, City=@city, State=@state, Zip=@zip WHERE FirstName=@name";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@firstName", contact.firstName);
                command.Parameters.AddWithValue("@lastName", contact.lastName);
                command.Parameters.AddWithValue("@email", contact.email);
                command.Parameters.AddWithValue("@phone", contact.phone);
                command.Parameters.AddWithValue("@address", contact.address);
                command.Parameters.AddWithValue("@city", contact.city);
                command.Parameters.AddWithValue("@state", contact.state);
                command.Parameters.AddWithValue("@zip", contact.zip);
                command.Parameters.AddWithValue("@name", name);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Record updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No records updated.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating record: " + ex.Message);
                }
            }
        }

        static void DeleteRecord(string name)
        {
            string query = "delete from ContactTable where FirstName=@name";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Record deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Record not found");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        static void SelectAllContacts()
        {
            string query = "SELECT * FROM ContactTable;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"First Name: {reader["firstName"]},Last Name: {reader["lastName"]}, Email: {reader["email"]}, Phone: {reader["phone"]}, Address: {reader["address"]},City: {reader["city"]},State: {reader["state"]},Zip: {reader["zip"]}");
                }
            }
        }
    }
}
