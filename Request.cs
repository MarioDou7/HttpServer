﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTPServer
{
    public enum RequestMethod
    {
        GET,
        POST,
        HEAD
    }

    public enum HTTPVersion
    {
        HTTP10,
        HTTP11,
        HTTP09
    }

    class Request
    {
        string[] requestLines;
        RequestMethod method;
        public string relativeURI;
        Dictionary<string, string> headerLines;

        public Dictionary<string, string> HeaderLines
        {
            get { return headerLines; }
        }

        HTTPVersion httpVersion;
        string requestString;
        string[] contentLines;

        public Request(string requestString)
        {
            this.requestString = requestString;
        }
        /// <summary>
        /// Parses the request string and loads the request line, header lines and content, returns false if there is a parsing error
        /// </summary>
        /// <returns>True if parsing succeeds, false otherwise.</returns>
        public bool ParseRequest()
        {
            /*            throw new NotImplementedException();
            */
            //TODO: parse the receivedRequest using the \r\n delimeter
            requestLines = this.requestString.Split(new char[] {'\r','\n'});
            // check that there is atleast 3 lines: Request line, Host Header, Blank line (usually 4 lines with the last empty line for empty content)
            if (requestLines.Length < 3)
                return false;

            // Parse Request line
            if (!(this.ParseRequestLine())) return false;
            // Validate blank line exists
            if (!(this.ValidateBlankLine())) return false;

            // Load header lines into HeaderLines dictionary
        }

        private bool ParseRequestLine()
        {
            string requestline = requestLines[0];
            string[] tokens = requestline.Split(' ');

            if (tokens.Length != 3)      return false;
            if (tokens[0] != "GET")      return false;
            if (this.ValidateIsURI(tokens[1]))    return false;
            if (tokens[2] != "HTTP/1.1") return false;

            this.method = RequestMethod.GET;
            this.httpVersion = HTTPVersion.HTTP11;
            //to be continued

            return true;

        }

        private bool ValidateIsURI(string uri)
        {
            return Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute);
        }

        private bool LoadHeaderLines()
        {
            
        }

        private bool ValidateBlankLine()
        {
            if (requestLines[-1] == "")
                return true;

            return false;
        }

    }
}