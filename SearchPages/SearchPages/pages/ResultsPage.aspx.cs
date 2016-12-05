using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SearchPages.pages
{

    public class Book
    {
        public string BOOK_ID { get; set; }
        public string BOOK_NAME { get; set; }
        public string AUTHOR { get; set; }
        public string GENRE { get; set; }
        public string SUMMARY { get; set; }
        public string YEAR_PUBLISHED { get; set; }
        public string PRICE { get; set; }
        public string MOVIE_ID { get; set; }
    }

    public class Movie
    {
        public string MOVIE_ID { get; set; }
        public string TITLE { get; set; }
        public string DIRECTOR { get; set; }
        public string MAIN_ACTOR { get; set; }
        public string MAIN_ACTRESS { get; set; }
        public string GENRE { get; set; }
        public string SUMMARY { get; set; }
        public string YEAR_PUBLISHED { get; set; }
        public string PRICE { get; set; }
        public string BOOK_ID { get; set; }
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
                searchMovies.CommandText = "SELECT * FROM [MOVIE_INVENTORY]";
                searchMovies.Connection = db;

                SqlCommand searchBooks = new SqlCommand();
                searchBooks.CommandType = System.Data.CommandType.Text;
                searchBooks.CommandText = "SELECT * FROM [BOOK_INVENTORY]";
                searchBooks.Connection = db;

                

                try
                {
                    db.Open();

                    using (SqlDataReader readerMovies = searchMovies.ExecuteReader())
                    {
                        while (readerMovies.Read())
                        {
                            System.Diagnostics.Debug.WriteLine("At least 1 Movie");

                            string mID = readerMovies.GetString(0);
                            string mTitle = readerMovies.GetString(1);
                            string mDirector = readerMovies.GetString(2);
                            string mMainActor = readerMovies.GetString(3);
                            string mMainActress = readerMovies.GetString(4);
                            string mGenre = readerMovies.GetString(5);
                            string mSummary = readerMovies.GetString(6);
                            string mYearPublished = readerMovies.GetString(7);
                            string mPrice = readerMovies.GetDecimal(8).ToString();

                            string mBID = "";
                            if(!readerMovies.IsDBNull(9))
                            {
                                mBID = readerMovies.GetString(9);
                            }
                            

                            System.Diagnostics.Debug.WriteLine("Name: " + mTitle);

                            movieList.Add(new Movie { MOVIE_ID = mID, TITLE = mTitle, DIRECTOR = mDirector, MAIN_ACTOR = mMainActor, MAIN_ACTRESS = mMainActress, GENRE = mGenre, SUMMARY = mSummary, YEAR_PUBLISHED = mYearPublished, PRICE = mPrice, BOOK_ID = mBID });
                        }
                    }

                    using (SqlDataReader readerBooks = searchBooks.ExecuteReader())
                    {
                        while(readerBooks.Read())
                        {
                            System.Diagnostics.Debug.WriteLine("At least 1 Book");

                            string mID = readerBooks.GetString(0);
                            string mName = readerBooks.GetString(1);
                            string mAuthor = readerBooks.GetString(2);
                            string mGenre = readerBooks.GetString(3);
                            string mSummary = readerBooks.GetString(4);
                            string mYearPublished = readerBooks.GetString(5);
                            string mPrice = readerBooks.GetDecimal(6).ToString();

                            string mMID = "";
                            if(!readerBooks.IsDBNull(7))
                            {
                                mMID = readerBooks.GetString(7);
                            }
                            

                            System.Diagnostics.Debug.WriteLine("Name: " + mName);

                            bookList.Add(new Book { BOOK_ID = mID, BOOK_NAME = mName, AUTHOR = mAuthor, GENRE = mGenre, SUMMARY = mSummary, YEAR_PUBLISHED = mYearPublished, PRICE = mPrice, MOVIE_ID = mMID });
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

                foreach(var item in movieList.ToList())
                {
                    if(item.TITLE.Equals(title))
                    {
                        results.Add(item);
                        movieList.Remove(item);
                    }
                }

                foreach(var item in bookList.ToList())
                {
                    if(item.BOOK_NAME.Equals(title))
                    {
                        results.Add(item);
                        bookList.Remove(item);
                    }
                }

                foreach (var item in movieList.ToList())
                {
                    string[] subStrings = title.Split(' ');

                    for(int i = 0; i < subStrings.Length; i++)
                    {
                        if (item.TITLE.Contains(subStrings[i]))
                        {
                            results.Add(item);
                            movieList.Remove(item);
                            break;
                        }
                    }

                    
                }

                foreach (var item in bookList.ToList())
                {
                    string[] subStrings = title.Split(' ');

                    for(int i = 0; i < subStrings.Length; i++)
                    {
                        if (item.BOOK_NAME.Contains(subStrings[i]))
                        {
                            results.Add(item);
                            bookList.Remove(item);
                            break;
                        }
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


                List<Book> bookList = new List<Book>();
                List<Movie> movieList = new List<Movie>();

                SqlCommand searchMovies = new SqlCommand();
                searchMovies.CommandType = System.Data.CommandType.Text;
                searchMovies.CommandText = "SELECT * FROM [MOVIE_INVENTORY]";
                searchMovies.Connection = db;

                SqlCommand searchBooks = new SqlCommand();
                searchBooks.CommandType = System.Data.CommandType.Text;
                searchBooks.CommandText = "SELECT * FROM [BOOK_INVENTORY]";
                searchBooks.Connection = db;



                try
                {
                    db.Open();

                    using (SqlDataReader readerMovies = searchMovies.ExecuteReader())
                    {
                        while (readerMovies.Read())
                        {
                            System.Diagnostics.Debug.WriteLine("At least 1 Movie");

                            string mID = readerMovies.GetString(0);
                            string mTitle = readerMovies.GetString(1);
                            string mDirector = readerMovies.GetString(2);
                            string mMainActor = readerMovies.GetString(3);
                            string mMainActress = readerMovies.GetString(4);
                            string mGenre = readerMovies.GetString(5);
                            string mSummary = readerMovies.GetString(6);
                            string mYearPublished = readerMovies.GetString(7);
                            string mPrice = readerMovies.GetDecimal(8).ToString();

                            string mBID = "";
                            if (!readerMovies.IsDBNull(9))
                            {
                                mBID = readerMovies.GetString(9);
                            }


                            System.Diagnostics.Debug.WriteLine("Name: " + mTitle);

                            movieList.Add(new Movie { MOVIE_ID = mID, TITLE = mTitle, DIRECTOR = mDirector, MAIN_ACTOR = mMainActor, MAIN_ACTRESS = mMainActress, GENRE = mGenre, SUMMARY = mSummary, YEAR_PUBLISHED = mYearPublished, PRICE = mPrice, BOOK_ID = mBID });
                        }
                    }

                    using (SqlDataReader readerBooks = searchBooks.ExecuteReader())
                    {
                        while (readerBooks.Read())
                        {
                            System.Diagnostics.Debug.WriteLine("At least 1 Book");

                            string mID = readerBooks.GetString(0);
                            string mName = readerBooks.GetString(1);
                            string mAuthor = readerBooks.GetString(2);
                            string mGenre = readerBooks.GetString(3);
                            string mSummary = readerBooks.GetString(4);
                            string mYearPublished = readerBooks.GetString(5);
                            string mPrice = readerBooks.GetDecimal(6).ToString();

                            string mMID = "";
                            if (!readerBooks.IsDBNull(7))
                            {
                                mMID = readerBooks.GetString(7);
                            }


                            System.Diagnostics.Debug.WriteLine("Name: " + mName);

                            bookList.Add(new Book { BOOK_ID = mID, BOOK_NAME = mName, AUTHOR = mAuthor, GENRE = mGenre, SUMMARY = mSummary, YEAR_PUBLISHED = mYearPublished, PRICE = mPrice, MOVIE_ID = mMID });
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

                foreach (var item in movieList.ToList())
                {
                    if (String.Compare(item.TITLE, title) == 0 && (String.Compare(item.GENRE, genre) == 0 || String.Compare(genre , "Any") == 0) && (String.Compare(item.YEAR_PUBLISHED, year) == 0) || (String.Compare(year, "Any") == 0))
                    {
                        results.Add(item);
                        movieList.Remove(item);
                    }
                }

                foreach (var item in bookList.ToList())
                {
                    if (String.Compare(item.BOOK_NAME, title) == 0 && (String.Compare(item.GENRE, genre) == 0 || String.Compare(genre, "Any") == 0) && (String.Compare(item.YEAR_PUBLISHED, year) == 0) || (String.Compare(year, "Any") == 0))
                    {
                        results.Add(item);
                        bookList.Remove(item);
                    }
                }

                foreach (var item in movieList.ToList())
                {
                    string[] subStrings = title.Split(' ');

                    for (int i = 0; i < subStrings.Length; i++)
                    {
                        if (String.Compare(item.TITLE, title) == 0 && (String.Compare(item.GENRE, genre) == 0 || String.Compare(genre, "Any") == 0) && (String.Compare(item.YEAR_PUBLISHED, year) == 0) || (String.Compare(year, "Any") == 0))
                        {
                            results.Add(item);
                            movieList.Remove(item);
                            break;
                        }
                    }


                }

                foreach (var item in bookList.ToList())
                {
                    string[] subStrings = title.Split(' ');

                    for (int i = 0; i < subStrings.Length; i++)
                    {
                        if (String.Compare(item.BOOK_NAME, title) == 0 && (String.Compare(item.GENRE, genre) == 0 || String.Compare(genre, "Any") == 0) && (String.Compare(item.YEAR_PUBLISHED, year) == 0) || (String.Compare(year, "Any") == 0))
                        {
                            results.Add(item);
                            bookList.Remove(item);
                            break;
                        }
                    }

                }


                GridView1.DataSource = results;
                GridView1.DataBind();

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchPage.aspx?");
        }
    }
}