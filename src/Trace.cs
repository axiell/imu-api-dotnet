/* KE Software Open Source Licence
** 
** Notice: Copyright (c) 2011-2013 KE SOFTWARE PTY LTD (ACN 006 213 298)
** (the "Owner"). All rights reserved.
** 
** Licence: Permission is hereby granted, free of charge, to any person
** obtaining a copy of this software and associated documentation files
** (the "Software"), to deal with the Software without restriction,
** including without limitation the rights to use, copy, modify, merge,
** publish, distribute, sublicense, and/or sell copies of the Software,
** and to permit persons to whom the Software is furnished to do so,
** subject to the following conditions.
** 
** Conditions: The Software is licensed on condition that:
** 
** (1) Redistributions of source code must retain the above Notice,
**     these Conditions and the following Limitations.
** 
** (2) Redistributions in binary form must reproduce the above Notice,
**     these Conditions and the following Limitations in the
**     documentation and/or other materials provided with the distribution.
** 
** (3) Neither the names of the Owner, nor the names of its contributors
**     may be used to endorse or promote products derived from this
**     Software without specific prior written permission.
** 
** Limitations: Any person exercising any of the permissions in the
** relevant licence will be taken to have accepted the following as
** legally binding terms severally with the Owner and any other
** copyright owners (collectively "Participants"):
** 
** TO THE EXTENT PERMITTED BY LAW, THE SOFTWARE IS PROVIDED "AS IS",
** WITHOUT ANY REPRESENTATION, WARRANTY OR CONDITION OF ANY KIND, EXPRESS
** OR IMPLIED, INCLUDING (WITHOUT LIMITATION) AS TO MERCHANTABILITY,
** FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. TO THE EXTENT
** PERMITTED BY LAW, IN NO EVENT SHALL ANY PARTICIPANT BE LIABLE FOR ANY
** CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
** TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
** SOFTWARE OR THE USE OR OTHER DEALINGS WITH THE SOFTWARE.
** 
** WHERE BY LAW A LIABILITY (ON ANY BASIS) OF ANY PARTICIPANT IN RELATION
** TO THE SOFTWARE CANNOT BE EXCLUDED, THEN TO THE EXTENT PERMITTED BY
** LAW THAT LIABILITY IS LIMITED AT THE OPTION OF THE PARTICIPANT TO THE
** REPLACEMENT, REPAIR OR RESUPPLY OF THE RELEVANT GOODS OR SERVICES
** (INCLUDING BUT NOT LIMITED TO SOFTWARE) OR THE PAYMENT OF THE COST OF SAME.
*/
using System;
using System.IO;

namespace IMu
{
    public class Trace
    {
        /* Static Properties */
        public static string File
        {
            get
            {
                return _file;
            }
            set
            {
                _file = value;
                
                if (_handle != null && _handle != System.Console.Out)
                    _handle.Close();
				if (_stream != null)
					_stream.Close();

                if (_file == null || _file == "")
                {
                    _file = "";
					_stream = null;
                    _handle = null;
                }
                else if (_file == "STDOUT")
				{
					_stream = null;
                    _handle = System.Console.Out;
				}
                else
                {
                    try
                    {
						FileMode mode = FileMode.Append;
						FileAccess access = FileAccess.Write;
                        _stream = new FileStream(_file, mode, access);
                        _handle = new StreamWriter(_stream);
                    }
                    catch (System.Exception)
                    {
                        _file = "";
						_stream = null;
                        _handle = null;
                    }
                }
            }
        }

        public static int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }

		public static string Prefix
		{
			get
			{
				return _prefix;
			}
			set
			{
				_prefix = value;
			}
		}

        /* Methods */
        public static void
        Write(int level, string format, params object[] args)
        {
            if (_handle == null)
                return;
			if (level > _level)
				return;

			/* time */
			DateTime now = DateTime.Now;
            string y = now.Year.ToString();
            string m = String.Format("{0:d2}", now.Month);
            string d = String.Format("{0:d2}", now.Day);
			string D = y + "-" + m + "-" + d;

			string H = String.Format("{0:d2}", now.Hour);
            string M = String.Format("{0:d2}", now.Minute);
            string S = String.Format("{0:d2}", now.Second);
            string T = H + ":" + M + ":" + S;

            /* process id */
            if (_pid == null)
                _pid = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();

			/* function information */
			/* TODO */

			/* Build the prefix */
			string prefix = _prefix;
            prefix = prefix.Replace("%y", y);
            prefix = prefix.Replace("%m", m);
            prefix = prefix.Replace("%d", d);
            prefix = prefix.Replace("%D", D);

            prefix = prefix.Replace("%H", H);
            prefix = prefix.Replace("%M", M);
            prefix = prefix.Replace("%S", S);
            prefix = prefix.Replace("%T", T);

            prefix = prefix.Replace("%l", level.ToString());

            prefix = prefix.Replace("%p", _pid);

			/* Write it out */
			if (_stream != null)
			{
				try
				{
					_stream.Lock(0, 1);
 				}
                catch (Exception)
                {
                    return;
                }
                try
                {
                    _stream.Seek(0, SeekOrigin.End);
                }
                catch (Exception)
                {
                    _stream.Unlock(0, 1);
                    return;
                }
            }
			_handle.Write(prefix);
            _handle.Write(format, args);
            _handle.WriteLine();
            _handle.Flush();
			if (_stream != null)
			{
				try
				{
					_stream.Unlock(0, 1);
				}
				catch (System.Exception)
				{
				}
			}
        }

        private static string _file = "STDOUT";
		private static FileStream _stream = null;
        private static TextWriter _handle = System.Console.Out;

        private static int _level = 1;
		private static string _prefix = "%D %T: ";
        private static string _pid = null;
    }
}
