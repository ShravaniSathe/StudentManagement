<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StudentManagement.Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>
<html>
<head>
    <title>Student Management</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f6f9;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        #form1 {
            background-color: #ffffff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.2);
            width: 60%; /* Standard form width */
            min-width: 400px; /* To avoid the form getting too narrow */
        }

        h2 {
            color: #333;
            text-align: center;
        }

        .form-field {
            margin-bottom: 15px;
        }

        .form-field label {
            font-size: 16px;
            color: black;
            font-weight: bold; /* Make all labels bold */
        }

        .gender-field .radio-buttons label {
            font-weight: normal; /* Male and Female text not bold */
            margin-left: 10px;
            margin-bottom: 0;
        }

        .form-field input, .form-field select {
            width: 100%;
            padding: 10px;
            margin-top: 5px;
            border-radius: 5px;
            border: 1px solid #ccc;
            box-sizing: border-box;
        }

        .form-field input[type="file"] {
            padding: 5px;
        }

        .gender-field {
            display: flex;
            flex-direction: column;
            align-items: flex-start;
        }

        .gender-field .radio-buttons {
            display: flex;
            flex-direction: row;
            align-items: center;
        }
       
        .button-container {
            display: flex;
            justify-content: space-between;
            margin-top: 20px;
        }

        .button-container input, .button-container button {
            padding: 10px 20px;
            border: none;
            background-color: #007bff;
            color: white;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
        }

        .button-container input:hover, .button-container button:hover {
            background-color: #0056b3;
        }

        .button-container button {
            background-color: #28a745;
        }

        .button-container button:hover {
            background-color: #218838;
        }

        .button-container .btn-cancel {
            background-color: #dc3545;
        }

        .button-container .btn-cancel:hover {
            background-color: #c82333;
        }

        hr {
            margin-top: 20px;
            border: 0;
            border-top: 1px solid #ccc;
        }

        .grid-container {
            display: flex;
            justify-content: center; /* Center the grid horizontally */
            margin-top: 20px;
            margin-bottom: 20px;
        }

        .grid-container .GridView {
            width: 100%;
            border-collapse: collapse;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }

        .grid-container .GridView th, .grid-container .GridView td {
            padding: 15px;
            text-align: center;
            border: 3px solid #ddd; /* Bold grid borders */
        }

        .grid-container .GridView th {
            background-color: #007bff;
            color: white;
        }

        .grid-container .GridView td {
            background-color: #f9f9f9;
        }

        .grid-container .GridView tr:nth-child(even) td {
            background-color: #f1f1f1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Student Form</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />

        <!-- Student Form Fields -->
        <div class="form-field" style="font-weight: bold;">
            <asp:Label Text="Name:" AssociatedControlID="txtName" runat="server" />
            <asp:TextBox ID="txtName" runat="server" />
            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" InitialValue="" ErrorMessage="Name is required" ForeColor="Red" />
        </div>

        <!-- Gender Selection -->
        <div class="form-field gender-field">
            <asp:Label Text="Gender:" runat="server" style="font-weight: bold;" />
            <asp:RadioButtonList ID="rblGender" runat="server">
                <asp:ListItem Text="Male" Value="Male" />
                <asp:ListItem Text="Female" Value="Female" />
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="rblGender" InitialValue="" ErrorMessage="Gender is required" ForeColor="Red" />
        </div>

        <div class="form-field">
            <asp:Label Text="Mobile:" AssociatedControlID="txtMobile" runat="server" />
            <asp:TextBox ID="txtMobile" runat="server" />
            <asp:RegularExpressionValidator ID="revMobile" runat="server" ControlToValidate="txtMobile" ValidationExpression="^\d{10}$" ErrorMessage="Enter a valid 10-digit mobile number" ForeColor="Red" />
        </div>

        <div class="form-field">
            <asp:Label Text="Email:" AssociatedControlID="txtEmail" runat="server" />
            <asp:TextBox ID="txtEmail" runat="server" />
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" InitialValue="" ErrorMessage="Email is required" ForeColor="Red" />
        </div>

        <!-- Course and CV Fields -->
        <div class="form-field">
            <asp:Label Text="Course:" runat="server" style="font-weight: bold;" />
            <asp:DropDownList ID="ddlCourse" runat="server">
                <asp:ListItem Text="Select" Value="" />
                <asp:ListItem Text="MBA" Value="MBA" />
                <asp:ListItem Text="MCA" Value="MCA" />
                <asp:ListItem Text="B Tech" Value="B Tech" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse" InitialValue="" ErrorMessage="Course is required" ForeColor="Red" />
        </div>

        <div class="form-field">
            <asp:Label Text="CV:" AssociatedControlID="fuCV" runat="server" />
            <asp:FileUpload ID="fuCV" runat="server" />
            <asp:RequiredFieldValidator ID="rfvCV" runat="server" ControlToValidate="fuCV" ErrorMessage="CV is required" ForeColor="Red" />
        </div>

        <!-- Submit Button -->
        <div class="button-container">
            <asp:Button Text="Submit" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" />
            <asp:Button Text="Update" ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Visible="false" />
        </div>

        <hr />

        <!-- GridView to display the students -->
        <div class="grid-container">
            <asp:GridView ID="gvStudents" AutoGenerateColumns="false" DataKeyNames="Id" runat="server" OnRowEditing="gvStudents_RowEditing" OnRowUpdating="gvStudents_RowUpdating" OnRowCancelingEdit="gvStudents_RowCancelingEdit" OnRowDeleting="gvStudents_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Course" HeaderText="Course" SortExpression="Course" />
                    <asp:BoundField DataField="CV" HeaderText="CV" SortExpression="CV" />
                    <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" ButtonType="Button" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
