using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConsoleAdatbazis
{
    class Program
    {



        static void Main(string[] args)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "pizza";

            MySqlConnection connection = new MySqlConnection(builder.ConnectionString);
            Console.WriteLine("Kérem adja meg a fealadat sorszámot 23-tól 28-ig");
            int fadatsorszam = Convert.ToInt32(Console.ReadLine());

            switch (fadatsorszam)
            {
                case 23:
                    Console.WriteLine("23.Hány házhoz szállítása volt az egyes futároknak?");
                    try
                    {
                        connection.Open();

                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT fnev ,COUNT(fazon) FROM rendeles JOIN futar USING(fazon) GROUP BY futar.fnev;";
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string fnev = dr.GetString("fnev");
                                string fazon = dr.GetString("COUNT(fazon)");
                              
                                Console.WriteLine($"{fnev}, {fazon}");
                            }
                        }

                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(0);
                    }

                    break;

                case 24:

                    Console.WriteLine("24.A fogyasztás alapján mi a pizzák népszerűségi sorrendje?");
                    try
                    {
                        connection.Open();

                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT pnev,SUM(db)FROM rendeles JOIN tetel USING(razon) JOIN pizza USING(pazon) GROUP BY pizza.pnev ORDER BY SUM(db) DESC; ";
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string pnev = dr.GetString("pnev");
                                string sumdb = dr.GetString("SUM(db)");

                                Console.WriteLine($"{pnev}, {sumdb}");
                            }
                        }

                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(0);
                    }
                    break;

                case 25:
                    Console.WriteLine("25.A rendelések összértéke alapján mi a vevők sorrendje?");
                    try
                    {
                        connection.Open();

                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT vnev, SUM(db * par) FROM rendeles JOIN vevo USING(vazon)JOIN tetel USING(razon)JOIN pizza USING(pazon)GROUP BY vevo.vnev ORDER BY SUm(tetel.db* pizza.par) DESC; ";
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string vnev = dr.GetString("vnev");
                                string sumdbar = dr.GetString("SUM(db * par)");

                                Console.WriteLine($"{vnev} {sumdbar}");
                            }
                        }

                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(0);
                    }
                    break;

                case 26:
                    Console.WriteLine("26.Melyik a legdrágább pizza?");
                    try
                    {
                        connection.Open();

                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT MAX(par) FROM pizza; ";
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                int maxar = dr.GetInt32("MAX(par)");
                               

                                Console.WriteLine($" {maxar}");
                            }
                        }

                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(0);
                    }
                    break;


                case 27:
                    Console.WriteLine("27.Ki szállította házhoz a legtöbb pizzát?");
                    try
                    {
                        connection.Open();

                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT fnev,SUM(db) FROM rendeles JOIN futar USING(fazon)JOIN tetel USING(razon)GROUP BY futar.fnevORDER BY SUM(db) DESCLIMIT 1; ";
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string fnev2 = dr.GetString("fnev");
                                int sumdb = dr.GetInt32("SUM(db)");

                                Console.WriteLine($" {fnev2} {sumdb} ");
                            }
                        }

                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(0);
                    }
                    break;

                case 28:
                    Console.WriteLine("28.Ki ette a legtöbb pizzát?");
                    try
                    {
                        connection.Open();

                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT vnev,SUM(db) FROM rendeles JOIN vevo USING(vazon)JOIN tetel USING(razon)GROUP BY vevo.vnevORDER BY SUM(db) DESCLIMIT 1; ";
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string vnev2 = dr.GetString("vnev");
                                int sumdb2 = dr.GetInt32("SUM(db)");

                                Console.WriteLine($" {vnev2} {sumdb2} ");
                            }
                        }

                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(0);
                    }
                    break;



            }



            Console.ReadKey();
        }
    }
}
