﻿using System;
using System.Diagnostics;
using System.IO;

namespace FlagSync.Core
{
    public class Logger
    {
        /// <summary>
        /// Singleton instance of the logger (must be manually initialized)
        /// </summary>
        public static Logger Current { get; set; }

        /// <summary>
        /// Gets the path of the log file.
        /// </summary>
        /// <value>The path of the log file.</value>
        public string Path { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="path">The path of the log file.</param>
        public Logger(string path)
        {
            this.Path = path;

            try
            {
                using (File.Create(path))
                { }
            }

            catch (DirectoryNotFoundException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="message">The error message</param>
        public void LogError(string message)
        {
            this.Log(message, "ERROR");
        }

        /// <summary>
        /// Logs a succeed message
        /// </summary>
        /// <param name="message">The succeed message</param>
        public void LogSucceed(string message)
        {
            this.Log(message, "SUCCEED");
        }

        /// <summary>
        /// Logs a status message
        /// </summary>
        /// <param name="message">The status message</param>
        public void LogStatusMessage(string message)
        {
            this.Log(message, "STATUS");
        }

        /// <summary>
        /// Adds a log
        /// </summary>
        /// <param name="log">The log message</param>
        /// <param name="type">The message type, e.g "ERROR" or "SUCCEED"</param>
        private void Log(string log, string type)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(this.Path, true))
                {
                    writer.WriteLine(DateTime.Now.ToString() + " " + type + ": " + log);
                }
            }

            catch (IOException e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}