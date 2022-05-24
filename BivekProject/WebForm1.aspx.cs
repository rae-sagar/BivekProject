using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;

namespace BivekProject
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        StringBuilder table = new StringBuilder();

        string strcon = ConfigurationManager.ConnectionStrings["HTMLDB"].ConnectionString;
        void Load_data()
        {

            SqlConnection con = new SqlConnection(strcon);
            con.Open();

            DateTime StartDate = Convert.ToDateTime(txtBoxTwo.Text);
            DateTime EndDate = Convert.ToDateTime(txtBoxThree.Text);
            if (drpList.SelectedValue == "0")
            {
                int[] droplistNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                string[] dropListNames = { "random", "Mobile Remit", "Songu-ri", "Hyehwa", "DDM CIS", "Mongol Town", "Gwangju", "Suwon", "GME Online", "Dongdaemun", "Ansan", "Hwaseong", "Gimhae" };

                foreach (int i in droplistNumbers)
                {
                    int l = 0;

                    foreach (DateTime day in EachCalendarDay(StartDate, EndDate))
                    {
                        placeHolderId.Controls.Clear();

                        SqlCommand comm = new SqlCommand("HTMLTableSP", con);
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Parameters.AddWithValue("@Flag", "Show");
                        comm.Parameters.AddWithValue("@TableValue", i);
                        comm.Parameters.AddWithValue("@DateSelected", string.Format("{0:yyyy/MM/dd}", day.ToString()));
                        comm.ExecuteNonQuery();

                        var forDate = day.ToString();

                        SqlDataReader rd = comm.ExecuteReader();

                        int idForTable = 1;
                        // ---------- Report Details -------------------- 
                        table.Append("<div class='ReportKPIClass'><label>Branch KPI Reports</label><br>");
                        table.Append("<label>From Date:<span class='labelClass'>'" + day.ToShortDateString() + "'</span></label> &nbsp<label>To Date: <span class='labelClass'>'" + day.ToShortDateString() + "'</span></label><br>");
                        table.Append("<label>Branch: <span class='labelClass'>'" + dropListNames[i] + "'</span></label></div>");

                        // ---------- Excel Download Button Section ----------- 
                        table.Append("<div style='position: relative;'><button onClick='Down_Excel(\"" + dropListNames[i].Replace(' ', 'a') + (i + l) + "\")' class='downloadBtnClass' id='" + dropListNames[i] + forDate + "'>");
                        table.Append("<img src='/excel img/excel2.jpg' height='20'/></button></div>");

                        // ----------- Table Heading --------- 
                        table.Append("<table border='1' class='table table-hover tableClass' id='" + dropListNames[i].Replace(' ', 'a') + (i + l) + "'>");
                        table.Append("<tr id='HeadingId'><th class='hiddenHeader'>HiddenId</th><th class='hiddenHeader'>BranchId</th><th class='hiddenHeader'>HiddenDate</th>");
                        table.Append("<th class='headingClass'>Sn</th><th class='headingClass'>Username</th><th class='headingClass'>Name</th><th class='headingClass'>Nationality</th>");
                        table.Append("<th class='headingClass'>Registration</th><th class='headingClass'>GMELoan</th><th class='headingClass'>SimCard</th><th class='headingClass'>GMEPass</th><th class='headingClass'>IssueSolved</th>");
                        table.Append("<th class='headingClass'>Other(Share,Bond,TaxRefund)</th><th class='headingClass'>StaffEfficiency</th><th class='headingClass'>BranchEfficiency</th><th class='headingClass'>Edit</th></tr>");

                        if (rd.HasRows)
                        {
                            while (rd.Read())
                            {
                                table.Append("<tr class='rowClass'>");
                                table.Append("<td class='hiddenColumnId'>" + rd[0] + "</td>");
                                table.Append("<td class='hiddenColumnId'>" + rd[1] + "</td>");
                                table.Append("<td class='hiddenColumnId'>" + rd[2] + "</td>");
                                table.Append("<td id='rowId'>" + idForTable + "</td>");
                                table.Append("<td>" + rd[3] + "</td>");
                                table.Append("<td>" + rd[4] + "</td>");
                                table.Append("<td>" + rd[5] + "</td>");
                                table.Append("<td><div class='row_data' id='RegId'>" + rd[6] + "</div></td>");
                                table.Append("<td><div class='row_data' id='GmeLoanId'>" + rd[7] + "</div></td>");
                                table.Append("<td><div class='row_data' id='SimCardId'>" + rd[8] + "</div></td>");
                                table.Append("<td><div class='row_data' id='GmePassId'>" + rd[9] + "</div></td>");
                                table.Append("<td><div class='row_data' id='IssueId'>" + rd[10] + "</div></td>");
                                table.Append("<td><div class='row_data' id='OtherId'>" + rd[11] + "</div></td>");
                                table.Append("<td>" + rd[12] + "</td>");
                                table.Append("<td><div id='BEclass'>" + rd[13] + "</div></td>");
                                table.Append("<td><div><input type='button' value='Update' class='btn btn-danger UpdateClass'/></div></td>");
                                table.Append("</tr>");

                                idForTable++;
                            }
                        }
                        else
                        {
                            table.Append("<tr><td colspan='13' style='text-align:center;font-weight: bold;'>No data found.</td></tr>");
                        }
                        l++;
                        table.Append("</table>");
                        placeHolderId.Controls.Add(new Literal { Text = table.ToString() });
                        rd.Close();
                    }
                }
            }
            else
            {
                foreach (DateTime day in EachCalendarDay(StartDate, EndDate))
                {
                    var forDate = day.ToShortDateString();
                    placeHolderId.Controls.Clear();

                    SqlCommand comm = new SqlCommand("HTMLTableSP", con);
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@Flag", "Show");
                    comm.Parameters.AddWithValue("@TableValue", drpList.SelectedValue);
                    comm.Parameters.AddWithValue("@DateSelected", string.Format("{0:yyyy/MM/dd}", day.ToString()));
                    comm.ExecuteNonQuery();


                    SqlDataReader rd = comm.ExecuteReader();

                    int idForTable = 1;
                    //------------------ Report Details --------------------
                    table.Append("<div class='ReportKPIClass'><label>Branch KPI Reports</label><br>");
                    table.Append("<label>From Date:<span class='labelClass'> '" + day.ToShortDateString() + "'</span></label> &nbsp<label>To Date:<span class='labelClass'> '" + day.ToShortDateString() + "'</span></label><br>");
                    table.Append("<label>Branch:<span class='labelClass'> '" + drpList.SelectedItem.Text + "'</span></label></div>");

                    // ----------------- Excel Download Button Section -----------------------
                    table.Append("<div style='position: relative;'><button id='" + forDate + "'  onClick='Down_Excel(\"" + forDate.Replace('/', 'a') + "\")' class='downloadBtnClass'>");
                    table.Append("<img src='/excel img/excel2.jpg' height='20'/></button></div>");

                    // ----------------- Table Heading ---------------------------------------
                    table.Append("<table border='1' class='table table-hover tableClass' id='" + forDate.Replace('/', 'a') + "' style='margin-left:20px; width:95%'>");
                    table.Append("<tr id='HeadingId'><th class='hiddenHeader'>HiddenId</th><th class='hiddenHeader'>BranchId</th><th class='hiddenHeader'>HiddenDate</th>");
                    table.Append("<th class='headingClass'>Sn</th><th class='headingClass'>Username</th><th class='headingClass'>Name</th><th class='headingClass'>Nationality</th>");
                    table.Append("<th class='headingClass'>Registration</th><th class='headingClass'>GMELoan</th><th class='headingClass'>SimCard</th><th class='headingClass'>GMEPass</th><th class='headingClass'>IssueSolved</th>");
                    table.Append("<th class='headingClass'>Other(Share,Bond,TaxRefund)</th><th class='headingClass'>StaffEfficiency</th><th class='headingClass'>BranchEfficiency</th><th class='headingClass'>Edit</th></tr>");

                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            table.Append("<tr class='rowClass'>");
                            table.Append("<td class='hiddenColumnId'>" + rd[0] + "</td>");
                            table.Append("<td class='hiddenColumnId'>" + rd[1] + "</td>");
                            table.Append("<td class='hiddenColumnId'>" + rd[2] + "</td>");
                            table.Append("<td id='rowId'>" + idForTable + "</td>");
                            table.Append("<td>" + rd[3] + "</td>");
                            table.Append("<td>" + rd[4] + "</td>");
                            table.Append("<td>" + rd[5] + "</td>");
                            table.Append("<td><div class='row_data' id='RegId'>" + rd[6] + "</div></td>");
                            table.Append("<td><div class='row_data' id='GmeLoanId'>" + rd[7] + "</div></td>");
                            table.Append("<td><div class='row_data' id='SimCardId'>" + rd[8] + "</div></td>");
                            table.Append("<td><div class='row_data' id='GmePassId'>" + rd[9] + "</div></td>");
                            table.Append("<td><div class='row_data' id='IssueId'>" + rd[10] + "</div></td>");
                            table.Append("<td><div class='row_data' id='OtherId'>" + rd[11] + "</div></td>");
                            table.Append("<td>" + rd[12] + "</td>");
                            table.Append("<td>" + rd[13] + "</td>");
                            table.Append("<td><div><input type='button' value='Update' class='btn btn-danger UpdateClass'/></div></td>");
                            table.Append("</tr>");

                            idForTable++;
                        }
                    }
                    else
                    {
                        table.Append("<tr><td colspan='13' style='text-align:center;font-weight: bold;'>No data found.</td></tr>");
                    }
                    table.Append("</table>");
                    placeHolderId.Controls.Add(new Literal { Text = table.ToString() });
                    rd.Close();
                }
                con.Close();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Search_Click(object sender, EventArgs e)
        {
            Load_data();

        }
        public static IEnumerable<DateTime> EachCalendarDay(DateTime startDate, DateTime endDate)
        {
            for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1)) yield
            return date;
        }

        [System.Web.Services.WebMethod]

        public static string Up_data(int Id, int Reg, int GmeLoan, int SimCard, int GmePass, int Issue, int Other)
        {
            string strcon = ConfigurationManager.ConnectionStrings["HTMLDB"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand comm = new SqlCommand("HTMLTableSP", con);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Flag", "Update");
            comm.Parameters.AddWithValue("@Id", Id);
            comm.Parameters.AddWithValue("@Registration", Reg);
            comm.Parameters.AddWithValue("@GMELoan", GmeLoan);
            comm.Parameters.AddWithValue("@SimCard", SimCard);
            comm.Parameters.AddWithValue("@GmePass", GmePass);
            comm.Parameters.AddWithValue("@IssueSolved", Issue);
            comm.Parameters.AddWithValue("@Other", Other);



            SqlDataReader res = comm.ExecuteReader();

            if (res.HasRows)
            {
                while (res.Read())
                {
                    int errorCode = Convert.ToInt32(res[0]);
                    //string msg = Convert.ToString(res[1]);

                    if (errorCode == 0)
                    {
                        return "Successfully Updated.";
                    }
                    else
                    {
                        return "Error.";
                    }
                }
            }

            con.Close();
            return "Error.";

            //var thisPage = new FinalTable();
            //thisPage.Load_data();

            //FinalTable obj = new FinalTable();
            //obj.Load_data();
        }
    }
}
