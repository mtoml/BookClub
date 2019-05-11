<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="adminpanel.aspx.cs" Inherits="BookClub.adminpanel" %>

<!DOCTYPE html>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<title>2018 Voting Page</title>

	<link rel="stylesheet" href="assets/demo.css" />
	<link rel="stylesheet" href="assets/form-basic.css" />
    <link rel="Stylesheet" href="assets/tables.css" />
</head>
<body>
<header>
		<center><h1>The Electric Shepherds</h1></center>
        <a id="lnkLogoff" onserverclick="LogoffButton_Click" runat="server">Logoff</a>
    </header>
        
    <ul>
        <li><a href="voting.aspx">Voting</a></li>
        <li><a href="2018results.aspx">Results</a></li>
        <li><a href="adminpanel.aspx" class="active">Admin Panel</a></li>
        <%--<li><a href="form-mini.html">Mini</a></li>
        <li><a href="form-labels-on-top.html">Labels on Top</a></li>
        <li><a href="form-validation.html">Validation</a></li>
        <li><a href="form-search.html">Search</a></li>--%>
    </ul>


    <div class="main-content">

        <!-- You only need this form and the form-basic.css -->

        <form id="frmmain" runat="server" class="form-basic" method="post" action="#">
            <%--<object align="right"><asp:Button ID="cmdSignOut" runat="server" text="Sign Out" OnClick="LogoffButton_Click" /></object>--%>
            <div class="form-title-row">
                <h1>Admin Panel</h1>
            </div>

            <div class="form-row">
                <%--<asp:PlaceHolder ID="phRecords" runat="server"></asp:PlaceHolder>--%>
                <center><table id="tableContent" style="width:auto"  runat="server"></table></center>
                <%--<center><asp:Label ID="ThankYouForVoting" runat="server" ForeColor="Red" Text="Thank you for voting. Please check the results page for fun statistics"
            Visible="False"></asp:Label></center>--%>
                <%-- %><label>
                    <span>Full name</span>
                    <input type="text" name="name">
                </label>--%>
            </div>

            <%--<div class="form-row">
                <label>
                    <span>Email</span>
                    <input type="email" name="email">
                </label>
            </div>

            <div class="form-row">
                <label>
                    <span>Dropdown</span>
                    <select name="dropdown">
                        <option>Option One</option>
                        <option>Option Two</option>
                        <option>Option Three</option>
                        <option>Option Four</option>
                    </select>
                </label>
            </div>

            <div class="form-row">
                <label>
                    <span>Textarea</span>
                    <textarea name="textarea"></textarea>
                </label>
            </div>

            <div class="form-row">
                <label>
                    <span>Checkbox</span>
                    <input type="checkbox" name="checkbox" checked>
                </label>
            </div>

            <div class="form-row">
                <label><span>Radio</span></label>
                <div class="form-radio-buttons">

                    <div>
                        <label>
                            <input type="radio" name="radio">
                            <span>Radio option 1</span>
                        </label>
                    </div>

                    <div>
                        <label>
                            <input type="radio" name="radio">
                            <span>Radio option 2</span>
                        </label>
                    </div>

                    <div>
                        <label>
                            <input type="radio" name="radio">
                            <span>Radio option 3</span>
                        </label>
                    </div>

                </div>
            </div>--%>

            <div class="form-row">
                <center><asp:Button id="btnSubmit" runat="server" OnClick="btnSubmit_ClickAsync" Text="Submit" /></center>
            </div>

        </form>

    </div>
</body>
</html>