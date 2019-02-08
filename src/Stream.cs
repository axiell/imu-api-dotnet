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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace IMu
{
	class Stream
	{
		/* Static Properties */
		public static int BlockSize
		{
			get
			{
				return _blockSize;
			}
			set
			{
				_blockSize = value;
			}
		}

		/* Constructor */
		public
		Stream(Socket socket)
		{
			_socket = socket;

			try
			{
				_stream = new NetworkStream(_socket);
				_input = new BinaryReader(_stream);
				_output = new BinaryWriter(_stream);
			}
			catch (Exception e)
			{
				throw new IMuException("StreamIOSetup", e.Message);
			}

			_next = ' ';
			_token = null;
			_string = null;
			_file = null;
		}

		/* Methods */
		public Object
		Get()
		{
			Object what = null;
			try
			{
				GetNext();
				GetToken();
				what = GetValue();
			}
			catch (IMuException e)
			{
				throw e;
			}
			catch (Exception e)
			{
				throw new IMuException("StreamGet", e.Message);
			}
			return what;
		}

		public void
		Put(Object what)
		{
			try
			{
				PutValue(what, 0);
				PutLine();
				_output.Flush();
				_stream.Flush();
			}
			catch (IMuException e)
			{
				throw e;
			}
			catch (Exception e)
			{
				throw new IMuException("StreamPut", e.Message);
			}
		}

		private static int _blockSize = 8192;

		private Socket _socket;

		private NetworkStream _stream;
		private BinaryReader _input;
		private BinaryWriter _output;

        private char _next;
        private string _token;
        private string _string;
        private FileStream _file;

        private Object
        GetValue()
        {
            if (_token == "end")
                return null;
            if (_token == "string")
                return _string;
            if (_token == "number")
            {
                if (_string.Contains("."))
                    return double.Parse(_string);
                return long.Parse(_string);
            }
            if (_token == "{")
            {
                Map map = new Map();
                GetToken();
                while (_token != "}")
                {
                    string name = "";
                    if (_token == "string")
                        name = _string;
                    else if (_token == "identifier")
                        // Extension - allow simple identifiers
                        name = _string;
                    else
                        throw new IMuException("StreamSyntaxName", _token);

                    GetToken();
                    if (_token != ":")
                        throw new IMuException("StreamSyntaxColon", _token);

                    GetToken();
                    map.Add(name, GetValue());

                    GetToken();
                    if (_token == ",")
                        GetToken();
                }
                return map;
            }
            if (_token == "[")
            {
                ArrayList list = new ArrayList();
                GetToken();
                while (_token != "]")
                {
                    list.Add(GetValue());

                    GetToken();
                    if (_token == ",")
                        GetToken();
                }
                return list.ToArray();
            }
            if (_token == "true")
                return true;
            if (_token == "false")
                return false;
            if (_token == "null")
                return null;
            if (_token == "binary")
                return _file;

            throw new IMuException("StreamSyntaxToken", _token);
        }
       
        private void
        GetToken()
        {
			while (char.IsWhiteSpace(_next))
				GetNext();
			_string = "";
			_file = null;
			if (_next.Equals('"'))
			{
				_token = "string";
				GetNext();
				while (! _next.Equals('"'))
				{
					if (_next.Equals('\\'))
					{
						GetNext();
						if (_next.Equals('b'))
							_next = '\b';
						else if (_next.Equals('f'))
							_next = '\f';
						else if (_next.Equals('n'))
							_next = '\n';
						else if (_next.Equals('r'))
							_next = '\r';
						else if (_next.Equals('t'))
							_next = '\t';
						else if (_next.Equals('u'))
						{
							GetNext();
							string str = "";
							for (int i = 0; i < 4; i++)
							{
								if (char.IsDigit(_next))
									str += _next;
								else if (char.IsLetter(_next))
								{
									char lower = char.ToLower(_next);
									if (lower > 'f')
										break;
									str += lower;
								}
								else
									break;
								GetNext();
							}
							if (str.Length == 0)
								throw new IMuException("StreamSyntaxUnicode");
							int num = Convert.ToInt32(str, 16);
							_next = Convert.ToChar(num);
						}
					}
					_string += _next;
					GetNext();
				}
				GetNext();
			}
			else if (Char.IsDigit(_next) || _next.Equals('-'))
			{
				_token = "number";
				_string += _next;
				GetNext();
				while (char.IsDigit(_next))
				{
					_string += _next;
					GetNext();
				}
				if (_next.Equals('.'))
				{
					_string += _next;
					GetNext();
					while (char.IsDigit(_next))
					{
						_string += _next;
						GetNext();
					}
				}
				if (_next.Equals('e') || _next.Equals('E'))
				{
					_string += 'e';
					GetNext();
					if (_next.Equals('+'))
					{
						_string += '+';
						GetNext();
					}
					else if (_next.Equals('-'))
					{
						_string += '-';
						GetNext();
					}
					while (char.IsDigit(_next))
					{
						_string += _next;
						GetNext();
					}
				}
			}
			else if (char.IsLetter(_next) || _next.Equals('_'))
			{
				_token = "identifier";
				while (char.IsLetterOrDigit(_next) || _next.Equals('_'))
				{
					_string += _next;
					GetNext();
				}
				string lower = _string.ToLower();
				if (lower == "false")
					_token = "false";
				else if (lower == "null")
					_token = "null";
				else if (lower == "true")
					_token = "true";
			}
			else if (_next.Equals('*'))
			{
				// Extension - allow embedded binary data
				_token = "binary";
				GetNext();
				while (char.IsDigit(_next))
				{
					_string += _next;
					GetNext();
				}
				if (_string.Length == 0)
					throw new IMuException("StreamSyntaxBinary");
				long size = long.Parse(_string);
				while (! _next.Equals('\n'))
					GetNext();

				// Save data in a temporary file
				TempFile temp = new TempFile();
				FileStream stream = temp.GetOutputStream();
				byte[] data = new byte[_blockSize];
				long left = size;
				while (left > 0)
				{
					int read = _blockSize;
					if ((long) read > left)
						read = (int) left;
					int done = _input.Read(data, 0, read);
					if (done == 0)
						throw new IMuException("StreamEOF", "binary");
					stream.Write(data, 0, done);
					left -= done;
				}
				_file = temp.GetInputStream();

				GetNext();
			}
			else
			{
				_token = "" + _next;
				GetNext();
			}
		}

		private char
		GetNext()
		{
			int c = _input.ReadChar();
			if (c < 0)
				throw new IMuException("StreamEOF", "character");
			_next = (char) c;
			return _next;
		}

		private void
		PutValue(object what, int indent)
		{
            if (what == null)
                PutData("null");
            else if (what is string)
                PutString(what as string);
            else if (what is int)
                PutData(((int)what).ToString());
            else if (what is long)
                PutData(((long)what).ToString());
            else if (what is double)
                PutData(((double)what).ToString());
            else if (what is Map)
                PutMap(what as Map, indent);
            else if (what is Hashtable)
                PutHashtable(what as Hashtable, indent);
            else if (what is Array)
                PutArray(what as Array, indent);
            else if (what is ArrayList)
                PutArray((what as ArrayList).ToArray(), indent);
            else if (what is List<object>)
                PutArray((what as List<object>).ToArray(), indent);
            else if (what is bool)
                PutData((bool)what ? "true" : "false");
            else if (what is FileStream)
                PutStream(what as FileStream);
            else
                throw new IMuException("StreamType", what.GetType().ToString());
		}

		private void
		PutString(string what)
		{
            PutData('"');
            char[] chars = what.ToCharArray(0, what.Length);
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '"' || chars[i] == '\\')
                    PutData('\\');
                PutData(chars[i]);
            }
            PutData('"');
		}

		private void
		PutMap(Map what, int indent)
		{
            PutData('{');
            PutLine();
            IDictionaryEnumerator iter = what.GetEnumerator();
            int i = 0;
            while (iter.MoveNext())
            {
                PutIndent(indent + 1);
                PutString(iter.Key.ToString());
                PutData(" : ");
                PutValue(iter.Value, indent + 1);
                if (i < what.Count - 1)
                    PutData(',');
                PutLine();
                i++;
            }
            PutIndent(indent);
            PutData('}');
		}

        private void
        PutHashtable(Hashtable what, int indent)
        {
            PutData('{');
            PutLine();
            IDictionaryEnumerator iter = what.GetEnumerator();
            int i = 0;
            while (iter.MoveNext())
            {
                PutIndent(indent + 1);
                PutString(iter.Key.ToString());
                PutData(" : ");
                PutValue(iter.Value, indent + 1);
                if (i < what.Count - 1)
                    PutData(',');
                PutLine();
                i++;
            }
            PutIndent(indent);
            PutData('}');
        }

		private void
		PutArray(Array what, int indent)
		{
            PutData('[');
            PutLine();
            int i = 0;
            foreach (Object obj in what)
            {
                PutIndent(indent + 1);
                PutValue(obj, indent + 1);
                if (i < what.Length - 1)
                    PutData(',');
                PutLine();
                i++;
            }
            PutIndent(indent);
            PutData(']');
		}

        private void
        PutStream(FileStream what)
        {
            long size = what.Length;
            PutData('*');
            PutData(size.ToString());
            PutLine();

            byte[] data = new Byte[BlockSize];
            long left = size;
            while (left > 0)
            {
				int need = BlockSize;
				if ((long) need > left)
					need = (int) left;
                int done = what.Read(data, 0, need);
                if (done == 0)
                    break;
                _output.Write(data, 0, done);
                left -= done;
            }
			if (left > 0)
			{
				/* The file did not contain enough bytes
				** so the output is padded with nulls
				*/
				data = new Byte[BlockSize];
				while (left > 0)
				{
					int need = BlockSize;
					if ((long) need > left)
						need = (int) left;
					_output.Write(data, 0, need);
					left -= need;
				}
			}
        }

        private void
        PutIndent(int indent)
        {
            for (int i = 0; i < indent; i++)
                PutData('\t');
        }

        private void
        PutLine()
        {
            PutData('\r');
            PutData('\n');
        }

        private void
        PutData(char chr)
        {
            _output.Write(chr);
        }

        private void
        PutData(string str)
        {
            _output.Write(str.ToCharArray(0, str.Length));
        }
	}
}
