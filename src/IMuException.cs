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

namespace IMu
{
    /*!
    ** Class for IMu-specific exceptions.
    **
	** @extends System.Exception
	**
    ** @usage
    **   IMu.IMuException
    ** @end
    ** 
    ** @since 1.0
    */
    public class IMuException : ApplicationException 
    {
		/* Constructors */
        /*!
        ** Creates an IMu-specific exception.
        ** 
        ** @param id
        **   A `String` exception code.
        ** 
        ** @param args
        **   Any additional arguments used to provide further information about
        **   the exception.
        **   
        */
        /// <summary>
        /// Creates an IMu-specific exception.
        /// </summary>
        /// <param name="id">A string exception code.</param>
        /// <param name="args">Any additional arguments used to provide further
        /// information about the exception.</param>
        public
        IMuException(string id, params object[] args)
            : base()
        {
            _id = id;
            _args = args;
            Trace.Write(2, "exception: {0}", ToString());
        }

        /*!
        ** Creates an IMu-specific exception.
        ** 
        ** @param id
        **   A `string` exception code.
        */
        /// <summary>
        /// Creates an IMu-specific exception.
        /// </summary>
        /// <param name="id">A string exception code.</param>
        public
		IMuException(string id)
			: base()
		{
			_id = id;
			_args = null;
            Trace.Write(2, "exception: {0}", ToString());
		}

		/* Properties */
        /*!
        ** @property Args
		**   The set of arguments associated with the exception.
        */
        /// <summary>
		/// The set of arguments associated with the exception.
        /// </summary>
		public object[] Args
		{
			get
			{
				return _args;
			}
			set
			{
				_args = value;
			}
		}

        /*!
        ** @property ID 
        **   The unique identifier assiged to the server-side object once it has
        **   been created.
        */
        /// <summary>
        /// The unique identifier assiged to the server-side object once it has 
        /// been created.
        /// </summary>
        public string ID
        {
            get
            {
                return _id;
            }
        }

        /* Methods */
        /*!
        ** Overrides the standard `Object` **ToString( )** method.
        ** 
        ** @returns
        **   A `string` description of the exception.
        */
        /// <summary>
        /// Overrides the standard Object ToString method.
        /// </summary>
        /// <returns>A string description of the exception.</returns>
        public override string
        ToString()
        {
            string str = _id;
            if (_args != null && _args.Length > 0)
            {
                str += " (";
                for (int i = 0; i < _args.Length; i++)
                {
                    if (i > 0)
                        str += ',';
					if (_args[i] != null)
						str += _args[i].ToString();
					else
						str += "null";
                }
                str += ')';
            }
            return str;
        }

        private string _id;
        private object[] _args;
    }
}
