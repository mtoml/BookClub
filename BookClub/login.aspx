<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="BookClub.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<!--[if lt IE 7]> <html class="lt-ie9 lt-ie8 lt-ie7" lang="en"> <![endif]-->
<!--[if IE 7]> <html class="lt-ie9 lt-ie8" lang="en"> <![endif]-->
<!--[if IE 8]> <html class="lt-ie9" lang="en"> <![endif]-->
<!--[if gt IE 8]><!--> <html lang="en"> <!--<![endif]-->
<head>
  
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
  <title>Login</title>
  <link rel="stylesheet" href="assets/loginpage.css">
  <!--[if lt IE 9]><script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script><![endif]-->
</head>
<body>
 <section class="container">
    <div class="login">
      <h1>Login to Book Club</h1>
      <form id="frmLogin" runat="server">
          
        <p>
            Username:
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
        </p>
        <p>
            Password:
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnLoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" />
        </p>
        <p>
            <asp:Label ID="InvalidCredentialsMessage" runat="server" ForeColor="Red" Text="Your username or password is invalid. Please try again."
            Visible="False"></asp:Label>
        </p>
      </form>
    </div>
  </section>
</body>
</html>