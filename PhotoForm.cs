﻿using System;
using System.IO;
using System.Windows.Forms;

namespace FileworxsNews
{
    public partial class PhotoForm : Form
    {
        private void OnSaveButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitleField.Text) ||
                string.IsNullOrEmpty(txtDescriptionField.Text) ||
                string.IsNullOrEmpty(txtBodyField.Text))
            {
                checkUploadPhotoWarning.Hide();
                nullFieldsWarnning.Show();
            }
            else if (!string.IsNullOrEmpty(lblFilePath.Text))
            {
                if (!_editForm)
                {
                    formPhoto = new Photo();
                }

                formPhoto.Title = txtTitleField.Text;
                formPhoto.Description = txtDescriptionField.Text;
                formPhoto.Body = txtBodyField.Text;
                formPhoto.photoPath = lblFilePath.Text;

                FileHandler.JsonSerialization(formPhoto);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                nullFieldsWarnning.Hide();
                checkUploadPhotoWarning.Show();
            }
        }
        private void OnBrowsePhotoClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select an Image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string _sourcePath = openFileDialog.FileName;
                lblFilePath.Text = _sourcePath;
                pictureView.ImageLocation = _sourcePath;
                lblFilePath.Show();
            }
        }
        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private static bool _editForm;
        public Photo formPhoto;
        public PhotoForm()
        {
            InitializeComponent();
            nullFieldsWarnning.Hide();
            checkUploadPhotoWarning.Hide();
            _editForm = false;
        }
        public PhotoForm(Photo _photo)
        {
            InitializeComponent();
            txtTitleField.Text = _photo.Title;
            txtDescriptionField.Text = _photo.Description;
            txtBodyField.Text = _photo.Body;
            pictureView.ImageLocation = _photo.photoPath;
            lblFilePath.Text = _photo.photoPath;

            formPhoto = _photo;
            _editForm = true;

            nullFieldsWarnning.Hide();
            checkUploadPhotoWarning.Hide();
        }
    }
}
