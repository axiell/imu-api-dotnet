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
using System.Net;
using System.Net.Sockets;

namespace IMu
{
	/*!
	** Manages a connection to an IMu server.
	**
	** The server’s host name and port can be specified by setting properties on
	** the object or by setting class-based default properties.
	**
	** @usage
	**   IMu.Session
	** @end
	**
	** @since 1.0
	**
	** @example
	**   Connect to the default server.
	**
	** @code
	**   using IMu;
	**
	**   Session session = new Session();
	**   session.Connect();
	*/
	/*!
	** @example
	**   Set the default host and port and then connect.
	**
	** @code
	**   using IMu;
	**
	**   Session.DefaultHost = "server.com";
	**   Session.DefaultPort = 40136;
	**
	**   Session session = new Session();
	**   session.Connect();
	*/
    /// <summary>
    /// Manages a connection to an IMu server.
    /// The server's host name and port can be specified by setting properties on
    /// the object or by setting class-based default properties.
    /// </summary>
    public class Session
    {
		/* Static Properties */
		/*!
        ** @property DefaultHost
        **   The name of the host used to create a connection if no object-specific
        **   host has been supplied.
		*/
        /// <summary>
        /// The name of the host used to create a connection if no object-specific
        /// host has been supplied.
        /// </summary>
		public static string DefaultHost
		{
			get
			{
				return _defaultHost;
			}
			set
			{
				_defaultHost = value;
			}
		}

		/*!
        ** @property DefaultPort
		**   The number of the port used to create a connection if no
		**   object-specific host has been supplied.
		*/
        /// <summary>
		/// The number of the port used to create a connection if no
		/// object-specific host has been supplied.
        /// </summary>
		public static int DefaultPort
		{
			get
			{
				return _defaultPort;
			}
			set
			{
				_defaultPort = value;
			}
		}

		/* Constructors */
		/*!
		** Creates a `Session` object with the specified ``host`` and ``port``.
		**
		** @param host
		**   The default server host.
		**
		** @param port
		**   The default server port.
		*/
        /// <summary>
        /// Creates a Session object with the specified host and port.
        /// </summary>
        /// <param name="host">The default server host.</param>
        /// <param name="port">The default server port.</param>
		public
		Session(string host, int port)
		{
			Initialise();
			_host = host;
			_port = port;
		}

		/*!
		** Creates a `Session` object with the specified ``host``.
		**
		** The port to connect on will be taken from the 
		** ``DefaultPort`` class property [$<link>(:session:DefaultPort)].
		** 
		** @param host
		**   The default server host.
		*/
        /// <summary>
        /// Creates a Session object with the specified host.
		/// The port to connect on will be taken from the DefaultPort class
		/// property.
        /// </summary>
        /// <param name="host">The default server host.</param>
		public
		Session(string host)
		{
			Initialise();
			_host = host;
		}

		/*!
		** Creates a `Session` object with the specified ``port``.
		**
		** The host to connect to will be taken from the 
		** ``DefaultHost`` class property [$<link>(:session:DefaultHost)].
		** 
		** @param port
		**   The default server port.
		*/
        /// <summary>
        /// Creates a Session object with the specified port.
		/// The host to connect to will be taken from the DefaultHost class
		/// property.
        /// </summary>
        /// <param name="port">The default server port.</param>
        public
        Session(int port)
        {
            Initialise();
            _port = port;
        }

		/*!
		** Creates a `Session` object.
		**
		** The host to connect to will be taken from the 
		** ``DefaultHost`` class property [$<link>(:session:DefaultHost)].
		**
		** The port to connect on will be taken from the 
		** ``DefaultPort`` class property [$<link>(:session:DefaultPort)].
		*/
        /// <summary>
        /// Creates a Session object.
		/// The host to connect to will be taken from the DefaultHost class 
		/// property.
		/// The port to connect on will be taken from the DefaultPort class
		/// property.
        /// </summary>
		public
		Session()
		{
			Initialise();
		}

		/* Properties */
		/*!
        ** @property Close
        **   A flag controlling whether the connection to the server should be 
        **   closed after the next request. 
		**   This flag is passed to the server as part of the next request to 
		**   allow it to clean up.
		*/
        /// <summary>
        /// A flag controlling whether the connection to the server should be
        /// closed after the next request. This flag is passed to the server as
		/// part of the next request to allow it to clean up.
        /// </summary>
		public bool Close
		{
			get
			{
				if (! _closeSet)
					return false;
				return _close;
			}
			set
			{
				_close = value;
				_closeSet = true;
			}
		}

