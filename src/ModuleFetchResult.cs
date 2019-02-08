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
    ** Provides results from a call to the `Module` **Fetch( )** method 
	** [$<link>(:module:Fetch)].
    ** 
    ** @usage
    **   IMu.ModuleFetchResult
    ** @end
    ** 
    ** @since 1.0
    */
    /// <summary>
    /// Provides results from a call to the Module Fetch method.
    /// </summary>
    public class ModuleFetchResult
    {
		/* Properties */
        /*!
        ** @property Count
        **   The number of records returned in the result.
        */
        /// <summary>
        /// The number of records returned in the result.
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
            }
        }

        /*!
        ** @property Hits
        **   The best estimate of the size of the result set after the 
		**   $<Fetch> method has completed.
        **   When the $<Module> object generates a result set using $<FindTerms>
		**   or $<FindWhere>, the number of matches is occasionally an overestimate
		**   of the true number of matches. 
		**   After the $<Fetch> method has been called, the IMu server may have
		**   a better estimate of the true number of matches so it is included 
		**   in the result.
        */
        /// <summary>
        /// The best estimate of the size of the result set after the Fetch 
		/// method has completed.
        /// When the Module object generates a result set using FindTermsor 
		/// FindWhere, the number of matches is occasionally an overestimate of
		/// the true number of matches. After the Fetch method has been called,
		/// the IMu server may have a better estimate of the true number of
		/// matches so it is included in the result.
        /// </summary>
        public long Hits
        {
            get
            {
                return _hits;
            }
        }

        /*!
        ** @property Rows
        **   The array of the records actually fetched. 
		**   Each record is represented by a ``Map`` object, with the map keys 
		**   being the names of the columns requested in the $<Fetch> call.
        */
        /// <summary>
        /// The array of the records actually fetched. 
		/// Each record is represented by a Map object, with the map keys 
		/// being the names of the columns requested in the Fetch call.
        /// </summary>
        public Map[] Rows
        {
            get
            {
                return _rows;
            }
        }

        internal int _count;
        internal long _hits;
        internal Map[] _rows;
    }
}
