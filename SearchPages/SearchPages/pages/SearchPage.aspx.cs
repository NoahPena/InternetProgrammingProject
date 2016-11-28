using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;

namespace SearchPages.pages
{
    public partial class SearchPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            List<String> years = new List<string>();
            List<String> genres = new List<string>();

            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlCommand selectFromMovies = new SqlCommand();
            selectFromMovies.CommandType = System.Data.CommandType.Text;
            selectFromMovies.CommandText = "SELECT [Year] FROM [Movies]";
            selectFromMovies.Connection = db;

            SqlCommand selectFromBooks = new SqlCommand();
            selectFromBooks.CommandType = System.Data.CommandType.Text;
            selectFromBooks.CommandText = "SELECT [Year] FROM [Books]";
            selectFromBooks.Connection = db;

            SqlCommand selectGenreFromMovies = new SqlCommand();
            selectGenreFromMovies.CommandType = System.Data.CommandType.Text;
            selectGenreFromMovies.CommandText = "SELECT [Genre] FROM [Movies]";
            selectGenreFromMovies.Connection = db;

            SqlCommand selectGenreFromBooks = new SqlCommand();
            selectGenreFromBooks.CommandType = System.Data.CommandType.Text;
            selectGenreFromBooks.CommandText = "SELECT [Genre] FROM [Books]";
            selectGenreFromBooks.Connection = db;

            db.Open();

            years.Add("Any");
            genres.Add("Any");

            try
            {

                using (SqlDataReader readerMovies = selectFromMovies.ExecuteReader())
                {
                    while(readerMovies.Read())
                    {
                        years.Add(readerMovies.GetString(0));
                    }
                }

                using (SqlDataReader readerBooks = selectFromBooks.ExecuteReader())
                {
                    while (readerBooks.Read())
                    {
                        years.Add(readerBooks.GetString(0));
                    }
                }

                searchYearDropdown.DataSource = years;
                searchYearDropdown.DataBind();


                using (SqlDataReader reader = selectGenreFromMovies.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        genres.Add(reader.GetString(0));
                    }
                }

                using (SqlDataReader reader = selectGenreFromBooks.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        genres.Add(reader.GetString(0));
                    }
                }

                searchGenreDropdown.DataSource = genres;
                searchGenreDropdown.DataBind();

            }
            catch
            {

            }
            finally
            {
                db.Close();
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (advancedOptions.Visible)
            {
                Session["AdvancedOptions"] = true;
                Session["Name"] = TitleSearchBox.Text;
                Session["Year"] = searchYearDropdown.Text;
                Session["Genre"] = searchGenreDropdown.Text;

                if(BookAndMovieList.SelectedItem.Text.Equals("Yes"))
                {
                    Session["BookAndMovie"] = true;
                }
                else
                {
                    Session["BookAndMovie"] = false;
                }
            }
            else
            {
                Session["AdvancedOptions"] = false;
                Session["Name"] = TitleSearchBox.Text;

            }

            Response.Redirect("ResultsPage.aspx?");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            advancedOptions.Visible = !advancedOptions.Visible;
        }

    }
}