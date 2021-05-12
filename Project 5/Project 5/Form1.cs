using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace Project_5
{
    public partial class Form1 : Form
    {

        static MySqlConnection conn;
        public int flag = 0;

        static Microsoft.FSharp.Collections.FSharpList<Library5.HW5P2.Article> alldata;

        public Form1()
        {
            InitializeComponent();
        }

        public void establishConnection()
        {
            try
            {
                // Establish Connection to MySQL Server
                string database1 = textBox3.Text;
                string port1 = textBox4.Text;
                string username1 = textBox5.Text;
                string password1 = textBox6.Text;
                string database2 = textBox7.Text;

                string connStr = String.Format("server={0};user={1};database={2};port={3};password={4}", database1, username1, database2, port1, password1);
                Console.WriteLine(connStr);
                conn = new MySqlConnection(connStr);                                                     
                conn.Open();
           
            }
            catch (Exception ex)
            {
                flag = 1;
                return;
            }
        }

        public bool csvConnection()
        {
            string filename = textBox1.Text;

            if (filename == "")
            {
                return false;
            }
            else
            {
                alldata = Library5.HW5P2.readfile(filename);
                return true;
            }
        }

        //-----------------------SQL BUTTONS------------------------------------------------------------------------------------

        private void button1_Click(object sender, EventArgs e)
        {
            flag = 0;
            establishConnection();
            if(flag == 1)
            {
                textBox8.Text = "Invalid SQL Connection";
                textBox8.Text += System.Environment.NewLine + "Try Again!";
                
            }
            else
            {
                //textBox8.Text = "Valid SQL Connection";
                string stringInput = textBox2.Text;

                if(stringInput == "")
                {
                    textBox8.Clear();
                    textBox8.Text = "Error Invalid Input. Try Again!";
                    return;
                }

                int input = Int32.Parse(stringInput);

                try
                {
                    int nid = input;

                    string query = String.Format(@"
                            SELECT title
                            FROM news
                            WHERE news_id = {0};", nid);

                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();

                    textBox8.Clear();

                    textBox8.AppendText(String.Format("{0}", reader.GetName(0)));
                    textBox8.Text += System.Environment.NewLine;

                    while (reader.Read())
                    {
                        //Console.WriteLine(reader[0]);
                        textBox8.Text += reader[0];
                        textBox8.Text += System.Environment.NewLine;
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flag = 0;
            establishConnection();
            if (flag == 1)
            {
                textBox8.Text = "Invalid SQL Connection";
                textBox8.Text += System.Environment.NewLine + "Try Again!";

            }
            else
            {

                MySqlDataReader reader;

                try
                {
                    // Write (copy from queries folder) the query
                    string query = String.Format(@" 
                                                SELECT news_id, LENGTH(body_text) AS length
                                                FROM news
                                                WHERE LENGTH(body_text)>100
                                                ORDER BY news_id;
                                            ");

                    // Build a Command which holds the query and the location of the target server
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                    reader = cmd.ExecuteReader();

                    // Retrieve the results into a DataReader
                    textBox8.Clear();
                    // Output the header from the DataReader
                    textBox8.AppendText(String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1)));
                    textBox8.Text += System.Environment.NewLine;
                    // Loop through the rows of the DataReader to output the values from the DataReader

                    while (reader.Read())
                    {
                        textBox8.AppendText(String.Format("{0}\t{1}", reader.GetString(0), reader.GetInt32(1)));
                        textBox8.Text += System.Environment.NewLine;
                    }

                    // Close the DataReader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            flag = 0;
            establishConnection();
            if (flag == 1)
            {
                textBox8.Text = "Invalid SQL Connection";
                textBox8.Text += System.Environment.NewLine + "Try Again!";

            }
            else
            {
                MySqlDataReader reader;
                try
                {
                    // Write (copy from queries folder) the query
                    string query = String.Format(@" 
                                                SELECT title, DATE_FORMAT(STR_TO_DATE(publish_date, '%c/%d/%y'), '%M') AS Month
                                                FROM news
                                                ORDER BY STR_TO_DATE(publish_date, '%m/%d/%y');
                                            ");
                    // Build a Command which holds the query and the location of the target server
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    // Retrieve the results into a DataReader
                    // Output the header from the DataReader
                    textBox8.Clear();
                    textBox8.AppendText(String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1)));
                    textBox8.Text += System.Environment.NewLine;
                    // Loop through the rows of the DataReader to output the values from the DataReader
                    while (reader.Read())
                    {
                        textBox8.AppendText(String.Format("{0}\t{1}", reader.GetString(0), reader.GetString(1)));
                        textBox8.Text += System.Environment.NewLine;
                    }
                    // Close the DataReader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            flag = 0;
            establishConnection();
            if (flag == 1)
            {
                textBox8.Text = "Invalid SQL Connection";
                textBox8.Text += System.Environment.NewLine + "Try Again!";

            }
            else
            {
                MySqlDataReader reader;
                try
                {
                    // Write (copy from queries folder) the query
                    string query = String.Format(@" 
                                                SELECT publisher
                                                FROM publisher_table
                                                JOIN news
                                                USING (publisher_id)
                                                GROUP BY publisher
                                                ORDER BY publisher;
                                            ");
                    // Build a Command which holds the query and the location of the target server
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    // Retrieve the results into a DataReader
                    // Output the header from the DataReader
                    textBox8.Clear();
                    textBox8.AppendText(String.Format("{0}", reader.GetName(0)));
                    textBox8.Text += System.Environment.NewLine;
                    // Loop through the rows of the DataReader to output the values from the DataReader
                    while (reader.Read())
                    {
                        textBox8.AppendText(String.Format("{0}", reader.GetString(0)));
                        textBox8.Text += System.Environment.NewLine;
                    }
                    // Close the DataReader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            flag = 0;
            establishConnection();
            if (flag == 1)
            {
                textBox8.Text = "Invalid SQL Connection";
                textBox8.Text += System.Environment.NewLine + "Try Again!";

            }
            else
            {
                MySqlDataReader reader;
                try
                {
                    // Write (copy from queries folder) the query
                    string query = String.Format(@" 
                                                SELECT country, COUNT(news_id) AS articleCount
                                                FROM country_table
                                                LEFT JOIN news
                                                USING (country_id)
                                                GROUP BY country
                                                ORDER BY articleCount DESC;
                                            ");

                    // Build a Command which holds the query and the location of the target server
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                    reader = cmd.ExecuteReader();

                    // Retrieve the results into a DataReader

                    // Output the header from the DataReader
                    textBox8.Clear();
                    textBox8.AppendText(String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1)));
                    textBox8.Text += System.Environment.NewLine;

                    // Loop through the rows of the DataReader to output the values from the DataReader

                    while (reader.Read())
                    {
                        textBox8.AppendText(String.Format("{0}\t{1}", reader.GetString(0), reader.GetInt32(1)));
                        textBox8.Text += System.Environment.NewLine;
                    }

                    // Close the DataReader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            flag = 0;
            establishConnection();
            if (flag == 1)
            {
                textBox8.Text = "Invalid SQL Connection";
                textBox8.Text += System.Environment.NewLine + "Try Again!";

            }
            else
            {
                MySqlDataReader reader;
                try
                {
                    // Write (copy from queries folder) the query
                    string query = String.Format(@" 
                                                SELECT ROUND(AVG(news_guard_score),3) AS `Average Score`
                                                FROM news;
                                            ");
                    // Build a Command which holds the query and the location of the target server
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    // Retrieve the results into a DataReader
                    // Output the header from the DataReader
                    textBox8.Clear();
                    textBox8.AppendText(String.Format("{0}", reader.GetName(0)));
                    textBox8.Text += System.Environment.NewLine;
                    // Loop through the rows of the DataReader to output the values from the DataReader
                    while (reader.Read())
                    {
                        textBox8.AppendText(String.Format("{0,0:N3}", reader.GetFloat(0)));
                        textBox8.Text += System.Environment.NewLine;
                    }
                    // Close the DataReader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            flag = 0;
            establishConnection();
            if (flag == 1)
            {
                textBox8.Text = "Invalid SQL Connection";
                textBox8.Text += System.Environment.NewLine + "Try Again!";

            }
            else
            {
                MySqlDataReader reader;

                try
                {
                    // Write (copy from queries folder) the query
                    string query = String.Format(@" 
                                                SELECT month, numArticles, overall, ROUND(100*numArticles/overall,3) AS percentage
                                                FROM
                                                (
                                                SELECT month, monthnum, COUNT(publish_date) AS numArticles, overallCount AS overall
                                                FROM
                                                (
                                                SELECT DATE_FORMAT(STR_TO_DATE(publish_date, '%m/%d/%y'), '%M') AS month, 
                                                       DATE_FORMAT(STR_TO_DATE(publish_date, '%m/%d/%y'), '%m') AS monthnum,
	                                                   publish_date
                                                FROM news
                                                ) AS T1
                                                JOIN
                                                (
                                                SELECT COUNT(*) overallCount FROM news
                                                ) AS T2
                                                GROUP BY month, monthnum, overallCount
                                                ) AS T3
                                                ORDER BY monthnum;
                                            ");

                    // Build a Command which holds the query and the location of the target server
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                    reader = cmd.ExecuteReader();

                    // Retrieve the results into a DataReader

                    // Output the header from the DataReader
                    textBox8.Clear();
                    textBox8.AppendText(String.Format("{0}\t{1}\t{2}\t{3}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3)));
                    textBox8.Text += System.Environment.NewLine;
                    // Loop through the rows of the DataReader to output the values from the DataReader

                    while (reader.Read())
                    {
                        textBox8.AppendText(String.Format("{0}\t{1}\t{2}\t{3,3:N3}", reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetFloat(3)));
                        textBox8.Text += System.Environment.NewLine;
                    }

                    // Close the DataReader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            flag = 0;
            establishConnection();
            if (flag == 1)
            {
                textBox8.Text = "Invalid SQL Connection";
                textBox8.Text += System.Environment.NewLine + "Try Again!";

            }
            else
            {
                MySqlDataReader reader;

                try
                {
                    // Write (copy from queries folder) the query
                    string query = String.Format(@" 
                                                SELECT publisher, ROUND(AVG(reliability)*100, 3) AS percentage
                                                FROM news
                                                JOIN publisher_table
                                                USING (publisher_id)
                                                GROUP BY publisher
                                                ORDER BY percentage DESC, publisher;
                                            ");

                    // Build a Command which holds the query and the location of the target server
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                    reader = cmd.ExecuteReader();

                    // Retrieve the results into a DataReader

                    // Output the header from the DataReader
                    textBox8.Clear();
                    textBox8.AppendText(String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1)));
                    textBox8.Text += System.Environment.NewLine;

                    // Loop through the rows of the DataReader to output the values from the DataReader

                    while (reader.Read())
                    {
                        textBox8.AppendText(String.Format("{0}\t{1,1:N3}", reader.GetString(0), reader.GetFloat(1)));
                        textBox8.Text += System.Environment.NewLine;
                    }

                    // Close the DataReader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            flag = 0;
            establishConnection();
            if (flag == 1)
            {
                textBox8.Text = "Invalid SQL Connection";
                textBox8.Text += System.Environment.NewLine + "Try Again!";

            }
            else
            {
                MySqlDataReader reader;

                try
                {
                    // Write (copy from queries folder) the query
                    string query = String.Format(@" 
                                                SELECT country, ROUND(AVG(news_guard_score),3) AS avg_news_score
                                                FROM news
                                                JOIN country_table
                                                USING (country_id)
                                                GROUP BY country
                                                ORDER BY AVG(news_guard_score) DESC, country ASC;
                                            ");

                    // Build a Command which holds the query and the location of the target server
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                    reader = cmd.ExecuteReader();

                    // Retrieve the results into a DataReader

                    // Output the header from the DataReader
                    textBox8.Clear();
                    textBox8.AppendText(String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1)));
                    textBox8.Text += System.Environment.NewLine;

                    // Loop through the rows of the DataReader to output the values from the DataReader

                    while (reader.Read())
                    {
                        textBox8.AppendText(String.Format("{0}\t{1,1:N3}", reader.GetString(0), reader.GetFloat(1)));
                        textBox8.Text += System.Environment.NewLine;
                    }

                    // Close the DataReader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            flag = 0;
            establishConnection();
            if (flag == 1)
            {
                textBox8.Text = "Invalid SQL Connection";
                textBox8.Text += System.Environment.NewLine + "Try Again!";

            }
            else
            {
                MySqlDataReader reader;

                try
                {
                    // Write (copy from queries folder) the query
                    string query = String.Format(@" 
                                                SELECT author, political_bias, COUNT(*) AS numArticles
                                                FROM news
                                                JOIN news_authors
                                                USING (news_id)
                                                JOIN author_table
                                                USING (author_id)
                                                JOIN political_bias_table
                                                USING (political_bias_id)
                                                GROUP BY author, political_bias
                                                ORDER BY author, COUNT(*) DESC, political_bias;
                                            ");

                    // Build a Command which holds the query and the location of the target server
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                    reader = cmd.ExecuteReader();

                    // Retrieve the results into a DataReader

                    // Output the header from the DataReader
                    textBox8.Clear();
                    textBox8.AppendText(String.Format("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2)));
                    textBox8.Text += System.Environment.NewLine;

                    // Loop through the rows of the DataReader to output the values from the DataReader

                    while (reader.Read())
                    {
                        textBox8.AppendText(String.Format("{0}\t{1}\t{2}", reader.GetString(0), reader.GetString(1), reader.GetInt32(2)));
                        textBox8.Text += System.Environment.NewLine;
                    }

                    // Close the DataReader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        //-----------------------F# BUTTONS------------------------------------------------------------------------------------

        
        private void button11_Click(object sender, EventArgs e)
        { 
            if(csvConnection())
            {
                string stringInput = textBox2.Text;

                if (stringInput == "")
                {
                    textBox8.Clear();
                    textBox8.Text = "Error Invalid Input. Try Again!";
                    return;
                }

                int input = Int32.Parse(stringInput);
                var result = Library5.HW5P2.getTitle(input, alldata);

                textBox8.Clear();
                textBox8.AppendText(String.Format("Title: {0}", result));
                textBox8.Text += System.Environment.NewLine;
            }
            else
            {
                textBox8.Clear();
                textBox8.Text = "Error. Not a Valid CSV File Name";
            }
        }

        
        private void button12_Click(object sender, EventArgs e)
        {
            if (csvConnection())
            {
                string stringInput = textBox2.Text;

                if (stringInput == "")
                {
                    textBox8.Clear();
                    textBox8.Text = "Error Invalid Input. Try Again!";
                    return;
                }

                int input = Int32.Parse(stringInput);
                var result = Library5.HW5P2.wordCount(input, alldata);

                textBox8.Clear();
                textBox8.AppendText(String.Format("Number of Words in The Article: {0}", result));
                textBox8.Text += System.Environment.NewLine;
            }
            else
            {
                textBox8.Clear();
                textBox8.Text = "Error. Not a Valid CSV File Name";
            }
        }

        
        private void button13_Click(object sender, EventArgs e)
        {
            if (csvConnection())
            {
                string stringInput = textBox2.Text;

                if (stringInput == "")
                {
                    textBox8.Clear();
                    textBox8.Text = "Error Invalid Input. Try Again!";
                    return;
                }

                int input = Int32.Parse(stringInput);
                var result = Library5.HW5P2.getMonthName(input, alldata);

                textBox8.Clear();
                textBox8.AppendText(String.Format("Month of Chosen Article: {0}", result));
                textBox8.Text += System.Environment.NewLine;
            }
            else
            {
                textBox8.Clear();
                textBox8.Text = "Error. Not a Valid CSV File Name";
            }
        }

        
        private void button14_Click(object sender, EventArgs e)
        {
            if (csvConnection())
            {
                textBox8.Clear();

                Microsoft.FSharp.Collections.FSharpList<string> publisherNames = Library5.HW5P2.publishers(alldata); ;
                textBox8.AppendText("Unique Publishers: ");
                textBox8.Text += System.Environment.NewLine;


                textBox8.AppendText(String.Join(Environment.NewLine, publisherNames));
                textBox8.Text += System.Environment.NewLine;
            }
            else
            {
                textBox8.Clear();
                textBox8.Text = "Error. Not a Valid CSV File Name";
            }
        }

        
        private void button15_Click(object sender, EventArgs e)
        {
            if (csvConnection())
            {
                textBox8.Clear();

                Microsoft.FSharp.Collections.FSharpList<string> countryNames = Library5.HW5P2.countries(alldata);
                textBox8.AppendText("Unique Countries: ");
                textBox8.Text += System.Environment.NewLine;

                textBox8.AppendText(String.Join(Environment.NewLine, countryNames));
                textBox8.Text += System.Environment.NewLine;
            }
            else
            {
                textBox8.Clear();
                textBox8.Text = "Error. Not a Valid CSV File Name";
            }
        }

        
        private void button16_Click(object sender, EventArgs e)
        {
            if (csvConnection())
            {
                double overallguard = Library5.HW5P2.avgNewsguardscoreForArticles(alldata);

                textBox8.Clear();
                textBox8.AppendText(String.Format("Average News Guard Score for All Articles: {0}", overallguard));
                textBox8.Text += System.Environment.NewLine;
            }
            else
            {
                textBox8.Clear();
                textBox8.Text = "Error. Not a Valid CSV File Name";
            }
        }

        
        private void button17_Click(object sender, EventArgs e)
        {
            if (csvConnection())
            {
                Microsoft.FSharp.Collections.FSharpList<Tuple<string, int>> nArticles = Library5.HW5P2.numberOfArticlesEachMonth(alldata);
                textBox8.Clear();

                textBox8.AppendText("Number of Articles for Each Month:");
                textBox8.Text += System.Environment.NewLine;

                string output = Library5.HW5P2.buildHistogram(nArticles, alldata.Length, "");

                output = output.Replace("/n", System.Environment.NewLine);

                textBox8.AppendText(output);
                textBox8.Text += System.Environment.NewLine;
            }
            else
            {
                textBox8.Clear();
                textBox8.Text = "Error. Not a Valid CSV File Name";
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (csvConnection())
            {
                Microsoft.FSharp.Collections.FSharpList<Tuple<string, double>> reliablepct = Library5.HW5P2.reliableArticlePercentEachPublisher(alldata);

                textBox8.Clear();
                textBox8.AppendText("Percentage of Articles That Are Reliable for Each Publisher: ");
                textBox8.Text += System.Environment.NewLine;

                Microsoft.FSharp.Collections.FSharpList<string> lines1 = Library5.HW5P2.printNamesAndPercentages(reliablepct);
                foreach (string line in lines1)
                {
                    textBox8.AppendText(line);
                    textBox8.Text += System.Environment.NewLine;
                }
            }
            else
            {
                textBox8.Clear();
                textBox8.Text = "Error. Not a Valid CSV File Name";
            }
        }

       
        private void button19_Click(object sender, EventArgs e)
        {
            if (csvConnection())
            {
                Microsoft.FSharp.Collections.FSharpList<Tuple<string, double>> result = Library5.HW5P2.averageguardscore(alldata);
                Microsoft.FSharp.Collections.FSharpList<string> lines1 = Library5.HW5P2.printNamesAndFloats(result);

                textBox8.Clear();
                textBox8.AppendText("Average News Guard Score for Each Country: ");
                textBox8.Text += System.Environment.NewLine;
                // Call the library function transforming the list of pairs into a list of strings
                // Output the list of strings, one per line
                foreach (string line in lines1)
                {
                    textBox8.AppendText(line);
                    textBox8.Text += System.Environment.NewLine;
                }
            }
            else
            {
                textBox8.Clear();
                textBox8.Text = "Error. Not a Valid CSV File Name";
            }
        }

        
        private void button20_Click(object sender, EventArgs e)
        {
            if (csvConnection())
            {
                // Call the library function to get the List of (string, double) pairs
                Microsoft.FSharp.Collections.FSharpList<Tuple<string, double>> result = Library5.HW5P2.avgNewsguardscoreEachBias(alldata);

                textBox8.Clear();
                textBox8.AppendText("The Average News Guard Score for Each Political Bias Category: ");
                textBox8.Text += System.Environment.NewLine;
                // Call the library function to construct the histogram
                // output the string generated by the F# code


                string output = Library5.HW5P2.buildHistogramFloat(result, "");

                output = output.Replace("/n", System.Environment.NewLine);

                textBox8.AppendText(output);
                textBox8.Text += System.Environment.NewLine;
            }
            else
            {
                textBox8.Clear();
                textBox8.Text = "Error. Not a Valid CSV File Name";
            }
        }

        //-----------------------Text Boxes------------------------------------------------------------------------------------

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
