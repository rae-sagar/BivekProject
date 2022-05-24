<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="BivekProject.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html>

<html>
<head>
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.12.0/jquery.validate.min.js"></script>
   <script src="notjs/jquery.table2excel.js"></script>
    

    <style type="text/css">
        #HeadingId, #searchID{
            background-color:red;
            color:white;          
        }
        .ReportKPIClass{
            float:left;
            margin-left:20px;
        }
        .tableClass{
            margin-left:20px;
            width:95%;
        }
        .downloadBtnClass{
            background-color:transparent;
            border-color:transparent;
            float:right; 
            margin-right:35px;
            position:relative; 
            top:50px;
        }
        .rowClass,.headingClass{
            text-align:center;
        }
        .RedValidation:after {
            content:" *";
            color: red;
         }
        .RedValidation{
            font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
        }
        .boxClass{
            position:relative;
            right:60px;
        }
        .labelClass{
            color:firebrick;
            font-style:italic;

        }
    </style>
    <script type="text/javascript">

        function Down_Excel(idN) {
            debugger;
  
            var idName = idN;
            $('#'+idN).table2excel();        
        }
         
        function Valid_form() {
            var firstDate = document.getElementById("txtBoxTwo").value;
            var secondDate = document.getElementById("txtBoxThree").value;
            if (firstDate == "") {
                 $('#txtBoxTwo').css('border-color','red');
                alert("Plese select the date.");               
                return false;
            }
            if (secondDate == "") {
                $('#txtBoxTwo').css('border-color','black');
                $('#txtBoxThree').css('border-color','red');
                alert("Please select the date.");               
                return false;
            }
            return true;
        }

        $(document).ready(function () {          
            //------------hide section---------
            $('.hiddenHeader').hide();
            $('.hiddenColumnId').hide();
          
            //--------------makeEditable------
            $(document).on('click', '.row_data', function () {
                $(this).closest('div').attr('contenteditable', 'true')
                $(this).addClass('bg-warning').css('padding', '5px')
                $(this).focus()
            });

            $(document).on('focusout', '.row_data', function () {
                var row_div = $(this)
                .removeAttr('contenteditable')
                .removeClass('bg-warning')
                .css('padding', '')
            });         

           $(".row_data").keypress(function (e) {
              if(e.which == 46){
                  if ($(this).val().indexOf('.') != -1) {
                     alert("Number only.");
                    return false;
              }
                 }

               if (e.which != 8 && e.which != 0 && e.which != 46 && (e.which < 48 || e.which > 57)) {
                   alert("Number only.");
                  return false;
              }
            });

           

            $('#BEclass').attr('rowspan')

            $('.UpdateClass').click(function () {
                var currentRow = $(this).closest('tr');
                var col1 = currentRow.find('td:eq(0)').text();
                var col2 = currentRow.find('td:eq(7)').text();
                var col3 = currentRow.find('td:eq(8)').text();
                var col4 = currentRow.find('td:eq(9)').text();
                var col5 = currentRow.find('td:eq(10)').text();
                var col6 = currentRow.find('td:eq(11)').text();
                var col7 = currentRow.find('td:eq(12)').text();                
                
                var jsondata = {
                    Id: col1,
                    Reg: col2,
                    GmeLoan: col3,
                    SimCard: col4,
                    GmePass: col5,
                    Issue: col6,
                    Other: col7
                };

                     $.ajax({
                    url: 'WebForm1.aspx/Up_data',
                         type: 'Post',
                         data: JSON.stringify(jsondata),
                    contentType: 'application/json; charset=utf - 8',
                    dataType: 'json',
                    success: function (responce) {
                        alert(responce.d);
                    },      
                    failure: function() {
                         alert('Failed');
                    }
                    });          
            });
        });               
    </script> 
    <title></title>
</head>
<body>
    <label style="background-color:red; color:white; margin-left:20px">KPI Report Entry</label>
   <form id="form1" runat="server" class="col-md-12">
         <div class="panel panel-default" style="width:50%; background-color:ghostwhite; padding-top:20px; margin-left:6px;">         
            <div class="panel-body">
                <div class="form-group">
                    <asp:Label CssClass="col-md-4 RedValidation" Text="From Date:" runat="server" />
                    <asp:TextBox ID="txtBoxTwo" CssClass="col-md-8 input-sm boxClass" TextMode="Date" runat="server" />               
                </div>
            </div>

            <div class="panel-body">
                <div class="form-group">
                    <asp:Label CssClass="col-md-4 RedValidation" Text="To Date:" runat="server" />
                    <asp:TextBox ID="txtBoxThree" CssClass=" col-md-8 input-sm boxClass" TextMode="Date" runat="server" />                   
                </div>
            </div>

            <div class="panel-body">
                <div class="form-group">
                    <asp:Label CssClass="col-md-4 RedValidation" Text="Branch Name:" runat="server" />
                    <asp:DropDownList ID="drpList" CssClass=" col-md-8 input-sm boxClass" runat="server">
                        <asp:ListItem Text="All Branch" Value="0" />
                        <asp:ListItem Text="Mobile Remit" Value="1" />
                        <asp:ListItem Text="Songu-ri" Value="2" />
                        <asp:ListItem Text="Hyehwa" Value="3" />
                        <asp:ListItem Text="DDM CIS" Value="4" />
                        <asp:ListItem Text="Mongol Town" Value="5" />
                        <asp:ListItem Text="Gwangju" Value="6" />
                        <asp:ListItem Text="Suwon" Value="7" />
                        <asp:ListItem Text="GME Online" Value="8" />
                        <asp:ListItem Text="Dongdaemun" Value="9" />
                        <asp:ListItem Text="Ansan" Value="10" />
                        <asp:ListItem Text="Hwaseong" Value="11" />
                        <asp:ListItem Text="Gimhae" Value="12" />
                    </asp:DropDownList>                   
                </div>
            </div>
            
            <div class="panel-body">
                <asp:Button Text="Search" ID="searchID" CssClass="btn col-md-2 RedValidation" runat="server" OnClientClick="return Valid_form()" OnClick="Search_Click"/>              
            </div>
        </div>  
    </form>

     <asp:PlaceHolder ID="placeHolderId" runat="server"></asp:PlaceHolder>
</body>
</html>
</asp:Content>
