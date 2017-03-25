using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeBlog.DAL;
using Moq;
using FakeBlog.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace FakeBlog.Tests.DAL
{
    //[TestClass]
    //public class FakeBlogRepoTests
    //{
    //    public Mock<FakeBlogContext> fakeContext { get; set; }
    //    public FakeBlogRepository repo { get; set; }
    //    public Mock<DbSet<Post>> mockPostSet { get; set; }
    //    public IQueryable<Post> queryPost { get; set; }
    //    public List<Post> fakePostTable { get; set; }

    //    public ApplicationUser sammy { get; set; }
    //    public ApplicationUser sally { get; set; }
    //    public Post postA { get; set; }
    //    public Post postB { get; set; }
    //    public Post postC { get; set; }

    //    [TestInitialize]
    //    public void Setup()
    //    {
    //        fakePostTable = new List<Post>();
    //        fakeContext = new Mock<FakeBlogContext>();
    //        mockPostSet = new Mock<DbSet<Post>>();
    //        repo = new FakeBlogRepository(fakeContext.Object);
    //        sammy = new ApplicationUser { AuthorId = "sammy-author-id", UserName = "Sammy", Id = "sammy-user-id", Email = "sammy@gmail.com" };
    //        sally = new ApplicationUser { AuthorId = "sally-author-id", UserName = "Sally", Id = "sally-user-id", Email = "sally@gmail.com" };
    //        postA = new Post { PostId = 12345, IsDraft = false, Title = "My First Post", Contents = "Sample text goes here.  I wonder what I will write about in the future.  No one will ever read this so it's ok.", DateCreated = DateTime.Now };
    //        postB = new Post { PostId = 23456, IsDraft = true, Title = "My Second Post", Contents = "I can't believe people read my first post ... I wonder what I will write about in the future.  No one will ever read this so it's ok.", DateCreated = DateTime.Now };
    //        postC = new Post { PostId = 34567, IsDraft = true, Title = "My First Bit Of Ideas", Contents = "This is going to be a blog post about bugs, will write more later.", DateCreated = DateTime.Now };
    //    }
    //    public void InitializeTempDatabase()
    //    {
    //        queryPost = fakePostTable.AsQueryable();
    //        mockPostSet.As<IQueryable<Post>>().Setup(p => p.Provider).Returns(queryPost.Provider);
    //        mockPostSet.As<IQueryable<Post>>().Setup(p => p.Expression).Returns(queryPost.Expression);
    //        mockPostSet.As<IQueryable<Post>>().Setup(p => p.ElementType).Returns(queryPost.ElementType);
    //        mockPostSet.As<IQueryable<Post>>().Setup(p => p.GetEnumerator()).Returns(() => queryPost.GetEnumerator());
    //        mockPostSet.Setup(p => p.Add(It.IsAny<Post>())).Callback((Post post) => fakePostTable.Add(post));
    //        mockPostSet.Setup(p => p.Remove(It.IsAny<Post>())).Callback((Post post) => fakePostTable.Remove(post));
    //        fakeContext.Setup(p => p.Posts).Returns(mockPostSet.Object);
    //    }

    //    [TestMethod]
    //    public void EnsureICanCreateInstanceOfRepo()
    //    {
    //        FakeBlogRepository repo = new FakeBlogRepository();
    //        Assert.IsNotNull(repo);
    //    }

    //    [TestMethod]
    //    public void EnsureIHaveNotNullContext()
    //    {
    //        FakeBlogRepository repo = new FakeBlogRepository();
    //        Assert.IsNotNull(repo.Context);
    //    }

    //    [TestMethod]
    //    public void EnsureICanInjectContextInstance()
    //    {
    //        FakeBlogContext context = new FakeBlogContext();
    //        FakeBlogRepository repo = new FakeBlogRepository(context);
    //        Assert.IsNotNull(repo.Context);
    //    }

    //    [TestMethod]
    //    public void EnsureICanCreatePost()
    //    {
    //        //Arrange
    //        InitializeTempDatabase();
    //        repo.AddPost(postA, sammy); //set up above prior to all tests
    //        int actualAnswer = 1;
    //        int expectedAnswer = repo.Context.Posts.Count();
    //        //Assert
    //        Assert.AreEqual(expectedAnswer, actualAnswer);
    //    }

    //    [TestMethod]
    //    public void EnsureICanGetSinglePostById()
    //    {
    //        //Arrange
    //        fakePostTable.Add(postA);
    //        InitializeTempDatabase();
    //        Post actualPost = repo.GetPost(postA.PostId); //set up above prior to all tests
    //        string actualPostTitle = repo.GetPost(postA.PostId).Title;
    //        string expectedPostTitle = "My First Post";
    //        Assert.IsNotNull(actualPost);
    //        Assert.AreEqual(expectedPostTitle, actualPostTitle);
    //    }

    //    [TestMethod]
    //    public void EnsureICanKnowIfPostIsDraft()
    //    {
    //        fakePostTable.Add(postB);
    //        InitializeTempDatabase();
    //        Post isThisADraft = repo.GetPost(postB.PostId);
    //        bool actualResult = isThisADraft.IsDraft;
    //        //get the bool value and compare to the expected
    //        Assert.AreEqual(true, actualResult);
    //    }

    //    [TestMethod]
    //    public void EnsureICanKnowIfPostIsNotADraft()
    //    {
    //        fakePostTable.Add(postA);
    //        InitializeTempDatabase();
    //        Post isThisADraft = repo.GetPost(postA.PostId);
    //        bool actualResult = isThisADraft.IsDraft;
    //        //get the bool value and compare to the expected
    //        Assert.AreEqual(false, actualResult);
    //    }

    //    [TestMethod]
    //    public void EnsureICanSwitchStatusFromDraftToPublished()
    //    {
    //        fakePostTable.Add(postC);
    //        InitializeTempDatabase();
    //        repo.PublishPost(postC.PostId);
    //        Assert.IsFalse(postC.IsDraft);

    //        //get the bool value and compare to the expected
    //    }

    //    [TestMethod]
    //    public void EnsureICanReturnContentsOfDraft()
    //    {
    //        fakePostTable.Add(postA);
    //        InitializeTempDatabase();
    //        Post postFromDatabase = repo.GetPost(postA.PostId); //set up above prior to all tests
    //        string actualContents = postFromDatabase.Contents;
    //        string expectedContents = "Sample text goes here.  I wonder what I will write about in the future.  No one will ever read this so it's ok.";
    //        Assert.AreEqual(expectedContents, actualContents);
    //    }

    //    [TestMethod]
    //    public void EnsureICanEditContentofDraft()
    //    {
    //        fakePostTable.Add(postA);
    //        InitializeTempDatabase();
    //        string newContents = "I early want this.";
    //        repo.EditPostBody(postA.PostId, newContents);
    //        string actualContents = repo.GetPost(postA.PostId).Contents;
    //        Assert.AreEqual(newContents, actualContents);
    //    }

    //    [TestMethod]
    //    public void EnsureICanReturnTitleOfDraft()
    //    {
    //        fakePostTable.Add(postA);
    //        InitializeTempDatabase();
    //        Post postFromDatabase = repo.GetPost(postA.PostId); //set up above prior to all tests
    //        string actualTitle = postFromDatabase.Title;
    //        string expectedTitle = "My First Post";
    //        Assert.AreEqual(expectedTitle, actualTitle);
    //    }

    //    [TestMethod]
    //    public void EnsureICanEditTitleofDraft()
    //    {
    //        fakePostTable.Add(postA);
    //        InitializeTempDatabase();
    //        string newTitle = "New Title";
    //        repo.EditPostTitle(postA.PostId, newTitle);
    //        string actualTitle = repo.GetPost(postA.PostId).Title;
    //        Assert.AreEqual(newTitle, actualTitle);
    //    }

    //    [TestMethod]
    //    public void EnsureICanRemovePost()
    //    {
    //        fakePostTable.Add(postA);
    //        fakePostTable.Add(postB);
    //        fakePostTable.Add(postC);
    //        InitializeTempDatabase();
    //        int expectedPostCount = 2;
    //        repo.RemovePost(postC.PostId);
    //        int actualPostCount = repo.Context.Posts.Count();
    //        Assert.AreEqual(expectedPostCount, actualPostCount);
    //    }
    //}
}
