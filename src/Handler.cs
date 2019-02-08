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
namespace IMu
{
    /*!
    ** Provides a general low-level interface to creating server-side objects.
    ** 
    ** @usage
    **   IMu.Handler
    ** @end
    ** 
    ** @since 1.0
    */
    /// <summary>
    /// Provides a general low-level interface to creating server-side objects.
    /// </summary>
    public class Handler
    {
		/* Constructors */
        /*!
        ** Creates an object which can be used to interact with server-side objects.
        ** 
        ** @param session
		**   A `Session` [$<link>(:session:session)] object to be used to
		**   communicate with the IMu server.
        */
        /// <summary>
		/// Creates an object which can be used to interact with server-side
		/// objects.
        /// </summary>
        /// <param name="session">A Session object to be used to communicate 
		/// with the IMu server.</param>
		public
		Handler(Session session)
		{
			_session = session;

			_create = null;
			_destroy = false;
			_destroySet = false;
			_id = null;
			_language = null;
			_name = null;
		}

        /*!
        ** Creates an object which can be used to interact with server-side objects.
		** A new session is created automatically using the `Session`
		** [$<link>(:session:session)] class's default host and port values.
        */
        /// <summary>
        /// Creates an object which can be used to interact with server-side objects.
        /// A new session is created automatically using the Session class's 
        /// default host and port values.
        /// </summary>
		public Handler() :
			this(new Session())
		{
		}

		/* Properties */
        /*!
        ** @property Create
        **   An object to be passed to the server when the server-side object is
        **   created. 
		**
		**   To have any effect this must be set before any object methods
        **   are called. This property is usually only set by sub-classes of 
		**   `Handler` [$<link>(:handler:handler)].
        */
        /// <summary>
        /// An object to be passed to the server when the server-side object is 
        /// created. To have any effect this must be set before any object methods
        /// are called. This property is usually only set by sub-classes of Handler.
        /// </summary>
		public object Create
		{
			get
			{
				return _create;
			}
			set
			{
				_create = value;
			}
		}

        /*!
        ** @property Destroy
        **   A flag controlling whether the corresponding server-side object
		**   should e destroyed when the session is terminated.
        */
        /// <summary>
        /// A flag controlling whether the corresponding server-side object
        /// should be destroyed when the session is terminated.
        /// </summary>
		public bool Destroy
		{
			get
			{
				if (! _destroySet)
					return false;
				return _destroy;
			}
			set
			{
				_destroy = value;
				_destroySet = true;
			}
		}

        /*!
        ** @property ID 
        **   The unique identifier assigned to the server-side object once it has
        **   been created.
        */
        /// <summary>
        /// The unique identifier assigned to the server-side object once it has
        /// been created.
        /// </summary>
		public string ID
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}

        /*!
        ** @property Language
        **   The Language to be used in the server.
        */
        /// <summary>
        /// The Language to be used in the server.
        /// </summary>
		public string Language
		{
			get
			{
				return _language;
			}
			set
			{
				_language = value;
			}
		}

        /*!
        ** @property Name
        **   The name of the server-side object to be created. 
		**   This must be set before any object methods are called.
        */
        /// <summary>
        /// The name of the server-side object to be created. This must be set
        /// before any object methods are called.
        /// </summary>
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

        /*!
        ** @property Session
        **   The session object used by the handler to communicate with the IMu
        **   server.
        */
        /// <summary>
        /// The session object used by the handler to communicate with the IMu
        /// server.
        /// </summary>
		public Session Session
		{
			get
			{
				return _session;
			}
		}

		/* Methods */
        /*!
        ** Calls a method on the server-side object.
        ** 
        ** @param method
        **   The name of the method to be called.
        ** 
        ** @param parameters
        **   Any parameters to be passed to the method.
		**   The **Call()** method uses ``.Net's`` reflection to determin the
		**   structure of the parameters to be transmitted to the server.
        **   
        ** @returns
        **   An `object` containing the result returned by the server-side method.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Calls a method on the server-side object.
        /// </summary>
        /// <param name="method">The name of the method to be called.</param>
        /// <param name="parameters">
        /// Any parameters to be passed to the method. The Call method uses
        /// .Net's reflection to determin the structure of the parameters to be
        /// transmitted to the server.
        /// </param>
		/// <returns>An object containing the result returned by the
		/// server-side method.</returns>
		public object
		Call(string method, object parameters)
		{
			Map request = new Map();
			request.Add("method", method);
			if (parameters != null)
				request.Add("params", parameters);
			Map response = Request(request);
			return response["result"];
		}

        /*!
        ** Calls a method on the server-side object.
        ** 
        ** @param method
        **   The name of the method to be called.
        **   
        ** @returns
        **   An `object` containing the result returned by the server-side method.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Calls a method on the server-side object.
        /// </summary>
        /// <param name="method">The name of the method to be called.</param>
		/// <returns>An object containing the result returned by the
		/// server-side method.</returns>
		public object
		Call(string method)
		{
			return Call(method, null);
		}

        /*!
        ** Submits a low-level request to the IMu server.
        ** This method is chiefly used by the **Call( )** method
		** [$<link>(:handler:Call)] above.
        ** 
        ** @param request
        **   A `Map` [$<link>(:map:map)] object containing the request parameters.
        **   
        ** @returns
        **   A `Map` [$<link>(:map:map)] object containing the server's response.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Submits a low-level request to the IMu server.
        /// This method is chiefly used by the Call method above.
        /// </summary>
        /// <param name="request">A Map object containing the request parameters.</param>
        /// <returns>A Map object containing the server's response.</returns>
		public Map
		Request(Map request)
		{
			if (_id != null)
				request.Add("id", _id);
			else if (_name != null)
			{
				request.Add("name", _name);
				if (_create != null)
					request.Add("create", _create);
			}
			if (_destroySet)
				request.Add("destroy", _destroy);
			if (_language != null)
				request.Add("language", _language);

			Map response = _session.Request(request);

			if (response.ContainsKey("id"))
				_id = response["id"] as string;

			return response;
		}

		protected Session _session;

		protected object _create;
		protected bool _destroy;
        protected bool _destroySet;
		protected string _id;
		protected string _language;
		protected string _name;
	}
}
