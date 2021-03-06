﻿using System;
using System.Text;
using ServiceStack;

namespace Way2Teste02.Models
{
    public static class StringExtensions
    {
        public static string AppendPath(this string uri, params string[] uriComponents)
        {
            return AppendUrlPaths(uri, uriComponents);
        }
        public static string AppendUrlPaths(this string uri, params string[] uriComponents)
        {
            var sb = new StringBuilder(uri.WithTrailingSlash());
            var i = 0;
            foreach (var uriComponent in uriComponents)
            {
                if (i++ > 0) sb.Append('/');
                sb.Append(uriComponent);
            }
            return sb.ToString();
        }

        public static string Fmt(this string text, params object[] args)
        {
            return String.Format(text, args);
        }
        public static string WithTrailingSlash(this string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            if (path[path.Length - 1] != '/')
            {
                return path + "/";
            }
            return path;
        }
    }
}
