using System;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

class Program
{
    static void Main()
    {
        Program program = new Program();
        string connectionString = "Data Source=ITT-MEHUL-NA;Initial Catalog=TestDB;Integrated Security=True;";
       
        Console.WriteLine("Enter Your choice");
        Console.WriteLine("1. Add data");
        Console.WriteLine("2. Update data");
        Console.WriteLine("3. View data");
        Console.WriteLine("4. Exit");
        int choice = Convert.ToInt32(Console.ReadLine());
        switch(choice)
        {
            case 1:
                program.addUser(connectionString);
                break;
            case 2:
                program.updateUser(connectionString);
                break;
            case 3:
                program.viewUser(connectionString);
                break;
        }
    }
    public void addUser(string connectionString)
    {
        Console.WriteLine("Enter the username and password");
        string username = Console.ReadLine();
        string password = Console.ReadLine();
        string insertQuery = "INSERT INTO TestTable (username, password) VALUES (@Username, @Password)";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Rows affected: {rowsAffected}");
                Console.WriteLine("User added successfully.");

                connection.Close();
            }
        }
    }
    public void updateUser(string connectionString)
    {
        Console.WriteLine("Enter the username,newusername and password");
        string username = Console.ReadLine();
        string newusername = Console.ReadLine();
        string newPassword = Console.ReadLine();
        string insertQuery = "UPDATE TestTable set username=@newusename password=@password WHERE username=@newusername";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", newPassword);
                command.Parameters.AddWithValue("@password", newusername);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine("User added successfully.");

                connection.Close();
            }
        }
    }

    public void viewUser(string connectionString)
    {
        Console.WriteLine("Enter the username");
        string username = Console.ReadLine();
        string insertQuery = "SELECT * FROM TestTable WHERE Username = @Username";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@username", username);


                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Username: {reader["Username"]}, Password: {reader["Password"]}");
                    }
                }
                else
                {
                    Console.WriteLine($"User '{username}' not found.");
                }

                reader.Close();
                connection.Close();
            }
        }
    }
}
