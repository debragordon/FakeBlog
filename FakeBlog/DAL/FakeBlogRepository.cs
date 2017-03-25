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

        public void AddPost(string name, string contents, ApplicationUser owner) 
        {
            _blogConnection.Open();

            try
            {
                var addPostCommand = _blogConnection.CreateCommand();
                addPostCommand.CommandText = @"
                    INSERT INTO Posts(Title,Contents,AuthorId)
                    VALUES(@title,@contents,@authorId)";

                var nameParameter = new SqlParameter("title", SqlDbType.VarChar);
                nameParameter.Value = name;
                addPostCommand.Parameters.Add(nameParameter);

                var contentsParameter = new SqlParameter("contents", SqlDbType.VarChar);
                contentsParameter.Value = contents;
                addPostCommand.Parameters.Add(contentsParameter);

                var authorParameter = new SqlParameter("authorId", SqlDbType.Int);
                authorParameter.Value = owner.Id;
                addPostCommand.Parameters.Add(authorParameter);

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

            _blogConnection.Open();
            try
            {
                var getPostCommamnd = _blogConnection.CreateCommand();
                getPostCommamnd.CommandText = @"
                SELECT PostId, IsDraft, Title, Contents, DateCReated, AuthorId
                FROM Posts
                WHERE PostId = @postId";

                var postIdParameter = new SqlParameter("postId", SqlDbType.Int);
                postIdParameter.Value = postId;
                getPostCommamnd.Parameters.Add(postIdParameter);

                var matchingPosts = getPostCommamnd.ExecuteReader();
                if (matchingPosts.Read())
                {
                    return new Post
                    {
                        AuthorId = new ApplicationUser { Id = matchingPosts.GetString(5) },
                        Contents = matchingPosts.GetString(3),
                        Title = matchingPosts.GetString(2),
                        PostId = matchingPosts.GetInt32(0),
                        DateCreated = matchingPosts.GetDateTime(4),
                        IsDraft = matchingPosts.GetBoolean(1)
                    };
                }
            }
            finally
            {
                _blogConnection.Close();
            }
            return null;
        }

        public void EditPostBody(int postId, string newContents)
        {
            //Post postToEdit = GetPost(postId);
            //if (postToEdit.Contents != null)
            //{
            //    postToEdit.Contents = newContents;
            //    Context.SaveChanges();
            //}

            try
            {
                var editContentsCommand = _blogConnection.CreateCommand();
                editContentsCommand.CommandText = @"
                    UPDATE Posts
                    SET Contents = @newContents
                    WHERE PostId = @id";

                var postIdParameter = new SqlParameter("postId", SqlDbType.Int);
                postIdParameter.Value = postId;
                editContentsCommand.Parameters.Add(postIdParameter);

                var contentsParameter = new SqlParameter("contents", SqlDbType.VarChar);
                contentsParameter.Value = newContents;
                editContentsCommand.Parameters.Add(contentsParameter);

                _blogConnection.Open();
                editContentsCommand.ExecuteNonQuery();
            }
            finally
            {
                _blogConnection.Close();
            }
        }

        public void EditPostTitle(int postId, string newTitle)
        {
            //Post postToEdit = GetPost(postId);
            //if (postToEdit.Title != null)
            //{
            //    postToEdit.Title = newTitle;
            //    Context.SaveChanges();
            //}
            try
            {
                var editTitleCommand = _blogConnection.CreateCommand();
                editTitleCommand.CommandText = @"
                    UPDATE Posts
                    SET Title = @newTitle
                    WHERE PostId = @id";

                var postIdParameter = new SqlParameter("postId", SqlDbType.Int);
                postIdParameter.Value = postId;
                editTitleCommand.Parameters.Add(postIdParameter);

                var contentsParameter = new SqlParameter("newTitle", SqlDbType.VarChar);
                contentsParameter.Value = newTitle;
                editTitleCommand.Parameters.Add(contentsParameter);

                _blogConnection.Open();
                editTitleCommand.ExecuteNonQuery();
            }
            finally
            {
                _blogConnection.Close();
            }

        }

        public void PublishPost(int postId)
        {
            //Post postToEdit = GetPost(postId);
            //if (postToEdit.IsDraft.Equals(true))
            //{
            //    postToEdit.IsDraft = false;
            //    Context.SaveChanges();
            //}
            try
            {
                var publishPostCommand = _blogConnection.CreateCommand();
                publishPostCommand.CommandText = @"
                    UPDATE Posts
                    SET IsDraft = 0
                    WHERE PostId = @id";

                var postIdParameter = new SqlParameter("postId", SqlDbType.Int);
                postIdParameter.Value = postId;
                publishPostCommand.Parameters.Add(postIdParameter);

                _blogConnection.Open();
                publishPostCommand.ExecuteNonQuery();
            }
            finally
            {
                _blogConnection.Close();
            }


        }

        public void RemovePost(int postId)
        {
            //Post postToRemove = GetPost(postId);
            //if (postToRemove != null)
            //{
            //    Context.Posts.Remove(postToRemove);
            //    Context.SaveChanges();
            //}

            try
            {
                var removePostCommand = _blogConnection.CreateCommand();
                removePostCommand.CommandText = @"
                    DELETE
                    FROM Posts
                    WHERE postId = @postId";

                var postIdParam = new SqlParameter("postId", SqlDbType.Int);
                postIdParam.Value = postId;
                removePostCommand.Parameters.Add(postIdParam);

                _blogConnection.Open();
                var rowsAffected = removePostCommand.ExecuteNonQuery();

                if (rowsAffected != 1)
                {
                    throw new Exception($"Query didn't work, {rowsAffected} rows were affected");
                }
            }
            finally
            {
                _blogConnection.Close();
            }
        }
    }
}