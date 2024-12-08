﻿using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentManagement.Models;
using StudentManagement.Repositories.Abstraction;
using StudentManagement.Repositories.Implementation;

namespace StudentManagement
{
    public partial class Default : Page
    {
        private readonly IStudentRepository _repository;

        public Default()
        {
            _repository = new StudentRepository();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void BindGrid()
        {
            try
            {
                gvStudents.DataSource = _repository.GetAllStudents();
                gvStudents.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string gender = rdoMale.Checked ? "Male" : rdoFemale.Checked ? "Female" : string.Empty;

                if (string.IsNullOrEmpty(gender))
                {
                    lblMessage.Text = "Please select a gender.";
                    return;
                }

                string fileName = null;
                if (fuCV.HasFile)
                {
                    fileName = Path.GetFileName(fuCV.FileName);
                    string filePath = Server.MapPath("~/UploadedFiles/" + fileName);
                    fuCV.SaveAs(filePath);
                }

                Student student = new Student
                {
                    Name = txtName.Text,
                    Gender = gender,
                    Mobile = txtMobile.Text,
                    Email = txtEmail.Text,
                    Course = ddlCourse.SelectedValue,
                    CV = fileName
                };

                _repository.InsertStudent(student);
                lblMessage.Text = "Student added successfully!";
                ClearForm();
                BindGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

        protected void gvStudents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                int studentId = Convert.ToInt32(gvStudents.DataKeys[e.NewEditIndex].Value);
                Student student = _repository.GetStudentById(studentId);

                if (student != null)
                {
                    txtName.Text = student.Name;
                    rdoMale.Checked = student.Gender == "Male";
                    rdoFemale.Checked = student.Gender == "Female";
                    txtMobile.Text = student.Mobile;
                    txtEmail.Text = student.Email;
                    ddlCourse.SelectedValue = student.Course;
                    ViewState["StudentId"] = student.Id;
                    ViewState["StudentCV"] = student.CV;

                    // Show the Update button and hide the Submit button
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string gender = rdoMale.Checked ? "Male" : rdoFemale.Checked ? "Female" : string.Empty;

                if (string.IsNullOrEmpty(gender))
                {
                    lblMessage.Text = "Please select a gender.";
                    return;
                }

                string fileName = null;
                if (fuCV.HasFile)
                {
                    fileName = Path.GetFileName(fuCV.FileName);
                    string filePath = Server.MapPath("~/UploadedFiles/" + fileName);
                    fuCV.SaveAs(filePath);
                }

                if (ViewState["StudentId"] == null)
                {
                    lblMessage.Text = "Student ID is missing.";
                    return;
                }

                string studentCV = ViewState["StudentCV"]?.ToString() ?? string.Empty;

                Student student = new Student
                {
                    Id = int.Parse(ViewState["StudentId"].ToString()),
                    Name = txtName.Text,
                    Gender = gender,
                    Mobile = txtMobile.Text,
                    Email = txtEmail.Text,
                    Course = ddlCourse.SelectedValue,
                    CV = fileName ?? studentCV
                };

                _repository.UpdateStudent(student);
                lblMessage.Text = "Student updated successfully!";
                gvStudents.EditIndex = -1;
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                ClearForm();
                BindGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

        protected void gvStudents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvStudents.EditIndex = -1;
            BindGrid();
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            ClearForm();
        }

        protected void gvStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int studentId = Convert.ToInt32(gvStudents.DataKeys[e.RowIndex].Value);
                _repository.DeleteStudent(studentId);
                lblMessage.Text = "Student deleted successfully!";
                BindGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

        // Missing gvStudents_RowUpdating Method
        protected void gvStudents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int studentId = Convert.ToInt32(gvStudents.DataKeys[e.RowIndex].Value);

                // Retrieve the updated values
                string updatedName = ((TextBox)gvStudents.Rows[e.RowIndex].FindControl("txtEditName")).Text;
                string updatedMobile = ((TextBox)gvStudents.Rows[e.RowIndex].FindControl("txtEditMobile")).Text;
                string updatedEmail = ((TextBox)gvStudents.Rows[e.RowIndex].FindControl("txtEditEmail")).Text;
                string updatedCourse = ((DropDownList)gvStudents.Rows[e.RowIndex].FindControl("ddlEditCourse")).SelectedValue;

                // Update the student information
                Student student = new Student
                {
                    Id = studentId,
                    Name = updatedName,
                    Mobile = updatedMobile,
                    Email = updatedEmail,
                    Course = updatedCourse
                };

                _repository.UpdateStudent(student);
                gvStudents.EditIndex = -1;
                lblMessage.Text = "Student updated successfully!";
                BindGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

        private void ClearForm()
        {
            txtName.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlCourse.SelectedIndex = 0;
            rdoMale.Checked = false;
            rdoFemale.Checked = false;
            fuCV.Attributes.Clear();
        }
    }
}