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
    ** Provides access to an EMu module.
    ** 
	** @extends IMu.Handler
	**
    ** @usage
    **   IMu.Module
    ** @end
    ** 
    ** @since 1.0
    */
    /// <summary>
    /// Provides access to an EMu module.
    /// </summary>
    public class Module : Handler
    {
		/* Constructors */
        /*!
        ** Creates an object which can be used to access the EMu module specified
        ** by ``table``.
        ** 
        ** @param table
        **   Name of the EMu module to be accessed.
        **   
        ** @param session
		**   A `Session` object [$<link>(:session:session)] to be used to
		**   communicate with the IMu server.
        */
        /// <summary>
        /// Creates an object which can be used to access the EMu module specified
        /// by table.
        /// </summary>
        /// <param name="table">Name of the EMu module to be accessed.</param>
        /// <param name="session">A Session object to be used to communicate with
		/// the IMu server.</param>
		public Module(string table, Session session)
			: base(session)
		{
			Initialise(table);
		}

        /*!
        ** Creates an object which can be used to access the EMu module specified
		** by ``table`` and the `Session` class's [$<link>(:session:session)]
		** default **host** [$<link>(:session:Host)] and **port**
		** [$<link>(:session:Port)] values.
        ** 
        ** @param table
        **   Name of the EMu module to be accessed.
        */
        /// <summary>
        /// Creates an object which can be used to access the EMu module specified
		/// by table and the Session class's default host and port values.
        /// </summary>
        /// <param name="table">Name of the EMu module to be accessed.</param>
		public Module(string table)
			: base()
		{
			Initialise(table);
		}

		/* Properties */
        /*!
        ** @property Table
		**   The name of the table associated with the `Module` object
		**   [$<link>(:module:module)].
        */
        /// <summary>
        /// The name of the table associated with the Module object.
        /// </summary>
		public string Table
		{
			get
			{
				return _table;
			}
		}

		/* Methods */
        /*!
        ** Associates a set of columns with a logical name in the server. 
		**
		** The name can be used instead of a column list when retrieving data 
		** using **Fetch( )** [$<link>(:module:Fetch)].
        ** 
        ** @param name
        **   The logical name to associate with the set of columns.
        **   
        ** @param columns
        **   A `String` containing the names of the columns to be used when  
        **   ``name`` is passed to **Fetch( )** [$<link>(:module:Fetch)]. 
		**   The column names must be separated by a ``semi-colon`` or a 
		**   ``comma``.
        **   
        ** @returns
        **   The number of sets (including this one) registered in the server.
		**
		** @throws IMuException
		**   If a server-side error occurred.
        */
        /// <summary>
        /// Associates a set of columns with a logical name in the server. The name 
        /// can be used instead of a column list when retrieving data using Fetch.
        /// </summary>
        /// <param name="name">The logical name to associate with the set of columns.</param>
        /// <param name="columns">A string containing the names of the columns to be 
        /// used when name is passed to Fetch. The column names must be separated by
        /// a semi-colon of a comma.</param>
        /// <returns>The number of sets (including this one) registered in the server.</returns>
        public int
        AddFetchSet(string name, string columns)
        {
            return DoAddFetchSet(name, columns);
        }

        /*!
        ** Associates a set of columns with a logical name in the server. 
		** The name can be used instead of a column list when retrieving data 
		** using **Fetch( )** [$<link>(:module:Fetch)].
        ** 
        ** @param name
        **   The logical name to associate with the set of columns.
        **   
        ** @param columns
        **   An array of `String`\s containing the names of the columns to be used 
		**   when ``name`` is passed to **Fetch( )** [$<link>(:module:Fetch)].
        **   
        ** @returns
        **   The number of sets (including this one) registered in the server.
		**
		** @throws IMuException
		**   If a server-side error occurred.
        */
        /// <summary>
        /// Associates a set of columns with a logical name in the server. The name 
        /// can be used instead of a column list when retrieving data using Fetch.
        /// </summary>
        /// <param name="name">The logical name to associate with the set of columns.</param>
        /// <param name="columns">An array of strings containing the names of the columns to be 
        /// used when name is passed to Fetch.</param>
        /// <returns>The number of sets (including this one) registered in the server.</returns>
        public int
		AddFetchSet(string name, string[] columns)
		{
            return DoAddFetchSet(name, columns);
		}

        /*!
		** Associates a set of columns with a logical name in the server. The
		** name can be used instead of a column list when retrieving data using
		** **Fetch( )** [$<link>(:module:Fetch)].
        ** 
        ** @param name
        **   The logical name to associate with the set of columns.
        **   
        ** @param columns
        **   A list of `String`\s containing the names of the columns to be 
		**   used when ``name`` is passed to **Fetch( )** 
		**   [$<link>(:module:Fetch)].
        **   
        ** @returns
        **   The number of sets (including this one) registered in the server.
		** 
		** @throws IMuException
		**   If a server-side error occurred.
        */
        /// <summary>
        /// Associates a set of columns with a logical name in the server. 
		/// The name can be used instead of a column list when retrieving data 
		/// using Fetch.
        /// </summary>
        /// <param name="name">The logical name to associate with the set of 
		/// columns.</param>
        /// <param name="columns">A list of strings containing the names of the 
		/// columns to be used when name is passed to Fetch.</param>
        /// <returns>The number of sets (including this one) registered in the 
		/// server.</returns>
        public int
        AddFetchSet(string name, List<string> columns)
        {
            return DoAddFetchSet(name, columns.ToArray());
        }

        /*!
        ** Associates several sets of columns with logical names in the server.
		**
		** This is the equivalent of calling **AddFetchSet( )** 
		** [$<link>(:module:AddFetchSet)] for each entry in the map but is
		** more efficient.
        ** 
        ** @param sets
        **   A `Map` [$<link>(:map:map)] containing a set of mappings between a
		**   name and a set of columns.
        **   
        ** @returns
        **   The number of sets (including these) registered in the server.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Associates several sets of columns with logical names in the server.
        /// This is the equivalent of calling AddFetchSet for each entry in
        /// the map but is more efficient.
        /// </summary>
        /// <param name="sets">A Map containing a set of mappings between a name
		/// and a set of columns.</param>
        /// <returns>The number of sets (including these) registered in the 
		/// server.</returns>
		public int
		AddFetchSets(Map sets)
		{
			long count = (long) Call("addFetchSets", sets);
            return (int) count;
		}

        /*!
        ** Associates a set of columns with a logical name in the server.
		**
		** The name can be used when specifying search terms to be passed to 
		** **FindTerms( )** [$<link>(:module:FindTerms)].
        ** The search becomes the equivalent of an ``OR`` search involving the 
		** columns.
        ** 
        ** @param name
        **   The logical name to associated with the set of columns.
        **   
        ** @param columns
        **   A `String` containing the names of columns to be used when ``name``
        **   is passed to **FindTerms( )** [$<link>(:module:FindTerms)]. 
		**   The column names must be separated by a ``semi-colon`` or a
		**   ``comma``.
        **   
        ** @returns
        **   The number of aliases (including this one) registered in the server.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Associates a set of columns with a logical name in the server. The name
        /// can be used when specifying search terms to be passed to FindTerms.
        /// The search becomes the equivalent of an OR search involving the columns.
        /// </summary>
        /// <param name="name">The logical name to associated with the set of 
		/// columns.</param>
        /// <param name="columns">A string containing the names of columns to be
		/// used when name is passed to FindTerms. The column names must be 
		/// separated by a semi-colon or a comma.</param>
        /// <returns>The number of aliases (including this one) registered in 
		/// the server.</returns>
        public int
        AddSearchAlias(string name, string columns)
        {
            return DoAddSearchAlias(name, columns);
        }

        /*!
        ** Associates a set of columns with a logical name in the server. 
		**
		** The name can be used when specifying search terms to be passed to 
		** **FindTerms( )** [$<link>(:module:FindTerms)].
        ** The search becomes the equivalent of an ``OR`` search involving the 
		** columns.
        ** 
        ** @param name
        **   The logical name to associated with the set of columns.
        **   
        ** @param columns
        **   An array of `String`\s containing the names of columns to be used 
		**   when ``name`` is passed to **FindTerms( )**
		**   [$<link>(:module:FindTerms)].
        **   
        ** @returns
        **   The number of aliases (including this one) registered in the server.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Associates a set of columns with a logical name in the server. 
		/// The name can be used when specifying search terms to be passed to 
		/// FindTerms.
        /// The search becomes the equivalent of an OR search involving the 
		/// columns.
        /// </summary>
        /// <param name="name">The logical name to associated with the set of 
		/// columns.</param>
        /// <param name="columns">An array of strings containing the names of 
		/// columns to be used when name is passed to FindTerms.</param>
        /// <returns>The number of aliases (including this one) registered in 
		/// the server.</returns>
        public int
        AddSearchAlias(string name, string[] columns)
        {
            return DoAddSearchAlias(name, columns);
        }

        /*!
        ** Associates a set of columns with a logical name in the server. 
		**
		** The name can be used when specifying search terms to be passed to 
		** **FindTerms( )** [$<link>(:module:FindTerms)].
		** The search becomes the equivalent of an ``OR`` search involving the
		** columns.
        ** 
        ** @param name
        **   The logical name to be associated with the set of columns.
        **   
        ** @param columns
        **   A list of `String`\s containing the names of columns to be used 
		**   when ``name`` is passed to **FindTerms( )**
		**   [$<link>(:module:FindTerms)].
		**   
        ** @returns
        **   The number of aliases (including this one) registered in the server.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Associates a set of columns with a logical name in the server. 
		/// The name can be used when specifying search terms to be passed to 
		/// FindTerms.
        /// The search becomes the equivalent of an OR search involving the columns.
        /// </summary>
        /// <param name="name">The logical name to associated with the set of 
		/// columns.</param>
        /// <param name="columns">A list of string containing the names of 
		/// columns to be used when name is
        /// passed to FindTerms.</param>
        /// <returns>The number of aliases (including this one) registered in 
		/// the server.</returns>
        public int
        AddSearchAlias(string name, List<string> columns)
        {
            return DoAddSearchAlias(name, columns.ToArray());
        }

        /*!
        ** Associates a set of columns with a logical name in the server. 
		**
		** This is the equivalent of calling **AddSearchAlias( )**
		** [$<link>(:module:AddSearchAlias)] for each entry in the map
		** but is more efficient.
        ** 
        ** @param aliases
		**   A `Map` [$<link>(:map:map)] containing a set of mappings between a
		**   name and a set of columns.
        **   
        ** @returns
        **   The number of sets (including these) registered in the server.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Associates a set of columns with a logical name in the server. This 
        /// is the equivalent of calling AddSearchAlias for each entry in the map
        /// but is more efficient.
        /// </summary>
        /// <param name="aliases">A map containing a set of mappings between a 
		/// name and a set of columns.</param>
        /// <returns>The number of sets (including these) registered in the 
		/// server.</returns>
		public int
		AddSearchAliases(Map aliases)
		{
            long count = (long) Call("addSearchAliases", aliases);
            return (int) count;
		}

        /*!
        ** Associates a set of sort keys with a logical name in the server.
		**
        ** The name can be used instead of a sort key list when sorting the current
        ** result set using **Sort( )** [$<link>(:module:Sort)].
        ** 
        ** @param name
        **   The logical name to associate with the set of columns.
        **   
        ** @param keys
        **   A `String` containing the names of the keys to be used when ``name``
        **   is passed to **Sort( )** [$<link>(:module:Sort)].
		**   The keys must be separated by a ``semi-colon`` or a ``comma``.
        **   
        ** @returns 
        **   The number of sets (including this one) registered in the server.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Associates a set of sort keys with a logical name in the server.
        /// The name can be used instead of a sort key list when sorting the 
        /// current result set using Sort.
        /// </summary>
        /// <param name="name">The logical name to associate with the set of 
		/// columns.</param>
        /// <param name="keys">A string containing the names of the keys to be 
		/// used when name is passed to Sort. The keys must be separated by a 
		/// semi-colon or a comma.</param>
        /// <returns>The number of sets (including this one) registered in the 
		/// server.</returns>
		public int
		AddSortSet(string name, string keys)
		{
			return DoAddSortSet(name, keys);
		}

        /*!
        ** Associates a set of sort keys with a logical name in the server.
        ** 
        ** @param name
        **   The logical name to associate with the set of columns.
        **   
        ** @param keys
        **   An array of `String`\s containing the names of the keys to be used
        **   when ``name`` is passed to **Sort( )** [$<link>(:module:Sort)].
		**   The keys must be separated by a ``semi-colon`` or a ``comma``.
        **   
        ** @returns 
        **   The number of sets (including this one) registered in the server.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Associates a set of sort keys with a logical name in the server.
        /// </summary>
        /// <param name="name">The logical name to associate with the set of 
		/// columns.</param>
        /// <param name="keys">An array of strings containing the names of the 
		/// keys to be used when nameis passed to Sort. The keys must be 
		/// separated by a semi-colon or a comma.</param>
        /// <returns>The number of sets (including this one) registered in the 
		/// server.</returns>
        public int
        AddSortSet(string name, string[] keys)
        {
            return DoAddSortSet(name, keys);
        }

        /*!
        ** Associates a set of sort keys with a logical name in the server.
        ** 
        ** @param name
        **   The logical name to associate with the set of columns.
        **   
        ** @param keys
        **   A list of `String`\s containing the names of the keys to be used 
		**   when ``name`` is passed to **Sort( )** [$<link>(:module:Sort)]. 
		**   The keys must be separated by a ``semi-colon`` or a ``comma``.
        **   
        ** @returns 
        **   The number of sets (including this one) registered in the server.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Associates a set of sort keys with a logical name in the server.
        /// </summary>
        /// <param name="name">The logical name to associate with the set of 
		/// columns.</param>
        /// <param name="keys">A list of strings containing the names of the 
		/// keys to be used when name is passed to Sort. The keys must be 
		/// separated by a semi-colon or a comma.</param>
        /// <returns>The number of sets (including this one) registered in the 
		/// server.</returns>
        public int
        AddSortSet(string name, List<string> keys)
        {
            return DoAddSortSet(name, keys.ToArray());
        }

        /*!
        ** Associates several sets of sort keys with logical names in the server.
		**
		** This is the equivalent of calling **AddSortSet( )** 
		** [$<link>(:module:AddSortSet)] for each entry in the map but is more
		** efficient.
        ** 
        ** @param sets
		**   A `Map` [$<link>(:map:map) containing a set of mappings between a
		**   name and a set of keys.
        **   
        ** @returns 
        **   The number of sets (including these) registered in the server.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Associates several sets of sort keys with logical names in the server.
        /// This is the equivalent of calling AddSortSet for each entry in the
        /// map but is more efficient.
        /// </summary>
        /// <param name="sets">A map containing a set of mappings between a name
		/// and a set of keys.</param>
        /// <returns>The number of sets (including these) registered in the 
		/// server.</returns>
		public int
		AddSortSets(Map sets)
		{
            long count = (long) Call("addSortSets", sets);
            return (int) count;
		}

        /*!
        ** Fetches ``count`` records from the position described by a combination
        ** of ``flag`` and ``offset``.
        ** 
        ** @param flag
        **   The position to start fetching records from.
        **   Must be one of:
        **     ``start``
        **     ``current``
        **     ``end``
        **   
        ** @param offset
        **   The position relative to ``flag`` to start fetching from.
        **   
        ** @param count
        **   The number of records to fetch. 
		**   A ``count`` of ``0`` is permitted to change the location of the 
		**   current record without returning any results. 
        **   A ``count`` of less than ``0`` causes all the remaining records in
        **   the result set to be returned.
        **   
        ** @param columns
		**   A `String` contiaining the names of the columns to be returned for
		**   each record or the name of a column set which has been registered
		**   previously using **AddFetchSet( )** [$<link>(:module:AddFetchSet)].
		**   The column names must be separated by a ``semi-colon`` or a 
		**   ``comma``.
        **   
        ** @returns 
        **   A `ModuleFetchResult` object.
		**   [$<link>(:modulefetchresult:modulefetchresult)].
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Fetches count records from the position described by a combination
        /// of flag and offset.
        /// </summary>
        /// <param name="flag">
        /// The position to start fetching records from.
        ///   Must be one of:
        ///     "start"
        ///     "current"
        ///     "end"
        /// </param>
        /// <param name="offset">The position relative to flag to start 
		/// fetching from.</param>
        /// <param name="count">The number of records to fetch. A count of 
		/// zero is permitted to change the location of the current record 
		/// without returning any results. A count of less than zero causes 
		/// all the remaining records in the result set to be returned.</param>
        /// <param name="columns">A string contiaining the names of the columns 
		/// to be returned for each record or the name of a column set which has
		/// been registered previously using AddFetchSet. The column names must 
		/// be separated by a semi-colon or a comma.</param>
        /// <returns>A ModuleFetchResult object.</returns>
        public ModuleFetchResult
        Fetch(string flag, long offset, int count, string columns)
        {
            return DoFetch(flag, offset, count, columns);
        }

        /*!
        ** Fetches ``count`` records from the position described by a combination
        ** of ``flag`` and ``offset``.
        ** 
        ** @param flag
        **   The position to start fetching records from.
        **   Must be one of:
        **     ``start``
        **     ``current``
        **     ``end``
        **   
        ** @param offset
        **   The position relative to ``flag`` to start fetching from.
        **   
        ** @param count
        **   The number of records to fetch. 
		**   A ``count`` of ``0`` is permitted to change the location of the 
		**   current record without returning any results. 
        **   A ``count`` of less than ``0`` causes all the remaining records in
        **   the result set to be returned.
        **   
        ** @param columns
        **   An array of `string`\s contiaining the names of the columns to be
		**   returned for each record or the name of a column set which has been
		**   registered previously using **AddFetchSet( )** 
		**   [$<link>(:module:AddFetchSet)].
        **   
        ** @returns 
        **   A `ModuleFetchResult` object 
		**   [$<link>(:modulefetchresult:modulefetchresult)]. 
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Fetches count records from the position described by a combination
        /// of flag and offset.
        /// </summary>
        /// <param name="flag">
        /// The position to start fetching records from.
        ///   Must be one of:
        ///     "start"
        ///     "current"
        ///     "end"
        /// </param>
        /// <param name="offset">The position relative to flag to start fetching from.</param>
        /// <param name="count">The number of records to fetch. A count of zero 
		/// is permitted to change the location of the current record without 
		/// returning any results. A count of less than zero causes all the 
		/// remaining records in the result set to be returned.</param>
        /// <param name="columns">An array of strings contiaining the names of 
		/// the columns to be returned for each record or the name of a column 
		/// set which has been registered previously using AddFetchSet.</param>
        /// <returns>A ModuleFetchResult object.</returns>
        public ModuleFetchResult
        Fetch(string flag, long offset, int count, string[] columns)
        {
            return DoFetch(flag, offset, count, columns);
        }

        /*!
        ** Fetches ``count`` records from the position described by a combination
        ** of ``flag`` and ``offset``.
        ** 
        ** @param flag
        **   The position to start fetching records from.
        **   Must be one of:
        **     ``start``
        **     ``current``
        **     ``end``
        **   
        ** @param offset
        **   The position relative to ``flag`` to start fetching from.
        **   
        ** @param count
        **   The number of records to fetch. 
		**   A ``count`` of ``0`` is permitted to change the location of the 
		**  current record without returning any results. 
        **   A ``count`` of less than ``0`` causes all the remaining records in the
        **   result set to be returned.
        **   
        ** @param columns
        **   A list of `string`\s contiaining the names of the columns to be 
		**   returned for each record or the name of a column set which has been
		**   registered previously using **AddFetchSet( )** 
		**   [$<link>(:module:AddFetchSet)].
        **   
        ** @returns 
        **   A `ModuleFetchResult` object 
		**   [$<link>(:modulefetchresult:modulefetchresult].
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Fetches count records from the position described by a combination
        /// of flag and offset.
        /// </summary>
        /// <param name="flag">
        /// The position to start fetching records from.
        ///   Must be one of:
        ///     "start"
        ///     "current"
        ///     "end"
        /// </param>
        /// <param name="offset">The position relative to flag to start 
		/// fetching from.</param>
        /// <param name="count">The number of records to fetch. A count of 
		/// zero is permitted to change the location of the current record 
		/// without returning any results. A count of less than zero causes 
		/// all the remaining records in the result set to be returned.</param>
        /// <param name="columns">A list of strings contiaining the names of the
		/// columns to be returned for each record or the name of a column set 
		/// which has been registered previously using AddFetchSet.</param>
        /// <returns>A ModuleFetchResult object.</returns>
        public ModuleFetchResult
        Fetch(string flag, long offset, int count, List<string> columns)
        {
            return DoFetch(flag, offset, count, columns.ToArray());
        }

        /*!
        ** Fetches ``count`` records from the position described by a combination
        ** of ``flag`` and ``offset``.
        ** 
        ** @param flag
        **   The position to start fetching records from.
        **   Must be one of:
        **     ``start``
        **     ``current``
        **     ``end``
        **   
        ** @param offset
        **   The position relative to ``flag`` to start fetching from.
        **   
        ** @param count
        **   The number of records to fetch.
		**   A ``count`` of ``0`` is permitted to change the location of the 
		**   current record without returning any results. 
        **   A ``count`` of less than ``0`` causes all the remaining records in
        **   the result set to be returned.
        **   
        ** @returns 
        **   A `ModuleFetchResult` object
		**   [$<link>(:modulefetchresult:modulefetchresult)].
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Fetches count records from the position described by a combination
        /// of flag and offset.
        /// </summary>
        /// <param name="flag">
        /// The position to start fetching records from.
        ///   Must be one of:
        ///     "start"
        ///     "current"
        ///     "end"
        /// </param>
        /// <param name="offset">The position relative to flag to start 
		/// fetching from.</param>
        /// <param name="count">The number of records to fetch. A count of 
		/// zero is permitted to change the location of the current record 
		/// without returning any results. A count of less than zero causes
		/// all the remaining records in the result set to be returned.</param>
        /// <returns>A ModuleFetchResult object. The results returned will 
        /// include the pseudo-column rownum for each fetched record.</returns>
        public ModuleFetchResult
        Fetch(string flag, long offset, int count)
        {
            return DoFetch(flag, offset, count, null);
        }

        public ModuleFetchResult
        Fetch(ModuleFetchPosition pos, int count, string columns)
        {
            return DoFetch(pos._flag, pos._offset, count, columns);
        }

        public ModuleFetchResult
        Fetch(ModuleFetchPosition pos, int count, string[] columns)
        {
            return DoFetch(pos._flag, pos._offset, count, columns);
        }

        public ModuleFetchResult
        Fetch(ModuleFetchPosition pos, int count, List<string> columns)
        {
            return DoFetch(pos._flag, pos._offset, count, columns.ToArray());
        }

        public ModuleFetchResult
        Fetch(ModuleFetchPosition pos, int count)
        {
            return DoFetch(pos._flag, pos._offset, count, null);
        }

        /*!
        ** Searches for a record with the key value ``key`.
        ** 
        ** @param key
        **   The key of the record being searched for.
        **   
        ** @returns
        **   The number of records found. 
		**   This will be either ``1`` if the record was found or ``0`` if not 
		**   found.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Searches for a record with the key value key.
        /// </summary>
        /// <param name="key">The key of the record being searched for.</param>
        /// <returns>The number of records found. This will be either 1 if the
        /// record was found or 0 if not found.</returns>
		public long
		FindKey(long key)
		{
			return (long) Call("findKey", key);
		}

        /*!
        ** Searches for records with key values in the array ``keys``.
        ** 
        ** @param keys
        **   The list of keys being searched for.
        **   
        ** @returns
        **   The number of records found.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Searches for records with key values in the array keys.
        /// </summary>
        /// <param name="keys">The list of keys being searched for.</param>
        /// <returns>If a server-side error occurred.</returns>
		public long
		FindKeys(long[] keys)
		{
			return (long) Call("findKeys", keys);
		}

        /*!
        ** Searches for records with key values in the array ``keys``.
        ** 
        ** @param keys
        **   An array of keys being searched for.
        **   
        ** @returns
        **   The number of records found.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Searches for records with key values in the array keys.
        /// </summary>
        /// <param name="keys">An array of keys being searched for.</param>
        /// <returns>If a server-side error occurred.</returns>
        public long
        FindKeys(List<long> keys)
        {
            return (long) Call("findKeys", keys.ToArray());
        }

        /*!
        ** Searches for records which match the search terms specified in 
		** ``terms``.
        ** 
        ** @param terms
        **   The search terms.
        **   
        ** @returns
        **   An estimate of the number of records found.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Searches for records which match the search terms specified in terms.
        /// </summary>
        /// <param name="terms">The search terms.</param>
        /// <returns>An estimate of the number of records found.</returns>
		public long
		FindTerms(Terms terms)
		{
			return (long) Call("findTerms", terms.ToArray());
		}

        /*!
        ** Searches for records which match the TexQL ``WHERE`` clause.
        ** 
        ** @param where
        **   The TexQL ``WHERE`` clause to use.
        **   
        ** @returns
        **   An estimate of the number of records found.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Searches for records which match the TexQL where clause.
        /// </summary>
        /// <param name="where">The TexQL where clause to use.</param>
        /// <returns>An estimate of the number of records found.</returns>
		public long
		FindWhere(string where)
		{
			return (long) Call("findWhere", where);
		}

		public Map
		Insert(Map values, string columns)
		{
			return DoInsert(values, columns);
		}

        public Map
        Insert(Map values, string[] columns)
        {
            return DoInsert(values, columns);
        }

        public Map
        Insert(Map values, List<string> columns)
        {
            return DoInsert(values, columns.ToArray());
        }

		public Map
		Insert(Map values)
		{
			return DoInsert(values, null);
		}

        public long
        Remove(string flag, long offset, int count)
        {
            return DoRemove(flag, offset, count);
        }

        public long
        Remove(string flag, long offset)
        {
            return DoRemove(flag, offset, null);
        }

        public long
        Remove(ModuleFetchPosition pos, int count)
        {
            return DoRemove(pos._flag, pos._offset, count);
        }

        public long
        Remove(ModuleFetchPosition pos)
        {
            return DoRemove(pos._flag, pos._offset, null);
        }

        /*!
        ** Restores a set of records from a file on the server machine which 
        ** contains a list of keys, one per line.
        ** 
        ** @param file
        **   The file on the server machine containing the keys.
        **   
        ** @returns
        **   The number of records found.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Restores a set of records from a file on the server machine which 
		/// contains a list of keys, one per line.
        /// </summary>
        /// <param name="file">The file on the server machine containing the 
		/// keys.</param>
        /// <returns>The number of records found.</returns>
		public long
		RestoreFromFile(string file)
		{
			Map args = new Map();
			args.Add("file", file);
			return (long) Call("restoreFromFile", args);
		}

        /*!
        ** Restores a set of records from a temporary file on the server machine
        ** which contains a list of keys, one per line. 
		** Operates the same way as **RestoreFromFile( )**
		** [$<link>(:module:RestoreFromFile)] except that the ``file`` parameter
		** is relative to the server's temporary directory.
        ** 
        ** @param file
        **   The file on the server machine containing the keys.
        **   
        ** @returns 
        **   The number of records found.
        **   
        ** @throws IMuException
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Restores a set of records from a temporary file on the server machine
        /// which contains a list of keys, one per line. Operates the same way as
        /// RestoreFromFile except that the file parameter is relative to the 
        /// server's temporary directory.
        /// </summary>
        /// <param name="file">The file on the server machine containing the keys.</param>
        /// <returns>The number of records found.</returns>
		public long
		RestoreFromTemp(string file)
		{
			Map args = new Map();
			args.Add("file", file);
			return (long) Call("restoreFromTemp", args);
		}

        /*!
        ** Sorts the current result set by the sort keys in ``keys``. 
		** Each sort key is a column name optionally preceded by a ``+`` 
		** (for an ascending sort) or a ``-`` (for a descending sort).
        ** 
        ** @param keys
        **   A `String` containing the list of sort keys. 
		**   The keys must be separated by a ``semi-colon`` or a ``comma``.
        **   
        ** @param flags
        **   A `String` containing a set of flags specifying the behaviour of 
		**   the sort. 
        **   The flags must be separated by a semi-colon or a comma.
        **   
        ** @returns
		**   A `ModuleSortResult` object
		**   [$<link>(:modulesortresult:modulesortresult)]. 
		**   If the ``report`` flag has not been specified the result will be
		**   ``null``.
        **   
        ** @throws IMuException 
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Sorts the current result set by the sort keys in keys. Each sort key
        /// is a column name optionally preceded by a "+" (for an ascending sort)
        /// or a "-" (for a descending sort).
        /// </summary>
        /// <param name="keys">A string containing the list of sort keys. The 
		/// keys must be separated by a semi-colon or a comma.</param>
        /// <param name="flags">A string containing a set of flags specifying 
		/// the behaviour of the sort. The flags must be separated by a semi-colon 
		/// or a comma.</param>
        /// <returns>A ModuleSortResult object. If the report flag has 
		///not been specified the result will be null.</returns>
        public ModuleSortResult
        Sort(string keys, string flags)
        {
            return DoSort(keys, flags);
        }

        /*!
        ** Sorts the current result set by the sort keys in ``keys``. 
		** Each sort key is a column name optionally preceded by a ``+`` 
		** (for an ascending sort) or a ``-`` (for a descending sort).
        ** 
        ** @param keys
        **   A string containing the list of sort keys. 
		**   The keys must be separated by a ``semi-colon`` or a ``comma``.
        **   
        ** @param flags
        **   An array of `String`\s specifying the behaviour of the sort. 
        **   
        ** @returns
        **   A `ModuleSortResult` object
		**   [$<link>(:modulesortresult:modulesortresult). 
		**   If the ``report`` flag has not been specified the result will be 
		**   ``null``.
        **   
        ** @throws IMuException 
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Sorts the current result set by the sort keys in keys. Each sort key
        /// is a column name optionally preceded by a "+" (for an ascending sort) or 
        /// a "-" (for a descending sort).
        /// </summary>
        /// <param name="keys">A string containing the list of sort keys. 
		/// The keys must be separated by a semi-colon or a comma.</param>
        /// <param name="flags">An array of strings specifying the behaviour of 
		/// the sort. </param>
        /// <returns>A ModuleSortResult object. If the report flag has 
		/// not been specified the result will be null.</returns>
        public ModuleSortResult
        Sort(string keys, string[] flags)
        {
            return DoSort(keys, flags);
        }

        /*!
        ** Sorts the current result set by the sort keys in ``keys``. 
		** Each sort key is a column name optionally preceded by a ``+`` (for an
		** ascending sort) or a ``-`` (for a descending sort).
        ** 
        ** @param keys
        **   A `String` containing the list of sort keys.
		**   The keys must be separated by a ``semi-colon`` or a ``comma``.
        **   
        ** @param flags
        **   A list of `String`\s specifying the behaviour of the sort. 
        **   
        ** @returns
        **   A `ModuleSortResult` object
		**   [$<link>(:modulesortresult:modulesortresult)]. If the ``report`` 
		**   flag has not been specified the result will be ``null``.
        **   
        ** @throws IMuException 
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Sorts the current result set by the sort keys in keys. Each sort key
        /// is a column name optionally preceded by a "+" (for an ascending sort) or 
        /// a "-" (for a descending sort).
        /// </summary>
        /// <param name="keys">A string containing the list of sort keys. The 
		/// keys must be separated by a semi-colon or a comma.</param>
        /// <param name="flags">A list of strings specifying the behaviour of 
		/// the sort. </param>
        /// <returns>A ModuleSortResult object. If the report flag has not been 
		/// specified the result will be null.</returns>
        public ModuleSortResult
        Sort(string keys, List<string> flags)
        {
            return DoSort(keys, flags.ToArray());
        }

        /*!
        ** Sorts the current result set by the sort keys in ``keys``.
		** Each sort key is a column name optionally preceded by a ``+`` (for an
		** ascending sort) or a ``-`` (for a descending sort).
        ** 
        ** @param keys
        **   An array of `String`\s containing the list of sort keys.
        **   
        ** @param flags
        **   A string containing a set of flags specifying the behaviour of the 
		**   sort. 
        **   The flags must be separated by a ``semi-colon`` or a ``comma``.
        **   
        ** @returns
        **   A `ModuleSortResult` object
		**   [$<link>(:modulesortresult:modulesortresult).
		**   If the ``report`` flag has not been specified the result will be 
		**   ``null``.
        **   
        ** @throws IMuException 
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Sorts the current result set by the sort keys in keys. Each sort key
        /// is a column name optionally preceded by a "+" (for an ascending sort) or 
        /// a "-" (for a descending sort).
        /// </summary>
        /// <param name="keys">An array of strings containing the list of sort 
		/// keys.</param>
        /// <param name="flags">A string containing a set of flags specifying 
		/// the behaviour of the sort. The flags must be separated by a semi-colon
		/// or a comma.</param>
        /// <returns>A ModuleSortResult object. If the report flag has not been 
		/// specified the result will be null.</returns>
        public ModuleSortResult
        Sort(string[] keys, string flags)
        {
            return DoSort(keys, flags);
        }

        /*!
        ** Sorts the current result set by the sort keys in ``keys``. 
		** Each sort key is a column name optionally preceded by a ``+`` (for an
		** ascending sort) or a ``-`` (for a descending sort).
        ** 
        ** @param keys
        **   An array of `String`\s containing the list of sort keys.
        **   
        ** @param flags
        **   An array of `String`\s specifying the behaviour of the sort. 
        **   
        ** @returns
        **   A `ModuleSortResult` object
		**   [$<link>(:modulesortresult:modulesortresult).
		**   If the ``report`` flag has not been specified the result will be 
		**   ``null``.
        **   
        ** @throws IMuException 
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Sorts the current result set by the sort keys in keys. Each sort key
        /// is a column name optionally preceded by a "+" (for an ascending sort) or 
        /// a "-" (for a descending sort).
        /// </summary>
        /// <param name="keys">An array of strings containing the list of sort keys.</param>
        /// <param name="flags">An array of strings specifying the behaviour of the sort. </param>
        /// <returns>A ModuleSortResult object. If the report flag has not been specified
        ///   the result will be null.</returns>
		public ModuleSortResult
		Sort(string[] keys, string[] flags)
		{
			return DoSort(keys, flags);
		}

        /*!
        ** Sorts the current result set by the sort keys in ``keys``. 
		**
		** Each sort key is a column name optionally preceded by a ``+`` (for an
		** ascending sort) or a ``-`` (for a descending sort).
        ** 
        ** @param keys
        **   An array of `String`\s containing the list of sort keys.
        **   
        ** @param flags
        **   A list of `String`\s specifying the behaviour of the sort. 
        **   
        ** @returns
        **   A `ModuleSortResult` object
		**   [$<link>(:modulesortresult:modulesortresult). 
		**   If the ``report`` flag has not been specified the result will be 
		**   ``null``.
        **   
        ** @throws IMuException 
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Sorts the current result set by the sort keys in keys. Each sort key
        /// is a column name optionally preceded by a "+" (for an ascending sort) or 
        /// a "-" (for a descending sort).
        /// </summary>
        /// <param name="keys">An array of strings containing the list of sort keys.</param>
        /// <param name="flags">A list of strings specifying the behaviour of the sort. </param>
        /// <returns>A ModuleSortResult object. If the report flag has not been specified
        /// the result will be null.</returns>
        public ModuleSortResult
        Sort(string[] keys, List<string> flags)
        {
            return DoSort(keys, flags.ToArray());
        }

        /*!
        ** Sorts the current result set by the sort keys in ``keys``. 
		** Each sort key is a column name optionally preceded by a ``+`` (for an
		** ascending sort) or a ``-`` (for a descending sort).
        ** 
        ** @param keys
        **   A list of `String`\s containing the list of sort keys.
        **   
        ** @param flags
        **   A `String` containing a set of flags specifying the behaviour of 
		**   the sort. 
        **   The flags must be separated by a ``semi-colon`` or a ``comma``.
        **   
        ** @returns
        **   A `ModuleSortResult` object
		**   [$<link>(:modulesortresult:modulesortresult)]. 
		**   If the ``report`` flag has not been specified the result will be 
		**   ``null``.
        **   
        ** @throws IMuException 
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Sorts the current result set by the sort keys in keys. Each sort key
        /// is a column name optionally preceded by a "+" (for an ascending sort) or 
        /// a "-" (for a descending sort).
        /// </summary>
        /// <param name="keys">A list of strings containing the list of sort keys.</param>
        /// <param name="flags">A string containing a set of flags specifying the 
		/// behaviour of the sort. The flags must be separated by a semi-colon or
		/// a comma.</param>
        /// <returns>A ModuleSortResult object. If the report flag has not been specified
        ///   the result will be null.</returns>
        public ModuleSortResult
        Sort(List<string> keys, string flags)
        {
            return DoSort(keys.ToArray(), flags);
        }

        /*!
        ** Sorts the current result set by the sort keys in ``keys``. 
		** Each sort key is a column name optionally preceded by a ``+`` (for an
		** ascending sort) or a ``-`` (for a descending sort).
        ** 
        ** @param keys
        **   A list of `String`\s containing the list of sort keys.
        **   
        ** @param flags
        **   An array of `String`\s specifying the behaviour of the sort. 
        **   
        ** @returns
        **   A `ModuleSortResult` object
		**   [$<link>(:modulesortresult:modulesortresult).
		**   If the ``report`` flag has not been specified the result will be 
		**   ``null``.
        **   
        ** @throws IMuException 
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Sorts the current result set by the sort keys in keys. Each sort key
        /// is a column name optionally preceded by a "+" (for an ascending sort) or 
        /// a "-" (for a descending sort).
        /// </summary>
        /// <param name="keys">A list of strings containing the list of sort keys.</param>
        /// <param name="flags">An array of strings specifying the behaviour of the sort. </param>
        /// <returns>A ModuleSortResult object. If the report flag has not been specified
        /// the result will be null.</returns>
        public ModuleSortResult
        Sort(List<string> keys, string[] flags)
        {
            return DoSort(keys.ToArray(), flags);
        }

        /*!
        ** Sorts the current result set by the sort keys in ``keys``. 
		** Each sort key is a column name optionally preceded by a ``+`` (for an
		** ascending sort) or a ``-`` (for a descending sort).
        ** 
        ** @param keys
        **   A list of `String`\s containing the list of sort keys.
        **   
        ** @param flags
        **   A list of `String`\s specifying the behaviour of the sort. 
        **   
        ** @returns
        **   A `ModuleSortResult` object
		**   [$<link>(:modulesortresult:modulesortresult).
		**   If the ``report`` flag has not been specified the result will be 
		**   ``null``.
        **   
        ** @throws IMuException 
        **   If a server-side error occurred.
        */
        /// <summary>
        /// Sorts the current result set by the sort keys in keys. Each sort key
        /// is a column name optionally preceded by a "+" (for an ascending sort) or 
        /// a "-" (for a descending sort).
        /// </summary>
        /// <param name="keys">A list of strings containing the list of sort keys.</param>
        /// <param name="flags">A list of strings specifying the behaviour of the sort. </param>
        /// <returns>A ModuleSortResult object. If the report flag has not been specified
        /// the result will be null.</returns>
        public ModuleSortResult
        Sort(List<string> keys, List<string> flags)
        {
            return DoSort(keys.ToArray(), flags.ToArray());
        }

        public ModuleSortResult
        Sort(string columns)
        {
            return DoSort(columns, null);
        }

        public ModuleSortResult
		Sort(string[] columns)
		{
			return DoSort(columns, null);
		}

        public ModuleSortResult
        Sort(List<string> columns)
        {
            return DoSort(columns.ToArray(), null);
        }

        public ModuleFetchResult
        Update(string flag, long offset, int count, Map values, string columns)
        {
            return DoUpdate(flag, offset, count, values, columns);
        }

        public ModuleFetchResult
        Update(string flag, int offset, int count, Map values, string[] columns)
        {
            return DoUpdate(flag, offset, count, values, columns);
        }

        public ModuleFetchResult
        Update(string flag, int offset, int count, Map values, List<string> columns)
        {
            return DoUpdate(flag, offset, count, values, columns.ToArray());
        }

        public ModuleFetchResult
        Update(string flag, long offset, int count, Map values)
        {
            return DoUpdate(flag, offset, count, values, null);
        }

        public ModuleFetchResult
        Update(ModuleFetchPosition pos, int count, Map values, string columns)
        {
            return DoUpdate(pos._flag, pos._offset, count, values, columns);
        }

        public ModuleFetchResult
        Update(ModuleFetchPosition pos, int count, Map values, string[] columns)
        {
            return DoUpdate(pos._flag, pos._offset, count, values, columns);
        }

        public ModuleFetchResult
        Update(ModuleFetchPosition pos, int count, Map values, List<string> columns)
        {
            return DoUpdate(pos._flag, pos._offset, count, values, columns.ToArray());
        }

        public ModuleFetchResult
        Update(ModuleFetchPosition pos, int count, Map values)
        {
            return DoUpdate(pos._flag, pos._offset, count, values, null);
        }

		protected string _table;
		
		protected int
		DoAddFetchSet(string name, object columns)
		{
			Map args = new Map();
			args.Add("name", name);
			args.Add("columns", columns);
			long count = (long) Call("addFetchSet", args);
            return (int) count;
		}
		
		protected int
		DoAddSearchAlias(string name, object columns)
		{
			Map args = new Map();
			args.Add("name", name);
			args.Add("columns", columns);
			long count = (long) Call("addSearchAlias", args);
            return (int) count;
		}
		
		protected int
		DoAddSortSet(string name, object keys)
		{
			Map args = new Map();
			args.Add("name", name);
			args.Add("columns", keys);
			long count = (long) Call("addSortSet", args);
            return (int) count;
		}
		
		protected ModuleFetchResult
		DoFetch(string flag, long offset, int count, object columns)
		{
			Map args = new Map();
			args.Add("flag", flag);
			args.Add("offset", offset);
			args.Add("count", count);
			if (columns != null)
				args.Add("columns", columns);
			return MakeFetchResult(Call("fetch", args));
		}

		protected Map
		DoInsert(Map values, object columns)
		{
			Map args = new Map();
			args.Add("values", values);
			if (columns != null)
				args.Add("columns", columns);
			return Call("insert", args) as Map;
		}

		protected long
		DoRemove(string flag, long offset, object count)
		{
			Map args = new Map();
			args.Add("flag", flag);
			args.Add("offset", offset);
			if (count != null)
				args.Add("count", count);
			return (long) Call("remove", args);
		}
		
		protected ModuleSortResult
		DoSort(object columns, object flags)
		{
			Map args = new Map();
			args.Add("columns", columns);
			if (flags != null)
				args.Add("flags", flags);
            return MakeSortResult(Call("sort", args));
 		}
		
		protected ModuleFetchResult
		DoUpdate(string flag, long offset, int count, Map values, object columns)
		{
			Map args = new Map();
			args.Add("flag", flag);
			args.Add("offset", offset);
			args.Add("count", count);
			args.Add("values", values);
			if (columns != null)
				args.Add("columns", columns);
			return MakeFetchResult(Call("update", args));
		}

		protected void
		Initialise(string table)
		{
			_name = "Module";
			_create = table;
			
			_table = table;
		}
		
		protected ModuleFetchResult
		MakeFetchResult(object raw)
		{
			Map data = raw as Map;
			
			ModuleFetchResult result = new ModuleFetchResult();
			result._hits = (long) data["hits"];
			
			/* Copy array.
			** Can't cast directly as this violates type safety rules.
			*/
			object[] rows = data["rows"] as object[];
            result._rows = new Map[rows.Length];
            for (int i = 0; i < rows.Length; i++)
                result._rows[i] = rows[i] as Map;

            result._count = result._rows.Length;
					
			return result;
		}

        protected ModuleSortResult
        MakeSortResult(object raw)
        {
            if (raw == null)
                return null;

            ModuleSortResult result = new ModuleSortResult();
            object[] list = raw as object[];
            result._count = list.Length;
            result._terms = new ModuleSortTerm[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                ModuleSortTerm term = new ModuleSortTerm();
                Map map = list[i] as Map;
                term._value = map.GetString("value");
                term._count = map.GetLong("count");
                if (map.ContainsKey("list"))
                    term._nested = MakeSortResult(map["list"]);
                else
                    term._nested = null;

                result._terms[i] = term;
            }
            return result;
        }

    }
}