		/*!
        ** @property Context
        **   The unique identifer assigned by the server to the current session.
		*/
        /// <summary>
        /// The unique identifier assigned by the server to the current session.
        /// </summary>
		public string Context
		{
			get
			{
				return _context;
			}
			set
			{
				_context = value;
			}
		}

		/*!
        ** @property Host
        **   The name of the host used to create the connection.
        **   Setting this property after the connection has been established has
		**   no effect.
		*/
        /// <summary>
        /// The name of the host used to create the connection.
        /// Setting this property after the connection has been established has
		/// no effect.
        /// </summary>
		public string Host
		{
			get
			{
				return _host;
			}
			set
			{
				_host = value;
			}
		}

		/*!
        ** @property Port
        **   The number of the port used to create the connection. 
        **   Setting this property after the connection has been established has
		**   no effect.
		*/
        /// <summary>
        /// The number of the port used to create the connection.
        /// Setting this property after the connection has been established has
		/// no effect.
        /// </summary>
		public int Port
		{
			get
			{
				return _port;
			}
			set
			{
				_port = value;
			}
		}

		/*!
        ** @property Suspend
        **   A flag controlling whether the server process handling this session
		**   should begin listening on a distinct, process-specific port to 
		**   ensure a new session connects to the same server process. 
		**
		**   This is part of IMu's mechanism for maintaining state.
        **   If this flag is set to ``true``, then after the next request is 
		**   made to the server, the `Session`\'s ``port`` 
		**   [$<link>(:session:Port)] property will be altered to the
		**   process-specific port number.
		*/
        /// <summary>
        /// A flag controlling whether the server process handling this session 
		/// should begin listening on a distinct, process-specific port to 
		/// ensure a new session connects to the same server process. 
		/// This is part of IMu's mechanism for maintaining state.
        /// If this flag is set to true, then after the next request is made to 
		/// the server, the Session's Port property will be altered to the 
		/// process-specific port number.
        /// </summary>
		public bool Suspend
		{
			get
			{
				if (! _suspendSet)
					return false;
				return _suspend;
			}
			set
			{
				_suspend = value;
				_suspendSet = true;
			}
		}

		/* Methods */
		/*!
		** Opens a connection to an IMu server.
		**
		** @throws IMuException
		**   The connection could not be opened.
		*/
        /// <summary>
        /// Opens a connection to an IMu server.
        /// </summary>
		public void
		Connect()
		{
			if (_socket != null)
				return;

			Trace.Write(2, "connecting to {0}:{1}", _host, _port);
			try
			{
				/* Create socket */
				AddressFamily family = AddressFamily.InterNetwork;
				SocketType type = SocketType.Stream;
				ProtocolType protocol = ProtocolType.Tcp;
				_socket = new Socket(family, type, protocol);

				/* Get host information */
				IPHostEntry host = Dns.GetHostEntry(_host);
				IPAddress address = host.AddressList[0];

				/* Connect */
				IPEndPoint peer = new IPEndPoint(address, _port);
				_socket.Connect(peer);
			}
			catch (Exception e)
			{
                string mesg = e.Message;
				Trace.Write(2, "connection failed: {0}", mesg);
                _socket = null;
				throw new IMuException("SessionConnect", _host, _port, mesg);
			}
			Trace.Write(2, "connected ok");
			_stream = new Stream(_socket);
		}

		/*!
        ** Closes the connection to the IMu server.
		*/
        /// <summary>
        /// Closes the connection to the IMu server.
        /// </summary>
		public void
		Disconnect()
		{
			if (_socket == null)
				return;

			Trace.Write(2, "closing connection");
			try
			{
				_socket.Close();
			}
			catch (Exception)
			{
				// ignore
			}
			Initialise();
		}

		/*!
		** Logs in as the given user with the given password.
		**
		** The ``password`` parameter may be ``null``.
		** This will cause the server to use server-side authentication (such as
		** .rhosts authentication) to authenticate the user.
		**
		** @param login
		**   The name of the user to login as.
		**
		** @param password
		**   The user's password for authentication.
		**
		** @param spawn
		**   A flag indicating whether the process should create a new child
		**   process specifically for handling the newly logged in user's
		**   requests.  
		**   This value defaults to **true**.
		**
		** @throws IMuException
		**   The login request failed.
		**
		** @throws Exception
		**   A low-level socket communication error occurred.
		*/
        /// <summary>
        /// Logs in as the given ser with the given password.
		/// The passowrd parameter may be null. This will cause the server to 
		/// use server-side authentication (such as .rhosts authentication) to
		/// authenticate the user.
        /// If the spawn parameter is set to true, this will cause the server
        /// to create a new child process specifically to handle the newly logged 
        /// in user's requests.
        /// </summary>
        /// <param name="login">The name of the user to login as.</param>
        /// <param name="password">The user's password for authentication.</param>
        /// <param name="spawn">A flag indicating whether the process should create a
        /// new child process specifically for handling the newly logged in user's
        /// requests. This value defaults to true.</param>
        /// <returns></returns>
        public Map
        Login(string login, string password, bool spawn)
        {
			Map request = new Map();
			request.Add("login", login);
			request.Add("password", password);
			request.Add("spawn", spawn);
			return Request(request);
        }

