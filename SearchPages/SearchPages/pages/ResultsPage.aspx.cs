using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SearchPages.pages
{

    public class Movie
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string bookID { get; set; }
    }

    public class Book
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string ISBN { get; set; }
        public string movieID { get; set; }
    }

    public partial class ResultsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);

            System.Diagnostics.Debug.WriteLine("What it do?");

            if (!(bool)Session["AdvancedOptions"])
            {
                System.Diagnostics.Debug.WriteLine("It twerks");

                string title = (string)Session["Name"];

                List<Book> bookList = new List<Book>();
                List<Movie> movieList = new List<Movie>();

                SqlCommand searchMovies = new SqlCommand();
                searchMovies.CommandType = System.Data.CommandType.Text;
                searchMovies.CommandText = "SELECT * FROM [Movies]";
                searchMovies.Connection = db;

                SqlCommand searchBooks = new SqlCommand();
                searchBooks.CommandType = System.Data.CommandType.Text;
                searchBooks.CommandText = "SELECT * FROM [Books]";
                searchBooks.Connection = db;

                db.Open();

                try
                {
                    using (SqlDataReader readerMovies = searchMovies.ExecuteReader())
                    {
                        while (readerMovies.Read())
                        {
                            System.Diagnostics.Debug.WriteLine("At least 1 Movie");

                            string mID = readerMovies.GetSqlInt32(0).ToString();
                            string mName = readerMovies.GetString(1);
                            string mQuantity = readerMovies.GetSqlInt32(2).ToString();
                            string mPrice = readerMovies.GetSqlMoney(3).ToString();
                            string mGenre = readerMovies.GetString(4);
                            string mYear = readerMovies.GetString(5);
                            string mBook = readerMovies.GetSqlInt32(6).ToString();

                            System.Diagnostics.Debug.WriteLine("Name: " + mName);

                            movieList.Add(new Movie { ID = mID, Name = mName, Quantity = mQuantity, Price = mPrice, Genre = mGenre, Year = mYear, bookID = mBook });
                        }
                    }

                    using (SqlDataReader readerBooks = searchBooks.ExecuteReader())
                    {
                        while(readerBooks.Read())
                        {
                            System.Diagnostics.Debug.WriteLine("At least 1 Book");

                            string bID = readerBooks.GetSqlInt32(0).ToString();
                            string bName = readerBooks.GetString(1);
                            string bQuantity = readerBooks.GetSqlInt32(2).ToString();
                            string bPrice = readerBooks.GetSqlMoney(3).ToString();
                            string bGenre = readerBooks.GetString(4);
                            string bYear = readerBooks.GetString(5);
                            string bISBN = readerBooks.GetString(6);
                            string bMovie = readerBooks.GetSqlInt32(7).ToString();

                            System.Diagnostics.Debug.WriteLine("Name: " + bName);

                            bookList.Add(new Book { ID = bID, Name = bName, Quantity = bQuantity, Price = bPrice, Genre = bGenre, Year = bYear, ISBN = bISBN, movieID = bMovie });
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Error");
                }
                finally
                {
                    db.Close();
                }

                var results = new List<Object>();

                foreach(var item in movieList)
                {
                    if(item.Name.Contains(title))
                    {
                        results.Add(item);
                    }
                }

                foreach(var item in bookList)
                {
                    if(item.Name.Contains(title))
                    {
                        results.Add(item);
                    }
                }


                GridView1.DataSource = results;
                GridView1.DataBind();
            }
            else
            {
                string title = (string)Session["Name"];
                string year = (string)Session["Year"];
                string genre = (string)Session["Genre"];
                bool bookAndMovie = (bool)Session["BookAndMovie"];

               
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchPage.aspx?");
        }
    }
}