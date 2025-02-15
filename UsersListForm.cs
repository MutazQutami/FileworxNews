﻿using System.Windows.Forms;
using Newtonsoft.Json;

namespace FileworxsNews
{
    public partial class UsersListForm : Form
    {

        private void UsersList_Resize(object sender, EventArgs e)
        {
            ResizeUsersTable();
        }

        public UsersListForm()
        {
            InitializeComponent();
            ResizeUsersTable();
            RetreiveUsers();
        }


        public void ResizeUsersTable()
        {
            int _columnWidth = userList.Width;


            for (int i = 0; i < userList.Columns.Count; i++)
            {
                userList.Columns[i].Width = (int)(_columnWidth * 0.20);
            }


        }

        public void RetreiveUsers()
        {

            userList.Items.Clear();

            List<User> _users = FileHandler.JsonDeserializationObjects(new User()).Cast<User>().ToList();

            foreach (User item in _users)
            {
                ListViewItem listItem = new ListViewItem(item.Name);
                listItem.SubItems.Add(item.LogInName);
                listItem.SubItems.Add(item.Date.ToString());
                listItem.SubItems.Add(item.Password.ToString());
                listItem.SubItems.Add(item.Last_modifier);

                listItem.Tag = item;
                userList.Items.Add(listItem);

            }
        }

        private void userList_DoubleClick(object sender, EventArgs e)
        {




            if (userList.SelectedItems.Count > 0)
            {
                ListViewItem _selectedItem = userList.SelectedItems[0];
                var _selectedObject = _selectedItem.Tag;

                if (_selectedObject is User _selectedUser)
                {

                    UserForm form = new UserForm(_selectedUser);
                    this.Enabled = false;
                    form.ShowDialog();
                    this.Enabled = true; // Re-enable when done

                    if (form.DialogResult == DialogResult.OK)
                    {

                        User user1 = new User();

                        user1 = form.formUser;

                        _selectedItem.Text = user1.Name;  // Update the first column
                        _selectedItem.SubItems[1].Text = user1.LogInName;
                        //_selectedItem.SubItems[2].Text = user1.Name;
                        _selectedItem.SubItems[3].Text = user1.Password;
                        _selectedItem.SubItems[4].Text = user1.Last_modifier;

                    }

                }


            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            UserForm new1 = new UserForm();

            if (new1.ShowDialog() == DialogResult.OK)
            {
                User new2 = new1.formUser;
                ListViewItem _listItem = new ListViewItem(new2.Name);


                _listItem.SubItems.Add(new2.LogInName);
                _listItem.SubItems.Add(new2.Date.ToString());
                _listItem.SubItems.Add(new2.Password);
                _listItem.SubItems.Add(new2.Last_modifier);
                _listItem.SubItems.Add(new2.GuidValue.ToString());
                
                _listItem.Tag = new2;

                userList.Items.Insert(0, _listItem);


                return;
            }
        }
    }
}