		/*!
		** Logs in as the given user with the given password.
		**
		** The ``password`` parameter may be ``null``.
		** This will cause the server to use server-side authentication (such as
		** .rhosts authentication) to authenticate the user.
		**
		** ``spawn`` defaults to ``true`` causing the server to create a new 
		** child process specifically for handling the newly logged in
		** user's requests.
		**
		** @param login
		**   The name of the user to login as.
		**
		** @param password
		**   The user's password for authentication.
		**
		** @throws IMuException
		**   The login request failed.
		**
		** @throws Exception
		**   A low-level socket error occurred.
		**
		**
		*/
        /// <summary>
        /// Logs in as the given user with the given password.
		/// The password parameter may be null. This will cause the server to 
		/// use server-side authentication (such as .rhosts authentication)
		/// to authenticate the user.
		/// Spawn defaults to true causing the server to create a new child
		/// process specifically for handling the newly logged in user's requests.
        /// </summary>
        /// <param name="login">The name of the user to login as.</param>
        /// <param name="password">The user's password for authentication</param>
        /// <returns></returns>
        public Map
        Login(string login, string password)
        {
			return Login(login, password, true);
        }

		/*!
		** Logs in as the given user.
		**
		** Uses server-side authentication (such as .rhosts authentication) to
		** authenticate the user.
		**
		** The server will spawn a new child process specifically for handling
		** the newly logged in user's requests.
		**
		** @param login
		**   The name of the user to login as.
		**
		** @throws IMuException
		**   The login request failed.
		**
		** @throws Exception
		**   A low-level socket error occurred.
		*/
        public Map
        Login(string login)
        {
			return Login(login, null, true);
        }

		/*!
		** Logs the user out of the server.
		**
		** @since 2.0
		*/
		public Map
		Logout()
		{
			Map request = new Map();
			request.Add("logout", true);
			return Request(request);
		}

		/*!
        ** Submits a low-level request to the IMu server.
        ** 
        ** @param request
        **   A `Map` [$<link>(:map:map)] containing the request parameters.
        **   
        ** @returns
        **   A `Map` [$<link>(:map:map)] object containing the server's response.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
		*/
        /// <summary>
        /// Submits a low-level request to the IMu server.
        /// </summary>
        /// <param name="request">A Map containing the request parameters.</param>
        /// <returns>A Map object containing the server's response</returns>
        public Map
        Request(Map request)
        {
			Connect();

			if (_closeSet)
				request.Add("close", _close);
			if (_context != null)
				request.Add("context", _context);
			if (_suspendSet)
				request.Add("suspend", _suspend);

			_stream.Put(request);
			object raw = _stream.Get();
			if (! (raw is Map))
			{
				string type = raw.GetType().ToString();
				Trace.Write(2, "bad type of response from server: {0}", type);
				throw new IMuException("SessionResponse", type);
			}
			Map response = raw as Map;

			if (response.ContainsKey("context"))
				_context = response["context"] as string;
			if (response.ContainsKey("reconnect"))
				_port = Int32.Parse(response["reconnect"].ToString());

			string status = response["status"] as string;
			if (status == "error")
			{
				Trace.Write(2, "server error");

				string id = "SessionServerError";
				if (response.ContainsKey("error"))
					id = response["error"] as string;
				else if (response.ContainsKey("id"))
					id = response["id"] as string;

                IMuException e = new IMuException(id);

				if (response.ContainsKey("args"))
					e.Args = response["args"] as object[];

				Trace.Write(2, "throwing server exception {0}", e.ToString());

				throw e;
			}

			return response;
        }

		private static string _defaultHost = "127.0.0.1";
		private static int _defaultPort = 40000;

		private bool _close;
		private bool _closeSet;
		private string _context;
		private string _host;
		private int _port;
		private Socket _socket;
		private Stream _stream;
		private bool _suspend;
		private bool _suspendSet;

		private void
		Initialise()
		{
			_close = false;
			_closeSet = false;
			_context = null;
			_host = _defaultHost;
			_port = _defaultPort;
			_socket = null;
			_stream = null;
			_suspend = false;
			_suspendSet = false;
		}
    }
}
