using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using FakeBlog.Models;
using FakeBlog.Contracts.Controllers;

namespace FakeBlog.DAL
{
    public class FakeBlogRepository : IPostRepository
    {
        IDbConnection _blogConnection;

        public FakeBlogRepository(IDbConnection blogConnection)
        {
            _blogConnection = blogConnection;
        }

        public void AddPost(string name, ApplicationUser owner) 
        {
            _blogConnection.Open();

            try
            {
                var addPostCommand = _blogConnection.CreateCommand();
                addPostCommand.CommandText = "Insert into Boards(Name,Owner_Id) values(@name,@ownerId)";
                var nameParameter = new SqlParameter("name", SqlDbType.VarChar);
                nameParameter.Value = name;
                addPostCommand.Parameters.Add(nameParameter);
                var ownerParameter = new SqlParameter("owner", SqlDbType.Int);
                ownerParameter.Value = owner.Id;
                addPostCommand.Parameters.Add(ownerParameter);

                addPostCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _blogConnection.Close();
            }

        }

        // stopped here
        public Post GetPost(int postId)
        {
            //Post postIWant = Context.Posts.FirstOrDefault(p => p.PostId == postId);
            //return postIWant;
        }

        public void EditPostBody(int postId, string newContents)
        {
            //Post postToEdit = GetPost(postId);
            //if (postToEdit.Contents != null)
            //{
            //    postToEdit.Contents = newContents;
            //    Context.SaveChanges();
            //}
        }

        public void EditPostTitle(int postId, string newTitle)
        {
            //Post postToEdit = GetPost(postId);
            //if (postToEdit.Title != null)
            //{
            //    postToEdit.Title = newTitle;
            //    Context.SaveChanges();
            //}
        }

        public void PublishPost(int postId)
        {
            //Post postToEdit = GetPost(postId);
            //if (postToEdit.IsDraft.Equals(true))
            //{
            //    postToEdit.IsDraft = false;
            //    Context.SaveChanges();
            //}
        }

        public void RemovePost(int postId)
        {
            //Post postToRemove = GetPost(postId);
            //if (postToRemove != null)
            //{
            //    Context.Posts.Remove(postToRemove);
            //    Context.SaveChanges();
            //}
        }
    }
}