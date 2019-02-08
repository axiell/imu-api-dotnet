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
using System.Collections.Generic;

namespace IMu
{
    /*!
    ** This class is used to create a set of search terms that is passed to the 
	** IMu server.
	**
	** A `Terms` object can be passed to the **FindTerms( )** method
	** [<$link>(:module:FindTerms)] of either a `Module`
	** [$<link>(:module:module)]or `Modules` object.
    ** 
    ** @usage
    **   IMu.Terms
    ** @end
    ** 
    ** @since 1.0
    */
    /// <summary>
    /// This class is used to create a set of search terms that is passed to the
	/// IMu server.
    /// A Terms object can be passed to the FindTerms method of either a Module
	/// or Modules object.
    /// </summary>
    public class Terms
    {
		/* Constructors */
        /*!
        ** Creates a new `Terms` object with the given ``kind``.
		**
		** @param kind
		**   The ``kind`` can be either:
		**   	* ``TermsKind.AND`` (for a set of AND terms) 
		**   	-OR-
		**   	* ``TermsKind.OR`` (for a set of OR terms)
        */
        /// <summary>
        /// Creates a new ``Terms`` object with the given ``kind``.
        /// </summary>
		/// <param name="kind">The ``kind`` can be either ``TermsKind.AND``
		/// (for a set of AND terms) or ``TermsKind.OR`` (for a set of OR
		/// terms).</param>
	    public
	    Terms(TermsKind kind)
	    {
		    _kind = kind;
		    _list = new List<Object>();
	    }
	
		/*!
		** Creates a new ``AND`` `Terms` object.
		**
		** This is the equivalent of:
		** @code
		**   Terms(TermsKind.AND)
		*/
	    public
	    Terms()
            : this(TermsKind.AND)
	    {
	    }
	    
		/* Properties */
        /*!
        ** @property Kind
        **   The kind of terms list as specified when the object was constructed.
        **   Will be either:
        **      * ``TermsKind.AND``
        **      -OR-
        **      * ``TermsKind.OR``
        */
        /// <summary>
        /// The kind of therms list as specified when the object was constructed.
        /// Will be either:
        ///     * TermsKind.AND
        ///     -OR-
        ///     * TermsKind.OR
        /// </summary>
	    public TermsKind Kind
	    {
		    get
            {
                return _kind;
            }
	    }
	    
        /*!
        ** @property List
        **   The list of search terms themselves.
        **   Each element in the list can be either:
        **      * A two or three element array comprising:
        **          - a column name
        **          - text to search for
        **          - an optional operator
        **      * A nested `Terms` object
        */
        /// <summary>
        /// The list of search terms themselves.
        /// Each element in the list can be either:
        ///     * A two or three element array comprising:
        ///         - a column name
        ///         - text to search for
        ///         - an optional operator
        ///     * A nested Terms object
        /// </summary>
	    public Object[] List
	    {
		    get
            {
                return _list.ToArray();
            }
	    }
	    
		/* Methods */
        /*!
        ** Adds a new term to the list.
        ** 
        ** @param name
        **   The name of a column or a search alias.
        **   
        ** @param value
        **   The value to match.
        **   
        ** @param op
        **   An operator to apply (such as ``contains``, ``=``,
        **   ``<``, etc.) for the server to apply when searching.
        */
        /// <summary>
        /// Adds a new term to the list.
        /// </summary>
        /// <param name="name">The name of a column or a search alias.</param>
        /// <param name="value">The value to match.</param>
        /// <param name="operator">An operator to apply (such as "contains", 
		/// "=", "&lt;" etc.) for the server to apply when searching.</param>
	    public void
	    Add(string name, string value, string op)
	    {
		    Object[] term = { name, value, op};
		    _list.Add(term);
	    }

        /*!
        ** Adds a new term to the list.
		**
		** This is the preferred method for adding terms in many cases as it 
		** allows the server to choose the most suitable operator.
        ** 
        ** @param name
        **   The name of a column or a search alias.
        **   
        ** @param value
        **   The value to match.
        */
        /// <summary>
        /// Adds a new term to the list.
		/// This is the preferred method for adding terms in many cases as it 
		/// allows the server to choose the most suitable operator.
        /// </summary>
        /// <param name="name">The name of a column or a search alias.</param>
        /// <param name="value">The value to match.</param>
	    public void
	    Add(String name, String value)
	    {
		    Add(name, value, null);
	    }
	
        /*!
        ** Adds an initially empty nested set of ``AND`` terms to the list.
		**
        ** This is a shortcut for:
        ** @code
        **   AddTerms(TermsKind.AND)
        ** 
        ** @returns
        **   The newly added `Terms` object.
        */
        /// <summary>
        /// Adds an initally empty nested set of AND terms to the list.
        /// This is a shortcut for: AddTerms(TermsKind.AND)
        /// </summary>
        /// <returns>The newly added Terms object.</returns>
	    public Terms
	    AddAnd()
	    {
		    return AddTerms(TermsKind.AND);
	    }

        /*!
        ** Adds an initially empty nested set of ``OR`` terms to the list.
        **
		** This is a shortcut for:
        ** @code
        **   AddTerms(TermsKind.OR)
        ** 
        ** @returns
        **   The newly added `Terms` object.
        */
        /// <summary>
        /// Adds an initally empty nested set of OR terms to the list.
        /// This is a shortcut for: AddTerms(TermsKind.OR)
        /// </summary>
        /// <returns>The newly added Terms object.</returns>
	    public Terms
	    AddOr()
	    {
		    return AddTerms(TermsKind.OR);
	    }
	
        /*!
        ** Adds an initially empty nested set of terms to the list.
		** 
		** @param kind
		**   The `boolean` operator to use for search terms added to the
		**   returned `Terms` object.
        ** 
        ** @returns
        **   The newly added `Terms` object.
        */
        /// <summary>
        /// Adds an initially empty nested set of terms to the list.
        /// </summary>
        /// <param name="kind"></param>
        /// <returns>The newly added Terms object.</returns>
	    public Terms
	    AddTerms(TermsKind kind)
	    {
		    Terms child = new Terms(kind);
		    _list.Add(child);
		    return child;
	    }

        public object[]
        ToArray()
        {
            object[] result = new object[2];

            result[0] = _kind.ToString();

            int size = _list.Count;
            object[] list = new object[size];
            for (int i = 0; i < size; i++)
            {
                object term = _list[i];
                if (term is Terms)
                    term = (term as Terms).ToArray();
                list[i] = term;
            }
            result[1] = list;

            return result;
        }

	    private TermsKind _kind;
	    private List<Object> _list;
    }
}
