﻿using System;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class SearchManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;

        public SearchManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Search Menu");
            Console.WriteLine(" 1) Search Blogs");
            Console.WriteLine(" 2) Search Authors");
            Console.WriteLine(" 3) Search Posts");
            Console.WriteLine(" 4) Search All");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Tag> ");
                    string blogTagName = Console.ReadLine();
                    SearchBlogs(blogTagName);
                    return this;
                case "2":
                    Console.Write("Tag> ");
                    string authorTagName = Console.ReadLine();
                    SearchAuthors(authorTagName);
                    return this;
                case "3":
                    Console.Write("Tag> ");
                    string postTagName = Console.ReadLine();
                    SearchPosts(postTagName);
                    return this;
                case "4":
                    Console.Write("Tag> ");
                    string tagName = Console.ReadLine();
                    SearchAll(tagName);
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void SearchAuthors(string tagName)
        {
            SearchResults<Author> results = _tagRepository.SearchAuthors(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }
        }
        private void SearchBlogs(string tagName)
        {
            SearchResults<Blog> results = _tagRepository.SearchBlogs(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }
        }
        private void SearchPosts(string tagName)
        {
            SearchResults<Post> results = _tagRepository.SearchPosts(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }
        }
        private void SearchAll(string tagName)
        {
            SearchAuthors(tagName);
            Console.WriteLine("--------------------");
            SearchBlogs(tagName);
            Console.WriteLine("--------------------");
            SearchPosts(tagName);
        }
    }
}